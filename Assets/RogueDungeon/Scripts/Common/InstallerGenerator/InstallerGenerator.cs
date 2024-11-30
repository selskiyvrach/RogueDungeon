using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Common.InstallerGenerator
{
    public static class InstallerGenerator
    {
        private const string InstallersFolder = "Assets/Installers";

        [MenuItem("Generate/Installer Classes")]
        public static void GenerateInstallerClasses()
        {
            if (!Directory.Exists(InstallersFolder))
            {
                Directory.CreateDirectory(InstallersFolder);
            }

            var typesWithAttribute = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttribute<CreateFactoryInstallerAttribute>() != null);

            foreach (var type in typesWithAttribute)
            {
                var attribute = type.GetCustomAttribute<CreateFactoryInstallerAttribute>();
                var fileName = type.Name + "FactoryInstaller.cs";
                var scriptPath = Path.Combine(InstallersFolder, fileName);
                var directory = Path.GetDirectoryName(scriptPath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (!File.Exists(scriptPath))
                {
                    var installerCode = GenerateInstallerCode(type, attribute.BindAs ?? type, attribute.SerializedParams ?? Array.Empty<Type>(), attribute.FactoryParams ?? Array.Empty<Type>());
                    File.WriteAllText(scriptPath, installerCode);
                    Debug.Log($"Generated installer script for {type.Name} at {scriptPath}");
                }
                else
                {
                    Debug.Log($"Installer script for {type.Name} already exists at {scriptPath}");
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Generate/Installer Assets")]
        public static void CreateInstallerAssets()
        {
            var typesWithAttribute = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttribute<CreateFactoryInstallerAttribute>() != null);

            foreach (var type in typesWithAttribute)
            {
                var fileName = type.Name + "FactoryInstaller.asset";
                var assetPath = Path.Combine(InstallersFolder, fileName);
                var assetFullPath = Path.Combine(Application.dataPath, InstallersFolder["Assets/".Length..], fileName);

                if (!File.Exists(assetFullPath))
                {
                    CreateInstallerAsset(type, assetPath);
                    Debug.Log($"Generated installer asset for {type.Name} at {assetPath}");
                }
                else
                {
                    Debug.Log($"Installer asset for {type.Name} already exists at {assetPath}");
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static string GenerateInstallerCode(Type targetType, Type bindAs, Type[] serializeFields, Type[] factoryParameters)
        {
            // Collect all namespaces for the using statements
            var usingStatements = string.Join("\n", new List<string>
                {
                    "UnityEngine",
                    "Zenject"
                }
                .Concat(new[] { targetType.Namespace, bindAs.Namespace })
                .Concat(serializeFields.Select(n => n.Namespace))
                .Concat(factoryParameters.Select(n => n.Namespace))
                .Where(n => !string.IsNullOrEmpty(n)).Distinct()
                .Select(n => $"using {n};"));

            var fieldNames = new Dictionary<string, int>();
            var finalFieldNames = new List<string>();
            var serializedFields = string.Join("\n\t", serializeFields.Select(n =>
            {
                var baseFieldName = char.ToLower(n.Name[0]) + n.Name.Substring(1);

                if (!fieldNames.TryAdd(baseFieldName, 1))
                {
                    fieldNames[baseFieldName]++;
                }
                var finalFieldName = $"_{baseFieldName}{(fieldNames[baseFieldName] > 1 ? fieldNames[baseFieldName] : "")}";
                finalFieldNames.Add(finalFieldName);
        
                return $"[SerializeField] private {n.Name} {finalFieldName};";
            }));

            var bindings = string.Join("\n\t\t", Enumerable.Range(0, serializeFields.Length).Select(n => $"subContainer.BindInterfacesAndSelfTo<{serializeFields[n].Name}>().FromInstance({finalFieldNames[n]}).AsSingle();"));
            var parameters = factoryParameters.Length == 0 ? "" : string.Join(", ", factoryParameters.Select(n => n.Name)) + ", ";
            var parametersBindings = string.Join("\n\t\t",Enumerable.Range(0, factoryParameters.Length).Select(n => $"subContainer.BindInterfacesAndSelfTo<{factoryParameters[n].Name}>().FromInstance(param{n}).AsSingle();"));
    
            return 
                $@"{usingStatements}

public class {targetType.Name}FactoryInstaller : ScriptableObjectInstaller<{targetType.Name}FactoryInstaller>, IFactory<{parameters}{bindAs.Name}>
{{
    {serializedFields}

    public override void InstallBindings() => 
        Container.Bind<IFactory<{parameters}{bindAs.Name}>>().To<{targetType.Name}FactoryInstaller>().FromInstance(this);

    public {bindAs.Name} Create({string.Join(", ", Enumerable.Range(0, factoryParameters.Length).Select(n => $"{factoryParameters[n].Name} param{n}"))})
    {{
        var subContainer = Container.CreateSubContainer();
        {parametersBindings}
        {bindings}
        subContainer.Bind<{bindAs.Name}>().To<{targetType.Name}>().FromNew().AsSingle();
        return subContainer.Resolve<{bindAs.Name}>(); 
    }}
}}
";
        }

        private static void CreateInstallerAsset(Type targetType, string assetPath)
        {
            var installerTypeName = $"{targetType.Name}FactoryInstaller";
            var installerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == installerTypeName);

            if (installerType == null)
            {
                Debug.LogError($"Could not find generated installer class {installerTypeName}. Ensure the script has been compiled.");
                return;
            }

            var asset = ScriptableObject.CreateInstance(installerType);
            var directory = Path.GetDirectoryName(assetPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(asset, assetPath);
        }
    }
}
