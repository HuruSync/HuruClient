using System.Text.Json.Nodes;

namespace HuruClient.Parsers
{
    internal static class PetNameParser
    {
        internal unsafe static string Parse()
        {
            return Plugin.ClientState.LocalPlayer.CurrentMinion.Value.Value.Singular.ToString();
            var player = (FFXIVClientStructs.FFXIV.Client.Game.Character.Character*)Plugin.ClientState.LocalPlayer.Address;
            return $"{player->CompanionData.CompanionObject->NameString}";
        }
    }
}
