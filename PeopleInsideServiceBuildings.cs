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
	public class PeopleInsideServiceBuildingsButton : UIButton
	{
		InstanceID BuildingID = InstanceID.Empty;
		BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		
		public UIAlignAnchor Alignment;
		public UIPanel RefPanel;
		
		PeopleInsideServiceBuildingsPanel BuildingPanel;
		
		public override void Start() {
			
			var uiView = UIView.GetAView();
			
			////////////////////////////////////////////////
			////////////Building Button///////////////////
			///////////////////////////////////////////////
			
			this.name = "PeopleInsideServiceBuildingsButton";
			this.atlas = MyAtlas.FavCimsAtlas;
			this.size = new Vector2(32,36);
			this.playAudioEvents = true;
			this.AlignTo (RefPanel, Alignment);
			this.tooltipBox = uiView.defaultTooltipBox;
			
			if (FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel> () != null) {
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<PeopleInsideServiceBuildingsPanel>();
			} else {
				this.BuildingPanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(PeopleInsideServiceBuildingsPanel)) as PeopleInsideServiceBuildingsPanel;
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
				
				if(CityServiceWorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty) {
					BuildingID = CityServiceWorldInfoPanel.GetCurrentInstanceID();
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

					switch (buildingInfo.m_class.m_service) 
					{
					case ItemClass.Service.FireDepartment:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.HealthCare:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.PoliceDepartment:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.Garbage:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "IndustrialBuildingButtonIcon";
						this.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
						this.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.Electricity:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "IndustrialBuildingButtonIcon";
						this.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
						this.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.Education:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.Beautification:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Guests");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;/*
					case ItemClass.Service.Government:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;*/
					case ItemClass.Service.PublicTransport:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "IndustrialBuildingButtonIcon";
						this.hoveredBgSprite = "IndustrialBuildingButtonIconHovered";
						this.focusedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.pressedBgSprite = "IndustrialBuildingButtonIconHovered";
						this.disabledBgSprite = "IndustrialBuildingButtonIconDisabled";
						break;
					case ItemClass.Service.Monument:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("CitizenOnBuilding");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					default:
						this.tooltip = FavCimsLang.text("View_List") + " " + FavCimsLang.text("OnBuilding_Workers");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
						break;
					}

					/*
					if(buildingInfo.m_class.m_service != ItemClass.Service.None) {
						this.tooltip = FavCimsLang.text("CitizenOnBuilding");
						this.normalBgSprite = "CommercialBuildingButtonIcon";
						this.hoveredBgSprite = "CommercialBuildingButtonIconHovered";
						this.focusedBgSprite = "CommercialBuildingButtonIconHovered";
						this.pressedBgSprite = "CommercialBuildingButtonIconHovered";
						this.disabledBgSprite = "CommercialBuildingButtonIconDisabled";
					}*/
					
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

	public class PeopleInsideServiceBuildingsPanel : UIPanel
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
		
		const int MaxWorkersUnit = 40; // **Important** *same of MaxGuestsUnit*
		const int MaxGuestsUnit = 40;
		
		UIPanel WorkersPanel;
		UIPanel WorkersPanelSubRow;
		UIButton WorkersPanelIcon;
		UIButton WorkersPanelText;
		WorkersServiceBuildingPanelRow[] WorkersBodyRow = new WorkersServiceBuildingPanelRow[MaxWorkersUnit*5];
		
		UIPanel GuestsPanel;
		UIPanel GuestsPanelSubRow;
		UIButton GuestsPanelIcon;
		UIButton GuestsPanelText;
		GuestsServiceBuildingPanelRow[] GuestsBodyRow = new GuestsServiceBuildingPanelRow[MaxGuestsUnit*5];
		
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

		public override void Start() {
			try {
				this.width = 250;
				this.height = 0;
				this.name = "FavCimsPeopleInsideServiceBuildingsPanel";
				this.absolutePosition = new Vector3 (0, 0);
				this.Hide ();
				
				Title = this.AddUIComponent<UIPanel> ();
				Title.name = "PeopleInsideServiceBuildingsPanelTitle";
				Title.width = this.width;
				Title.height = 41;
				Title.relativePosition = Vector3.zero;
				
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite> ();
				TitleSpriteBg.name = "PeopleInsideServiceBuildingsPanelTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				
				//UIButton Building Name
				TitleBuildingName = Title.AddUIComponent<UIButton> ();
				TitleBuildingName.name = "PeopleInsideServiceBuildingsPanelName";
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
				TitleBuildingName.relativePosition = Vector3.zero;
				
				Body = this.AddUIComponent<UIPanel> ();
				Body.name = "PeopleInsideServiceBuildingsBody";
				Body.width = this.width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0;
				Body.relativePosition = new Vector3 (0, Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite> ();
				BodySpriteBg.name = "PeopleInsideServiceBuildingsDataContainer";
				BodySpriteBg.width = Body.width;
				BodySpriteBg.height = Body.height;
				BodySpriteBg.texture = TextureDB.VehiclePanelBackground;
				BodySpriteBg.relativePosition = Vector3.zero;
				
				BodyRows = BodySpriteBg.AddUIComponent<UIScrollablePanel> ();
				BodyRows.name = "BodyRows";
				BodyRows.width = BodySpriteBg.width - 24;
				BodyRows.autoLayoutDirection = LayoutDirection.Vertical;
				BodyRows.autoLayout = true;
				BodyRows.relativePosition = new Vector3 (12, 0);
				
				string[] LabelPanelName = new string[2] { "Workers", "Guests" };
				
				for (int i = 0; i < 2; i++) {
					
					if (i == 0) {
						
						WorkersPanel = BodyRows.AddUIComponent<UIPanel> ();
						WorkersPanel.width = 226;
						WorkersPanel.height = 25;
						WorkersPanel.name = "LabelPanel_" + LabelPanelName [i] + "_0";
						WorkersPanel.autoLayoutDirection = LayoutDirection.Vertical;
						WorkersPanel.autoLayout = true;
						WorkersPanel.Hide ();
						
						WorkersPanelSubRow = WorkersPanel.AddUIComponent<UIPanel> ();
						WorkersPanelSubRow.width = 226;
						WorkersPanelSubRow.height = 25;
						WorkersPanelSubRow.name = "TitlePanel_" + LabelPanelName [i] + "_0";
						WorkersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
						WorkersPanelSubRow.backgroundSprite = "bg_row2";
						
						WorkersPanelIcon = WorkersPanelSubRow.AddUIComponent<UIButton> ();
						WorkersPanelIcon.name = "LabelPanelIcon_" + LabelPanelName [i] + "_0";
						WorkersPanelIcon.width = 17;
						WorkersPanelIcon.height = 17;
						WorkersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
						WorkersPanelIcon.relativePosition = new Vector3 (5, 4);
						
						WorkersPanelText = WorkersPanelSubRow.AddUIComponent<UIButton> (); 
						WorkersPanelText.name = "LabelPanelText_" + LabelPanelName [i] + "_0";
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
						for (int a = 0; a < MaxWorkersUnit*5; a++) {
							WorkersBodyRow [row] = BodyRows.AddUIComponent (typeof(WorkersServiceBuildingPanelRow)) as WorkersServiceBuildingPanelRow;
							WorkersBodyRow [row].name = "Row_" + LabelPanelName[i] + "_" + a.ToString ();
							WorkersBodyRow [row].OnBuilding = 0;
							WorkersBodyRow [row].citizen = 0;
							WorkersBodyRow [row].Hide ();
							row++;
						}
						
					} else {
						
						GuestsPanel = BodyRows.AddUIComponent<UIPanel> ();
						GuestsPanel.width = 226;
						GuestsPanel.height = 25;
						GuestsPanel.name = "LabelPanel_" + LabelPanelName [i] + "_0";
						GuestsPanel.autoLayoutDirection = LayoutDirection.Vertical;
						GuestsPanel.autoLayout = true;
						GuestsPanel.Hide ();
						
						GuestsPanelSubRow = GuestsPanel.AddUIComponent<UIPanel> ();
						GuestsPanelSubRow.width = 226;
						GuestsPanelSubRow.height = 25;
						GuestsPanelSubRow.name = "TitlePanel_" + LabelPanelName [i] + "_0";
						GuestsPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
						GuestsPanelSubRow.backgroundSprite = "bg_row2";
						
						GuestsPanelIcon = GuestsPanelSubRow.AddUIComponent<UIButton> ();
						GuestsPanelIcon.name = "LabelPanelIcon_" + LabelPanelName [i] + "_0";
						GuestsPanelIcon.width = 17;
						GuestsPanelIcon.height = 17;
						GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
						GuestsPanelIcon.relativePosition = new Vector3 (5, 4);
						
						GuestsPanelText = GuestsPanelSubRow.AddUIComponent<UIButton> (); 
						GuestsPanelText.name = "LabelPanelText_" + LabelPanelName [i] + "_0";
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
						for (int a = 0; a < MaxGuestsUnit*5; a++) {
							GuestsBodyRow [row] = BodyRows.AddUIComponent (typeof(GuestsServiceBuildingPanelRow)) as GuestsServiceBuildingPanelRow;
							GuestsBodyRow [row].name = "Row_" + LabelPanelName[i] + "_" + a.ToString ();
							GuestsBodyRow [row].OnBuilding = 0;
							GuestsBodyRow [row].citizen = 0;
							GuestsBodyRow [row].Hide ();
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
				Footer.name = "PeopleInsideServiceBuildingsFooter";
				Footer.width = this.width;
				Footer.height = 12;
				Footer.relativePosition = new Vector3 (0, Title.height + Body.height);
				FooterSpriteBg = Footer.AddUIComponent<UITextureSprite> ();
				FooterSpriteBg.width = Footer.width;
				FooterSpriteBg.height = Footer.height;
				FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				FooterSpriteBg.relativePosition = Vector3.zero;
				
				UIComponent FavCimsBuildingPanelTrigger_esc = UIView.Find<UIButton> ("Esc");
				
				if (FavCimsBuildingPanelTrigger_esc != null) {
					FavCimsBuildingPanelTrigger_esc.eventClick += (component, eventParam) => this.Hide ();
				}
				
			} catch (Exception ex) {
				Debug.Error (" Service Building Panel Start() : " + ex.ToString ());
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
						WorkersPanel.Hide();
						//WorkersPanel.height = 25;
						
						for(int a = 0; a < MaxWorkersUnit*5; a++) {
							WorkersBodyRow[a].Hide();
							WorkersBodyRow[a].citizen = 0;
							WorkersBodyRow[a].OnBuilding = 0;
							WorkersBodyRow[a].firstRun = true;
						}
						
						GuestsPanel.Hide();
						//GuestsPanel.height = 25;
						
						for(int a = 0; a < MaxGuestsUnit*5; a++) {
							GuestsBodyRow[a].Hide();
							GuestsBodyRow[a].citizen = 0;
							GuestsBodyRow[a].OnBuilding = 0;
							GuestsBodyRow[a].firstRun = true;
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
				
				if(!CityServiceWorldInfoPanel.GetCurrentInstanceID().IsEmpty && CityServiceWorldInfoPanel.GetCurrentInstanceID() != BuildingID) {
					
					Wait = true;
					
					CimsOnBuilding.Clear();
					WorkersCount = 0;
					GuestsCount = 0;
					
					WorkersPanel.Hide();
					//WorkersPanel.height = 25;
					
					for(int a = 0; a < MaxWorkersUnit*5; a++) {
						WorkersBodyRow[a].Hide();
						WorkersBodyRow[a].citizen = 0;
						WorkersBodyRow[a].OnBuilding = 0;
						WorkersBodyRow[a].firstRun = true;
					}
					
					GuestsPanel.Hide();
					//GuestsPanel.height = 25;
					
					for(int a = 0; a < MaxGuestsUnit*5; a++) {
						GuestsBodyRow[a].Hide();
						GuestsBodyRow[a].citizen = 0;
						GuestsBodyRow[a].OnBuilding = 0;
						GuestsBodyRow[a].firstRun = true;
					}
					
					BuildingID = CityServiceWorldInfoPanel.GetCurrentInstanceID();
					
					if(BuildingID.IsEmpty) {
						return;
					}
					Wait = false;
				}
				
				if (this.isVisible && !BuildingID.IsEmpty) {
					
					Garbage = true;
					
					this.absolutePosition = new Vector3 (RefPanel.absolutePosition.x + RefPanel.width + 5, RefPanel.absolutePosition.y);
					this.height = RefPanel.height - 15;
					if(25 + ((float)CimsOnBuilding.Count * 25) < (this.height - Title.height - Footer.height)) {
						Body.height = this.height - Title.height - Footer.height;
					} else if(25 + ((float)CimsOnBuilding.Count * 25) > 400) {
						Body.height = 400;
					} else {
						Body.height = 25 + ((float)CimsOnBuilding.Count * 25);
					}
					
					//se dimensione pannello = refpanel => scollbar hide?
					BodySpriteBg.height = Body.height;
					Footer.relativePosition = new Vector3(0, Title.height + Body.height);
					BodyRows.height = Body.height;
					BodyPanelScrollBar.height = Body.height;
					BodyScrollBar.height = Body.height;
					BodyPanelTrackSprite.size = BodyPanelTrackSprite.parent.size;
					//thumbSprite.autoSize = true;
					
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
						int total_workers = 0;

						while (BuildingUnits != 0 && unitnum < MaxGuestsUnit) {
							
							uint nextUnit = MyCitizen.m_units.m_buffer [BuildingUnits].m_nextUnit;
							
							for (int i = 0; i < 5; i++) {
								uint citizen = MyCitizen.m_units.m_buffer [BuildingUnits].GetCitizen (i);
								
								Citizen cItizen = MyCitizen.m_citizens.m_buffer[citizen];
								
								//if (citizen != 0 && !CimsOnBuilding.ContainsKey(citizen) && cItizen.GetBuildingByLocation() == BuildingID.Building) {
								if (citizen != 0) {

									int forcedToGuest = 0;

									InstanceID Target;
									CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer [citizen].GetCitizenInfo (citizen);
									string status = citizenInfo.m_citizenAI.GetLocalizedStatus (citizen, ref this.MyCitizen.m_citizens.m_buffer [citizen], out Target);

									//int keyIndex = Array.FindIndex(Locale., w => w.IsKey);
									//string statusKey = search <= values[key]

									if(cItizen.m_workBuilding == BuildingID.Building && buildingInfo.m_class.m_service == ItemClass.Service.Education) {
										if(Locale.Get("CITIZEN_STATUS_AT_SCHOOL") == status) 
											forcedToGuest = 1;
									}

									if(BuildingID.Building == cItizen.m_workBuilding && forcedToGuest == 0)
										total_workers++;

									if(!CimsOnBuilding.ContainsKey(citizen)) {

										if(buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment) {
											TitleBuildingName.text = FavCimsLang.text("OnPolice_Building_Service");
										} else if(buildingInfo.m_class.m_service == ItemClass.Service.Education) {
											TitleBuildingName.text = FavCimsLang.text("OnEducation_Building_Service");
										} else if(buildingInfo.m_class.m_service == ItemClass.Service.HealthCare) {
											TitleBuildingName.text = FavCimsLang.text("OnMedical_Building_Service");
										} else if(buildingInfo.m_class.m_service == ItemClass.Service.Beautification) {
											TitleBuildingName.text = FavCimsLang.text ("OnBuilding_Guests");
										} else if (buildingInfo.m_class.m_service == ItemClass.Service.Monument) {
											TitleBuildingName.text = FavCimsLang.text ("CitizenOnBuildingTitle");
										} else {
											TitleBuildingName.text = FavCimsLang.text ("OnBuilding_Workers");
										}
											
										if (BuildingID.Building == cItizen.m_workBuilding && forcedToGuest == 0) {
											
											WorkersPanel.Show();
											WorkersPanelIcon.normalFgSprite = "BworkingIcon";
											WorkersPanelText.text = FavCimsLang.text("OnBuilding_Workers") + " (" + FavCimsLang.text("OnBuilding_TotalWorkers") + " " + total_workers + ")";
											
											if(cItizen.GetBuildingByLocation() == BuildingID.Building && cItizen.CurrentLocation != Citizen.Location.Moving) { //Solo quelli che si trovano nell'edificio
												
												WorkersCount++;
												
												if(WorkersPanel != null && WorkersBodyRow[unitnum] != null) {	

													if(WorkersBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(WorkersBodyRow[rownum].citizen)) {
														Wait = true;
														CimsOnBuilding.Remove(WorkersBodyRow[rownum].citizen);
													}

													CimsOnBuilding.Add(citizen, BuildingUnits);
													
													//WorkersBodyRow[rownum].parent.height += 25;
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
												WorkersPanelText.text = FavCimsLang.text("OnBuilding_NoWorkers") + " (" + FavCimsLang.text("OnBuilding_TotalWorkers") + " " + total_workers + ")";
											}
											
										} else {
											
											GuestsPanel.Show();

											if(buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment) {

												GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
												GuestsPanelIcon.normalFgSprite = "FavCimsCrimeArrested";
												GuestsPanelText.text = FavCimsLang.text("Citizen_Under_Arrest");

											} else if(buildingInfo.m_class.m_service == ItemClass.Service.Education) {

												GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
												GuestsPanelIcon.normalFgSprite = "IconPolicySchoolsOut";
												GuestsPanelText.text = FavCimsLang.text("Citizen_at_School");

											} else if(buildingInfo.m_class.m_service == ItemClass.Service.HealthCare) {

												GuestsPanelIcon.atlas = UIView.GetAView().defaultAtlas;
												GuestsPanelIcon.normalFgSprite = "SubBarHealthcareDefault";
												GuestsPanelText.text = FavCimsLang.text("Citizen_on_Clinic");

											} else {

												GuestsPanelIcon.atlas = MyAtlas.FavCimsAtlas;
												GuestsPanelIcon.normalFgSprite = "BcommercialIcon";
												GuestsPanelText.text = FavCimsLang.text("OnBuilding_Guests");

											}
											
											if(cItizen.GetBuildingByLocation() == BuildingID.Building && cItizen.CurrentLocation != Citizen.Location.Moving) { //Solo quelli che si trovano nell'edificio
												
												GuestsCount++;
												
												if(GuestsPanel != null && GuestsBodyRow[unitnum] != null) {	

													if(GuestsBodyRow[rownum].citizen != 0 && CimsOnBuilding.ContainsKey(GuestsBodyRow[rownum].citizen)) {
														Wait = true;
														CimsOnBuilding.Remove(GuestsBodyRow[rownum].citizen);
													}

													CimsOnBuilding.Add(citizen, BuildingUnits);
													
													//GuestsBodyRow[rownum].parent.height += 25;
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
												if(buildingInfo.m_class.m_service == ItemClass.Service.PoliceDepartment) {
													GuestsPanelText.text = FavCimsLang.text("OnBuilding_noArrested");
												} else if(buildingInfo.m_class.m_service == ItemClass.Service.Education) {
													GuestsPanelText.text = "Non ci sono studenti";
												} else if(buildingInfo.m_class.m_service == ItemClass.Service.HealthCare) {
													GuestsPanelText.text = "Nessun paziente";
												} else {
													GuestsPanelText.text = FavCimsLang.text("OnBuilding_NoGuests");
												}
											}
										}
									}
								}
								rownum++;
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

	public class WorkersServiceBuildingPanelRow : ResidentialBuildingPanelRow
	{
		public override bool Wait() {
			if (PeopleInsideServiceBuildingsPanel.Wait) {
				return true;
			}
			return false;
		}

		public override Dictionary<uint,uint> GetCimsDict() {
			return PeopleInsideServiceBuildingsPanel.CimsOnBuilding;
		}

		public override void DecreaseWorkersCount() {
			PeopleInsideServiceBuildingsPanel.WorkersCount--;
		}
		
		public override void DecreaseGuestsCount() {
			PeopleInsideServiceBuildingsPanel.GuestsCount--;
		}
	}

	public class GuestsServiceBuildingPanelRow : WorkersServiceBuildingPanelRow
	{

	}
}