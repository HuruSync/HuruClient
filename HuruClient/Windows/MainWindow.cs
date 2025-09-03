using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Configuration;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using HuruClient.Parsers;

namespace HuruClient.Windows;

public class MainWindow : Window, IDisposable
{
    private readonly Plugin plugin;


    // We give this window a hidden ID using ##
    // So that the user will see "My Amazing Window" as window title,
    // but for ImGui the ID is "My Amazing Window##With a hidden ID"
    public MainWindow(Plugin plugin)
        : base("Debug Info", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(200, 200),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        // Do not use .Text() or any other formatted function like TextWrapped(), or SetTooltip().
        // These expect formatting parameter if any part of the text contains a "%", which we can't
        // provide through our bindings, leading to a Crash to Desktop.
        // Replacements can be found in the ImGuiHelpers Class

        var localPlayer = Plugin.ClientState.LocalPlayer;
        if (localPlayer == null)
        {
            ImGui.TextUnformatted("Our local player is currently not loaded.");
            return;
        }

        ImGui.TextUnformatted("Loaded:");
        DebugInfo();
        ImGui.TextUnformatted("====");
    }

    private void DebugInfo()
    {
        var activePlugins = Plugin.PluginInterface.InstalledPlugins.Where(x => x.IsLoaded);

        foreach (var match in activePlugins)
        {
            switch (match.Name)
            {
                case "Moodles":
                    break;
                case "Pet Nicknames":
                    ImGui.TextUnformatted(PetNameParser.Parse());
                    break;
                case "Honorific":
                    ImGui.TextUnformatted(HonorificParser.Parse());
                    break;
                default: continue;
            }

            ImGui.TextColored(0xff00dd00, match.Name);
        }
    }
}
