using Lumina.Excel.Sheets;
using Newtonsoft.Json;

namespace HuruClient.Parsers
{
    internal static class HonorificParser
    {
        internal unsafe static string? Parse()
        {
            var configPath = Path.Combine(Plugin.PluginInterface.ConfigDirectory.Parent.FullName, "Honorific.json");
            var character = Plugin.ClientState.LocalPlayer;

            using var stream = File.OpenRead(configPath);
            using var reader = new StreamReader(stream);
            var parsedConfig = JsonConvert.DeserializeObject<HonorificConfig>(reader.ReadToEnd());

            return parsedConfig.TryGetCharacterConfig(character.Name.TextValue, character.HomeWorld.RowId, out var honorificConfig)
                ? honorificConfig.DefaultTitle.Title
                : "Not found";

            var player = (FFXIVClientStructs.FFXIV.Client.Game.Character.Character*)character.Address;

            var vanillaTitle = Plugin.DataManager.GetExcelSheet<Title>().GetRowOrDefault(player->TitleId);
            return $"{vanillaTitle.Value.Masculine}";
        }
    }
}
