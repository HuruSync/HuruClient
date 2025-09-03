using Newtonsoft.Json;

namespace HuruClient.Parsers
{
    internal static class PetNameParser
    {
        internal unsafe static string Parse()
        {
            var configPath = Path.Combine(Plugin.PluginInterface.ConfigDirectory.Parent.FullName, "PetRenamer.json");
            var character = Plugin.ClientState.LocalPlayer;
            var player = (FFXIVClientStructs.FFXIV.Client.Game.Character.Character*)character.Address;

            var json = File.ReadAllText(configPath);
            var parsedConfig = JsonConvert.DeserializeObject<dynamic>(json);

            var pet = parsedConfig.SerializableUsersV5[0]
                //.FirstOrDefault(x => x.Name == character.Name && x.Homeworld == character.HomeWorld.RowId)
                .SerializableNameDatas[0]
                //.FirstOrDefault(FFXIVClientStructs => FFXIVClientStructs.IDS.contains(player->CompanionData.CompanionId))
                .Names[0];

            return pet != null
                ? pet
                : player->CompanionData.CompanionObject->NameString;
        }
    }
}
