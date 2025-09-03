using Dalamud.Configuration;
using Newtonsoft.Json;

namespace HuruClient.Parsers
{
    public class HonorificConfig : IPluginConfiguration
    {
        public int Version { get; set; }

        public Dictionary<uint, Dictionary<string, HonorificCharacter>> WorldCharacterDictionary = new();

        public bool TryGetCharacterConfig(string name, uint world, out HonorificCharacter? characterConfig)
        {
            characterConfig = null;
            if (!WorldCharacterDictionary.TryGetValue(world, out var w)) return false;
            return w.TryGetValue(name, out characterConfig);
        }
    }

    public class HonorificCharacter
    {
        public HonorificTitle DefaultTitle = new();
    }

    public class HonorificTitle
    {
        public string? Title = string.Empty;
    }

}
