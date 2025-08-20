using System.Collections.Generic;

namespace Libs.Utils.DotNet
{
    public interface IUniqueNameGenerator
    {
        string GetUniqueName(string name);
    }

    public class UniqueNameGenerator : IUniqueNameGenerator
    {
        private readonly Dictionary<string, int> _occurences = new();
        
        public string GetUniqueName(string name)
        {
            _occurences.TryAdd(name, -1);
            _occurences[name]++;
            return $"{name}_{_occurences[name]}";
        }
    }
}