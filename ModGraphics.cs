using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FavoriteCims
{
	public class FavoritesCimsButton : UIPanel
	{
		//Menu Button
		UIButton FavCimsButton;

		//Triggers
		UIComponent FavCimsPanelTrigger_paneltime;
		UIComponent FavCimsPanelTrigger_chirper;
		UIComponent FavCimsPanelTrigger_esc;
		UIComponent FavCimsPanelTrigger_infopanel;
		UIComponent FavCimsPanelTrigger_bottombars;

		public static void FavCimsPanelToggle() {

			if (!FavCimsMainClass.FavCimsPanel.isVisible) {
				FavCimsMainClass.FavCimsPanel.CenterTo (FavCimsMainClass.FullScreenContainer);
				FavCimsMainClass.FavCimsPanel.Show();
			} else {
				FavCimsMainClass.FavCimsPanel.Hide();
			}			
		}

		public void FavCimsPanelOff () {
			
			if (FavCimsMainClass.FavCimsPanel.isVisible && !FavCimsMainClass.FavCimsPanel.containsMouse && !FavCimsButton.containsMouse && (FavCimsPanelTrigger_paneltime != null && !FavCimsPanelTrigger_paneltime.containsMouse)) {
				FavCimsMainClass.FavCimsPanel.Hide ();
			}
		}

		public override void Start() {

			var uiView = UIView.GetAView();

			////////////////////////////////////////////////
			///////////Favorite Button Manu Panel/////////
			///////////////////////////////////////////////

			this.name = "FavCimsMenuPanel";
			this.width = 49;
			this.height = 49;
			//FavCimsMenuPanel.AlignTo (MainMenuPos, UIAlignAnchor.TopLeft);
			this.BringToFront();
			this.autoLayout = true;
			
			////////////////////////////////////////////////
			///////////////Favorite Button////////////////
			///////////////////////////////////////////////

			FavCimsButton = this.AddUIComponent<UIButton>();
			FavCimsButton.normalBgSprite = "FavoriteCimsButton";
			//TestButton.disabledBgSprite = "";
			FavCimsButton.hoveredBgSprite = "FavoriteCimsButtonHovered";
			FavCimsButton.focusedBgSprite = "FavoriteCimsButtonFocused";
			FavCimsButton.pressedBgSprite = "FavoriteCimsButtonPressed";
			FavCimsButton.playAudioEvents = true;
			FavCimsButton.name = "FavCimsButton";
			FavCimsButton.tooltipBox = uiView.defaultTooltipBox;
			FavCimsButton.atlas = MyAtlas.FavCimsAtlas;
			FavCimsButton.size = new Vector2(49,49);
			FavCimsButton.eventClick += (component, eventParam) => FavCimsPanelToggle ();

			/////////////////////////////////////////////////////////
			///Triggers for self close panel when open another window
			/////////////////////////////////////////////////////////
			
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
		}

		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;

			FavCimsButton.tooltip = FavCimsLang.text ("FavCimsButton_tooltip");

			//Hotkey
			if (Input.GetMouseButton (2) && Input.GetKeyDown(KeyCode.F))
			{
				FavCimsPanelToggle();
			}
			
			//Main Button Focus
			if (FavCimsMainClass.FavCimsPanel.isVisible) {
				FavCimsButton.Focus ();
			} else {
				FavCimsButton.Unfocus ();
			}
		}
	}

	public class AddToFavButton : UIButton
	{
		InstanceID ThisHuman = InstanceID.Empty;
		InstanceManager MyInstance = Singleton<InstanceManager>.instance;
		CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

		public override void Start() {

			var uiView = UIView.GetAView();

			////////////////////////////////////////////////
			////////////Humans Subscribe Button////////////
			///////////////////////////////////////////////

			this.name = "FavCimsStarButton";
			this.normalBgSprite = "icon_fav_unsubscribed";
			this.atlas = MyAtlas.FavCimsAtlas;
			this.size = new Vector2(32,32);
			this.playAudioEvents = true;
			this.AlignTo (FavCimsMainClass.FavCimsHumanPanel, UIAlignAnchor.BottomRight);
			this.tooltipBox = uiView.defaultTooltipBox;
			this.eventClick += (component, eventParam) => FavCimsCore.UpdateMyCitizen("toggle");
		}

		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;

			if (this.isVisible) { 
				
				if(!HumanWorldInfoPanel.GetCurrentInstanceID().IsEmpty) {
					
					ThisHuman = HumanWorldInfoPanel.GetCurrentInstanceID();
					int citizenID = (int)((UIntPtr)ThisHuman.Citizen);
					string CitizenName = MyInstance.GetName (ThisHuman);
					
					if(CitizenName != null && CitizenName.Length > 0) {
						
						this.tooltip = FavCimsLang.text ("FavStarButton_disable_tooltip");
						this.normalBgSprite = "icon_fav_subscribed";
						
						if(!FavCimsCore.RowID.ContainsKey (citizenID) && !FavoriteCimsMainPanel.RowsAlreadyExist(ThisHuman)) {
							
							object L = FavCimsCore.GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
							do { }
							while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));
							
							try
							{
								CitizenRow FavCimsCitizenSingleRowPanel = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
								if(FavCimsCitizenSingleRowPanel != null) {
									FavCimsCitizenSingleRowPanel.MyInstanceID = ThisHuman;
									FavCimsCitizenSingleRowPanel.MyInstancedName = CitizenName;
								}
							}
							finally
							{
								Monitor.Exit(L);
							}
							
						}
						
						return;
						
					}else{
						
						if(citizenID != 0 && FavCimsCore.RowID.ContainsKey (citizenID)) {
							MyInstance.SetName(ThisHuman, MyCitizen.GetCitizenName(ThisHuman.Citizen));
							this.tooltip = FavCimsLang.text ("FavStarButton_disable_tooltip");
							this.normalBgSprite = "icon_fav_subscribed";
						}else {
							this.tooltip = FavCimsLang.text ("FavStarButton_enable_tooltip");
							this.normalBgSprite = "icon_fav_unsubscribed";
						}
					}
				}
			}
		}
	}

	public class FavoriteCimsMainPanel : UIPanel
	{
		WindowController PanelMover;
		UITextureSprite FavCimsTitleSprite;
		UIButton FavCimsBCMenuButton;
		UITextureSprite FavCimsCBMenuSprite;
		Texture FavCimsCBETexture;
		Texture FavCimsCBDTexture;
		//UIButton FavCimsBBMenuButton;
		//UITextureSprite FavCimsBBMenuSprite;
		//Texture FavCimsBBETexture;
		//Texture FavCimsBBDTexture;
		//UIButton FavCimsBSMenuButton;
		//UITextureSprite FavCimsSBMenuSprite;
		//Texture FavCimsSBETexture;
		//Texture FavCimsSBDTexture;
		//Body
		UIPanel CitizensPanel;
		Texture FavCimsMainBodyTexture;
		UITextureSprite FavCimsBodySprite;
		public static UIButton FavCimsHappinesColText { get; set; }
		public static UIButton FavCimsNameColText { get; set; }
		public static UIButton FavCimsEduColText { get; set; }
		public static UIButton FavCimsWorkingPlaceColText { get; set; }
		public static UIButton FavCimsAgePhaseColText { get; set; }
		public static UIButton FavCimsAgeColText { get; set; }
		public static UIButton FavCimsHomeColText { get; set; }
		public static UIButton FavCimsLastActColText { get; set; }
		public static UIButton FavCimsCloseButtonCol { get; set; }
		public static UIScrollablePanel FavCimsCitizenRowsPanel;
		public static bool RowAlternateBackground = false;
		public static bool ColumnSpecialBackground = false;

		public static bool RowsAlreadyExist(InstanceID instanceID) {
			CitizenRow[] CitizenRows = FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow> ();
			
			foreach (CitizenRow Rows in CitizenRows) {
				if(Rows.MyInstanceID == instanceID)
					return true;
			}
			return false;
		}

		private void ReorderRowsBackgrounds() {
			
			object L = FavCimsCore.GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
			do { }
			while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));
			
			try
			{	
				CitizenRow[] CitizenRows = FavCimsCitizenRowsPanel.GetComponentsInChildren<CitizenRow> ();
				
				foreach (CitizenRow Rows in CitizenRows)
				{
					if (Rows != null && Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite") != null)
					{
						if(Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture != null) {
							Texture FavDot;
							if(Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture.name.Length > 0) {
								if(RowAlternateBackground == false) {
									FavDot = ResourceLoader.loadTexture ((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_1.png");
									FavDot.name = "FavDot_1";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = true;
								} else {
									FavDot = ResourceLoader.loadTexture ((int)Rows.width, 40, "UIMainPanel.Rows.bgrow_2.png");
									FavDot.name = "FavDot_2";
									Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
									RowAlternateBackground = false;
								}
								Rows.eventMouseLeave -= (UIComponent component, UIMouseEventParameter eventParam) => Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
								Rows.eventMouseLeave += (UIComponent component, UIMouseEventParameter eventParam) => Rows.Find<UITextureSprite>("FavCimsCitizenSingleRowBGSprite").texture = FavDot;
							}
						}
					}
				}
			}
			catch (Exception e) {
				Debug.Error("Reorder Background Error " + e.ToString());
			}
			finally
			{
				Monitor.Exit(L);
			}
			
		}
		
		public void change_visibility_event() {
			
			FavCimsBCMenuButton.text = FavCimsLang.text("FavCimsBCMenuButton_text");
			FavCimsBCMenuButton.tooltip = FavCimsLang.text("FavCimsBCMenuButton_tooltip");
			//FavCimsBBMenuButton.text = FavCimsLang.text("FavCimsBBMenuButton_text");
			//FavCimsBBMenuButton.tooltip = FavCimsLang.text("FavCimsBBMenuButton_tooltip");
			//FavCimsBSMenuButton.text = FavCimsLang.text("FavCimsBSMenuButton_text");
			//FavCimsBSMenuButton.tooltip = FavCimsLang.text("FavCimsBSMenuButton_tooltip");
			FavCimsHappinesColText.text = FavCimsLang.text("FavCimsHappinesColText_text");
			FavCimsHappinesColText.tooltip = FavCimsLang.text("FavCimsHappinesColText_tooltip");
			FavCimsNameColText.text = FavCimsLang.text("FavCimsNameColText_text");
			FavCimsNameColText.tooltip = FavCimsLang.text("FavCimsNameColText_tooltip");
			FavCimsAgePhaseColText.text = FavCimsLang.text("FavCimsAgePhaseColText_text");
			FavCimsAgePhaseColText.tooltip = FavCimsLang.text("FavCimsAgePhaseColText_tooltip");
			FavCimsAgeColText.text = FavCimsLang.text("FavCimsAgeColText_text");
			FavCimsAgeColText.tooltip = FavCimsLang.text("FavCimsAgeColText_tooltip");
			FavCimsEduColText.text = FavCimsLang.text("FavCimsEduColText_text");
			FavCimsEduColText.tooltip = FavCimsLang.text("FavCimsEduColText_tooltip");
			FavCimsHomeColText.text = FavCimsLang.text("FavCimsHomeColText_text");
			FavCimsHomeColText.tooltip = FavCimsLang.text("FavCimsHomeColText_tooltip");
			FavCimsWorkingPlaceColText.text = FavCimsLang.text("FavCimsWorkingPlaceColText_text");
			FavCimsWorkingPlaceColText.tooltip = FavCimsLang.text("FavCimsWorkingPlaceColText_tooltip");
			FavCimsLastActColText.text = FavCimsLang.text("FavCimsLastActColText_text");
			FavCimsLastActColText.tooltip = FavCimsLang.text("FavCimsLastActColText_tooltip");
			FavCimsCloseButtonCol.text = FavCimsLang.text("FavCimsCloseButtonCol_text");
			FavCimsCloseButtonCol.tooltip = FavCimsLang.text("FavCimsCloseButtonCol_tooltip");
		}
		
		//public void ButtonEnabler(UITextureSprite sPrite) {
			
			//if (sPrite == FavCimsCBMenuSprite) {
				
				//FavCimsCBMenuSprite.texture = FavCimsCBETexture;
				//FavCimsBBMenuSprite.texture = FavCimsBBDTexture;
				//FavCimsSBMenuSprite.texture = FavCimsSBDTexture;
				
			//} else if (sPrite == FavCimsBBMenuSprite) {
				
				//FavCimsCBMenuSprite.texture = FavCimsCBDTexture;
				//FavCimsBBMenuSprite.texture = FavCimsBBETexture;
				//FavCimsSBMenuSprite.texture = FavCimsSBDTexture;
				
			//} else if (sPrite == FavCimsSBMenuSprite) {
				
				//FavCimsCBMenuSprite.texture = FavCimsCBDTexture;
				//FavCimsBBMenuSprite.texture = FavCimsBBDTexture;
				//FavCimsSBMenuSprite.texture = FavCimsSBETexture;
				
			//}
			//return;
		//}

		public override void Start ()
		{
			var uiView = UIView.GetAView();

			this.name = "FavCimsPanel";
			this.width = 1200;
			this.height = 700;
			this.opacity = 0.95f;
			this.eventVisibilityChanged += (component, value) => change_visibility_event ();
						
			//Main Panel BG Texture
			Texture FavCimsMainBGTexture = ResourceLoader.loadTexture ((int)this.width, (int)this.height, "UIMainPanel.mainbg.png");
			FavCimsMainBGTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsMainBGTexture.filterMode = FilterMode.Bilinear;
			//FavCimsMainBGTexture.anisoLevel = 9; Set 1 Bad to 9 Very God
			FavCimsMainBGTexture.name = "FavCimsMainBGTexture";
			UITextureSprite FavCimsMainBGSprite;
			FavCimsMainBGSprite = this.AddUIComponent<UITextureSprite> ();
			FavCimsMainBGSprite.name = "FavCimsMainBGSprite";
			FavCimsMainBGSprite.texture = FavCimsMainBGTexture;
			FavCimsMainBGSprite.relativePosition = new Vector3 (0, 0);
			
			FavCimsMainBGSprite.eventMouseDown += delegate {
				if (Input.GetMouseButton (0)) {
					if (this.GetComponentInChildren<WindowController> () != null) {
						this.PanelMover = this.GetComponentInChildren<WindowController> ();
						this.PanelMover.ComponentToMove = this;
						this.PanelMover.Stop = false;
						this.PanelMover.Start ();
					} else {
						this.PanelMover = this.AddUIComponent (typeof(WindowController)) as WindowController;
						this.PanelMover.ComponentToMove = this;
					}
					this.opacity = 0.5f;
				}
			};
			
			FavCimsMainBGSprite.eventMouseUp += delegate {
				if (this.PanelMover != null) {
					this.PanelMover.Stop = true;
					this.PanelMover.ComponentToMove = null;
					this.PanelMover = null;
				}
				this.opacity = 1f;
			};
			
			//Main Panel Title Texture
			Texture FavCimsTitleTexture;
			FavCimsTitleTexture = ResourceLoader.loadTexture ((int)this.width, 58, "UIMainPanel.favcimstitle.png");
			
			FavCimsTitleTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsTitleTexture.filterMode = FilterMode.Bilinear;
			FavCimsTitleTexture.mipMapBias = -0.5f;
			//FavCimsTitleTexture.anisoLevel = 9; // Set 1 Bad to 9 Very God
			
			FavCimsTitleTexture.name = "FavCimsTitleTexture";
			FavCimsTitleSprite = FavCimsMainBGSprite.AddUIComponent<UITextureSprite> ();
			FavCimsTitleSprite.name = "FavCimsTitleSprite";
			FavCimsTitleSprite.texture = FavCimsTitleTexture;
			float FavCimsTitleSpriteRelPosX = ((this.width / 2) - (float)FavCimsTitleTexture.width / 2);
			FavCimsTitleSprite.relativePosition = new Vector3 (FavCimsTitleSpriteRelPosX, 0);
			
			///////////////////////////////////////////////
			//Game Default Close Button 
			//////////////////////////////////////////////
			
			UIButton FavCimsMenuCloseButton = this.AddUIComponent<UIButton> ();
			FavCimsMenuCloseButton.name = "FavCimsMenuCloseButton";
			FavCimsMenuCloseButton.width = 32;
			FavCimsMenuCloseButton.height = 32;
			FavCimsMenuCloseButton.normalBgSprite = "buttonclose";
			FavCimsMenuCloseButton.hoveredBgSprite = "buttonclosehover";
			FavCimsMenuCloseButton.pressedBgSprite = "buttonclosepressed";
			FavCimsMenuCloseButton.opacity = 1;
			FavCimsMenuCloseButton.useOutline = true;
			FavCimsMenuCloseButton.playAudioEvents = true;
			
			FavCimsMenuCloseButton.eventClick += (component, eventParam) => FavoritesCimsButton.FavCimsPanelToggle ();
			
			//Printing
			FavCimsMenuCloseButton.relativePosition = new Vector3 (this.width - (FavCimsMenuCloseButton.width * 1.5f), ((float)FavCimsTitleTexture.height / 2) - FavCimsMenuCloseButton.height / 2);
			
			///////////////////////////////////////////////
			//Main Panel Menu Background Texture
			///////////////////////////////////////////////
			
			Texture FavCimsBGMenuTexture;
			FavCimsBGMenuTexture = ResourceLoader.loadTexture ((int)this.width - 10, 70, "UIMainPanel.submenubar.png");
			
			FavCimsBGMenuTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsBGMenuTexture.filterMode = FilterMode.Bilinear;
			FavCimsBGMenuTexture.name = "FavCimsBGMenuTexture";
			UITextureSprite FavCimsBGMenuSprite = FavCimsMainBGSprite.AddUIComponent<UITextureSprite> ();
			FavCimsBGMenuSprite.name = "FavCimsBGMenuSprite";
			FavCimsBGMenuSprite.texture = FavCimsBGMenuTexture;
			float FavCimsBGMenuSpriteRelPosX = ((this.width / 2) - (float)FavCimsBGMenuTexture.width / 2);
			FavCimsBGMenuSprite.relativePosition = new Vector3 (FavCimsBGMenuSpriteRelPosX, 58);
			
			//Citizen Button Texture (Enabled & Disabled)
			FavCimsCBETexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.citizenbuttonenabled.png");
			FavCimsCBDTexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.citizenbuttondisabled.png");
			
			FavCimsCBETexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsCBETexture.filterMode = FilterMode.Bilinear;
			FavCimsCBETexture.name = "FavCimsCBETexture";
			FavCimsCBETexture.mipMapBias = -0.5f;
			FavCimsCBDTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsCBDTexture.filterMode = FilterMode.Bilinear;
			FavCimsCBDTexture.name = "FavCimsCBDTexture";
			FavCimsCBDTexture.mipMapBias = -0.5f;
			
			FavCimsCBMenuSprite = FavCimsMainBGSprite.AddUIComponent<UITextureSprite> ();
			FavCimsCBMenuSprite.name = "FavCimsBGMenuSprite";
			FavCimsCBMenuSprite.texture = FavCimsCBETexture;
			
			//Citizens Transparent Button (For Easy Text Change)
			FavCimsBCMenuButton = this.AddUIComponent<UIButton> ();
			FavCimsBCMenuButton.name = "FavCimsBCMenuButton";
			FavCimsBCMenuButton.width = FavCimsCBMenuSprite.width;
			FavCimsBCMenuButton.height = FavCimsCBMenuSprite.height;
			FavCimsBCMenuButton.useOutline = true;
			FavCimsBCMenuButton.playAudioEvents = true;
			FavCimsBCMenuButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsBCMenuButton.textScale = 1.8f;
			FavCimsBCMenuButton.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsBCMenuButton.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsBCMenuButton.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsBCMenuButton.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsBCMenuButton.textPadding.left = 15;
			FavCimsBCMenuButton.useDropShadow = true;
			FavCimsBCMenuButton.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing
			FavCimsCBMenuSprite.relativePosition = new Vector3 (27, 69);
			FavCimsBCMenuButton.relativePosition = new Vector3 (27, 69);
			
			///////////////////////////////////////////////
			//Buildings Button Texture (Enabled & Disabled)
			//////////////////////////////////////////////
			/*
			FavCimsBBETexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.buildingsbuttonenabled.png");
			FavCimsBBDTexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.buildingsbuttondisabled.png");
			
			FavCimsBBETexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsBBETexture.filterMode = FilterMode.Bilinear;
			FavCimsBBETexture.name = "FavCimsBBETexture";
			FavCimsBBETexture.mipMapBias = -0.5f;
			FavCimsBBDTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsBBDTexture.filterMode = FilterMode.Bilinear;
			FavCimsBBDTexture.name = "FavCimsBBETexture";
			FavCimsBBDTexture.mipMapBias = -0.5f;
			FavCimsBBMenuSprite = FavCimsMainBGSprite.AddUIComponent<UITextureSprite> ();
			FavCimsBBMenuSprite.name = "FavCimsBBMenuSprite";
			FavCimsBBMenuSprite.texture = FavCimsBBDTexture;

			//Buildings Transparent Button (For Easy Text Change)
			FavCimsBBMenuButton = this.AddUIComponent<UIButton> ();
			FavCimsBBMenuButton.name = "FavCimsBBMenuButton";
			FavCimsBBMenuButton.width = FavCimsBBETexture.width;
			FavCimsBBMenuButton.height = FavCimsBBETexture.height;
			FavCimsBBMenuButton.useOutline = true;
			FavCimsBBMenuButton.playAudioEvents = true;
			FavCimsBBMenuButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsBBMenuButton.textScale = 1.8f;
			FavCimsBBMenuButton.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsBBMenuButton.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsBBMenuButton.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsBBMenuButton.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsBBMenuButton.textPadding.left = 20;
			FavCimsBBMenuButton.useDropShadow = true;
			FavCimsBBMenuButton.tooltipBox = uiView.defaultTooltipBox;

			//Printing
			//FavCimsBBMenuSprite.relativePosition = new Vector3 (FavCimsCBMenuSprite.position.x + FavCimsBBMenuSprite.width + 21, 69); //html => margin-left:21px;
			//FavCimsBBMenuButton.relativePosition = new Vector3 (FavCimsBCMenuButton.position.x + FavCimsBBMenuButton.width + 35, 69);

			///////////////////////////////////////////////
			//Stats Button Texture (Enabled & Disabled)
			//////////////////////////////////////////////
			
			FavCimsSBETexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.statsbuttonenabled.png");
			FavCimsSBDTexture = ResourceLoader.loadTexture (200, 59, "UIMainPanel.statsbuttondisabled.png");
			
			FavCimsSBETexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsSBETexture.filterMode = FilterMode.Bilinear;
			FavCimsSBETexture.name = "FavCimsSBETexture";
			FavCimsSBETexture.mipMapBias = -0.5f;
			FavCimsSBDTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsSBDTexture.filterMode = FilterMode.Bilinear;
			FavCimsSBDTexture.name = "FavCimsSBETexture";
			FavCimsSBDTexture.mipMapBias = -0.5f;
			FavCimsSBMenuSprite = FavCimsMainBGSprite.AddUIComponent<UITextureSprite> ();
			FavCimsSBMenuSprite.name = "FavCimsSBMenuSprite";
			FavCimsSBMenuSprite.texture = FavCimsSBDTexture;
			
			//Stats Transparent Button (For Easy Text Change)
			FavCimsBSMenuButton = this.AddUIComponent<UIButton> ();
			FavCimsBSMenuButton.name = "FavCimsBSMenuButton";
			FavCimsBSMenuButton.width = FavCimsSBETexture.width;
			FavCimsBSMenuButton.height = FavCimsSBETexture.height;
			FavCimsBSMenuButton.useOutline = true;
			FavCimsBSMenuButton.playAudioEvents = true;
			FavCimsBSMenuButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsBSMenuButton.textScale = 1.8f;
			FavCimsBSMenuButton.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsBSMenuButton.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsBSMenuButton.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsBSMenuButton.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsBSMenuButton.textPadding.left = 0;
			FavCimsBSMenuButton.useDropShadow = true;
			FavCimsBSMenuButton.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing
			//FavCimsSBMenuSprite.relativePosition = new Vector3 (FavCimsBBMenuSprite.position.x + FavCimsSBMenuSprite.width + 21, 69); //html => margin-left:21px;
			//FavCimsBSMenuButton.relativePosition = new Vector3 (FavCimsBBMenuButton.position.x + FavCimsBSMenuButton.width + 21, 69); //html => margin-left:21px;

			///////////////////////////////////////////////
			//Click Operation for Main Buttons
			//////////////////////////////////////////////

			//FavCimsBCMenuButton.eventClick += (component, eventParam) => ButtonEnabler (FavCimsCBMenuSprite);
			//FavCimsBBMenuButton.eventClick += (component, eventParam) => ButtonEnabler (FavCimsBBMenuSprite);
			//FavCimsBSMenuButton.eventClick += (component, eventParam) => ButtonEnabler (FavCimsSBMenuSprite);
			*/
			
			///////////////////////////////////////////////
			//Citizens Panel
			//////////////////////////////////////////////
			
			CitizensPanel = this.AddUIComponent<UIPanel> ();
			CitizensPanel.name = "CitizensPanel";
			CitizensPanel.width = 1190;
			CitizensPanel.height = 558;
			CitizensPanel.relativePosition = new Vector3 (((this.width / 2) - (float)CitizensPanel.width / 2), 128);
			
			///////////////////////////////////////////////
			//Citizens Panel Body Background
			//////////////////////////////////////////////
			
			FavCimsMainBodyTexture = ResourceLoader.loadTexture (1190, 558, "UIMainPanel.bodybg.png");
			
			FavCimsMainBodyTexture.wrapMode = TextureWrapMode.Clamp;
			FavCimsMainBodyTexture.filterMode = FilterMode.Bilinear;
			FavCimsMainBodyTexture.name = "FavCimsMainBodyTexture";
			
			FavCimsBodySprite = CitizensPanel.AddUIComponent<UITextureSprite> ();
			FavCimsBodySprite.name = "FavCimsCBGBodySprite";
			FavCimsBodySprite.texture = FavCimsMainBodyTexture;
			//Printing
			FavCimsBodySprite.relativePosition = Vector3.zero;
			
			///////////////////////////////////////////////
			//Index Column Background
			//////////////////////////////////////////////
			
			Texture FavCimsIndexBgBar = ResourceLoader.loadTexture (1146, 26, "UIMainPanel.indexerbgbar.png");
			
			FavCimsIndexBgBar.wrapMode = TextureWrapMode.Clamp;
			FavCimsIndexBgBar.filterMode = FilterMode.Bilinear;
			FavCimsIndexBgBar.name = "FavCimsIndexBgBar";
			FavCimsIndexBgBar.mipMapBias = -0.5f;
			UITextureSprite FavCimsIndexBgBarSprite = CitizensPanel.AddUIComponent<UITextureSprite> ();
			FavCimsIndexBgBarSprite.name = "FavCimsIndexBgBarSprite";
			FavCimsIndexBgBarSprite.texture = FavCimsIndexBgBar;
			
			//Printing
			FavCimsIndexBgBarSprite.relativePosition = new Vector3 (21, 7);
			
			////////////////////////////////////////////////
			//Index Columns (Button for future sort order...)
			////////////////////////////////////////////////
			
			//Status
			FavCimsHappinesColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsHappinesColText.name = "FavCimsHappinesColText";
			FavCimsHappinesColText.width = 60;
			FavCimsHappinesColText.height = FavCimsIndexBgBar.height;
			FavCimsHappinesColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsHappinesColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsHappinesColText.playAudioEvents = true;
			FavCimsHappinesColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsHappinesColText.textScale = 0.7f;
			FavCimsHappinesColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsHappinesColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsHappinesColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsHappinesColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsHappinesColText.textPadding.left = 0;
			FavCimsHappinesColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Status
			FavCimsHappinesColText.relativePosition = new Vector3 (FavCimsIndexBgBarSprite.relativePosition.x + 6, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Name
			FavCimsNameColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsNameColText.name = "FavCimsNameColText";
			FavCimsNameColText.width = 180;
			FavCimsNameColText.height = FavCimsIndexBgBar.height;
			FavCimsNameColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsNameColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsNameColText.playAudioEvents = true;
			FavCimsNameColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsNameColText.textScale = 0.7f;
			FavCimsNameColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsNameColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsNameColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsNameColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsNameColText.textPadding.left = 0;
			FavCimsNameColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Name
			FavCimsNameColText.relativePosition = new Vector3 (FavCimsHappinesColText.relativePosition.x + FavCimsHappinesColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Age Phase
			FavCimsAgePhaseColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsAgePhaseColText.name = "FavCimsAgePhaseColText";
			FavCimsAgePhaseColText.width = 120;
			FavCimsAgePhaseColText.height = FavCimsIndexBgBar.height;
			FavCimsAgePhaseColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsAgePhaseColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsAgePhaseColText.playAudioEvents = true;
			FavCimsAgePhaseColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsAgePhaseColText.textScale = 0.7f;
			FavCimsAgePhaseColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsAgePhaseColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsAgePhaseColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsAgePhaseColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsAgePhaseColText.textPadding.left = 0;
			FavCimsAgePhaseColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Age Phase
			FavCimsAgePhaseColText.relativePosition = new Vector3 (FavCimsNameColText.relativePosition.x + FavCimsNameColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Age
			FavCimsAgeColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsAgeColText.name = "FavCimsAgeColText";
			FavCimsAgeColText.width = 40;
			FavCimsAgeColText.height = FavCimsIndexBgBar.height;
			FavCimsAgeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsAgeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsAgeColText.playAudioEvents = true;
			FavCimsAgeColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsAgeColText.textScale = 0.7f;
			FavCimsAgeColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsAgeColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsAgeColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsAgeColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsAgeColText.textPadding.left = 0;
			FavCimsAgeColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Age
			FavCimsAgeColText.relativePosition = new Vector3 (FavCimsAgePhaseColText.relativePosition.x + FavCimsAgePhaseColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Education
			FavCimsEduColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsEduColText.name = "FavCimsEduColText";
			FavCimsEduColText.width = 140;
			FavCimsEduColText.height = FavCimsIndexBgBar.height;
			FavCimsEduColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsEduColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsEduColText.playAudioEvents = true;
			FavCimsEduColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsEduColText.textScale = 0.7f;
			FavCimsEduColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsEduColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsEduColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsEduColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsEduColText.textPadding.left = 0;
			FavCimsEduColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Education
			FavCimsEduColText.relativePosition = new Vector3 (FavCimsAgeColText.relativePosition.x + FavCimsAgeColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Home
			FavCimsHomeColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsHomeColText.name = "FavCimsHomeColText";
			FavCimsHomeColText.width = 184;
			FavCimsHomeColText.height = FavCimsIndexBgBar.height;
			FavCimsHomeColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsHomeColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsHomeColText.playAudioEvents = true;
			FavCimsHomeColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsHomeColText.textScale = 0.7f;
			FavCimsHomeColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsHomeColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsHomeColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsHomeColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsHomeColText.textPadding.left = 0;
			FavCimsHomeColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Home
			FavCimsHomeColText.relativePosition = new Vector3 (FavCimsEduColText.relativePosition.x + FavCimsEduColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Working Place
			FavCimsWorkingPlaceColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsWorkingPlaceColText.name = "FavCimsWorkingPlaceColText";
			FavCimsWorkingPlaceColText.width = 180;
			FavCimsWorkingPlaceColText.height = FavCimsIndexBgBar.height;
			FavCimsWorkingPlaceColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsWorkingPlaceColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsWorkingPlaceColText.playAudioEvents = true;
			FavCimsWorkingPlaceColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsWorkingPlaceColText.textScale = 0.7f;
			FavCimsWorkingPlaceColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsWorkingPlaceColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsWorkingPlaceColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsWorkingPlaceColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsWorkingPlaceColText.textPadding.left = 0;
			FavCimsWorkingPlaceColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Working Place
			FavCimsWorkingPlaceColText.relativePosition = new Vector3 (FavCimsHomeColText.relativePosition.x + FavCimsHomeColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Last Activity
			FavCimsLastActColText = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsLastActColText.name = "FavCimsLastActColText";
			FavCimsLastActColText.width = 180;
			FavCimsLastActColText.height = FavCimsIndexBgBar.height;
			FavCimsLastActColText.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsLastActColText.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsLastActColText.playAudioEvents = true;
			FavCimsLastActColText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsLastActColText.textScale = 0.7f;
			FavCimsLastActColText.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsLastActColText.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsLastActColText.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsLastActColText.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsLastActColText.textPadding.left = 0;
			FavCimsLastActColText.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Last Activity
			FavCimsLastActColText.relativePosition = new Vector3 (FavCimsWorkingPlaceColText.relativePosition.x + FavCimsWorkingPlaceColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			//Close Button
			FavCimsCloseButtonCol = CitizensPanel.AddUIComponent<UIButton> ();
			FavCimsCloseButtonCol.name = "FavCimsCloseButtonCol";
			FavCimsCloseButtonCol.width = 50;
			FavCimsCloseButtonCol.height = FavCimsIndexBgBar.height;
			FavCimsCloseButtonCol.textVerticalAlignment = UIVerticalAlignment.Middle;
			FavCimsCloseButtonCol.textHorizontalAlignment = UIHorizontalAlignment.Center;
			FavCimsCloseButtonCol.playAudioEvents = true;
			FavCimsCloseButtonCol.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			FavCimsCloseButtonCol.textScale = 0.7f;
			FavCimsCloseButtonCol.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			FavCimsCloseButtonCol.hoveredTextColor = new Color32 (204, 102, 0, 20);
			FavCimsCloseButtonCol.pressedTextColor = new Color32 (153, 0, 0, 0);
			FavCimsCloseButtonCol.focusedTextColor = new Color32 (102, 153, 255, 147);
			FavCimsCloseButtonCol.textPadding.right = 6;
			FavCimsCloseButtonCol.tooltipBox = uiView.defaultTooltipBox;
			
			//Printing Close Button
			FavCimsCloseButtonCol.relativePosition = new Vector3 (FavCimsLastActColText.relativePosition.x + FavCimsLastActColText.width, FavCimsIndexBgBarSprite.relativePosition.y + 1);
			
			///////////////////////////////////////////////
			/////Rows Panel Body
			///////////////////////////////////////////////
			
			FavCimsCitizenRowsPanel = CitizensPanel.AddUIComponent<UIScrollablePanel> ();
			FavCimsCitizenRowsPanel.name = "FavCimsCitizenRowsPanel";
			FavCimsCitizenRowsPanel.width = FavCimsIndexBgBarSprite.width - 12;
			FavCimsCitizenRowsPanel.height = 500;
			
			FavCimsCitizenRowsPanel.autoLayoutDirection = LayoutDirection.Vertical;
			FavCimsCitizenRowsPanel.autoLayout = true;
			FavCimsCitizenRowsPanel.clipChildren = true;
			FavCimsCitizenRowsPanel.autoLayoutPadding = new RectOffset (0, 0, 0, 0);
			FavCimsCitizenRowsPanel.relativePosition = new Vector3 (FavCimsIndexBgBarSprite.relativePosition.x + 6, FavCimsIndexBgBarSprite.relativePosition.y + FavCimsIndexBgBarSprite.height);
			
			//Damn ScrollBar
			UIScrollablePanel FavCimsCitizenRowsPanelScrollBar = CitizensPanel.AddUIComponent<UIScrollablePanel> ();
			FavCimsCitizenRowsPanelScrollBar.name = "FavCimsCitizenRowsPanelScrollBar";
			FavCimsCitizenRowsPanelScrollBar.width = 10;
			FavCimsCitizenRowsPanelScrollBar.height = 500;
			FavCimsCitizenRowsPanelScrollBar.relativePosition = new Vector3 (FavCimsIndexBgBarSprite.relativePosition.x + FavCimsIndexBgBarSprite.width, FavCimsCitizenRowsPanel.relativePosition.y);
			
			UIScrollbar FavCimsMainPanelScrollBar = FavCimsCitizenRowsPanelScrollBar.AddUIComponent<UIScrollbar> ();
			FavCimsMainPanelScrollBar.width = 10;
			FavCimsMainPanelScrollBar.height = FavCimsCitizenRowsPanel.height;
			FavCimsMainPanelScrollBar.orientation = UIOrientation.Vertical;
			FavCimsMainPanelScrollBar.pivot = UIPivotPoint.TopRight;
			FavCimsMainPanelScrollBar.AlignTo (FavCimsMainPanelScrollBar.parent, UIAlignAnchor.TopRight);
			FavCimsMainPanelScrollBar.minValue = 0;
			FavCimsMainPanelScrollBar.value = 0;
			FavCimsMainPanelScrollBar.incrementAmount = 40;
			
			UISlicedSprite FavCimsMainPanelTrackSprite = FavCimsMainPanelScrollBar.AddUIComponent<UISlicedSprite> ();
			FavCimsMainPanelTrackSprite.relativePosition = FavCimsMainPanelScrollBar.relativePosition;
			FavCimsMainPanelTrackSprite.autoSize = true;
			FavCimsMainPanelTrackSprite.size = FavCimsMainPanelTrackSprite.parent.size;
			FavCimsMainPanelTrackSprite.fillDirection = UIFillDirection.Vertical;
			FavCimsMainPanelTrackSprite.spriteName = "ScrollbarTrack";
			
			FavCimsMainPanelScrollBar.trackObject = FavCimsMainPanelTrackSprite;
			
			UISlicedSprite thumbSprite = FavCimsMainPanelScrollBar.AddUIComponent<UISlicedSprite> ();
			thumbSprite.relativePosition = FavCimsMainPanelScrollBar.relativePosition;
			thumbSprite.autoSize = true;
			thumbSprite.width = thumbSprite.parent.width;
			thumbSprite.fillDirection = UIFillDirection.Vertical;
			thumbSprite.spriteName = "ScrollbarThumb";
			FavCimsMainPanelScrollBar.thumbObject = thumbSprite;
			FavCimsCitizenRowsPanel.verticalScrollbar = FavCimsMainPanelScrollBar;
			
			/* Thx to CNightwing for this piece of code */
			FavCimsCitizenRowsPanel.eventMouseWheel += (component, eventParam) => {
				var sign = Math.Sign (eventParam.wheelDelta);
				FavCimsCitizenRowsPanel.scrollPosition += new Vector2 (0, sign * (-1) * FavCimsMainPanelScrollBar.incrementAmount);
			};
			/* End */
			
			FavCimsCitizenRowsPanel.eventComponentAdded += (component, eventParam) => ReorderRowsBackgrounds ();
			FavCimsCitizenRowsPanel.eventComponentRemoved += (component, eventParam) => ReorderRowsBackgrounds ();
			
			///////////////////////////////////////////////
			/////Rows Panel Footer
			///////////////////////////////////////////////
			
			UITextureSprite FavCimsFooterBgBarSprite = CitizensPanel.AddUIComponent<UITextureSprite> ();
			FavCimsFooterBgBarSprite.name = "FavCimsFooterBgBarSprite";
			FavCimsFooterBgBarSprite.width = FavCimsIndexBgBarSprite.width;
			FavCimsFooterBgBarSprite.height = 15;
			FavCimsFooterBgBarSprite.texture = FavCimsIndexBgBar;
			
			//Printing 
			FavCimsFooterBgBarSprite.relativePosition = new Vector3 (21, FavCimsCitizenRowsPanel.relativePosition.y + FavCimsCitizenRowsPanel.height);
			//Row End

			//Load Initial Row (People Renamed)
			foreach(KeyValuePair<InstanceID, string> FavCitizen in FavCimsCore.FavoriteCimsList())
			{
				if(FavCitizen.Key.Type == InstanceType.Citizen) { // || FavCitizen.Key.Type == InstanceType.CitizenInstance
					CitizenRow FavCimsCitizenSingleRowPanel = FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
					FavCimsCitizenSingleRowPanel.MyInstanceID = FavCitizen.Key;
					FavCimsCitizenSingleRowPanel.MyInstancedName = FavCitizen.Value;
				}
				/* qui posso caricare anche i distretti, gli edifici ecc....
				else 
				{
					Debug.Log("#ID# : " + FavCitizen.Key + " #Name# : " + FavCitizen.Value + " #Type# : " + FavCitizen.Key.Type.ToString());
				}
				*/
			}
		}
	}
}
