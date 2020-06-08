using System;
using Dalamud.Plugin;
using DalamudPluginProjectTemplate.Attributes;

namespace DalamudPluginProjectTemplate
{
    public class Plugin : IDalamudPlugin
    {
        private DalamudPluginInterface pluginInterface;
        private PluginCommandManager<Plugin> commandManager;
        private PluginConfiguration config;
        private PluginUI ui;

        public string Name => "Your Plugin's Display Name";

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;

            // If your plugin doesn't need a configuration, you can safely remove all references to this variable and class.
            this.config = (PluginConfiguration)this.pluginInterface.GetPluginConfig() ?? new PluginConfiguration();

            // Likewise here.
            this.ui = new PluginUI();
            this.pluginInterface.UiBuilder.OnBuildUi += this.ui.Draw;

            this.commandManager = new PluginCommandManager<Plugin>(this, this.pluginInterface);
        }

        [Command("/example1")]
        [HelpMessage("Example help message.")]
        [ShowInHelp]
        public void ExampleCommand1(string command, string args)
        {
            // You may want to assign these references to private variables for convenience.
            // Keep in mind that the local player does not exist until after logging in.
            var chat = this.pluginInterface.Framework.Gui.Chat;
            var world = this.pluginInterface.ClientState.LocalPlayer.CurrentWorld.GameData;
            chat.Print($"Hello {world.Name}!");
            PluginLog.Log("Message sent successfully.");
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.commandManager.Dispose();

                // You may not want to save a configuration until after you're done tweaking the class layout.
                //this.pluginInterface.SavePluginConfig(this.config);

                this.pluginInterface.UiBuilder.OnBuildUi -= this.ui.Draw;

                this.pluginInterface.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
