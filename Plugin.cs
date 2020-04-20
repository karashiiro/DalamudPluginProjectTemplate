using Dalamud.Game.Command;
using Dalamud.Plugin;
using Lumina.Excel.GeneratedSheets;
using System;
using static Dalamud.Game.Command.CommandInfo;

namespace DalamudPluginProjectTemplate
{
    public class Plugin : IDalamudPlugin
    {
        private DalamudPluginInterface pluginInterface;
        private PluginConfiguration config;

        public string Name => "Your Plugin's Display Name";

        /// <summary>
        /// Initializes the Dalamud plugin. Please be aware that not all game objects will be initialized at this point;
        /// certain objects will need to be referenced after logging into the <see cref="World"/>.
        /// </summary>
        /// <param name="pluginInterface">The <see cref="DalamudPluginInterface"/> needed to access various Dalamud objects.</param>
        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
            // If your plugin doesn't need a configuration, you can safely remove all references to this variable and class.
            this.config = (PluginConfiguration)this.pluginInterface.GetPluginConfig() ?? new PluginConfiguration();

            AddComandHandlers();
        }

        /// <summary>
        /// All commands must implement <see cref="HandlerDelegate"/>.
        /// </summary>
        private void ExampleCommand1(string command, string args)
        {
            // You may want to assign these references to private variables for convenience.
            // Keep in mind that the local player does not exist until after logging in.
            var chat = this.pluginInterface.Framework.Gui.Chat;
            var world = this.pluginInterface.ClientState.LocalPlayer.CurrentWorld.GameData;
            chat.Print($"Hello {world.Name}!");
            Log("Message sent successfully.");
        }

        private void AddComandHandlers()
        {
            this.pluginInterface.CommandManager.AddHandler("/example1", new CommandInfo(ExampleCommand1)
            {
                HelpMessage = "Example help message.",
                ShowInHelp = true,
            });
        }

        private void RemoveCommandHandlers()
        {
            // Remember to remove your handlers when your plugin is unloaded on updates or game exits.
            // You may want to write some kind of organizer that does this for you.
            this.pluginInterface.CommandManager.RemoveHandler("/example1");
        }

        #region Logging Shortcuts
        // You may want to create a singleton or single-instance class to access these methods
        // throughout your plugin, especially if it's more complicated than this, but example
        // purposes, this is a private instanced shortcut.
        private void Log(string messageTemplate, params object[] values)
            => this.pluginInterface.Log(messageTemplate, values);

        private void LogError(string messageTemplate, params object[] values)
            => this.pluginInterface.LogError(messageTemplate, values);

        private void LogError(Exception exception, string messageTemplate, params object[] values)
            => this.pluginInterface.LogError(exception, messageTemplate, values);
        #endregion

        #region IDisposable Support
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                // TODO release managed resources here
                RemoveCommandHandlers();
                // You may not want to save a configuration until after you're done tweaking the class layout.
                //this.pluginInterface.SavePluginConfig(this.config);
                this.pluginInterface.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Plugin()
        {
            Dispose(false);
        }
        #endregion
    }
}
