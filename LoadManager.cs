using ICities;
using UnityEngine;

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using ColossalFramework;
using ColossalFramework.UI;
using ColossalFramework.Globalization;

//using ColossalFramework.IO;
//using ColossalFramework.Globalization;
//using ColossalFramework.DataBinding;
//using ColossalFramework.Importers;
//using UnityEngine.UI;
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
		//UITabstrip MainMenuPos;
		//UIPanel MainMenuPos;
		//FavoritesCimsButton FavCimsMenuPanel;
		//UIPanel MyPanel;
		//FavoritesCimsButton FavCimsMenuButton;

		public const int MaxTemplates = 5;
		public static FamilyPanelTemplate[] Templates = new FamilyPanelTemplate[MaxTemplates];
		
		//Main Mod Panel
		public static UIPanel FullScreenContainer;
		public static UIPanel FavCimsPanel { get; set; }
		
		//Humans Citizens Panel
		public static UIPanel FavCimsHumanPanel;
		public static UIPanel FavCimsTouristHumanPanel;
		//public static UIPanel FavCimsCityServiceHumanPanel;
		AddToFavButton FavStarButton;
		AddToFavButton FavStarTouristButton;
		//AddToFavButton FavStarServiceHumanButton;
		public static UIPanel FavCimsHumanPassengerPanel;
		VechiclePassengersButton PassengerButton;
		public static UIPanel FavCimsHumanPublicTransportPanel;
		VechiclePassengersButtonPT PublicTransportPassengersButton;
		//public static UIPanel FavCimsWorkPassengerPanel;
		//VechiclePassengersButtonWork WorkPassengerButton;

		public static UIButton mainButton;
		private UIGroupPanel m_groupPanel;

		public static UIPanel FavCimsPeopleBuildingPanel;
		PeopleInsideBuildingsButton PeopleBuildingButton;
		public static UIPanel FavCimsPeopleServiceBuildingPanel;
		PeopleInsideServiceBuildingsButton PeopleServiceBuildingButton;

		//Triggers
		UIComponent FavCimsPanelTrigger_paneltime;
		UIComponent FavCimsPanelTrigger_chirper;
		UIComponent FavCimsPanelTrigger_esc;
		UIComponent FavCimsPanelTrigger_infopanel;
		UIComponent FavCimsPanelTrigger_bottombars;

		public class UIGroupPanel : GeneratedGroupPanel
		{
			public override ItemClass.Service service
			{
				get
				{
					return ItemClass.Service.None;
				}
			}

			void Update()
			{
				//Hotkey
				if (Input.GetMouseButton (2) && Input.GetKeyDown(KeyCode.F))
				{
					FavCimsPanelToggle();
				}

				GameObject fp = GameObject.Find("FavCimsTabMenuPanel");

				if (fp != null) {

					UIPanel up = fp.GetComponent<UIPanel> ();

					if (up != null) {

						if (up.isVisible) {
							up.Hide ();
						}
					}
				}
			}

			public override string serviceName
			{
				get
				{
					return "FavoriteCims";
				}
			}

			protected override bool IsServiceValid(PrefabInfo info)
			{
				return true;
			}
		}

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

		public static void FavCimsPanelToggle() {
			if (!FavCimsMainClass.FavCimsPanel.isVisible) {
				FavCimsMainClass.FavCimsPanel.CenterTo (FavCimsMainClass.FullScreenContainer);
				FavCimsMainClass.FavCimsPanel.Show();
			} else {
				FavCimsMainClass.FavCimsPanel.Hide();
			}			
		}

		public void FavCimsPanelOff () {

			if (FavCimsMainClass.FavCimsPanel.isVisible && !FavCimsMainClass.FavCimsPanel.containsMouse && !mainButton.containsMouse && (FavCimsPanelTrigger_paneltime != null && !FavCimsPanelTrigger_paneltime.containsMouse)) {
				FavCimsMainClass.FavCimsPanel.Hide ();
			}
		}

		internal void CreateGraphics() 
		{
			try {

				////////////////////////////////////////////////
				///////////Favorite Button Manu Panel/////////
				///////////////////////////////////////////////


				GameObject gameObject = GameObject.Find("FavCimsMenuPanel");
				if (gameObject != null)
				{
					return;
				}

                FavCimsPanelTrigger_chirper = UIView.Find<UIPanel> ("ChirperPanel");
				FavCimsPanelTrigger_esc = UIView.Find<UIButton> ("Esc");
				FavCimsPanelTrigger_infopanel = UIView.Find<UIPanel> ("InfoPanel");
				FavCimsPanelTrigger_bottombars = UIView.Find<UISlicedSprite> ("TSBar");
				FavCimsPanelTrigger_paneltime = UIView.Find<UIPanel> ("PanelTime");

				if (FavCimsPanelTrigger_chirper != null && FavCimsPanelTrigger_paneltime != null) {
					FavCimsPanelTrigger_chirper.eventClick += (component, eventParam) => FavCimsPanelOff ();
				}

				if (FavCimsPanelTrigger_esc != null && FavCimsPanelTrigger_paneltime != null) {
					FavCimsPanelTrigger_esc.eventClick += (component, eventParam) => FavCimsPanelOff ();
				}

				if (FavCimsPanelTrigger_infopanel != null && FavCimsPanelTrigger_paneltime != null) {
					FavCimsPanelTrigger_infopanel.eventClick += (component, eventParam) => FavCimsPanelOff ();
				}

				if (FavCimsPanelTrigger_bottombars != null && FavCimsPanelTrigger_paneltime != null) {
					FavCimsPanelTrigger_bottombars.eventClick += (component, eventParam) => FavCimsPanelOff ();
				}

				var uiView = UIView.GetAView();
				TextureDB.LoadFavCimsTextures();
				Atlas.LoadAtlasIcons();

				UITabstrip tabstrip = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();

                if (tabstrip.Find("FavCimsMenuPanel") || GameObject.Find("MainToolbarButtonTemplate") || GameObject.Find("ScrollableSubPanelTemplate"))
                {
                    return;
                }

                GameObject asGameObject = UITemplateManager.GetAsGameObject("MainToolbarButtonTemplate");
				GameObject asGameObject2 = UITemplateManager.GetAsGameObject("ScrollableSubPanelTemplate");

				mainButton = tabstrip.AddTab("FavCimsMenuPanel", asGameObject, asGameObject2, new Type[] { typeof(UIGroupPanel) }) as UIButton;

				mainButton.normalBgSprite = "FavoriteCimsButton";
				//TestButton.disabledBgSprite = "";
				mainButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
				mainButton.focusedBgSprite = "FavoriteCimsButtonFocused";
				mainButton.pressedBgSprite = "FavoriteCimsButtonPressed";
				mainButton.playAudioEvents = true;
				mainButton.name = "FavCimsButton";
				mainButton.tooltipBox = uiView.defaultTooltipBox;
				mainButton.atlas = MyAtlas.FavCimsAtlas;
				mainButton.size = new Vector2(49,49);
				mainButton.eventClick += (component, eventParam) => FavCimsPanelToggle ();
				mainButton.tooltip = "Favorite Cims " + FavoriteCimsModMain.Version;

				Locale locale = (Locale)typeof(LocaleManager).GetField("m_Locale", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(LocaleManager.instance);
				Locale.Key key = new Locale.Key
				{
					m_Identifier = "TUTORIAL_ADVISER_TITLE",
					m_Key = mainButton.name
				};
				if (!locale.Exists(key))
				{
					locale.AddLocalizedString(key, "Favorite Cims " + FavoriteCimsModMain.Version);
				}
				key = new Locale.Key
				{
					m_Identifier = "TUTORIAL_ADVISER",
					m_Key = mainButton.name
				};
				if (!locale.Exists(key))
				{//"Thanks for subscribing to Favorite Cims!\n\nHotkey = Press Middle Mouse Button + F \n\nMod Version : v0.3e by Gianxs");
					locale.AddLocalizedString(key, "Thanks for subscribing to Favorite Cims!\n\nHotkey = Press Middle Mouse Button + F \nIf you like the mod please consider leaving a rating on the steam workshop.\nMod Version : " + FavoriteCimsModMain.Version + " by Gianxs");
				}

				FieldInfo m_ObjectIndex = typeof(MainToolbar).GetField("m_ObjectIndex", BindingFlags.Instance | BindingFlags.NonPublic);
				m_ObjectIndex.SetValue(ToolsModifierControl.mainToolbar, (int)m_ObjectIndex.GetValue(ToolsModifierControl.mainToolbar) + 1);

				//mainButton.gameObject.GetComponent<TutorialUITag>().tutorialTag = "";
				m_groupPanel = tabstrip.GetComponentInContainer(mainButton, typeof(UIGroupPanel)) as UIGroupPanel;

				if (m_groupPanel != null)
				{
					m_groupPanel.name = "FavCimsTabMenuPanel";
					m_groupPanel.enabled = true;
					m_groupPanel.component.isInteractive = true;
					m_groupPanel.m_OptionsBar = ToolsModifierControl.mainToolbar.m_OptionsBar;
					m_groupPanel.m_DefaultInfoTooltipAtlas = ToolsModifierControl.mainToolbar.m_DefaultInfoTooltipAtlas;
					if (ToolsModifierControl.mainToolbar.enabled)
					{
						m_groupPanel.RefreshPanel();
					}

				}

				/*

				UITabContainer BasePanel = UIView.Find<UITabContainer> ("TSContainer");

				if(BasePanel.Find<UIPanel>("FavCimsTabMenuPanel") != null) {
					MyPanel = BasePanel.Find<UIPanel>("FavCimsTabMenuPanel");
				}else{
					MyPanel = BasePanel.AddUIComponent(typeof(UIPanel)) as UIPanel;

					MyPanel.name = "FavCimsTabMenuPanel";
					MyPanel.width = 49;
					MyPanel.height = 49;

					//MyPanel.BringToFront();
					//MyPanel.autoLayout = true;
				}

				MainMenuPos = UIView.Find<UITabstrip> ("MainToolstrip");

				if(MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel") != null) {
					FavCimsMenuPanel = MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel");
				}else{
					FavCimsMenuPanel = MainMenuPos.AddUIComponent(typeof(FavoritesCimsButton)) as FavoritesCimsButton;
				}


				if(MainMenuPos.Find<FavoritesCimsButton>("FavCimsButton") != null) {
					FavCimsMenuButton = MainMenuPos.Find<FavoritesCimsButton>("FavCimsButton");
				}else{
					FavCimsMenuButton = MainMenuPos.AddUIComponent(typeof(FavoritesCimsButton)) as FavoritesCimsButton;
				}

				//MainMenuPos = UIView.GetAView().FindUIComponent<UITabstrip> ("MainToolstrip");
				MainMenuPos = UIView.Find<UITabstrip> ("MainToolstrip");
				//MainMenuPos = UIView.Find<UIPanel> ("InfoPanel");
				//MainMenuPos.AlignTo (UIView.Find<UISprite> ("Happiness"), UIAlignAnchor.BottomLeft);
				//MainMenuPos = ToolsModifierControl.mainToolbar.GetComponentInChildren<UITabstrip>();

				if(MainMenuPos.Find<FavoritesCimsButton>("FavCimsButton") != null) {
					FavCimsMenuButton = MainMenuPos.Find<FavoritesCimsButton>("FavCimsButton");
				}else{
					FavCimsMenuButton = MainMenuPos.AddUIComponent(typeof(FavoritesCimsButton)) as FavoritesCimsButton;
				}

				if(MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel") != null) {
					FavCimsMenuPanel = MainMenuPos.Find<FavoritesCimsButton>("FavCimsMenuPanel");
				}else{
					FavCimsMenuPanel = MainMenuPos.AddUIComponent(typeof(FavoritesCimsButton)) as FavoritesCimsButton;
				}
				*/

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
				////////////Human Buttons to Subscribe//////////
				///////////////////////////////////////////////

				FavCimsHumanPanel = FullScreenContainer.Find<UIPanel> ("(Library) CitizenWorldInfoPanel");

				if (FavCimsHumanPanel != null) {
					if(FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>() != null) {
						FavStarButton = FavCimsHumanPanel.GetComponentInChildren<AddToFavButton>();
					}else{
						FavStarButton = FavCimsHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
					}
					FavStarButton.RefPanel = FavCimsHumanPanel;
					FavStarButton.Alignment = UIAlignAnchor.BottomRight;
				}

				FavCimsTouristHumanPanel = FullScreenContainer.Find<UIPanel> ("(Library) TouristWorldInfoPanel");

				if (FavCimsTouristHumanPanel != null) {
					if(FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>() != null) {
						FavStarTouristButton = FavCimsTouristHumanPanel.GetComponentInChildren<AddToFavButton>();
					}else{
						FavStarTouristButton = FavCimsTouristHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
					}
					FavStarTouristButton.RefPanel = FavCimsTouristHumanPanel;
					FavStarTouristButton.Alignment = UIAlignAnchor.BottomRight;
				}

				/* Instance are deleted too fast
				FavCimsCityServiceHumanPanel = FullScreenContainer.Find<UIPanel> ("(Library) ServicePersonWorldInfoPanel");

				if (FavCimsCityServiceHumanPanel != null) {
					if(FavCimsCityServiceHumanPanel.GetComponentInChildren<AddToFavButton>() != null) {
						FavStarServiceHumanButton = FavCimsCityServiceHumanPanel.GetComponentInChildren<AddToFavButton>();
					}else{
						FavStarServiceHumanButton = FavCimsCityServiceHumanPanel.AddUIComponent(typeof(AddToFavButton)) as AddToFavButton;
						FavStarServiceHumanButton.RefPanel = FavCimsCityServiceHumanPanel;
						FavStarServiceHumanButton.Alignment = UIAlignAnchor.BottomRight;
					}
				}

				*/
				////////////////////////////////////////////////
				////////Private Vehicle Passengers Button//////
				///////////////////////////////////////////////

				FavCimsHumanPassengerPanel = FullScreenContainer.Find<UIPanel> ("(Library) CitizenVehicleWorldInfoPanel");

				if (FavCimsHumanPassengerPanel != null) {
					if(FavCimsHumanPassengerPanel.GetComponentInChildren<VechiclePassengersButton>() != null) {
						PassengerButton = FavCimsHumanPassengerPanel.GetComponentInChildren<VechiclePassengersButton>();
					}else{
						PassengerButton = FavCimsHumanPassengerPanel.AddUIComponent(typeof(VechiclePassengersButton)) as VechiclePassengersButton;
					}
					PassengerButton.RefPanel = FavCimsHumanPassengerPanel;
					PassengerButton.Alignment = UIAlignAnchor.BottomRight;
				}

				////////////////////////////////////////////////
				////////Service Vehicle Passengers Button//////
				///////////////////////////////////////////////
				/* Instance are deleted too fast
				FavCimsWorkPassengerPanel = FullScreenContainer.Find<UIPanel> ("(Library) CityServiceVehicleWorldInfoPanel");

				if (FavCimsWorkPassengerPanel != null) {
					if(FavCimsWorkPassengerPanel.GetComponentInChildren<VechiclePassengersButtonWork>() != null) {
						WorkPassengerButton = FavCimsWorkPassengerPanel.GetComponentInChildren<VechiclePassengersButtonWork>();
					}else{
						WorkPassengerButton = FavCimsWorkPassengerPanel.AddUIComponent(typeof(VechiclePassengersButtonWork)) as VechiclePassengersButtonWork;
					}
					WorkPassengerButton.RefPanel = FavCimsWorkPassengerPanel;
					WorkPassengerButton.Alignment = UIAlignAnchor.BottomRight;
				}
				*/
				////////////////////////////////////////////////
				///Public Transport Vehicle Passengers Button///
				///////////////////////////////////////////////

				FavCimsHumanPublicTransportPanel = FullScreenContainer.Find<UIPanel> ("(Library) PublicTransportVehicleWorldInfoPanel");

				if (FavCimsHumanPublicTransportPanel != null) {
					if(FavCimsHumanPublicTransportPanel.GetComponentInChildren<VechiclePassengersButtonPT>() != null) {
						PublicTransportPassengersButton = FavCimsHumanPublicTransportPanel.GetComponentInChildren<VechiclePassengersButtonPT>();
					}else{
						PublicTransportPassengersButton = FavCimsHumanPublicTransportPanel.AddUIComponent(typeof(VechiclePassengersButtonPT)) as VechiclePassengersButtonPT;
					}
					PublicTransportPassengersButton.RefPanel = FavCimsHumanPublicTransportPanel;
					PublicTransportPassengersButton.Alignment = UIAlignAnchor.BottomRight;
				}

				///////////////////////////////////////////////
				/////////People Inside Buildings Button////////
				///////////////////////////////////////////////

				FavCimsPeopleBuildingPanel = FullScreenContainer.Find<UIPanel> ("(Library) ZonedBuildingWorldInfoPanel");

				if (FavCimsPeopleBuildingPanel != null) {
					if(FavCimsPeopleBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>() != null) {
						PeopleBuildingButton = FavCimsPeopleBuildingPanel.GetComponentInChildren<PeopleInsideBuildingsButton>();
					}else{
						PeopleBuildingButton = FavCimsPeopleBuildingPanel.AddUIComponent(typeof(PeopleInsideBuildingsButton)) as PeopleInsideBuildingsButton;
					}
					PeopleBuildingButton.RefPanel = FavCimsPeopleBuildingPanel;
					PeopleBuildingButton.Alignment = UIAlignAnchor.BottomRight;
				}

				///////////////////////////////////////////////
				/////People Inside Service Buildings Button////
				///////////////////////////////////////////////

				FavCimsPeopleServiceBuildingPanel = FullScreenContainer.Find<UIPanel> ("(Library) CityServiceWorldInfoPanel");

				if (FavCimsPeopleServiceBuildingPanel != null) {
					if(FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideServiceBuildingsButton>() != null) {
						PeopleServiceBuildingButton = FavCimsPeopleServiceBuildingPanel.GetComponentInChildren<PeopleInsideServiceBuildingsButton>();
					}else{
						PeopleServiceBuildingButton = FavCimsPeopleServiceBuildingPanel.AddUIComponent(typeof(PeopleInsideServiceBuildingsButton)) as PeopleInsideServiceBuildingsButton;
					}
					PeopleServiceBuildingButton.RefPanel = FavCimsPeopleServiceBuildingPanel;
					PeopleServiceBuildingButton.Alignment = UIAlignAnchor.BottomRight;
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

                if(mainButton != null)
                {
                    GameObject.Destroy(mainButton.gameObject);
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