using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class InstallerGenerator
{
    private const string InstallersFolder = "Assets/GeneratedInstallers";

    [MenuItem("Generate/Installer Classes")]
    public static void GenerateInstallerClasses()
    {
        if (!Directory.Exists(InstallersFolder))
        {
            Directory.CreateDirectory(InstallersFolder);
        }

        var typesWithAttribute = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttribute<CreateScriptableInstallerAttribute>() != null);

        foreach (var type in typesWithAttribute)
        {
            var attribute = type.GetCustomAttribute<CreateScriptableInstallerAttribute>();
            var fileName = attribute.Name ?? type.Name + "GeneratedInstaller.cs";
            var scriptPath = Path.Combine(InstallersFolder, fileName);
            var directory = Path.GetDirectoryName(scriptPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(scriptPath))
            {
                var installerCode = GenerateInstallerCode(type, attribute.BindAs ?? type, attribute.SerializedFields ?? Array.Empty<Type>());
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
            .Where(type => type.GetCustomAttribute<CreateScriptableInstallerAttribute>() != null);

        foreach (var type in typesWithAttribute)
        {
            var attribute = type.GetCustomAttribute<CreateScriptableInstallerAttribute>();
            var fileName = attribute.Name ?? type.Name + "GeneratedInstaller.asset";
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
private static string GenerateInstallerCode(Type targetType, Type bindAs, Type[] serializeFields)
{
    // Collect all namespaces for the using statements
    var usingStatements = string.Join("\n", new List<string>
    {
        "UnityEngine",
        "Zenject"
    }
    .Concat(new[] { targetType.Namespace, bindAs.Namespace })
    .Concat(serializeFields.Select(n => n.Namespace))
    .Where(n => !string.IsNullOrEmpty(n)).Distinct()
    .Select(n => $"using {n};"));

    var fieldNames = new Dictionary<string, int>();
    var serializedFields = string.Join("", serializeFields.Select(n =>
    {
        var baseFieldName = char.ToLower(n.Name[0]) + n.Name.Substring(1);

        if (!fieldNames.TryAdd(baseFieldName, 1))
        {
            fieldNames[baseFieldName]++;
        }

        var finalFieldName = $"{baseFieldName}{(fieldNames[baseFieldName] > 1 ? fieldNames[baseFieldName] : "")}";
        
        return $"\n    [SerializeField] private {n.Name} _{finalFieldName};";
    }));

    return 
$@"{usingStatements}

public class {targetType.Name}Installer : ScriptableObject
{{
    {serializedFields}

    public {bindAs.Name} Install(DiContainer container)
    {{
        var subContainer = container.CreateSubContainer();
        subContainer.Bind<{bindAs.Name}>().To<{targetType.Name}>().FromNew().AsSingle();
        return subContainer.Resolve<{bindAs.Name}>(); 
    }}
}}
";
}

    private static void CreateInstallerAsset(Type targetType, string assetPath)
    {
        var installerTypeName = $"{targetType.Name}Installer";
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
