//using ColossalFramework;
using ColossalFramework.UI;
//using ColossalFramework.IO;
//using ColossalFramework.Globalization;
//using ColossalFramework.DataBinding;
//using ColossalFramework.Importers;
using ICities;
using UnityEngine;
//using UnityEngine.UI;
using System;
//using System.Text;
using System.Collections.Generic;
using System.Threading;
//using System.Reflection;
//using System.Threading.Tasks;
//using System.Linq;

namespace FavoriteCims
{
	public class FavCimsMainClass : LoadingExtensionBase
	{
		//Generic
		public UIView uiView;
		public static bool UnLoading = false;
		MyAtlas Atlas = new MyAtlas();
				
		//Menu Button
		UITabstrip MainMenuPos;
		FavoritesCimsButton FavCimsMenuPanel;

		public const int MaxTemplates = 5;
		public static FamilyPanelTemplate[] Templates = new FamilyPanelTemplate[MaxTemplates];
		
		//Main Mod Panel
		public static UIPanel FullScreenContainer;
		public static UIPanel FavCimsPanel { get; set; }
		
		//Humans Citizens Panel
		public static UIPanel FavCimsHumanPanel;
		AddToFavButton FavStarButton;

		public override void OnLevelLoaded(LoadMode mode)
		{
			if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
				return;

			UnLoading = false;
			CreateGraphics ();
		}
				
		internal void GenerateFamilyDetailsTpl() {
			
			for(int i = 0; i < MaxTemplates; i++) {
				if(FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i) != null) {
					Templates[i] = FullScreenContainer.Find<FamilyPanelTemplate>("FavCimsFamilyTemplate_" + i);
					Templates[i].MyInstanceID = InstanceID.Empty;
					Templates[i].Hide();
				}else {
					Templates[i] = FullScreenContainer.AddUIComponent(typeof(FamilyPanelTemplate)) as FamilyPanelTemplate;
					Templates[i].name = "FavCimsFamilyTemplate_" + i;
					Templates[i].MyInstanceID = InstanceID.Empty;
					Templates[i].Hide();
				}
			}
			return;
		}

		internal void CreateGraphics() {
			
			try {

				var uiView = UIView.GetAView();
				TextureDB.LoadFavCimsTextures();
				Atlas.LoadAtlasIcons();
				
				////////////////////////////////////////////////
				///////////Favorite Button Manu Panel/////////
				///////////////////////////////////////////////

				//MainMenuPos = UIView.GetAView().FindUIComponent<UITabstrip> ("MainToolstrip");
				MainMenuPos = UIView.Find<UITabstrip> ("MainToolstrip");

				if(MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel") != null) {
					FavCimsMenuPanel = MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel");
				}else{
					FavCimsMenuPanel = MainMenuPos.AddUIComponent(typeof(FavoritesCimsButton)) as FavoritesCimsButton;
				}

				////////////////////////////////////////////////
				////////////////Favorite Panel////////////////
				///////////////////////////////////////////////
				
				FullScreenContainer = UIView.Find<UIPanel> ("FullScreenContainer");
				FavCimsPanel = FullScreenContainer.AddUIComponent<FavoriteCimsMainPanel> ();
				FavCimsPanel.Hide ();

				FullScreenContainer.eventMouseDown += delegate {
					if (!FavCimsPanel.containsMouse) {
						FavCimsPanel.SendToBack ();
					} else {
						FavCimsPanel.BringToFront ();
					}
				};

				////////////////////////////////////////////////
				////////////Humans Button & Subscribe///////////
				///////////////////////////////////////////////

				FavCimsHumanPanel = FullScreenContainer.Find<UIPanel> ("(Library) CitizenWorldInfoPanel");
				
				if (FavCimsHumanPanel != null) {
					if(FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>() != null) {
						FavStarButton = FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>();
					}else{
						FavStarButton = FavCimsHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
					}
				}

				GenerateFamilyDetailsTpl();
				
			} catch (Exception e) {
				Debug.Error ("OnLoad List Error : " + e.ToString ());
			}
		}

		internal void DestroyGraphics() {

			UnLoading = true;
			FavCimsCore.ClearIdArray();
			
			try {

				if(FavCimsPanel != null) {
					GameObject.Destroy(FavCimsPanel.gameObject);
				}

			} catch (Exception e) {
				Debug.Error(e.ToString());
			}
		}
		
		public override void OnLevelUnloading ()
		{
			DestroyGraphics ();
		}
	}
}
