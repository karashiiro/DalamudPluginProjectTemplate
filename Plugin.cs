using System;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using DalamudPluginProjectTemplate.Attributes;

namespace DalamudPluginProjectTemplate
{
    public class Plugin : IDalamudPlugin
    {
        [PluginService]
        [RequiredVersion("1.0")]
        private DalamudPluginInterface PluginInterface { get; init; }

        [PluginService]
        [RequiredVersion("1.0")]
        private CommandManager Commands { get; init; }

        [PluginService]
        [RequiredVersion("1.0")]
        private ChatGui Chat { get; init; }

        [PluginService]
        [RequiredVersion("1.0")]
        private ClientState ClientState { get; init; }

        private readonly PluginCommandManager<Plugin> commandManager;
        private readonly Configuration config;
        private readonly PluginUI ui;

        public string Name => "Your Plugin's Display Name";

        public Plugin()
        {
            this.config = (Configuration)PluginInterface.GetPluginConfig() ?? new Configuration();
            this.config.Initialize(PluginInterface);

            this.ui = new PluginUI();
            PluginInterface.UiBuilder.Draw += this.ui.Draw;

            this.commandManager = new PluginCommandManager<Plugin>(this, Commands);
        }

        [Command("/example1")]
        [HelpMessage("Example help message.")]
        public void ExampleCommand1(string command, string args)
        {
            // You may want to assign these references to private variables for convenience.
            // Keep in mind that the local player does not exist until after logging in.
            var world = ClientState.LocalPlayer?.CurrentWorld.GameData;
            Chat.Print($"Hello {world?.Name}!");
            PluginLog.Log("Message sent successfully.");
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.commandManager.Dispose();

            PluginInterface.SavePluginConfig(this.config);

            PluginInterface.UiBuilder.Draw -= this.ui.Draw;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
