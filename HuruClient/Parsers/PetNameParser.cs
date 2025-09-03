using Dalamud.Bindings.ImGui;
using Newtonsoft.Json;

namespace HuruClient.Parsers
{
    internal static class PetNameParser
    {
        internal unsafe static string Parse()
        {
            var configPath = Path.Combine(Plugin.PluginInterface.ConfigDirectory.Parent.FullName, "PetRenamer.json");
            var character = Plugin.ClientState.LocalPlayer;

            var json = File.ReadAllText(configPath);
            var parsedConfig = JsonConvert.DeserializeObject<dynamic>(json);
            

            var petId = character.CurrentMinion.Value.Value.Model.RowId;
            ImGui.TextUnformatted(petId.ToString());
            foreach (var user in parsedConfig?.SerializableUsersV5)
            {
                if (user.Name != character.Name || user.Homeworld != character.HomeWorld.RowId)
                {
                    continue;
                }

                foreach (var data in user.SerializableNameDatas)
                {
                    var ids = JsonConvert.DeserializeObject<uint[]?>((string)data.IDS.ToString());
                    if (ids == null || !ids.Contains(petId))
                    {
                        continue;
                    }

                    return data.Names[0];
                }
            }

            // Fall back on default model name
            var player = (FFXIVClientStructs.FFXIV.Client.Game.Character.Character*)character.Address;
            return player->CompanionData.CompanionObject->NameString;
        }
    }
}
