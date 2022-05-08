

using System;

namespace nic_z_tego_nie_bd
{
	
	
	// This class allows you to handle specific events on the settings class:
	//  The SettingChanging event is raised before a setting's value is changed.
	//  The PropertyChanged event is raised after a setting's value is changed.
	//  The SettingsLoaded event is raised after the setting values are loaded.
	//  The SettingsSaving event is raised before the setting values are saved.
	public sealed partial class Settings
	{
		//Settings needs to be shared across all versions Repo: https://stackoverflow.com/questions/534261/how-do-you-keep-user-config-settings-across-different-assembly-versions-in-net
		public Settings() {
			var cos = Properties.Settings.Default.itemsUCsize;
			if (!(34 <= cos && cos <= 68)) throw new Exception("Haha chciałbyś");
			Properties.Settings.Default.itemsUCsize = 34;
			Properties.Settings.Default.Save();
			// // To add event handlers for saving and changing settings, uncomment the lines below:
			//
			// this.SettingChanging += this.SettingChangingEventHandler;
			//
			// this.SettingsSaving += this.SettingsSavingEventHandler;
			//
		}
		
		private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
			// Add code to handle the SettingChangingEvent event here.
		}
		
		private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
			// Add code to handle the SettingsSaving event here.
		}
	}
}
