using UnityEngine;
using System;
using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;

namespace FavoriteCims {
	public static class Debug {
		const string prefix = "FavoriteCimsMod: ";
		
		public static void Log(String message) {
			DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, prefix + message);
		}
		
		public static void Error(String message) {
			DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, prefix + message);
		}
		
		public static void Warning(String message) {
			DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, prefix + message);
		}

		public static bool withoutflood = false;
	}

	public static class GuiDebug {
	
		public static void Log(String message) {

			UIPanel FullScrCont = UIView.Find<UIPanel> ("FullScreenContainer");
			UILabel Label;

			if (FullScrCont != null) {

				if (FullScrCont.Find<UILabel> ("FavCimsDebugLabel") == null) {
					Label = FullScrCont.AddUIComponent<UILabel> ();
					Label.name = "FavCimsDebugLabel";
					Label.width = 700;
					Label.height = 300;
					Label.relativePosition = new Vector3 (200, 20);
					//Label.wordWrap = true;
				} else {
					Label = FullScrCont.Find<UILabel> ("FavCimsDebugLabel");
				}

				Label.text = message;
			}
		}

		public static void Destroy() {

			UIPanel FullScrCont = UIView.Find<UIPanel> ("FullScreenContainer");

			if (FullScrCont.Find<UILabel> ("FavCimsDebugLabel") != null)
				GameObject.Destroy (FullScrCont.Find<UILabel> ("FavCimsDebugLabel").gameObject);
		}
	}

	public static class GameTime {

		public static string FavCimsDate(string format, string oldformat) {

			if (Singleton<SimulationManager>.exists) {

				string d = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Day.ToString ();
				string m = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Month.ToString ();
				string y = Singleton<SimulationManager>.instance.m_currentGameTime.Date.Year.ToString ();

				if(oldformat != "n/a") {
					string[] elements = oldformat.Split('/');
					if(elements[0] != null && elements[1] != null && elements[2] != null) {
						return elements[1] + "/" + elements[0] + "/" + elements[2];
					}

					return oldformat;
				}else if (format == "dd-mm-yyyy") {
					return d + "/" + m + "/" + y;
				} else {
					return m + "/" + d + "/" + y;
				}

			}

			return format;

		}

		public static string FavCimsTime() {

			string h = Singleton<SimulationManager>.instance.m_currentGameTime.Hour.ToString ();
			string m = Singleton<SimulationManager>.instance.m_currentGameTime.Minute.ToString ();

			if (h.Length == 1) {
				h = "0" + h;
			}
			if (m.Length == 1) {
				m = "0" + m;
			}

			return h + ":" + m;

		}

	}
}