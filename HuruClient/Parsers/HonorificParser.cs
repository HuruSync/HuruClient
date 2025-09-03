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

            var json = File.ReadAllText(configPath);
            var parsedConfig = JsonConvert.DeserializeObject<Honorific.PluginConfig>(json);

            var player = (FFXIVClientStructs.FFXIV.Client.Game.Character.Character*)character.Address;

            var vanillaTitle = Plugin.DataManager.GetExcelSheet<Title>().GetRowOrDefault(player->TitleId);
            var vanillaGenderedTitle = player->Sex == 0 ? $"{vanillaTitle.Value.Masculine}" : $"{vanillaTitle.Value.Feminine}";

            return parsedConfig.TryGetCharacterConfig(character.Name.TextValue, character.HomeWorld.RowId, out var honorificConfig)
                ? honorificConfig.DefaultTitle.Title
                : vanillaGenderedTitle;
        }
    }
}
