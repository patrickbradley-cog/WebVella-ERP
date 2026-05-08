using Newtonsoft.Json;
using System;

namespace WebVella.Erp.Plugins.TravelERP
{
	public partial class TravelErpPlugin : ErpPlugin
	{
		private const int TRAVELERP_INIT_VERSION = 0;

		public void ProcessPatches()
		{
			var currentPluginSettings = new PluginSettings() { Version = TRAVELERP_INIT_VERSION };
			string jsonData = GetPluginData();
			if (!string.IsNullOrWhiteSpace(jsonData))
				currentPluginSettings = JsonConvert.DeserializeObject<PluginSettings>(jsonData);

			//Patch 20250101 - Initial TravelERP Setup
			{
				var patchVersion = 20250101;
				if (currentPluginSettings.Version < patchVersion)
				{
					currentPluginSettings.Version = patchVersion;
					Patch20250101();
				}
			}

			//Patch 20250102 - Add missing required fields that were silently dropped in 20250101
			{
				var patchVersion = 20250102;
				if (currentPluginSettings.Version < patchVersion)
				{
					currentPluginSettings.Version = patchVersion;
					Patch20250102();
				}
			}

			SavePluginData(JsonConvert.SerializeObject(currentPluginSettings));
		}
	}
}
