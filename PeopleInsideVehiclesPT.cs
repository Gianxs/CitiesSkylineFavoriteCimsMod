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
	public class VechiclePassengersButtonPT : UIButton
	{
		InstanceID VehicleID = InstanceID.Empty;
		VehicleManager MyVehicle = Singleton<VehicleManager>.instance;
		
		public UIAlignAnchor Alignment;
		public UIPanel RefPanel;
		FavCimsVechiclePanelPT VehiclePanel;
		
		public override void Start() {
			
			var uiView = UIView.GetAView();
			
			////////////////////////////////////////////////
			////////////Passengers Button///////////////////
			///////////////////////////////////////////////
			
			this.name = "FavCimsVehPassButton";
			this.normalBgSprite = "vehicleButton";
			this.hoveredBgSprite = "vehicleButtonHovered";
			this.focusedBgSprite = "vehicleButtonHovered";
			this.pressedBgSprite = "vehicleButtonHovered";
			this.disabledBgSprite = "vehicleButtonDisabled";
			this.atlas = MyAtlas.FavCimsAtlas;
			this.size = new Vector2(36,32);
			this.playAudioEvents = true;
			this.AlignTo (RefPanel, Alignment);
			this.tooltipBox = uiView.defaultTooltipBox;
			
			if (FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVechiclePanelPT> () != null) {
				this.VehiclePanel = FavCimsMainClass.FullScreenContainer.GetComponentInChildren<FavCimsVechiclePanelPT>();
			} else {
				this.VehiclePanel = FavCimsMainClass.FullScreenContainer.AddUIComponent(typeof(FavCimsVechiclePanelPT)) as FavCimsVechiclePanelPT;
			}
			
			this.VehiclePanel.VehicleID = InstanceID.Empty;
			this.VehiclePanel.Hide();
			
			this.eventClick += (component, eventParam) => {
				if(!VehicleID.IsEmpty && !VehiclePanel.isVisible) {
					this.VehiclePanel.VehicleID = VehicleID;
					this.VehiclePanel.RefPanel = RefPanel;
					this.VehiclePanel.Show();
				} else {
					this.VehiclePanel.VehicleID = InstanceID.Empty;
					this.VehiclePanel.Hide();
				}
			};
		}
		
		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;
			
			if (this.isVisible) {
				
				this.tooltip = FavCimsLang.text("View_NoPassengers");
				
				if(PublicTransportVehicleWorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty) {
					VehicleID = PublicTransportVehicleWorldInfoPanel.GetCurrentInstanceID();
				}

				if(VehiclePanel != null) {
					if(!VehiclePanel.isVisible) {
						this.Unfocus();
					} else {
						this.Focus();
					}
				}
				
				if (!VehicleID.IsEmpty && VehicleID.Type == InstanceType.Vehicle) {
					this.isEnabled = true; //normal sprite
					this.tooltip = FavCimsLang.text("View_PassengersList");
				} else { //Parked or VehicleID.Empty
					VehiclePanel.Hide ();
					this.Unfocus ();
					this.isEnabled = false; //disabled sprite
				}
			} else {
				this.isEnabled = false; //disabled sprite
				VehiclePanel.Hide ();
				VehicleID = InstanceID.Empty;
			}
		}
	}

	public class FavCimsVechiclePanelPT : UIPanel
	{
		const float Run = 0.5f;
		float seconds = Run;
		bool execute = false;
		bool firstRun = true;

		public static bool Wait = false;
		bool Garbage = false;

		public InstanceID VehicleID;
		public UIPanel RefPanel;
		VehicleManager MyVehicle = Singleton<VehicleManager>.instance;
		CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		public static Dictionary<uint, uint> CimsOnPTVeh = new Dictionary<uint, uint> ();

		const int MaxPassengersUnit = 20;

		UIPanel PassengersPanel;
		UIPanel PassengersPanelSubRow;
		UIButton PassengersPanelIcon;
		UIButton PassengersPanelText;
		PassengersVehiclePanelRow[] PassengersBodyRow = new PassengersVehiclePanelRow[MaxPassengersUnit*5];

		uint VehicleUnits;

		UIPanel Title;
		UITextureSprite TitleSpriteBg;
		UIButton TitleVehicleName;
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
		////////////Passengers Panel///////////////////
		///////////////////////////////////////////////
		
		public override void Start() {
			try
			{
				this.width = 250;
				this.height = 0;
				this.name = "FavCimsVechiclePanelPT";
				this.absolutePosition = new Vector3(0, 0);
				this.Hide ();
				
				Title = this.AddUIComponent<UIPanel> ();
				Title.name = "FavCimsVechiclePanelPTTitle";
				Title.width = this.width;
				Title.height = 41;
				Title.relativePosition = Vector3.zero;
				
				TitleSpriteBg = Title.AddUIComponent<UITextureSprite> ();
				TitleSpriteBg.name = "FavCimsVechiclePanelPTTitleBG";
				TitleSpriteBg.width = Title.width;
				TitleSpriteBg.height = Title.height;
				TitleSpriteBg.texture = TextureDB.VehiclePanelTitleBackground;
				TitleSpriteBg.relativePosition = Vector3.zero;
				
				//UIButton Vehicle Name
				TitleVehicleName = Title.AddUIComponent<UIButton> ();
				TitleVehicleName.name = "TitleVehiclePTName";
				TitleVehicleName.width = Title.width;
				TitleVehicleName.height = Title.height;
				TitleVehicleName.textVerticalAlignment = UIVerticalAlignment.Middle;
				TitleVehicleName.textHorizontalAlignment = UIHorizontalAlignment.Center;
				TitleVehicleName.playAudioEvents = false;
				TitleVehicleName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
				TitleVehicleName.font.size = 15;
				TitleVehicleName.textScale = 1f;
				TitleVehicleName.wordWrap = true;
				TitleVehicleName.textPadding.left = 5;
				TitleVehicleName.textPadding.right = 5;
				TitleVehicleName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleVehicleName.hoveredTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleVehicleName.pressedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleVehicleName.focusedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
				TitleVehicleName.useDropShadow = true;
				TitleVehicleName.dropShadowOffset = new Vector2 (1, -1);
				TitleVehicleName.dropShadowColor = new Color32 (0, 0, 0, 0);
				TitleVehicleName.relativePosition =  Vector3.zero;
				
				Body = this.AddUIComponent<UIPanel> ();
				Body.name = "VechiclePanelPTBody";
				Body.width = this.width;
				Body.autoLayoutDirection = LayoutDirection.Vertical;
				Body.autoLayout = true;
				Body.clipChildren = true;
				Body.height = 0;
				Body.relativePosition = new Vector3(0,Title.height);
				BodySpriteBg = Body.AddUIComponent<UITextureSprite> ();
				BodySpriteBg.name = "VechiclePanelPTDataContainer";
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

				PassengersPanel = BodyRows.AddUIComponent<UIPanel>();
				PassengersPanel.width = 226;
				PassengersPanel.height = 25;
				PassengersPanel.name = "LabelPanel_PT_0";
				PassengersPanel.autoLayoutDirection = LayoutDirection.Vertical;
				PassengersPanel.autoLayout = true;
				PassengersPanel.Hide();

				PassengersPanelSubRow = PassengersPanel.AddUIComponent<UIPanel>();
				PassengersPanelSubRow.width = 226;
				PassengersPanelSubRow.height = 25;
				PassengersPanelSubRow.name = "TitlePanel_PT_0";
				PassengersPanelSubRow.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelSubRow.backgroundSprite = "bg_row2";
				
				PassengersPanelIcon = PassengersPanelSubRow.AddUIComponent<UIButton> ();
				PassengersPanelIcon.name = "LabelPanelIcon_PT_0";
				PassengersPanelIcon.width = 17;
				PassengersPanelIcon.height = 17;
				PassengersPanelIcon.atlas = MyAtlas.FavCimsAtlas;
				PassengersPanelIcon.normalFgSprite = "passengerIcon";
				PassengersPanelIcon.relativePosition = new Vector3(5,4);
				
				PassengersPanelText = PassengersPanelSubRow.AddUIComponent<UIButton> (); 
				PassengersPanelText.name = "LabelPanelText_PT_0";
				PassengersPanelText.width = 200;
				PassengersPanelText.height = 25;
				PassengersPanelText.textVerticalAlignment = UIVerticalAlignment.Middle;
				PassengersPanelText.textHorizontalAlignment = UIHorizontalAlignment.Left;
				PassengersPanelText.playAudioEvents = true;
				PassengersPanelText.font = UIDynamicFont.FindByName ("OpenSans-Regular");
				PassengersPanelText.font.size = 15;
				PassengersPanelText.textScale = 0.80f;
				PassengersPanelText.useDropShadow = true;
				PassengersPanelText.dropShadowOffset = new Vector2 (1, -1);
				PassengersPanelText.dropShadowColor = new Color32 (0, 0, 0, 0);
				PassengersPanelText.textPadding.left = 5;
				PassengersPanelText.textPadding.right = 5;
				PassengersPanelText.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
				PassengersPanelText.isInteractive = false;
				PassengersPanelText.relativePosition = new Vector3 (PassengersPanelIcon.relativePosition.x + PassengersPanelIcon.width, 1);
				
				int row = 0;
				for(int a = 0; a < MaxPassengersUnit*5; a++) {
					PassengersBodyRow[row] = BodyRows.AddUIComponent (typeof(PassengersVehiclePanelRow)) as PassengersVehiclePanelRow;
					PassengersBodyRow[row].name = "RowPanel_PT_" + a.ToString();
					PassengersBodyRow[row].OnVehicle = 0;
					PassengersBodyRow[row].citizen = 0;
					PassengersBodyRow[row].Hide();
					row++;
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
				Footer.name = "VechiclePanelPTFooter";
				Footer.width = this.width;
				Footer.height = 12;
				Footer.relativePosition = new Vector3(0, Title.height + Body.height);
				FooterSpriteBg = Footer.AddUIComponent<UITextureSprite> ();
				FooterSpriteBg.width = Footer.width;
				FooterSpriteBg.height = Footer.height;
				FooterSpriteBg.texture = TextureDB.VehiclePanelFooterBackground;
				FooterSpriteBg.relativePosition = Vector3.zero;
				
				UIComponent FavCimsVechiclePanelTrigger_esc = UIView.Find<UIButton> ("Esc");
				
				if (FavCimsVechiclePanelTrigger_esc != null) {
					FavCimsVechiclePanelTrigger_esc.eventClick += (component, eventParam) => this.Hide();
				}
				
			}catch (Exception ex) {
				Debug.Error(" Passengers Panel Start() : " + ex.ToString());
			}
		}
		
		public override void Update() {
			if (FavCimsMainClass.UnLoading)
				return;

			if(VehicleID.IsEmpty) {
				
				if(Garbage) {
					
					Wait = true;
					
					CimsOnPTVeh.Clear();

					try
					{
						PassengersPanel.Hide();

						for(int a = 0; a < MaxPassengersUnit*5; a++) {
							PassengersBodyRow[a].Hide();
							PassengersBodyRow[a].citizen = 0;
							PassengersBodyRow[a].OnVehicle = 0;
							PassengersBodyRow[a].firstRun = true;
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
				if(!PublicTransportVehicleWorldInfoPanel.GetCurrentInstanceID().IsEmpty && PublicTransportVehicleWorldInfoPanel.GetCurrentInstanceID() != VehicleID) {

					Wait = true;
					CimsOnPTVeh.Clear();

					try
					{
						PassengersPanel.Hide();

						for(int a = 0; a < MaxPassengersUnit*5; a++) {
							PassengersBodyRow[a].Hide();
							PassengersBodyRow[a].citizen = 0;
							PassengersBodyRow[a].OnVehicle = 0;
							PassengersBodyRow[a].firstRun = true;
						}

						VehicleID = PublicTransportVehicleWorldInfoPanel.GetCurrentInstanceID();
						
						if(VehicleID.IsEmpty) {
							return;
						}

						Wait = false;
					}catch /*(Exception ex)*/ {
						//Debug.Error(" Flush Error : " + ex.ToString());
					}
				}
				
				if (this.isVisible && !VehicleID.IsEmpty) {

					Garbage = true;

					TitleVehicleName.text = FavCimsLang.text ("Vehicle_Passengers");

					this.absolutePosition = new Vector3 (RefPanel.absolutePosition.x + RefPanel.width + 5, RefPanel.absolutePosition.y);
					this.height = RefPanel.height - 15;
					if(25 + ((float)CimsOnPTVeh.Count * 25) < (this.height - Title.height - Footer.height)) {
						Body.height = this.height - Title.height - Footer.height;
					} else if(25 + ((float)CimsOnPTVeh.Count * 25) > 400) {
						Body.height = 400;
					} else {
						Body.height = 25 + ((float)CimsOnPTVeh.Count * 25);
					}

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

						VehicleUnits = MyVehicle.m_vehicles.m_buffer [VehicleID.Vehicle].m_citizenUnits;
						
						int unitnum = 0;
						int rownum = 0;

						if(CimsOnPTVeh.Count == 0) {
							PassengersPanelText.text = FavCimsLang.text("View_NoPassengers");
						}

						PassengersPanel.Show();

						while (VehicleUnits != 0 && unitnum < MaxPassengersUnit) {

							uint nextUnit = MyCitizen.m_units.m_buffer [VehicleUnits].m_nextUnit;

							for (int i = 0; i < 5; i++) {
								uint citizen = MyCitizen.m_units.m_buffer [VehicleUnits].GetCitizen (i);

								if (citizen != 0 && !CimsOnPTVeh.ContainsKey(citizen)) {

									PassengersPanelText.text = FavCimsLang.text("Vehicle_PasssengerIconText");

									if(PassengersPanel != null && PassengersBodyRow[unitnum] != null) {	

										if(PassengersBodyRow[rownum].citizen != 0 && CimsOnPTVeh.ContainsKey(PassengersBodyRow[rownum].citizen)) {
											Wait = true;
											CimsOnPTVeh.Remove(PassengersBodyRow[rownum].citizen);
										}

										CimsOnPTVeh.Add(citizen, VehicleUnits);

										PassengersBodyRow[rownum].citizen = citizen;
										PassengersBodyRow[rownum].OnVehicle = VehicleID.Vehicle;
										PassengersBodyRow[rownum].firstRun = true;
										PassengersBodyRow[rownum].Show();

										if(Wait)
											Wait = false;
									}
								}
								rownum++;
							}
							VehicleUnits = nextUnit;
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

	public class PassengersVehiclePanelRow : UIPanel
	{
		const float Run = 0.5f;
		float seconds = Run;
		bool execute = false;
		public bool firstRun = true;
		
		const float RecentTimer = 8f;
		float RecentSeconds = RecentTimer;
		
		InstanceID MyInstanceID = InstanceID.Empty;
		
		public ushort OnVehicle;
		public uint citizen;

		CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		VehicleManager MyVehicle = Singleton<VehicleManager>.instance;

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
			if (FavCimsVechiclePanelPT.Wait) {
				return true;
			}
			return false;
		}
		
		public virtual Dictionary<uint,uint> GetCimsDict() {
			return FavCimsVechiclePanelPT.CimsOnPTVeh;
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
					
					if (!GetCimsDict().ContainsKey (this.citizen)) {
						//Debug.Error (" Key not found : " + citizen.ToString () + " => " + this.MyCitizen.GetCitizenName (this.citizen));
						this.Hide();
						this.citizen = 0;
						this.OnVehicle = 0;
						this.firstRun = true;
						
						return;
					}
					
					this.MyInstanceID.Citizen = this.citizen;
					CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer [this.citizen].GetCitizenInfo (this.citizen);
					VehicleInfo vehicleInfo = this.MyVehicle.m_vehicles.m_buffer [OnVehicle].Info;
					
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
						
						this.Name.tooltip = status + " " + target;
					}
					
					if(vehicleInfo.m_class.m_service == ItemClass.Service.PublicTransport) {
						
						if (this.RecentSeconds <= 0) {
							this.Gender.normalFgSprite = null;
						} else {
							this.Gender.normalFgSprite = "greenArrowIcon";
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

					if (this.MyCitizen.m_citizens.m_buffer [citizen].m_vehicle == 0 || this.MyCitizen.m_citizens.m_buffer [citizen].m_vehicle != OnVehicle) {

						GetCimsDict().Remove (citizen);
						this.Hide ();
						this.citizen = 0;
						this.OnVehicle = 0;
						this.firstRun = true;
					}
				}
			}
		}
	}
}