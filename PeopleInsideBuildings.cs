using ColossalFramework;
using ColossalFramework.UI;
using ColossalFramework.Globalization;
using ICities;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FavoriteCims
{
	public class PeopleInsideBuildingsButton : UIButton
	{
		InstanceID BuildingID = InstanceID.Empty;
		BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		
		public UIAlignAnchor Alignment;
		public UIPanel RefPanel;
		PeopleInsideBuildingsPanel BuildingPanel;

		public override void Start() {
			
			var uiView = UIView.GetAView();
			
			////////////////////////////////////////////////
			////////////Building Button///////////////////
			///////////////////////////////////////////////

			this.name = "PeopleInsideBuildingsButton";
			this.atlas = MyAtlas.FavCimsAtlas;
			this.size = new Vector2(32,36);
			this.playAudioEvents = true;
			this.AlignTo (RefPanel, Alignment);
			this.tooltipBox = uiView.defaultTooltipBox;
			
			if (FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel> () != null) {
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideBuildingsPanel>();
			} else {
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideBuildingsPanel)) as PeopleInsideBuildingsPanel;
			}
			
			this.BuildingPanel.BuildingID = InstanceID.Empty;
			this.BuildingPanel.Hide();
			
			this.eventClick += (component, eventParam) => {
				if(!BuildingID.IsEmpty && !BuildingPanel.isVisible) {
					this.BuildingPanel.BuildingID = BuildingID;
					this.BuildingPanel.RefPanel = RefPanel;
					this.BuildingPanel.Show();
				} else {
					this.BuildingPanel.BuildingID = InstanceID.Empty;
					this.BuildingPanel.Hide();
				}
			};
		}
		
		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;
			
			if (this.isVisible) {
				
				this.tooltip = null;
				
				if(ZonedBuildingWorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty) {
					BuildingID = ZonedBuildingWorldInfoPanel.GetCurrentInstanceID();
				}
				
				if(BuildingPanel != null) {
					if(!BuildingPanel.isVisible) {
						this.Unfocus();
					} else {
						this.Focus();
					}
				}
				
				if (!BuildingID.IsEmpty && BuildingID.Type == InstanceType.Building) {

					BuildingInfo buildingInfo = MyBuilding.m_buildings.m_buffer [BuildingID.Building].Info;

					if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {
						this.tooltip = FavCimsLang.text("Citizens_HouseHolds");
						this.normalBgSprite = "BuildingButtonIcon";
						this.hoveredBgSprite = "BuildingButtonIconHovered";
						this.focusedBgSprite = "BuildingButtonIconHovered";
						this.pressedBgSprite = "BuildingButtonIconHovered";
						this.disabledBgSprite = "BuildingButtonIconDisabled";
					} else if (buildingInfo.m_class.m_service == ItemClass.Service.Commercial) {
						this.tooltip = FavCimsLang.text("CitizenOnBuilding");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
					} else if (buildingInfo.m_class.m_service == ItemClass.Service.Office) {
						this.tooltip = FavCimsLang.text("WorkersOnBuilding");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
					} else {
						this.tooltip = FavCimsLang.text("WorkersOnBuilding");
						this.normalBgSprite = "IndustrialBuildingButtonIcon";
						this.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
						this.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
					}

					if(Convert.ToInt32(MyBuilding.m_buildings.m_buffer[BuildingID.Building].m_citizenCount) == 0) {
						BuildingPanel.Hide ();
						this.tooltip = FavCimsLang.text("BuildingIsEmpty");
						this.isEnabled = false; //disabled sprite
					} else {
						this.isEnabled = true; //normal sprite
					}
				} else { 
					BuildingPanel.Hide ();
					this.Unfocus ();
					this.isEnabled = false; //disabled sprite
				}
			} else {
				this.isEnabled = false; //disabled sprite
				BuildingPanel.Hide ();
				BuildingID = InstanceID.Empty;
			}
		}
	}
	
	public class PeopleInsideBuildingsPanel : UIPanel
	{
		const float Run = 0.5f;
		float seconds = Run;
		bool execute = false;
		bool firstRun = true;

		public static bool Wait = false;
		bool Garbage = false;

		public InstanceID BuildingID;
		public UIPanel RefPanel;
		BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		public static Dictionary<uint, uint> CimsOnBuilding = new Dictionary<uint, uint> ();

		public static int WorkersCount = 0;
		public static int GuestsCount = 0;

		BuildingInfo buildingInfo;

		const int MaxHouseHolds = 26;
		const int MaxWorkersUnit = 20; // **Important** *same of MaxGuestsUnit*
		const int MaxGuestsUnit = 20;

		UIPanel[] ResidentialPanels = new UIPanel[MaxHouseHolds];
		UIPanel[] ResidentialPanelSubRow = new UIPanel[MaxHouseHolds];
		UIButton[] ResidentialPanelIcon = new UIButton[MaxHouseHolds];
		UIButton[] ResidentialPanelText = new UIButton[MaxHouseHolds];
		ResidentialBuildingPanelRow[] ResidentialBodyRow = new ResidentialBuildingPanelRow[MaxHouseHolds*5];

		UIPanel WorkersPanel;
		UIPanel WorkersPanelSubRow;
		UIButton WorkersPanelIcon;
		UIButton WorkersPanelText;
		WorkersBuildingPanelRow[] WorkersBodyRow = new WorkersBuildingPanelRow[MaxWorkersUnit*5];

		UIPanel GuestsPanel;
		UIPanel GuestsPanelSubRow;
		UIButton GuestsPanelIcon;
		UIButton GuestsPanelText;
		GuestsBuildingPanelRow[] GuestsBodyRow = new GuestsBuildingPanelRow[MaxGuestsUnit*5];

		uint BuildingUnits;
		
		UIPanel Title;
		UITextureSprite TitleSpriteBg;
		UIButton TitleBuildingName;
		UIPanel Body;
		UITextureSprite BodySpriteBg;
		UIScrollablePanel BodyRows;
		UIPanel Footer;
		UITextureSprite FooterSpriteBg;
		
		UIScrollablePanel BodyPanelScrollBar;
		UIScrollbar BodyScrollBar;
		UISlicedSprite BodyPanelTrackSprite;
		UISlicedSprite thumbSprite;
		
		////////////////////////////////////////////////
		////////////Inside Building Panel//////////////
		///////////////////////////////////////////////
		
		public override void Start() {
			try
			{
				this.width = 250;
				this.height = 0;
				this.name = "FavCimsPeopleInsideBuildingsPanel";
				this.absolutePosition = new Vector3(0, 0);
				this.Hide ();
				
				Title = this.AddUIComponent<UIPanel> ();
				Title.name = "PeopleInsideBuildingsPanelTitle";
				Title.width = this.width;
				Title.height = 41;
				Title.relativePosition = Vector3.zero;
				
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite> ();
				TitleSpriteBg.name = "PeopleInsideBuildingsPanelTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				
				//UIButton Building Name
				TitleBuildingName = Title.AddUIComponent<UIButton> ();
				TitleBuildingName.name = "PeopleInsideBuildingsPanelName";
				TitleBuildingName.width = Title.width;
				TitleBuildingName.height = Title.height;
				TitleBuildingName.textVerticalAlignment = UIVerticalAlignment.Middle;
				TitleBuildingName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				TitleBuildingName.playAudioEvents = false;
				TitleBuildingName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
				TitleBuildingName.font.size = 15;
				TitleBuildingName.textScale = 1f;
				TitleBuildingName.wordWrap = true;
				TitleBuildingName.textPadding.left = 5;
				TitleBuildingName.textPadding.right = 5;
				TitleBuildingName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleBuildingName.hoveredTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleBuildingName.pressedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleBuildingName.focusedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleBuildingName.useDropShadow = true;
				TitleBuildingName.dropShadowOffset = new Vector2 (1, -1);
				TitleBuildingName.dropShadowColor = new Color32 (0, 0, 0, 0);
				TitleBuildingName.relativePosition =  Vector3.zero;
				
				Body = this.AddUIComponent<UIPanel> ();
				Body.name = "PeopleInsideBuildingsBody";
				Body.width = this.width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0;
				Body.relativePosition = new Vector3(0,Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite> ();
				BodySpriteBg.name = "PeopleInsideBuildingsDataContainer";
				BodySpriteBg.width = Body.width;
				BodySpriteBg.height = Body.height;
				BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
				BodySpriteBg.relativePosition = Vector3.zero;

				BodyRows = BodySpriteBg.AddUIComponent<UIScrollablePanel> ();
				BodyRows.name = "BodyRows";
				BodyRows.width = BodySpriteBg.width - 24;
				BodyRows.autoLayoutDirection = LayoutDirection.Vertical;
				BodyRows.autoLayout = true;
				BodyRows.relativePosition = new Vector3(12,0);

				string[] LabelPanelName = new string[3] { "Residential", "Workers", "Guests" };

				for(int i = 0; i < 3; i++) {

					if(i == 0) {
						int row = 0;
						for(int a = 0; a < MaxHouseHolds; a++) {

							ResidentialPanels[a] = BodyRows.AddUIComponent<UIPanel>();
							ResidentialPanels[a].width = 226;
							ResidentialPanels[a].height = 25;
							ResidentialPanels[a].name = "LabelPanel_" + LabelPanelName[i] + "_" + a.ToString();
							ResidentialPanels[a].autoLayoutDirection = LayoutDirection.Vertical;
							ResidentialPanels[a].autoLayout = true;
							ResidentialPanels[a].Hide();
							
							ResidentialPanelSubRow[a] = ResidentialPanels[a].AddUIComponent<UIPanel>();
							ResidentialPanelSubRow[a].width = 226;
							ResidentialPanelSubRow[a].height = 25;
							ResidentialPanelSubRow[a].name = "TitlePanel_" + LabelPanelName[i] + "_" + a.ToString();
							ResidentialPanelSubRow[a].atlas = MyAtlas.FavCimsAtlas;
							ResidentialPanelSubRow[a].backgroundSprite = "bg_row2";
							
							ResidentialPanelIcon[a] = ResidentialPanelSubRow[a].AddUIComponent<UIButton> ();
							ResidentialPanelIcon[a].name = "LabelPanelIcon_" + LabelPanelName[i] + "_" + a.ToString();
							ResidentialPanelIcon[a].width = 17;
							ResidentialPanelIcon[a].height = 17;
							ResidentialPanelIcon[a].atlas = MyAtlas.FavCimsAtlas;
							ResidentialPanelIcon[a].relativePosition = new Vector3(5,4);
							
							ResidentialPanelText[a] = ResidentialPanelSubRow[a].AddUIComponent<UIButton> (); 
							ResidentialPanelText[a].name = "LabelPanelText_" + LabelPanelName[i] + "_" + a.ToString();
							ResidentialPanelText[a].width = 200;
							ResidentialPanelText[a].height = 25;
							ResidentialPanelText[a].textVerticalAlignment = UIVerticalAlignment.Middle;
							ResidentialPanelText[a].textHorizontalAlignment = UIHorizontalAlignment.Left;
							ResidentialPanelText[a].playAudioEvents = true;
							ResidentialPanelText[a].font = UIDynamicFont.FindByName ("OpenSans-Regular");
							ResidentialPanelText[a].font.size = 15;
							ResidentialPanelText[a].textScale = 0.80f;
							ResidentialPanelText[a].useDropShadow = true;
							ResidentialPanelText[a].dropShadowOffset = new Vector2 (1, -1);
							ResidentialPanelText[a].dropShadowColor = new Color32 (0, 0, 0, 0);
							ResidentialPanelText[a].textPadding.left = 5;
							ResidentialPanelText[a].textPadding.right = 5;
							ResidentialPanelText[a].textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
							ResidentialPanelText[a].isInteractive = false;
							ResidentialPanelText[a].relativePosition = new Vector3 (ResidentialPanelIcon[a].relativePosition.x + ResidentialPanelIcon[a].width, 1);

							for(int b = 0; b < 5; b++) {
								ResidentialBodyRow[row] = BodyRows.AddUIComponent (typeof(ResidentialBuildingPanelRow)) as ResidentialBuildingPanelRow;
								ResidentialBodyRow[row].name = "Row_" + LabelPanelName[i] + "_" + b.ToString();
								ResidentialBodyRow[row].OnBuilding = 0;
								ResidentialBodyRow[row].citizen = 0;
								ResidentialBodyRow[row].Hide();
								row++;
							}
						}

					} else if (i == 1) {

						WorkersPanel = BodyRows.AddUIComponent<UIPanel>();
						WorkersPanel.width = 226;
						WorkersPanel.height = 25;
						WorkersPanel.name = "LabelPanel_" + LabelPanelName[i] + "_0";
						WorkersPanel.autoLayoutDirection = LayoutDirection.Vertical;
						WorkersPanel.autoLayout = true;
						WorkersPanel.Hide();
						
						WorkersPanelSubRow = WorkersPanel.AddUIComponent<UIPanel>();
						WorkersPanelSubRow.width = 226;
						WorkersPanelSubRow.height = 25;
						WorkersPanelSubRow.name = "TitlePanel_" + LabelPanelName[i] + "_0";
						WorkersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
						WorkersPanelSubRow.backgroundSprite = "bg_row2";
						
						WorkersPanelIcon = WorkersPanelSubRow.AddUIComponent<UIButton> ();
						WorkersPanelIcon.name = "LabelPanelIcon_" + LabelPanelName[i] + "_0";
						WorkersPanelIcon.width = 17;
						WorkersPanelIcon.height = 17;
						WorkersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
						WorkersPanelIcon.relativePosition = new Vector3(5,4);
						
						WorkersPanelText = WorkersPanelSubRow.AddUIComponent<UIButton> (); 
						WorkersPanelText.name = "LabelPanelText_" + LabelPanelName[i] + "_0";
						WorkersPanelText.width = 200;
						WorkersPanelText.height = 25;
						WorkersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
						WorkersPanelText.textHorizontalAlignment = UIHorizontalAlignment.Left;
						WorkersPanelText.playAudioEvents = true;
						WorkersPanelText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
						WorkersPanelText.font.size = 15;
						WorkersPanelText.textScale = 0.80f;
						WorkersPanelText.useDropShadow = true;
						WorkersPanelText.dropShadowOffset = new Vector2 (1, -1);
						WorkersPanelText.dropShadowColor = new Color32 (0, 0, 0, 0);
						WorkersPanelText.textPadding.left = 5;
						WorkersPanelText.textPadding.right = 5;
						WorkersPanelText.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
						WorkersPanelText.isInteractive = false;
						WorkersPanelText.relativePosition = new Vector3 (WorkersPanelIcon.relativePosition.x + WorkersPanelIcon.width, 1);

						int row = 0;
						for(int a = 0; a < MaxWorkersUnit*5; a++) {
							WorkersBodyRow[row] = BodyRows.AddUIComponent (typeof(WorkersBuildingPanelRow)) as WorkersBuildingPanelRow;
							WorkersBodyRow[row].name = "Row_" + LabelPanelName[i] + "_" + a.ToString();
							WorkersBodyRow[row].OnBuilding = 0;
							WorkersBodyRow[row].citizen = 0;
							WorkersBodyRow[row].Hide();
							row++;
						}

					} else {

						GuestsPanel = BodyRows.AddUIComponent<UIPanel>();
						GuestsPanel.width = 226;
						GuestsPanel.height = 25;
						GuestsPanel.name = "LabelPanel_" + LabelPanelName[i] + "_0";
						GuestsPanel.autoLayoutDirection = LayoutDirection.Vertical;
						GuestsPanel.autoLayout = true;
						GuestsPanel.Hide();
						
						GuestsPanelSubRow = GuestsPanel.AddUIComponent<UIPanel>();
						GuestsPanelSubRow.width = 226;
						GuestsPanelSubRow.height = 25;
						GuestsPanelSubRow.name = "TitlePanel_" + LabelPanelName[i] + "_0";
						GuestsPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
						GuestsPanelSubRow.backgroundSprite = "bg_row2";
						
						GuestsPanelIcon = GuestsPanelSubRow.AddUIComponent<UIButton> ();
						GuestsPanelIcon.name = "LabelPanelIcon_" + LabelPanelName[i] + "_0";
						GuestsPanelIcon.width = 17;
						GuestsPanelIcon.height = 17;
						GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
						GuestsPanelIcon.relativePosition = new Vector3(5,4);
						
						GuestsPanelText = GuestsPanelSubRow.AddUIComponent<UIButton> (); 
						GuestsPanelText.name = "LabelPanelText_" + LabelPanelName[i] + "_0";
						GuestsPanelText.width = 200;
						GuestsPanelText.height = 25;
						GuestsPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
						GuestsPanelText.textHorizontalAlignment = UIHorizontalAlignment.Left;
						GuestsPanelText.playAudioEvents = true;
						GuestsPanelText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
						GuestsPanelText.font.size = 15;
						GuestsPanelText.textScale = 0.80f;
						GuestsPanelText.useDropShadow = true;
						GuestsPanelText.dropShadowOffset = new Vector2 (1, -1);
						GuestsPanelText.dropShadowColor = new Color32 (0, 0, 0, 0);
						GuestsPanelText.textPadding.left = 5;
						GuestsPanelText.textPadding.right = 5;
						GuestsPanelText.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
						GuestsPanelText.isInteractive = false;
						GuestsPanelText.relativePosition = new Vector3 (GuestsPanelIcon.relativePosition.x + GuestsPanelIcon.width, 1);

						int row = 0;
						for(int a = 0; a < MaxGuestsUnit*5; a++) {
							GuestsBodyRow[row] = BodyRows.AddUIComponent (typeof(GuestsBuildingPanelRow)) as GuestsBuildingPanelRow;
							GuestsBodyRow[row].name = "Row_"  + LabelPanelName[i] + "_" + a.ToString();
							GuestsBodyRow[row].OnBuilding = 0;
							GuestsBodyRow[row].citizen = 0;
							GuestsBodyRow[row].Hide();
							row++;
						}

					}
				}

				BodyPanelScrollBar = BodySpriteBg.AddUIComponent<UIScrollablePanel> ();
				BodyPanelScrollBar.name = "BodyPanelScrollBar";
				BodyPanelScrollBar.width = 10;
				BodyPanelScrollBar.relativePosition = new Vector3 (BodyRows.width + 12, 0);
				
				BodyScrollBar = BodyPanelScrollBar.AddUIComponent<UIScrollbar> ();
				BodyScrollBar.width = 10;
				BodyScrollBar.name = "BodyScrollBar";
				BodyScrollBar.orientation = UIOrientation.Vertical;
				BodyScrollBar.pivot = UIPivotPoint.TopRight;
				BodyScrollBar.AlignTo (BodyScrollBar.parent, UIAlignAnchor.TopRight);
				BodyScrollBar.minValue = 0;
				BodyScrollBar.value = 0;
				BodyScrollBar.incrementAmount = 25;
				
				BodyPanelTrackSprite = BodyScrollBar.AddUIComponent<UISlicedSprite> ();
				BodyPanelTrackSprite.autoSize = true;
				BodyPanelTrackSprite.name = "BodyScrollBarTrackSprite";
				//BodyPanelTrackSprite.size = BodyPanelTrackSprite.parent.size;
				BodyPanelTrackSprite.fillDirection = UIFillDirection.Vertical;
				BodyPanelTrackSprite.atlas = MyAtlas.FavCimsAtlas;
				BodyPanelTrackSprite.spriteName = "scrollbartrack";
				//BodyPanelTrackSprite.spriteName = "ScrollbarTrack";
				BodyPanelTrackSprite.relativePosition = BodyScrollBar.relativePosition;
				
				BodyScrollBar.trackObject = BodyPanelTrackSprite;
				
				thumbSprite = BodyScrollBar.AddUIComponent<UISlicedSprite> ();
				thumbSprite.name = "BodyScrollBarThumbSprite";
				thumbSprite.autoSize = true;
				thumbSprite.width = thumbSprite.parent.width;
				thumbSprite.fillDirection = UIFillDirection.Vertical;
				thumbSprite.atlas = MyAtlas.FavCimsAtlas;
				thumbSprite.spriteName = "scrollbarthumb";
				//thumbSprite.spriteName = "ScrollbarThumb";
				thumbSprite.relativePosition = BodyScrollBar.relativePosition;
				BodyScrollBar.thumbObject = thumbSprite;
				BodyRows.verticalScrollbar = BodyScrollBar;
				
				/* Thx to CNightwing for this piece of code */
				BodyRows.eventMouseWheel += (component, eventParam) => {
					var sign = Math.Sign (eventParam.wheelDelta);
					BodyRows.scrollPosition += new Vector2 (0, sign * (-1) * BodyScrollBar.incrementAmount);
				};
				/* End */

				Footer = this.AddUIComponent<UIPanel> ();
				Footer.name = "PeopleInsideBuildingsPanelFooter";
				Footer.width = this.width;
				Footer.height = 12;
				Footer.relativePosition = new Vector3(0, Title.height + Body.height);
				FooterSpriteBg = Footer.AddUIComponent<UITextureSprite> ();
				FooterSpriteBg.width = Footer.width;
				FooterSpriteBg.height = Footer.height;
				FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				FooterSpriteBg.relativePosition = Vector3.zero;
				
				UIComponent FavCimsBuildingPanelTrigger_esc = UIView.Find<UIButton> ("Esc");
				
				if (FavCimsBuildingPanelTrigger_esc != null) {
					FavCimsBuildingPanelTrigger_esc.eventClick += (component, eventParam) => this.Hide();
				}
				
			}catch (Exception ex) {
				Debug.Error(" Building Panel Start() : " + ex.ToString());
			}
		}
		
		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;

			if(BuildingID.IsEmpty) {

				if(Garbage) {
					
					Wait = true;
					
					CimsOnBuilding.Clear();
					WorkersCount = 0;
					GuestsCount = 0;
					
					try
					{
						if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {
							int i = 0;
							for(int a = 0; a < MaxHouseHolds; a++) {
								ResidentialPanels[a].Hide();

								for(int b = 0; b < 5; b++) {
									ResidentialBodyRow[i].Hide();
									ResidentialBodyRow[i].citizen = 0;
									ResidentialBodyRow[i].OnBuilding = 0;
									ResidentialBodyRow[i].firstRun = true;
									i++;
								}
							}
							
						} else if (buildingInfo.m_class.m_service == ItemClass.Service.Commercial) {
							WorkersPanel.Hide();

							for(int a = 0; a < MaxWorkersUnit*5; a++) {
								WorkersBodyRow[a].Hide();
								WorkersBodyRow[a].citizen = 0;
								WorkersBodyRow[a].OnBuilding = 0;
								WorkersBodyRow[a].firstRun = true;
							}
							
							GuestsPanel.Hide();

							for(int a = 0; a < MaxGuestsUnit*5; a++) {
								GuestsBodyRow[a].Hide();
								GuestsBodyRow[a].citizen = 0;
								GuestsBodyRow[a].OnBuilding = 0;
								GuestsBodyRow[a].firstRun = true;
							}
							
						} else {
							WorkersPanel.Hide();

							for(int a = 0; a < MaxWorkersUnit*5; a++) {
								WorkersBodyRow[a].Hide();
								WorkersBodyRow[a].citizen = 0;
								WorkersBodyRow[a].OnBuilding = 0;
								WorkersBodyRow[a].firstRun = true;
							}
						}
						
						Wait = false;
					}catch /*(Exception ex)*/ {
						//Debug.Error(" Flush Error : " + ex.ToString());
					}
					Garbage = false;
				}

				firstRun = true;

				return;
			}
			
			try
			{
				buildingInfo = MyBuilding.m_buildings.m_buffer [BuildingID.Building].Info;

				if(!ZonedBuildingWorldInfoPanel.GetCurrentInstanceID().IsEmpty && ZonedBuildingWorldInfoPanel.GetCurrentInstanceID() != BuildingID) {

					Wait = true;

					CimsOnBuilding.Clear();
					WorkersCount = 0;
					GuestsCount = 0;

					if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {

						int i = 0;
						for(int a = 0; a < MaxHouseHolds; a++) {
							ResidentialPanels[a].Hide();

							for(int b = 0; b < 5; b++) {
								ResidentialBodyRow[i].Hide();
								ResidentialBodyRow[i].citizen = 0;
								ResidentialBodyRow[i].OnBuilding = 0;
								ResidentialBodyRow[i].firstRun = true;
								i++;
							}
						}

					} else if (buildingInfo.m_class.m_service == ItemClass.Service.Commercial) {

						WorkersPanel.Hide();

						for(int a = 0; a < MaxWorkersUnit*5; a++) {
							WorkersBodyRow[a].Hide();
							WorkersBodyRow[a].citizen = 0;
							WorkersBodyRow[a].OnBuilding = 0;
							WorkersBodyRow[a].firstRun = true;
						}

						GuestsPanel.Hide();

						for(int a = 0; a < MaxGuestsUnit*5; a++) {
							GuestsBodyRow[a].Hide();
							GuestsBodyRow[a].citizen = 0;
							GuestsBodyRow[a].OnBuilding = 0;
							GuestsBodyRow[a].firstRun = true;
						}

					} else {

						WorkersPanel.Hide();

						for(int a = 0; a < MaxWorkersUnit*5; a++) {
							WorkersBodyRow[a].Hide();
							WorkersBodyRow[a].citizen = 0;
							WorkersBodyRow[a].OnBuilding = 0;
							WorkersBodyRow[a].firstRun = true;
						}

					}

					BuildingID = ZonedBuildingWorldInfoPanel.GetCurrentInstanceID();

					if(BuildingID.IsEmpty) {
						return;
					}
					Wait = false;
				}

				if (this.isVisible && !BuildingID.IsEmpty) {

					Garbage = true;
					
					this.absolutePosition = new Vector3 (RefPanel.absolutePosition.x + RefPanel.width + 5, RefPanel.absolutePosition.y);
					this.height = RefPanel.height - 15;
					if(50 + ((float)CimsOnBuilding.Count * 25) < (this.height - Title.height - Footer.height)) {
						Body.height = this.height - Title.height - Footer.height;
					} else if(50 + ((float)CimsOnBuilding.Count * 25) > 400) {
						Body.height = 400;
					} else {
						Body.height = 50 + ((float)CimsOnBuilding.Count * 25);
					}

					//se dimensione pannello = refpanel => scollbar hide?
					BodySpriteBg.height = Body.height;
					Footer.relativePosition = new Vector3(0, Title.height + Body.height);
					BodyRows.height = Body.height;
					BodyPanelScrollBar.height = Body.height;
					BodyScrollBar.height = Body.height;
					BodyPanelTrackSprite.size = BodyPanelTrackSprite.parent.size;

					seconds -= 1 * Time.deltaTime;
					
					if (seconds <= 0 || firstRun) {
						execute = true;
						seconds = Run;
					} else {
						execute = false;
					}
					
					if(execute) {
						
						firstRun = false;
						
						BuildingUnits = MyBuilding.m_buildings.m_buffer [BuildingID.Building].m_citizenUnits;

						int unitnum = 0;
						int rownum = 0;

						int limit;
						if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {
							limit = MaxHouseHolds;
						} else if(buildingInfo.m_class.m_service == ItemClass.Service.Commercial) {
							//if(MaxGuestsUnit >= MaxWorkersUnit) {
							limit = MaxGuestsUnit;
							//} else {
								//limit = MaxWorkersUnit;
							//}
						} else {
							limit = MaxWorkersUnit;
						}

						while (BuildingUnits != 0 && unitnum < limit) {

							uint nextUnit = MyCitizen.m_units.m_buffer [BuildingUnits].m_nextUnit;

							for (int i = 0; i < 5; i++) {
								uint citizen = MyCitizen.m_units.m_buffer [BuildingUnits].GetCitizen (i);

								Citizen cItizen = MyCitizen.m_citizens.m_buffer[citizen];

								//if (citizen != 0 && !CimsOnBuilding.ContainsKey(citizen) && cItizen.GetBuildingByLocation() == BuildingID.Building) {
								if (citizen != 0 && !CimsOnBuilding.ContainsKey(citizen)) {

									if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {

										TitleBuildingName.text = FavCimsLang.text ("Citizens_HouseHoldsTitle");

										if(ResidentialPanels[unitnum] != null) {	

											ResidentialPanels[unitnum].Show();

											ResidentialPanelIcon[unitnum].normalFgSprite = "BapartmentIcon";
											ResidentialPanelText[unitnum].text = FavCimsLang.text("OnBuilding_Residential") + " " + ((int)unitnum+1).ToString();

											if(ResidentialBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(ResidentialBodyRow[rownum].citizen)) {
												Wait = true;
												CimsOnBuilding.Remove(ResidentialBodyRow[rownum].citizen);
											}

											CimsOnBuilding.Add(citizen, BuildingUnits);

											//ResidentialBodyRow[rownum].parent.height += 25;
											ResidentialBodyRow[rownum].OnBuilding = BuildingID.Building;
											ResidentialBodyRow[rownum].citizen = citizen;
											ResidentialBodyRow[rownum].LocType = Citizen.Location.Home;
											ResidentialBodyRow[rownum].firstRun = true;
											ResidentialBodyRow[rownum].Show();

											if(Wait)
												Wait = false;
										}

									} else if(buildingInfo.m_class.m_service == ItemClass.Service.Industrial || buildingInfo.m_class.m_service == ItemClass.Service.Office) {
										TitleBuildingName.text = FavCimsLang.text ("WorkersOnBuilding");

										WorkersPanel.Show();
										WorkersPanelIcon.normalFgSprite = "BworkingIcon";
										WorkersPanelText.text = FavCimsLang.text("OnBuilding_Workers");

										if(cItizen.GetBuildingByLocation() == BuildingID.Building && cItizen.CurrentLocation != Citizen.Location.Moving) { //Solo quelli che si trovano nell'edificio

											WorkersCount++;

											if(WorkersPanel != null && WorkersBodyRow[unitnum] != null) {	

												if(WorkersBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen)) {
													Wait = true;
													CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
												}

												CimsOnBuilding.Add(citizen, BuildingUnits);
												
												WorkersBodyRow[rownum].OnBuilding = BuildingID.Building;
												WorkersBodyRow[rownum].citizen = citizen;
												WorkersBodyRow[rownum].LocType = Citizen.Location.Work;
												WorkersBodyRow[rownum].firstRun = true;
												WorkersBodyRow[rownum].Show();

												if(Wait)
													Wait = false;
											}
										}

										if(WorkersCount == 0) {
											WorkersPanelText.text = FavCimsLang.text("OnBuilding_NoWorkers");
										}

									} else {

										TitleBuildingName.text = FavCimsLang.text ("CitizenOnBuildingTitle");

										if (BuildingID.Building == cItizen.m_workBuilding) {

											WorkersPanel.Show();
											WorkersPanelIcon.normalFgSprite = "BworkingIcon";
											WorkersPanelText.text = FavCimsLang.text("OnBuilding_Workers");

											if(cItizen.GetBuildingByLocation() == BuildingID.Building && cItizen.CurrentLocation != Citizen.Location.Moving) { //Solo quelli che si trovano nell'edificio

												WorkersCount++;

												if(WorkersPanel != null && WorkersBodyRow[unitnum] != null) {	

													if(WorkersBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen)) {
														Wait = true;
														CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
													}

													CimsOnBuilding.Add(citizen, BuildingUnits);
													
													WorkersBodyRow[rownum].OnBuilding = BuildingID.Building;
													WorkersBodyRow[rownum].citizen = citizen;
													WorkersBodyRow[rownum].LocType = Citizen.Location.Work;
													WorkersBodyRow[rownum].firstRun = true;
													WorkersBodyRow[rownum].Show();

													if(Wait)
														Wait = false;
												}
											}

											if(WorkersCount == 0) {
												WorkersPanelText.text = FavCimsLang.text("OnBuilding_NoWorkers");
											}

										} else {

											GuestsPanel.Show();
											GuestsPanelIcon.normalFgSprite = "BcommercialIcon";
											GuestsPanelText.text = FavCimsLang.text("OnBuilding_Guests");

											if(cItizen.GetBuildingByLocation() == BuildingID.Building && cItizen.CurrentLocation != Citizen.Location.Moving) { //Solo quelli che si trovano nell'edificio

												GuestsCount++;

												if(GuestsPanel != null && GuestsBodyRow[unitnum] != null) {	

													if(GuestsBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(GuestsBodyRow[rownum].citizen)) {
														Wait = true;
														CimsOnBuilding.Remove(GuestsBodyRow[rownum].citizen);
													}

													CimsOnBuilding.Add(citizen, BuildingUnits);
													
													GuestsBodyRow[rownum].OnBuilding = BuildingID.Building;
													GuestsBodyRow[rownum].citizen = citizen;
													GuestsBodyRow[rownum].LocType = Citizen.Location.Visit;
													GuestsBodyRow[rownum].firstRun = true;
													GuestsBodyRow[rownum].Show();

													if(Wait)
														Wait = false;
												}
											}

											if(GuestsCount == 0) {
												GuestsPanelText.text = FavCimsLang.text("OnBuilding_NoGuests");
											}
										}
									}
								}
								rownum++;
							}

							if(BuildingUnits == 0 && buildingInfo.m_class.m_service == ItemClass.Service.Residential) {
								ResidentialPanels[unitnum].Hide();
							}

							BuildingUnits = nextUnit;
							if (++unitnum > 0x80000) {
								break;
							}
						}
					}
				} //non mettere else che tanto non si esegue per via dell'event click sul bottone.
			}catch /*(Exception ex)*/ {
				//Debug.Error(" FavCimsVechiclePanelPT Update Error : " + ex.ToString());
			}
		}
	}
	
	public class ResidentialBuildingPanelRow : UIPanel
	{
		const float Run = 0.5f;
		float seconds = Run;
		bool execute = false;
		public bool firstRun = true;

		const float RecentTimer = 8f;
		float RecentSeconds = RecentTimer;
		
		InstanceID MyInstanceID = InstanceID.Empty;

		public ushort OnBuilding;
		public uint citizen;

		public Citizen.Location LocType;

		CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		int RealAge;
		int citizenINT;
		
		bool tourist = false;
		
		UIButton Gender;
		UIButton Name;
		UIButton Age;
		UIButton Star;
		
		////////////////////////////////////////////////
		////////////Building Rows////////////////////
		///////////////////////////////////////////////
		
		public override void Start() {
					
			try
			{
				this.width = 226;
				this.height = 25;
				//this.name = "row";
				this.atlas = MyAtlas.FavCimsAtlas;
				this.backgroundSprite = "bg_row1";
				this.relativePosition = new Vector3 (0, 0);
				
				this.Gender = this.AddUIComponent<UIButton> ();
				this.Gender.name = "Gender";
				this.Gender.width = 17;
				this.Gender.height = 17;
				this.Gender.atlas = MyAtlas.FavCimsAtlas;
				this.Gender.relativePosition = new Vector3(5,4);
				
				this.Name = this.AddUIComponent<UIButton> (); 
				this.Name.name = "Name";
				this.Name.width = 131;
				this.Name.height = 25;
				this.Name.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
				this.Name.playAudioEvents = true;
				this.Name.font = UIDynamicFont.FindByName ("OpenSans-Regular");
				this.Name.font.size = 15;
				this.Name.textScale = 0.80f;
				//this.Name.wordWrap = true;
				this.Name.useDropShadow = true;
				this.Name.dropShadowOffset = new Vector2 (1, -1);
				this.Name.dropShadowColor = new Color32 (0, 0, 0, 0);
				this.Name.textPadding.left = 5;
				this.Name.textPadding.right = 5;
				this.Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				this.Name.hoveredTextColor = new Color32 (204, 102, 0, 20);
				this.Name.pressedTextColor = new Color32 (102, 153, 255, 147);
				this.Name.focusedTextColor = new Color32 (153, 0, 0, 0);
				this.Name.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
				this.Name.relativePosition = new Vector3 (this.Gender.relativePosition.x + this.Gender.width, 1);
				this.Name.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => FavCimsCore.GoToCitizen(this.position, this.MyInstanceID, this.tourist, eventParam);
				
				this.Age = this.AddUIComponent<UIButton> ();
				this.Age.name = "Age";
				this.Age.width = 23;
				this.Age.height = 19;
				this.Age.textHorizontalAlignment = UIHorizontalAlignment.Center;
				this.Age.textVerticalAlignment = UIVerticalAlignment.Middle;
				this.Age.font = UIDynamicFont.FindByName ("OpenSans-Regular");
				this.Age.textScale = 0.90f;
				this.Age.font.size = 15;
				this.Age.dropShadowOffset = new Vector2 (1, -1);
				this.Age.dropShadowColor = new Color32 (0, 0, 0, 0);
				this.Age.isInteractive = false;
				this.Age.relativePosition = new Vector3 (this.Name.relativePosition.x + this.Name.width + 10, 4);
				
				this.Star = this.AddUIComponent<UIButton> ();
				this.Star.name = "Star";
				this.Star.atlas = MyAtlas.FavCimsAtlas;
				this.Star.size = new Vector2 (16, 16);
				this.Star.playAudioEvents = true;
				this.Star.relativePosition = new Vector3 (this.Age.relativePosition.x + this.Age.width + 10, 4);
				this.Star.eventClick += (component, eventParam) => { 
					FavCimsCore.AddToFavorites (this.MyInstanceID);
				};
				
			}catch (Exception ex) {
				Debug.Error("Error in Passenger Creation " + ex.ToString());
			}
		}

		public virtual bool Wait() {
			if (PeopleInsideBuildingsPanel.Wait) {
				return true;
			}
			return false;
		}

		public virtual Dictionary<uint,uint> GetCimsDict() {
			return PeopleInsideBuildingsPanel.CimsOnBuilding;
		}

		public virtual void DecreaseWorkersCount() {
			PeopleInsideBuildingsPanel.WorkersCount--;
		}

		public virtual void DecreaseGuestsCount() {
			PeopleInsideBuildingsPanel.GuestsCount--;
		}

		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;

			if (Wait()) {
				return;
			}

			if(this.citizen == 0) {
				this.Hide();
				return;
			}

			if (this.isVisible) {

				if(this.firstRun)
					this.RecentSeconds = RecentTimer;

				this.seconds -= 1 * Time.deltaTime;
				this.RecentSeconds -= 1 * Time.deltaTime;
				
				if (this.seconds <= 0 || this.firstRun) {
					this.execute = true;
					this.seconds = Run;
				} else {
					this.execute = false;
				}

				if (this.execute) {

					this.firstRun = false;

					BuildingInfo buildingInfo = this.MyBuilding.m_buildings.m_buffer[OnBuilding].Info;

					if (!GetCimsDict().ContainsKey (this.citizen)) {
						//Debug.Error (" Key not found : " + citizen.ToString () + " => " + this.MyCitizen.GetCitizenName (this.citizen));
						this.Hide();
						this.citizen = 0;
						this.OnBuilding = 0;
						this.firstRun = true;
						
						return;
					}
					
					//Dead or Gone away
					if (this.MyCitizen.GetCitizenName (this.citizen) == null || this.MyCitizen.GetCitizenName (this.citizen).Length < 1) {

						if(buildingInfo.m_class.m_service != ItemClass.Service.Residential) {
							
							if (LocType == Citizen.Location.Visit) {
								DecreaseWorkersCount();
							} else {
								DecreaseGuestsCount();
							}
						}

						GetCimsDict().Remove (this.citizen);
						this.Hide ();
						this.citizen = 0;
						this.OnBuilding = 0;
						this.firstRun = true;
						
						return;
					}

					this.MyInstanceID.Citizen = this.citizen;

					CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer [this.citizen].GetCitizenInfo (this.citizen);

					InstanceID Target = InstanceID.Empty;
					string status = citizenInfo.m_citizenAI.GetLocalizedStatus (this.citizen, ref this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Citizen], out Target);
					string target = MyBuilding.GetBuildingName (Target.Building, this.MyInstanceID);

					if (citizenInfo.m_class.m_service == ItemClass.Service.Tourism) {
						
						this.tourist = true;
						
						this.Gender.tooltip = Locale.Get ("CITIZEN_OCCUPATION_TOURIST");
						this.Name.tooltip = status + " " + target;
						
						if (Citizen.GetGender (citizen) == Citizen.Gender.Female) {
							this.Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
							this.Gender.normalBgSprite = "touristIcon";
						} else {
							this.Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
							this.Gender.normalBgSprite = "touristIcon";
						}

					} else {

						this.Gender.tooltip = Locale.Get ("ASSETTYPE_CITIZEN"); //adult,ecc...??
						
						if (Citizen.GetGender (citizen) == Citizen.Gender.Female) {
							this.Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
							this.Gender.normalBgSprite = "Female";
						} else {
							this.Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
							this.Gender.normalBgSprite = "Male";
						}

						if (this.MyCitizen.m_citizens.m_buffer [citizen].Arrested) {
							this.Name.tooltip = FavCimsLang.text ("Jailed_into") + " " + target;
						} else {
							this.Name.tooltip = status + " " + target;
						}
					}

					if(MyCitizen.m_citizens.m_buffer[citizen].GetBuildingByLocation() == OnBuilding) {

						if (this.RecentSeconds <= 0) {
							this.Gender.normalFgSprite = null;
						} else {
							this.Gender.normalFgSprite = "greenArrowIcon";
						}

					} else {
						if(buildingInfo.m_class.m_service == ItemClass.Service.Residential) {
							this.Gender.normalFgSprite = "redArrowIcon";
							this.RecentSeconds = RecentTimer;
						}
					}

					this.Name.text = this.MyCitizen.GetCitizenName (this.citizen);
					
					//Real Age 
					this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [citizen].m_age);
					
					if (this.RealAge <= 12) { //CHILD
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
					} else if (this.RealAge <= 19) { //TEEN
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
					} else if (this.RealAge <= 25) { //YOUNG
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
					} else if (this.RealAge <= 65) { //ADULT
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
					} else if (this.RealAge <= 90) { //SENIOR
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
					} else { //FINAL
						this.Age.text = this.RealAge.ToString ();
						this.Age.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
					}

					this.citizenINT = (int)((UIntPtr)citizen);
					
					//Is in favorites?
					if (FavCimsCore.RowID.ContainsKey (this.citizenINT)) {
						//Yes
						this.Star.normalBgSprite = "icon_fav_subscribed";
						this.Star.tooltip = FavCimsLang.text ("FavStarButton_disable_tooltip");
					} else {
						//No
						this.Star.normalBgSprite = "icon_fav_unsubscribed";
						this.Star.tooltip = FavCimsLang.text ("FavStarButton_enable_tooltip");
					}
					
					if(buildingInfo.m_class.m_service == ItemClass.Service.Residential && this.MyCitizen.m_citizens.m_buffer[citizen].m_homeBuilding != OnBuilding) {
						GetCimsDict().Remove (citizen);
						this.Hide ();
						this.citizen = 0;
						this.OnBuilding = 0;
						this.firstRun = true;
					}

					if(buildingInfo.m_class.m_service != ItemClass.Service.Residential && this.MyCitizen.m_citizens.m_buffer [this.citizen].GetBuildingByLocation () != OnBuilding) {

						if (LocType == Citizen.Location.Work) {
							DecreaseWorkersCount();
						} else {
							DecreaseGuestsCount();
						}

						GetCimsDict().Remove (citizen);
						this.Hide ();
						this.citizen = 0;
						this.OnBuilding = 0;
						this.firstRun = true;
					}
				}
			}
		}
	}

	public class WorkersBuildingPanelRow : ResidentialBuildingPanelRow
	{

	}

	public class GuestsBuildingPanelRow : ResidentialBuildingPanelRow
	{
		
	}
}
