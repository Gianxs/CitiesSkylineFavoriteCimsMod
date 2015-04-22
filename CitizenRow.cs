using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;
using System.Threading;

namespace FavoriteCims
{
	public class CitizenRow : UIPanel
	{
		private const float Run = 0.5f; //Normal Run
		private float seconds = Run;
		private float secondsForceRun = 2f;
		private const float SlowRun = 30f; //Slow run for hidden or clipped rows.
		private float HiddenRowsSeconds = SlowRun;

		private bool FirstRun = true;
		private bool execute = false;

		public InstanceID MyInstanceID;
		public string MyInstancedName;

		private Dictionary<string, string> CitizenRowData = new Dictionary<string, string> ();
		private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		private BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		private InstanceManager MyInstance = Singleton<InstanceManager>.instance;
		private VehicleManager MyVehicle = Singleton<VehicleManager>.instance;
		private DistrictManager MyDistrict  = Singleton<DistrictManager>.instance;

		//Objects 
		private static readonly string[] sHappinessLevels = new string[] { "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy" };
		UIPanel FavCimsCitizenSingleRowPanel;
		private Texture FavDot;
		private Texture FavDot_hover;
		private UITextureSprite FavCimsCitizenSingleRowBGSprite;
		private UIPanel FavCimsCitizenHappinessPanel;
		private UIButton FavCimsRowCloseButton;
		private UIPanel FavCimsCitizenNamePanel;
		private UIButton FavCimsCitizenName;
		private UITextureSprite FavCimsNameColText_EmptySprite;
		private UIPanel FavCimsAgePhasePanel;
		private UIButton FavCimsAgePhase;

		private UITextureSprite FavCimsSeparatorSprite2;
		private UITextureSprite FavCimsSeparatorSprite3;
		private UITextureSprite FavCimsSeparatorSprite4;
		private UITextureSprite FavCimsSeparatorSprite5;
		private UITextureSprite FavCimsSeparatorSprite6;
		private UITextureSprite FavCimsSeparatorSprite7;
		private UITextureSprite FavCimsSeparatorSprite8;
		private UITextureSprite FavCimsSeparatorSprite9;
		private UIPanel FavCimsRealAgePanel;
		private UIButton FavCimsRealAge;
		private UIPanel FavCimsEducationPanel;
		private UIButton FavCimsEducation;
		private UIButton FavCimsHappyIcon;
		private UITextureSprite FavCimsHappyOverride;

		private UIPanel FavCimsCitizenHomePanel;
		private UIButton FavCimsCitizenHome;
		private UIButton OtherInfoButton;

		private UITextureSprite FavCimsCitizenHomeSprite;
		private UIPanel FavCimsWorkingPlacePanel;

		private UITextureSprite FavCimsWorkingPlaceSprite;
		private UIButton FavCimsWorkingPlace;
		private UIPanel FavCimsLastActivityPanel;
		private UIButton FavCimsLastActivity;
		private UIButton FavCimsLastActivityVehicleButton;
		private UIPanel FavCimsCloseRowPanel;

		private UITextureSprite FavCimsCitizenResidentialLevelSprite;
		private UITextureSprite FavCimsCitizenWorkPlaceLevelSprite;

		private UIButton FavCimsWorkingPlaceButtonGamDefImg;

		FamilyPanelTemplate MyFamily;

		string rowLang;

		//Var

		int citizenINT;

		int tmp_health;
		int tmp_wellbeing;
		int tmp_happiness;
		int tmp_age;

		int RealAge;
		int CitizenDistrict;
		int HomeDistrict;
		int WorkDistrict;
		int TargetDistrict;

		string CitizenName;
		string CitizenVehicleName;
		string DeathDate;
		string DeathTime;
		string CitizenTarget;
		string CitizenStatus;

		ushort CitizenHome;
		ushort CitizenVehicle;
		ushort WorkPlace;
		ushort InstanceCitizenID;
	
		bool GoingOutside;
		bool LeaveCity = false;
		bool DeadOrGone = false;
		bool HomeLess = false;
		bool CitizenIsDead = false;

		CitizenInstance citizenInstance;

		InstanceID WorkPlaceID;
		InstanceID CitizenHomeID;
		InstanceID MyTargetID;
		InstanceID MyVehicleID;

		CitizenInfo citizenInfo;
		BuildingInfo HomeInfo;
		BuildingInfo WorkInfo;
		VehicleInfo VehInfo;

		InstanceID CitizenDead;
		ushort hearse;

		internal static int GetTemplate() {

			for (int i = 0; i < FavCimsMainClass.MaxTemplates; i++) {

				if(FavCimsMainClass.Templates[i].MyInstanceID.IsEmpty) {
					return i;
				}

			}

			return -1;
		}
		
		public void GoToCitizen(InstanceID Target)
		{
			if (Target.IsEmpty)
				return;

			try {
				if (MyInstance.SelectInstance (Target)) {
					if (Input.GetMouseButton (2)) {

						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);

					} else if (Input.GetMouseButton (1)) {

						FavCimsMainClass.FavCimsPanel.Hide ();
						ToolsModifierControl.cameraController.SetTarget (Target, ToolsModifierControl.cameraController.transform.position, true);
						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);

					} else {

						ToolsModifierControl.cameraController.SetTarget (Target, ToolsModifierControl.cameraController.transform.position, true);
						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
					}
				}
			} catch(Exception e) {
				Debug.Error("Can't find the Citizen " + e.ToString());
			}
		}

		private void GoToHome(UIComponent component, UIMouseEventParameter p)
		{

			if (this.CitizenHomeID.IsEmpty)
				return;

			try {	
				if (Input.GetMouseButton (2)) {

					//DefaultTool.OpenWorldInfoPanel(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position);
					//ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, false);
					WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.CitizenHomeID);

				} else if (Input.GetMouseButton (1)) {
					
					FavCimsMainClass.FavCimsPanel.Hide ();
					//DefaultTool.OpenWorldInfoPanel(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position);
					ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
					WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.CitizenHomeID);

				} else {
					
					//DefaultTool.OpenWorldInfoPanel(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position);
					ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, true);
					WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.CitizenHomeID);
				}
			} catch(Exception e) {
				Debug.Error("Can't find the Home " + e.ToString());
			}
		}

		private void GoToWork(UIComponent component, UIMouseEventParameter p)
		{

			if (this.WorkPlaceID.IsEmpty)
				return;

			try {	
				if (Input.GetMouseButton (2)) {
					
					//ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, false);
					DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.WorkPlaceID);
					
				} else if (Input.GetMouseButton (1)) {
					
					FavCimsMainClass.FavCimsPanel.Hide ();
					ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.WorkPlaceID);
					
				} else {

					ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.WorkPlaceID);
				}
			} catch(Exception e) {
				Debug.Error("Can't find the WorkPlace " + e.ToString());
			}
		}
		
		private void GoToTarget(UIComponent component, UIMouseEventParameter p)
		{

			if (this.MyTargetID.IsEmpty)
				return;

			try {	
				if (Input.GetMouseButton (2)) {

					//ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, false);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.MyTargetID);
					DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);

				} else if (Input.GetMouseButton (1)) {
					
					FavCimsMainClass.FavCimsPanel.Hide ();
					ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.MyTargetID);
					
				} else {
					
					ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.MyTargetID);
				}
			} catch(Exception e) {
				Debug.Error("Can't find the Target " + e.ToString());
			}
		}

		private void GoToVehicle(UIComponent component, UIMouseEventParameter p)
		{

			if (this.MyVehicleID.IsEmpty)
				return;

			try {	
				if (Input.GetMouseButton (2)) {
					
					DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
					//ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, false);
					//WorldInfoPanel.Show<CitizenVehicleWorldInfoPanel> (this.position, this.MyVehicleID);
					
				} else if (Input.GetMouseButton (1)) {
					
					FavCimsMainClass.FavCimsPanel.Hide ();
					ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<CitizenVehicleWorldInfoPanel>  (this.position, this.MyVehicleID);
					
				} else {

					ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, true);
					DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<CitizenVehicleWorldInfoPanel>  (this.position, this.MyVehicleID);
				}
			} catch(Exception e) {
				Debug.Error("Can't find the Vehicle " + e.ToString());
			}
		}

		internal static string GetHappinessString(Citizen.Happiness happinessLevel)
		{
			return ("NotificationIcon" + sHappinessLevels[(int) happinessLevel]);
		}

		public override void Start() {

			try {

				uint citizen = this.MyInstanceID.Citizen;
				citizenINT = (int)((UIntPtr)citizen);
				this.CitizenName = this.MyInstance.GetName (this.MyInstanceID);

				if(this.MyInstancedName == null) {
					this.MyInstancedName = this.CitizenName;
				}

				if (citizenINT != 0 && !FavCimsCore.RowID.ContainsKey (citizenINT) && this.CitizenName != null && this.CitizenName.Length > 0) {

					FavCimsCore.InsertIdIntoArray (citizenINT);

					/////////////////////// 
					//Row Background Panel
					//////////////////////
					
					this.width = 1134;
					this.height = 41;
					this.autoLayoutDirection = LayoutDirection.Vertical;
					this.autoLayout = true;
					this.autoLayoutPadding = new RectOffset (0, 0, 1, 0);
					
					this.FavCimsCitizenSingleRowPanel = this.AddUIComponent<UIPanel> ();
					this.FavCimsCitizenSingleRowPanel.width = this.width;
					this.FavCimsCitizenSingleRowPanel.height = 40;
					this.FavCimsCitizenSingleRowBGSprite = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsCitizenSingleRowBGSprite.name = "FavCimsCitizenSingleRowBGSprite";
					this.FavCimsCitizenSingleRowBGSprite.width = this.FavCimsCitizenSingleRowPanel.width;
					this.FavCimsCitizenSingleRowBGSprite.height = this.FavCimsCitizenSingleRowPanel.height;
					this.FavCimsCitizenSingleRowBGSprite.AlignTo (this.FavCimsCitizenSingleRowPanel, UIAlignAnchor.TopLeft);
					
					//Background Color
					if (!FavoriteCimsMainPanel.RowAlternateBackground) {
						this.FavDot = ResourceLoader.loadTexture ((int)this.width, 40, "UIMainPanel.Rows.bgrow_1.png");
						this.FavDot.name = "FavDot_1";
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;
						FavoriteCimsMainPanel.RowAlternateBackground = true;
					} else {
						this.FavDot = ResourceLoader.loadTexture ((int)this.width, 40, "UIMainPanel.Rows.bgrow_2.png");
						this.FavDot.name = "FavDot_2";
						this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;
						FavoriteCimsMainPanel.RowAlternateBackground = false;
					}
					this.FavDot_hover = ResourceLoader.loadTexture ((int)this.width, 40, "UIMainPanel.Rows.bgrow_hover.png");
					
					//this.FavCimsCitizenSingleRowBGSprite.opacity = 0.60f;
					
					this.FavCimsCitizenSingleRowPanel.eventMouseEnter += (UIComponent component, UIMouseEventParameter eventParam) => this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot_hover;
					this.FavCimsCitizenSingleRowPanel.eventMouseLeave += (UIComponent component, UIMouseEventParameter eventParam) => this.FavCimsCitizenSingleRowBGSprite.texture = this.FavDot;

					////////////////////////
					//Happiness Column Panel
					////////////////////////
					
					this.FavCimsCitizenHappinessPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsCitizenHappinessPanel.name = "FavCimsCitizenHappinessPanel";
					this.FavCimsCitizenHappinessPanel.width = FavoriteCimsMainPanel.FavCimsHappinesColText.width;
					this.FavCimsCitizenHappinessPanel.height = 40;

					//Printing
					this.FavCimsCitizenHappinessPanel.relativePosition = new Vector3 (0, 0);

					//Happiness Icon
					this.FavCimsHappyIcon = this.FavCimsCitizenHappinessPanel.AddUIComponent<UIButton> ();
					this.FavCimsHappyIcon.width = 30;
					this.FavCimsHappyIcon.height = 30;
					this.FavCimsHappyIcon.isEnabled = false;
					this.FavCimsHappyIcon.playAudioEvents = false;
					this.FavCimsHappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;

					//Printing
					this.FavCimsHappyIcon.relativePosition = new Vector3 (15, 5);

					//Happiness Icon Override (if citizen is gone away)

					this.FavCimsHappyOverride = this.FavCimsHappyIcon.AddUIComponent<UITextureSprite> ();
					this.FavCimsHappyOverride.width = 30;
					this.FavCimsHappyOverride.height = 30;
					this.FavCimsHappyOverride.tooltipBox = UIView.GetAView().defaultTooltipBox;

					///////////////////
					//Name Column Panel
					///////////////////
					
					this.FavCimsCitizenNamePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsCitizenNamePanel.name = "FavCimsCitizenNamePanel";
					this.FavCimsCitizenNamePanel.width = FavoriteCimsMainPanel.FavCimsNameColText.width;
					this.FavCimsCitizenNamePanel.height = 40;
					
					//Printing
					this.FavCimsCitizenNamePanel.relativePosition = new Vector3 (this.FavCimsCitizenHappinessPanel.relativePosition.x + this.FavCimsCitizenHappinessPanel.width, 0);

					//Name Button
					this.FavCimsCitizenName = this.FavCimsCitizenNamePanel.AddUIComponent<UIButton> ();
					this.FavCimsCitizenName.name = "FavCimsCitizenName";
					this.FavCimsCitizenName.width = this.FavCimsCitizenNamePanel.width;
					this.FavCimsCitizenName.height = this.FavCimsCitizenNamePanel.height;
					this.FavCimsCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsCitizenName.textHorizontalAlignment = UIHorizontalAlignment.Left;
					this.FavCimsCitizenName.playAudioEvents = true;
					this.FavCimsCitizenName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsCitizenName.font.size = 15;
					this.FavCimsCitizenName.textScale = 1f;
					this.FavCimsCitizenName.wordWrap = true;
					this.FavCimsCitizenName.textPadding.left = 40;
					this.FavCimsCitizenName.textPadding.right = 5;
					this.FavCimsCitizenName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
					this.FavCimsCitizenName.hoveredTextColor = new Color32 (204, 102, 0, 20);
					this.FavCimsCitizenName.pressedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsCitizenName.focusedTextColor = new Color32 (153, 0, 0, 0);
					this.FavCimsCitizenName.useDropShadow = true;
					this.FavCimsCitizenName.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsCitizenName.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsCitizenName.maximumSize = new Vector2 (this.FavCimsCitizenNamePanel.width, this.FavCimsCitizenNamePanel.height);
					this.FavCimsCitizenName.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenName.eventMouseDown += (component, eventParam) => this.GoToCitizen(MyInstanceID);

					this.FavCimsNameColText_EmptySprite = this.FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsNameColText_EmptySprite.width = this.FavCimsCitizenName.width;
					this.FavCimsNameColText_EmptySprite.height = this.FavCimsCitizenName.height;
					this.FavCimsNameColText_EmptySprite.relativePosition = new Vector3 (0, 0);

					if(FavoriteCimsMainPanel.ColumnSpecialBackground == true) {
						this.FavCimsNameColText_EmptySprite.texture = ResourceLoader.loadTexture ((int)this.FavCimsNameColText_EmptySprite.width, 
						                                              (int)this.FavCimsNameColText_EmptySprite.height, "UIMainPanel.submenubar.png");
						this.FavCimsNameColText_EmptySprite.opacity = 0.7f;
					}

					FavoriteCimsMainPanel.FavCimsNameColText.eventClick += delegate {
						
						if (this.FavCimsNameColText_EmptySprite.texture == null) {
							this.FavCimsNameColText_EmptySprite.texture = ResourceLoader.loadTexture ((int)this.FavCimsNameColText_EmptySprite.width, 
							                                              (int)this.FavCimsNameColText_EmptySprite.height, "UIMainPanel.submenubar.png");
							this.FavCimsNameColText_EmptySprite.opacity = 0.7f;
							FavoriteCimsMainPanel.ColumnSpecialBackground = true;
						} else {
							this.FavCimsNameColText_EmptySprite.texture = null;
							FavoriteCimsMainPanel.ColumnSpecialBackground = false;
						}
						
					};

					//Printing
					this.FavCimsCitizenName.BringToFront ();
					this.FavCimsCitizenName.relativePosition = new Vector3 (0, 0);

					//Citizen Info Panel Button Toggle
					this.OtherInfoButton = this.FavCimsCitizenNamePanel.AddUIComponent<UIButton> ();
					this.OtherInfoButton.name = "FavCimsOtherInfoButton";
					this.OtherInfoButton.width = 20;
					this.OtherInfoButton.height = 20;
					this.OtherInfoButton.playAudioEvents = true;
					this.OtherInfoButton.normalBgSprite = "CityInfo";
					this.OtherInfoButton.hoveredBgSprite = "CityInfoHovered";
					this.OtherInfoButton.pressedBgSprite = "CityInfoPressed";
					//this.OtherInfoButton.focusedBgSprite = "CityInfoFocused";
					this.OtherInfoButton.disabledBgSprite = "CityInfoDisabled";
					this.OtherInfoButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

					//Printing
					this.OtherInfoButton.relativePosition = new Vector3(10,10);

					//Family Panel Toggle
					this.OtherInfoButton.eventClick += delegate {
						try
						{
							if(this.MyFamily == null && GetTemplate() >= 0) {
								this.MyFamily = FavCimsMainClass.Templates[GetTemplate()];
								this.MyFamily.MyInstanceID = this.MyInstanceID;
								this.MyFamily.Show();
								this.MyFamily.BringToFront ();
								this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
							}else if (this.MyFamily != null && !this.MyFamily.isVisible) {
								this.MyFamily.MyInstanceID = this.MyInstanceID;
								this.MyFamily.Show();
								this.MyFamily.BringToFront ();
								this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
							}else{
								if(this.MyFamily != null) {
									this.MyFamily.Hide();
									this.MyFamily.MyInstanceID = InstanceID.Empty;
									this.MyFamily = null;
									this.OtherInfoButton.normalBgSprite = "CityInfo";
								}
							}
						}catch (Exception e) {
							Debug.Error("Error when loading the template : " + e.ToString());
						}
					};

					//Icon Separator_2
					this.FavCimsSeparatorSprite2 = this.FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite2.name = "FavCimsSeparatorSprite2";
					this.FavCimsSeparatorSprite2.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite2.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Age Phase Column Panel
					///////////////////
					
					this.FavCimsAgePhasePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsAgePhasePanel.name = "FavCimsAgePhasePanel";
					this.FavCimsAgePhasePanel.width = FavoriteCimsMainPanel.FavCimsAgePhaseColText.width;
					this.FavCimsAgePhasePanel.height = 40;
					
					//Printing
					this.FavCimsAgePhasePanel.relativePosition = new Vector3 (this.FavCimsCitizenNamePanel.relativePosition.x + this.FavCimsCitizenNamePanel.width, 0);

					//Age Phase Button
					this.FavCimsAgePhase = this.FavCimsAgePhasePanel.AddUIComponent<UIButton> ();
					this.FavCimsAgePhase.name = "FavCimsAgePhase";
					this.FavCimsAgePhase.width = this.FavCimsAgePhasePanel.width;
					this.FavCimsAgePhase.height = this.FavCimsAgePhasePanel.height;
					this.FavCimsAgePhase.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsAgePhase.playAudioEvents = true;
					this.FavCimsAgePhase.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsAgePhase.font.size = 15;
					this.FavCimsAgePhase.textScale = 1f;
					this.FavCimsAgePhase.wordWrap = true;
					this.FavCimsAgePhase.textPadding.left = 5;
					this.FavCimsAgePhase.textPadding.right = 5;
					this.FavCimsAgePhase.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
					this.FavCimsAgePhase.outlineSize = 1;
					this.FavCimsAgePhase.outlineColor = new Color32 (0, 0, 0, 0);
					this.FavCimsAgePhase.useDropShadow = true;
					this.FavCimsAgePhase.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsAgePhase.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsAgePhase.maximumSize = new Vector2 (this.FavCimsAgePhasePanel.width, this.FavCimsAgePhasePanel.height);
					this.FavCimsAgePhase.isInteractive = false;

					//Printing
					this.FavCimsAgePhase.relativePosition = new Vector3 (0, 0);

					//Icon Separator_3
					this.FavCimsSeparatorSprite3 = this.FavCimsAgePhasePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite3.name = "FavCimsSeparatorSprite3";
					this.FavCimsSeparatorSprite3.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite3.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Age Column Panel
					///////////////////
					
					this.FavCimsRealAgePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsRealAgePanel.name = "FavCimsRealAgePanel";
					this.FavCimsRealAgePanel.width = FavoriteCimsMainPanel.FavCimsAgeColText.width;
					this.FavCimsRealAgePanel.height = 40;
					
					//Printing
					this.FavCimsRealAgePanel.relativePosition = new Vector3 (this.FavCimsAgePhasePanel.relativePosition.x + this.FavCimsAgePhasePanel.width, 0);
					
					//Real Age Button
					this.FavCimsRealAge = this.FavCimsRealAgePanel.AddUIComponent<UIButton> ();
					this.FavCimsRealAge.name = "FavCimsRealAge";
					this.FavCimsRealAge.width = this.FavCimsRealAgePanel.width;
					this.FavCimsRealAge.height = this.FavCimsRealAgePanel.height;
					this.FavCimsRealAge.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsRealAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsRealAge.playAudioEvents = true;
					this.FavCimsRealAge.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsRealAge.font.size = 15;
					this.FavCimsRealAge.textScale = 1f;
					this.FavCimsRealAge.wordWrap = true;
					this.FavCimsRealAge.textPadding.left = 5;
					this.FavCimsRealAge.textPadding.right = 5;
					this.FavCimsRealAge.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
					this.FavCimsRealAge.outlineSize = 1;
					this.FavCimsRealAge.outlineColor = new Color32 (0, 0, 0, 0);
					//this.FavCimsRealAge.hoveredTextColor = new Color32 (204, 102, 0, 20);
					//this.FavCimsRealAge.pressedTextColor = new Color32 (153, 0, 0, 0);
					//this.FavCimsRealAge.focusedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsRealAge.useDropShadow = true;
					this.FavCimsRealAge.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsRealAge.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsRealAge.maximumSize = new Vector2 (this.FavCimsRealAgePanel.width, this.FavCimsRealAgePanel.height);
					//this.FavCimsRealAge.isEnabled = false;
					this.FavCimsRealAge.isInteractive = false;
					
					//Printing
					this.FavCimsRealAge.relativePosition = new Vector3 (0, 0);

					//Icon Separator_4
					this.FavCimsSeparatorSprite4 = this.FavCimsRealAgePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite4.name = "FavCimsSeparatorSprite4";
					this.FavCimsSeparatorSprite4.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite4.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Education Column Panel
					///////////////////
					
					this.FavCimsEducationPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsEducationPanel.name = "FavCimsEducationPanel";
					this.FavCimsEducationPanel.width = FavoriteCimsMainPanel.FavCimsEduColText.width;
					this.FavCimsEducationPanel.height = 40;
					
					//Printing
					this.FavCimsEducationPanel.relativePosition = new Vector3 (this.FavCimsRealAgePanel.relativePosition.x + this.FavCimsRealAgePanel.width, 0);
					
					//Education Button
					this.FavCimsEducation = this.FavCimsEducationPanel.AddUIComponent<UIButton> ();
					this.FavCimsEducation.name = "FavCimsEducation";
					this.FavCimsEducation.width = this.FavCimsEducationPanel.width;
					this.FavCimsEducation.height = this.FavCimsEducationPanel.height;
					this.FavCimsEducation.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
					this.FavCimsEducation.playAudioEvents = true;
					this.FavCimsEducation.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsEducation.font.size = 15;
					this.FavCimsEducation.textScale = 1f;
					this.FavCimsEducation.wordWrap = true;
					this.FavCimsEducation.textPadding.left = 5;
					this.FavCimsEducation.textPadding.right = 5;
					this.FavCimsEducation.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
					this.FavCimsEducation.outlineSize = 1;
					this.FavCimsEducation.outlineColor = new Color32 (0, 0, 0, 0);
					//this.FavCimsEducation.hoveredTextColor = new Color32 (204, 102, 0, 20);
					//this.FavCimsEducation.pressedTextColor = new Color32 (153, 0, 0, 0);
					//this.FavCimsEducation.focusedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsEducation.useDropShadow = true;
					this.FavCimsEducation.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsEducation.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsEducation.maximumSize = new Vector2 (this.FavCimsEducationPanel.width, this.FavCimsEducationPanel.height);
					//this.FavCimsEducation.isEnabled = false;
					this.FavCimsEducation.isInteractive = false;
					
					//Printing
					this.FavCimsEducation.relativePosition = new Vector3 (0, 0);
					
					//Icon Separator_5
					this.FavCimsSeparatorSprite5 = this.FavCimsEducationPanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite5.name = "FavCimsSeparatorSprite5";
					this.FavCimsSeparatorSprite5.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite5.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Home Column Panel
					///////////////////
					
					this.FavCimsCitizenHomePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsCitizenHomePanel.name = "FavCimsCitizenHomePanel";
					this.FavCimsCitizenHomePanel.width = FavoriteCimsMainPanel.FavCimsHomeColText.width;
					this.FavCimsCitizenHomePanel.height = 40;
					
					//Printing
					this.FavCimsCitizenHomePanel.relativePosition = new Vector3 (this.FavCimsEducationPanel.relativePosition.x + this.FavCimsEducationPanel.width, 0);

					//Home Building
					this.FavCimsCitizenHome = this.FavCimsCitizenHomePanel.AddUIComponent<UIButton> ();
					this.FavCimsCitizenHome.name = "FavCimsCitizenHome";
					this.FavCimsCitizenHome.width = this.FavCimsCitizenHomePanel.width;
					this.FavCimsCitizenHome.height = this.FavCimsCitizenHomePanel.height;
					this.FavCimsCitizenHome.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsCitizenHome.textHorizontalAlignment = UIHorizontalAlignment.Left;
					//this.FavCimsCitizenHome.textPadding.left = 5;
					this.FavCimsCitizenHome.playAudioEvents = true;
					this.FavCimsCitizenHome.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsCitizenHome.font.size = 15;
					this.FavCimsCitizenHome.textScale = 0.85f;
					this.FavCimsCitizenHome.wordWrap = true;
					this.FavCimsCitizenHome.textPadding.left = 40;
					this.FavCimsCitizenHome.textPadding.right = 5;
					this.FavCimsCitizenHome.outlineColor = new Color32 (0, 0, 0, 0);
					this.FavCimsCitizenHome.outlineSize = 1;
					this.FavCimsCitizenHome.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
					this.FavCimsCitizenHome.hoveredTextColor = new Color32 (204, 102, 0, 20);
					this.FavCimsCitizenHome.pressedTextColor = new Color32 (153, 0, 0, 0);
					this.FavCimsCitizenHome.focusedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsCitizenHome.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
					this.FavCimsCitizenHome.useDropShadow = true;
					this.FavCimsCitizenHome.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsCitizenHome.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsCitizenHome.maximumSize = new Vector2 (this.FavCimsCitizenHomePanel.width, this.FavCimsCitizenHomePanel.height);
					this.FavCimsCitizenHome.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsCitizenHome.eventMouseDown += new MouseEventHandler(this.GoToHome);

					//Printing
					this.FavCimsCitizenHome.relativePosition = new Vector3 (0, 0);

					//Home sprites

					this.FavCimsCitizenHomeSprite = this.FavCimsCitizenHomePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsCitizenHomeSprite.name = "FavCimsCitizenHomeSprite";
					this.FavCimsCitizenHomeSprite.relativePosition = new Vector3 (10, 0);
					this.FavCimsCitizenHomeSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;

					this.FavCimsCitizenResidentialLevelSprite = this.FavCimsCitizenHomeSprite.AddUIComponent<UITextureSprite> ();
					this.FavCimsCitizenResidentialLevelSprite.name = "FavCimsCitizenResidentialLevelSprite";
					this.FavCimsCitizenResidentialLevelSprite.relativePosition = new Vector3 (0, 0);

					//Icon Separator_6
					this.FavCimsSeparatorSprite6 = this.FavCimsCitizenHomePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite6.name = "FavCimsSeparatorSprite6";
					this.FavCimsSeparatorSprite6.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite6.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Work Column Panel
					///////////////////
					
					this.FavCimsWorkingPlacePanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsWorkingPlacePanel.name = "FavCimsWorkingPlacePanel";
					this.FavCimsWorkingPlacePanel.width = FavoriteCimsMainPanel.FavCimsWorkingPlaceColText.width;
					this.FavCimsWorkingPlacePanel.height = 40;

					//Printing
					this.FavCimsWorkingPlacePanel.relativePosition = new Vector3 (this.FavCimsCitizenHomePanel.relativePosition.x + this.FavCimsCitizenHomePanel.width, 0);

					//Work Building
					this.FavCimsWorkingPlace = this.FavCimsWorkingPlacePanel.AddUIComponent<UIButton> ();
					this.FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
					this.FavCimsWorkingPlace.width = this.FavCimsWorkingPlacePanel.width;
					this.FavCimsWorkingPlace.height = this.FavCimsWorkingPlacePanel.height;
					this.FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsWorkingPlace.textHorizontalAlignment = UIHorizontalAlignment.Left;
					this.FavCimsWorkingPlace.playAudioEvents = true;
					this.FavCimsWorkingPlace.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsWorkingPlace.font.size = 15;
					this.FavCimsWorkingPlace.textScale = 0.85f;
					this.FavCimsWorkingPlace.wordWrap = true;
					this.FavCimsWorkingPlace.textPadding.left = 40;
					this.FavCimsWorkingPlace.textPadding.right = 5;
					this.FavCimsWorkingPlace.outlineColor = new Color32 (0, 0, 0, 0);
					this.FavCimsWorkingPlace.outlineSize = 1;
					this.FavCimsWorkingPlace.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
					this.FavCimsWorkingPlace.hoveredTextColor = new Color32 (204, 102, 0, 20);
					this.FavCimsWorkingPlace.pressedTextColor = new Color32 (153, 0, 0, 0);
					this.FavCimsWorkingPlace.focusedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsWorkingPlace.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
					this.FavCimsWorkingPlace.useDropShadow = true;
					this.FavCimsWorkingPlace.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsWorkingPlace.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsWorkingPlace.maximumSize = new Vector2 (this.FavCimsWorkingPlacePanel.width, this.FavCimsWorkingPlacePanel.height);
					this.FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsWorkingPlace.eventMouseDown += new MouseEventHandler(this.GoToWork);
					
					//Printing
					this.FavCimsWorkingPlace.relativePosition = new Vector3 (0, 0);

					//Work sprites

					this.FavCimsWorkingPlaceSprite = this.FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
					this.FavCimsWorkingPlaceSprite.width = 20;
					this.FavCimsWorkingPlaceSprite.height = 40;
					this.FavCimsWorkingPlaceSprite.relativePosition = new Vector3 (10, 0);
					this.FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;

					//I will put all icons inside the atlas soon.
					this.FavCimsWorkingPlaceButtonGamDefImg = this.FavCimsWorkingPlaceSprite.AddUIComponent<UIButton> ();
					this.FavCimsWorkingPlaceButtonGamDefImg.name = "FavCimsWorkingPlaceButtonGamDefImg";
					this.FavCimsWorkingPlaceButtonGamDefImg.width = 20;
					this.FavCimsWorkingPlaceButtonGamDefImg.height = 20;
					this.FavCimsWorkingPlaceButtonGamDefImg.relativePosition = new Vector3 (0, 10);
					this.FavCimsWorkingPlaceButtonGamDefImg.isInteractive = false;
					this.FavCimsWorkingPlaceButtonGamDefImg.tooltipBox = UIView.GetAView().defaultTooltipBox;

					this.FavCimsCitizenWorkPlaceLevelSprite = this.FavCimsWorkingPlaceSprite.AddUIComponent<UITextureSprite> ();
					this.FavCimsCitizenWorkPlaceLevelSprite.name = "FavCimsCitizenWorkPlaceLevelSprite";
					this.FavCimsCitizenWorkPlaceLevelSprite.relativePosition = new Vector3 (0, 0);

					//Icon Separator_7
					this.FavCimsSeparatorSprite7 = this.FavCimsWorkingPlacePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite7.name = "FavCimsSeparatorSprite7";
					this.FavCimsSeparatorSprite7.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite7.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Last Activity Column Panel
					///////////////////
					
					this.FavCimsLastActivityPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsLastActivityPanel.name = "FavCimsLastActivityPanel";
					this.FavCimsLastActivityPanel.width = FavoriteCimsMainPanel.FavCimsLastActColText.width;
					this.FavCimsLastActivityPanel.height = 40;

					//Printing
					this.FavCimsLastActivityPanel.relativePosition = new Vector3 (this.FavCimsWorkingPlacePanel.relativePosition.x + this.FavCimsWorkingPlacePanel.width, 0);

					//Last Activity
					this.FavCimsLastActivity = this.FavCimsLastActivityPanel.AddUIComponent<UIButton> ();
					this.FavCimsLastActivity.name = "FavCimsLastActivity";
					this.FavCimsLastActivity.width = this.FavCimsLastActivityPanel.width-40;
					this.FavCimsLastActivity.height = this.FavCimsLastActivityPanel.height;
					this.FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
					this.FavCimsLastActivity.textHorizontalAlignment = UIHorizontalAlignment.Left;
					this.FavCimsLastActivity.playAudioEvents = true;
					this.FavCimsLastActivity.font = UIDynamicFont.FindByName ("OpenSans-Regular");
					this.FavCimsLastActivity.font.size = 15;
					this.FavCimsLastActivity.textScale = 0.85f;
					this.FavCimsLastActivity.wordWrap = true;
					this.FavCimsLastActivity.textPadding.left = 0;
					this.FavCimsLastActivity.textPadding.right = 5;
					this.FavCimsLastActivity.outlineColor = new Color32 (0, 0, 0, 0);
					this.FavCimsLastActivity.outlineSize = 1;
					this.FavCimsLastActivity.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
					this.FavCimsLastActivity.hoveredTextColor = new Color32 (204, 102, 0, 20);
					this.FavCimsLastActivity.pressedTextColor = new Color32 (153, 0, 0, 0);
					this.FavCimsLastActivity.focusedTextColor = new Color32 (102, 153, 255, 147);
					this.FavCimsLastActivity.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
					this.FavCimsLastActivity.useDropShadow = true;
					this.FavCimsLastActivity.dropShadowOffset = new Vector2 (1, -1);
					this.FavCimsLastActivity.dropShadowColor = new Color32 (0, 0, 0, 0);
					this.FavCimsLastActivity.maximumSize = new Vector2 (this.FavCimsLastActivityPanel.width-40, this.FavCimsLastActivityPanel.height);
					this.FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsLastActivity.eventMouseDown += new MouseEventHandler(this.GoToTarget);
					
					//Printing
					this.FavCimsLastActivity.relativePosition = new Vector3 (40, 0);

					//Last Activity Button
					this.FavCimsLastActivityVehicleButton = this.FavCimsLastActivityPanel.AddUIComponent<UIButton> ();
					this.FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
					this.FavCimsLastActivityVehicleButton.width = 26;
					this.FavCimsLastActivityVehicleButton.height = 26;
					this.FavCimsLastActivityVehicleButton.relativePosition = new Vector3 (5, 7);
					this.FavCimsLastActivityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsLastActivityVehicleButton.eventMouseDown += new MouseEventHandler(this.GoToVehicle);

					//Icon Separator_8
					this.FavCimsSeparatorSprite8 = this.FavCimsLastActivityPanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite8.name = "FavCimsSeparatorSprite8";
					this.FavCimsSeparatorSprite8.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite8.relativePosition = new Vector3 (0, 0);

					///////////////////
					//Close Row Column Panel
					///////////////////
					
					this.FavCimsCloseRowPanel = this.FavCimsCitizenSingleRowPanel.AddUIComponent<UIPanel> ();
					this.FavCimsCloseRowPanel.name = "FavCimsCloseRowPanel";
					this.FavCimsCloseRowPanel.width = FavoriteCimsMainPanel.FavCimsCloseButtonCol.width;
					this.FavCimsCloseRowPanel.height = 40;

					//Printing
					this.FavCimsCloseRowPanel.relativePosition = new Vector3 (this.FavCimsLastActivityPanel.relativePosition.x + FavCimsLastActivityPanel.width, 0);

					//Close row & unfollow citizen Button
					this.FavCimsRowCloseButton = this.FavCimsCloseRowPanel.AddUIComponent<UIButton> ();
					this.FavCimsRowCloseButton.name = "FavCimsRowCloseButton";
					this.FavCimsRowCloseButton.width = 26;
					this.FavCimsRowCloseButton.height = 26;
					this.FavCimsRowCloseButton.normalBgSprite = "buttonclose";
					this.FavCimsRowCloseButton.hoveredBgSprite = "buttonclosehover";
					this.FavCimsRowCloseButton.pressedBgSprite = "buttonclosepressed";
					this.FavCimsRowCloseButton.opacity = 0.9f;
					//this.FavCimsRowCloseButton.useOutline = true;
					this.FavCimsRowCloseButton.playAudioEvents = true;
					this.FavCimsRowCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

					this.FavCimsRowCloseButton.eventClick += delegate {
						try {
							FavCimsCore.RemoveRowAndRemoveFav(this.MyInstanceID, citizenINT);
							if(this.MyFamily != null) {
								this.MyFamily.Hide();
								this.MyFamily.MyInstanceID = InstanceID.Empty;
								this.MyFamily = null;
							}

							if(UIView.Find<UILabel>("DefaultTooltip"))
								UIView.Find<UILabel>("DefaultTooltip").Hide();

							GameObject.Destroy(this.gameObject);

						}catch(Exception e) {
							Debug.Error("Can't remove row " + e.ToString());
						}
					};
					
					//Printing
					this.FavCimsRowCloseButton.relativePosition = new Vector3 ((this.FavCimsCloseRowPanel.width / 2) - (this.FavCimsRowCloseButton.width / 2), 7);
					
					//Icon Separator_9
					this.FavCimsSeparatorSprite9 = this.FavCimsCloseRowPanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsSeparatorSprite9.name = "FavCimsSeparatorSprite9";
					this.FavCimsSeparatorSprite9.texture = TextureDB.FavCimsSeparator;
					this.FavCimsSeparatorSprite9.relativePosition = new Vector3 (0, 0);

					//Row end//
				}
			} catch (Exception e) {
				Debug.Error ("CitizenRow Create Error : " + e.ToString ());
			}
		}
		
		public override void Update() {

			if (FavCimsMainClass.UnLoading)
				return;

			if (this.FirstRun) {

				this.secondsForceRun -= 1 * Time.deltaTime;

				if (this.secondsForceRun > 0) {
					this.execute = true;
				} else {
					this.FirstRun = false;
				}

			} else {
				if (!FavCimsMainClass.FavCimsPanel.isVisible || this.IsClippedFromParent ()) {

					this.FavCimsCitizenSingleRowPanel.Hide ();

					this.HiddenRowsSeconds -= 1 * Time.deltaTime;

					if(this.HiddenRowsSeconds <= 0) {
						execute = true;
						this.HiddenRowsSeconds = SlowRun;
					}else{
						execute = false;
					}
				} else {
					this.FavCimsCitizenSingleRowPanel.Show ();

					this.seconds -= 1 * Time.deltaTime;

					if (this.seconds <= 0) {
						this.execute = true;
						this.seconds = Run;
					} else {
						this.execute = false;
					}
				}
			}
		}
		
		public override void LateUpdate()
		{
			if (FavCimsMainClass.UnLoading)
				return;
		if (this.MyInstanceID.IsEmpty || !FavCimsCore.RowID.ContainsKey (citizenINT)) {
				if(this.MyFamily != null) {
					this.MyFamily.Hide();
					this.MyFamily.MyInstanceID = InstanceID.Empty;
					this.MyFamily = null;
				}
				GameObject.Destroy (this.gameObject);
				return;
			}

			if (this.DeadOrGone || this.HomeLess) {
				this.OtherInfoButton.isEnabled = false;
				this.OtherInfoButton.tooltip = FavCimsLang.text ("Citizen_Details_NoUnit");
			}else if(GetTemplate() == -1 && (this.MyFamily == null || this.MyFamily.MyInstanceID != MyInstanceID)) {
				if(this.MyFamily != null && this.MyFamily.MyInstanceID != MyInstanceID) {
					this.MyFamily = null;
				}
				this.OtherInfoButton.isEnabled = false;
				this.OtherInfoButton.tooltip = FavCimsLang.text ("Citizen_Details_fullTemplate");
			} else {
				if(this.MyFamily != null && this.MyFamily.MyInstanceID == this.MyInstanceID && this.MyFamily.isVisible) {
					this.OtherInfoButton.normalBgSprite = "CityInfoFocused";
				}else{
					this.OtherInfoButton.normalBgSprite = "CityInfo";
				}
				this.OtherInfoButton.isEnabled = true;
				this.OtherInfoButton.tooltip = FavCimsLang.text ("Citizen_Details");
			}

			uint citizen = this.MyInstanceID.Citizen;

			//Is dead?
			if((citizen != 0) && this.MyCitizen.m_citizens.m_buffer[citizen].Dead && !this.CitizenIsDead) {
				this.CitizenIsDead = true;
				this.CitizenRowData ["deathrealage"] = "0";
			}

			if (this.execute) {

				try {

					this.CitizenName = this.MyInstance.GetName (this.MyInstanceID);

					citizenINT = (int)((UIntPtr)citizen);

					if (this.CitizenName != null && this.CitizenName.Length > 0 && this.CitizenName != this.MyInstancedName) {
						this.MyInstancedName = this.CitizenName;
					}

					this.citizenInfo = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCitizenInfo (citizen);

					this.FavCimsRowCloseButton.tooltip = FavCimsLang.text ("FavStarButton_disable_tooltip");

					if (this.FavCimsCitizenSingleRowPanel != null && citizen != 0 && this.CitizenName == this.MyInstancedName && FavCimsCore.RowID.ContainsKey (citizenINT)) {

						//Citizen Gender
						this.CitizenRowData ["gender"] = Citizen.GetGender (citizen).ToString ();
						
						//Name
						this.CitizenRowData ["name"] = this.MyCitizen.GetCitizenName (citizen);
						this.FavCimsCitizenName.text = this.CitizenRowData ["name"];
						if (this.CitizenRowData ["gender"] == "Female") {
							this.FavCimsCitizenName.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
						}

						if (this.CitizenDistrict == 0) {
							this.FavCimsCitizenName.tooltip = FavCimsLang.text ("NowInThisDistrict") + FavCimsLang.text ("DistrictNameNoDistrict");
						} else {
							this.FavCimsCitizenName.tooltip = FavCimsLang.text ("NowInThisDistrict") + MyDistrict.GetDistrictName (this.CitizenDistrict);
						}

						//Citizen Health
						this.tmp_health = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_health;
						this.CitizenRowData ["health"] = Citizen.GetHealthLevel (this.tmp_health).ToString ();
						
						//Citizen Education
						var tmp_education = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].EducationLevel;
						this.CitizenRowData ["education"] = tmp_education.ToString ();
						this.FavCimsEducation.text = FavCimsLang.text ("Education_" + this.CitizenRowData ["education"] + "_" + this.CitizenRowData ["gender"]);
						
						if (this.CitizenRowData ["education"] == "ThreeSchools") {
							this.FavCimsEducation.textColor = new Color32 (102, 204, 0, 60); //r,g,b,a
						} else if (this.CitizenRowData ["education"] == "TwoSchools") {
							this.FavCimsEducation.textColor = new Color32 (255, 204, 0, 32);
						} else if (this.CitizenRowData ["education"] == "OneSchool") {
							this.FavCimsEducation.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
						} else {
							this.FavCimsEducation.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
						}
						
						//Citizen Wellbeing
						this.tmp_wellbeing = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_wellbeing;
						this.CitizenRowData ["wellbeing"] = Citizen.GetWellbeingLevel (tmp_education, this.tmp_wellbeing).ToString ();
						
						//Citizen Happiness
						this.tmp_happiness = Citizen.GetHappiness (this.tmp_health, this.tmp_wellbeing);
						//this.CitizenRowData ["happiness"] = Citizen.GetHappinessLevel (this.tmp_happiness).ToString (); //Bad, Poor, Good, Excellent, Suberb
						this.CitizenRowData ["happiness_icon"] = GetHappinessString (Citizen.GetHappinessLevel (this.tmp_happiness));
						this.FavCimsHappyIcon.normalBgSprite = this.CitizenRowData ["happiness_icon"];
						this.FavCimsHappyIcon.tooltip = FavCimsLang.text (this.CitizenRowData ["happiness_icon"]);


						//Age Group (Age Phase)
						this.tmp_age = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_age;
						this.CitizenRowData ["agegroup"] = Citizen.GetAgeGroup (this.tmp_age).ToString ();
						this.FavCimsAgePhase.text = FavCimsLang.text ("AgePhase_" + this.CitizenRowData ["agegroup"] + "_" + this.CitizenRowData ["gender"]);

						//Real Age 
						this.RealAge = FavCimsCore.CalculateCitizenAge (this.tmp_age);

						if (this.RealAge <= 12) { //CHILD
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (102, 204, 0, 60); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (102, 204, 0, 60); //r,g,b,a
						} else if (this.RealAge <= 19) { //TEEN
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
						} else if (this.RealAge <= 25) { //YOUNG
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
						} else if (this.RealAge <= 65) { //ADULT
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
						} else if (this.RealAge <= 90) { //SENIOR
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
						} else { //FINAL
							this.FavCimsRealAge.text = this.RealAge.ToString ();
							this.FavCimsRealAge.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
							this.FavCimsAgePhase.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
						}

						//Citizen Home
						this.CitizenHome = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_homeBuilding;
						if (this.CitizenHome != 0) {
							this.HomeLess = false;
							this.CitizenHomeID.Building = this.CitizenHome;
							this.FavCimsCitizenHome.text = this.MyBuilding.GetBuildingName (this.CitizenHome, this.MyInstanceID);
							this.FavCimsCitizenHome.isEnabled = true;
							this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTexture;
							this.HomeInfo = this.MyBuilding.m_buildings.m_buffer [CitizenHomeID.Index].Info;

							if (this.HomeInfo.m_class.m_service == ItemClass.Service.Residential) {

								this.FavCimsCitizenHome.tooltip = null;

								if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
									this.FavCimsCitizenHome.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialHigh.ToString ());
								} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 153, 0, 80); //r,g,b,a
									this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTexture;
									this.FavCimsCitizenHome.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialLow.ToString ());
								}

								switch (this.HomeInfo.m_class.m_level) 
								{
									case ItemClass.Level.Level5:
										this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [5];
										break;
									case ItemClass.Level.Level4:
										this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [4];
										break;
									case ItemClass.Level.Level3:
										this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [2];
										break;
									default:
										this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [1];
										break;
								}

								/*
								if (this.HomeInfo.m_class.m_level == ItemClass.Level.Level5) {
									this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [5];
								} else if (this.HomeInfo.m_class.m_level == ItemClass.Level.Level4) {
									this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [4];
								} else if (this.HomeInfo.m_class.m_level == ItemClass.Level.Level3) {
									this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [3];
								} else if (this.HomeInfo.m_class.m_level == ItemClass.Level.Level2) {
									this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [2];
								} else {
									this.FavCimsCitizenResidentialLevelSprite.texture = TextureDB.FavCimsResidentialLevel [1];
								}
								*/

								//District
								this.HomeDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [CitizenHomeID.Index].m_position);

								if (this.HomeDistrict == 0) {
									this.FavCimsCitizenHomeSprite.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
								} else {
									this.FavCimsCitizenHomeSprite.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.HomeDistrict);
								}
							}
						} else {
							this.FavCimsCitizenHome.text = FavCimsLang.text ("Citizen_HomeLess");
							this.FavCimsCitizenHome.isEnabled = false;
							this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
							this.FavCimsCitizenHomeSprite.tooltip = FavCimsLang.text ("DistrictNameNoDistrict");
							this.FavCimsCitizenHome.tooltip = FavCimsLang.text ("Citizen_HomeLess_tooltip");
							this.FavCimsCitizenResidentialLevelSprite.texture = null;
							this.HomeLess = true;
						}

						//Working Place
						this.WorkPlace = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_workBuilding;
						if (this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen) != ItemClass.Level.None) {
							this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
							this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
							this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							this.FavCimsWorkingPlace.tooltip = Locale.Get ("CITIZEN_SCHOOL_LEVEL", this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen).ToString ());
						} else if (this.WorkPlace == 0) {
							this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;

							if (this.tmp_age >= 180) {
								//In Pensione
								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
								this.FavCimsWorkingPlace.text = FavCimsLang.text ("Citizen_Retired");
								this.FavCimsWorkingPlace.isEnabled = false;
								this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Citizen_Retired_tooltip");
								this.FavCimsWorkingPlaceSprite.tooltip = null;
								this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							} else { 
								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture; //unemployed
								this.FavCimsWorkingPlace.text = Locale.Get ("CITIZEN_OCCUPATION_UNEMPLOYED");
								this.FavCimsWorkingPlace.isEnabled = false;
								this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Unemployed_tooltip");
								this.FavCimsWorkingPlaceSprite.tooltip = null;
								this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							}
						}

						if (this.WorkPlace != 0) {
							this.WorkPlaceID.Building = this.WorkPlace;
							this.FavCimsWorkingPlace.text = this.MyBuilding.GetBuildingName (this.WorkPlace, this.MyInstanceID);
							this.FavCimsWorkingPlace.isEnabled = true;
							this.WorkInfo = this.MyBuilding.m_buildings.m_buffer [WorkPlaceID.Index].Info;

							if (this.WorkInfo.m_class.m_service == ItemClass.Service.Commercial) {
								this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
								if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh) {
									this.FavCimsWorkingPlace.textColor = new Color32 (0, 51, 153, 147); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.CommercialHigh.ToString ());
								} else {
									this.FavCimsWorkingPlace.textColor = new Color32 (0, 153, 204, 130); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.CommercialLow.ToString ());
								}

								switch (this.WorkInfo.m_class.m_level) 
								{
									case ItemClass.Level.Level3:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [2];
										break;
									default:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [1];
										break;
								}

								/*
								if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level3) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [3];
								} else if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level2) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [2];
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [1];
								}
								*/

							} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Industrial) {

								this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", "Industrial");

								switch (this.WorkInfo.m_class.m_subService) 
								{
									case ItemClass.SubService.IndustrialFarming:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
										break;
									case ItemClass.SubService.IndustrialForestry:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
										break;
									case ItemClass.SubService.IndustrialOil:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
										break;
									case ItemClass.SubService.IndustrialOre:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
										break;
									default:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
										this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
										break;
								}
								/*
								if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialFarming) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialForestry) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialOil) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialOre) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
								} else {
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
								}
								*/

								switch (this.WorkInfo.m_class.m_level) 
								{
									case ItemClass.Level.Level3:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [2];
										break;
									default:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [1];
										break;
								}

								/*
								if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level3) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [3];
								} else if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level2) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [2];
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [1];
								}
								*/

							} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Office) {
								this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
								this.FavCimsWorkingPlace.textColor = new Color32 (0, 204, 255, 128); //r,g,b,a
								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;
								this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", "Office");

								switch (this.WorkInfo.m_class.m_level) 
								{
									case ItemClass.Level.Level3:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [2];
										break;
									default:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsCommercialLevel [1];
										break;
								}
								
								/*
								if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level3) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [3];
								} else if (this.WorkInfo.m_class.m_level == ItemClass.Level.Level2) {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [2];
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [1];
								}
								*/

							} else {
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								this.FavCimsWorkingPlace.textColor = new Color32 (153, 102, 51, 20); //r,g,b,a

								switch (this.WorkInfo.m_class.m_service) 
								{
									case ItemClass.Service.FireDepartment:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconFireSafety";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "FireDepartment");
										break;
									case ItemClass.Service.HealthCare:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconHealthcareFocused";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Healthcare");
										break;
									case ItemClass.Service.PoliceDepartment:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconPolice";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Police");
										break;
									case ItemClass.Service.Garbage:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyRecycling";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Garbage");
										break;
									case ItemClass.Service.Electricity:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyPowerSaving";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Electricity_job");
										break;
									case ItemClass.Service.Education:
										this.FavCimsWorkingPlace.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconEducationPressed";
										break;
									case ItemClass.Service.Beautification:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarBeautificationParksnPlazas";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Beautification");
										break;
									case ItemClass.Service.Government:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconGovernment";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Government_job");
										break;
									case ItemClass.Service.Water:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Water_job");
										break;
									case ItemClass.Service.PublicTransport:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFreePublicTransport";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "PublicTransport");
										break;
									case ItemClass.Service.Monument:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "FeatureMonumentLevel6";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Monuments");
										break;
									default:
										this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyNone";
										this.FavCimsWorkingPlace.tooltip = null;
										break;
								}

								/*
								if (this.WorkInfo.m_class.m_service == ItemClass.Service.FireDepartment) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconFireSafety";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "FireDepartment");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.HealthCare) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconHealthcareFocused";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Healthcare");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.PoliceDepartment) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconPolice";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Police");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Garbage) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyRecycling";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Garbage");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Electricity) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyPowerSaving";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Electricity_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Education) {

									this.FavCimsWorkingPlace.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "InfoIconEducationPressed";

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Beautification) { //parchi

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarBeautificationParksnPlazas";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Beautification");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Government) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconGovernment";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Government_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Water) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Water_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.PublicTransport) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFreePublicTransport";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "PublicTransport");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Monument) {

									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "FeatureMonumentLevel6";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Monuments");
								
								} else if (this.WorkInfo.m_class.m_service != ItemClass.Service.None) {
									
									this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyNone";
									this.FavCimsWorkingPlace.tooltip = null;
								}
								*/
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							}

							//District
							this.WorkDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [WorkPlaceID.Index].m_position);
							
							if (this.WorkDistrict == 0) {
								this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
							} else {
								this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.WorkDistrict);
							}

						} else {
							this.FavCimsWorkingPlace.isEnabled = false;
							this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
							this.FavCimsWorkingPlaceSprite.tooltip = null;
						}

						this.InstanceCitizenID = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_instance;
						this.citizenInstance = this.MyCitizen.m_instances.m_buffer [InstanceCitizenID];

						if (this.citizenInstance.m_targetBuilding != 0) {

							this.CitizenVehicle = this.MyCitizen.m_citizens.m_buffer [citizen].m_vehicle;
							this.MyVehicleID = InstanceID.Empty;

							GoingOutside = (MyBuilding.m_buildings.m_buffer [this.citizenInstance.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None;

							if (this.CitizenVehicle != 0) {

								this.MyVehicleID.Vehicle = this.CitizenVehicle;

								this.FavCimsLastActivityVehicleButton.isEnabled = true;

								this.VehInfo = this.MyVehicle.m_vehicles.m_buffer [this.CitizenVehicle].Info;

								this.CitizenVehicleName = this.MyVehicle.GetVehicleName (this.CitizenVehicle);

								if (this.VehInfo.m_class.m_service == ItemClass.Service.Residential) {
									//sta usando una macchina
									if (this.VehInfo.m_vehicleAI.GetOwnerID (this.CitizenVehicle, ref MyVehicle.m_vehicles.m_buffer [this.CitizenVehicle]).Citizen == citizen) {
										//sta usando la sua macchina.
										if (GoingOutside)
											LeaveCity = true;

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconCitizenVehicle";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristVehicle";
										
										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName;
									}

								} else if (this.VehInfo.m_class.m_service == ItemClass.Service.PublicTransport) {
									//sta usando un mezzo pubblico
									if (GoingOutside)
										LeaveCity = true;

									switch (this.VehInfo.m_class.m_subService)
									{
										case ItemClass.SubService.PublicTransportBus:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";
											this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Bus");

											break;

										case ItemClass.SubService.PublicTransportMetro:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";
											
											this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Metro");

											break;

										case ItemClass.SubService.PublicTransportPlane:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";
											
											this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Plane");

											break;
										case ItemClass.SubService.PublicTransportShip:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";
											
											this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Ship");

											break;
										case ItemClass.SubService.PublicTransportTrain:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";
											
											if (this.CitizenVehicleName == "VEHICLE_TITLE[Train Passenger]:0")
												this.CitizenVehicleName = Locale.Get ("VEHICLE_TITLE", "Train Engine");
											
											this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Train");

											break;
									}
									/*
									if (this.VehInfo.m_class.m_subService == ItemClass.SubService.PublicTransportBus) {

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";

										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Bus");

									} else if (this.VehInfo.m_class.m_subService == ItemClass.SubService.PublicTransportMetro) {

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";

										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Metro");

									} else if (this.VehInfo.m_class.m_subService == ItemClass.SubService.PublicTransportPlane) {

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";

										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Plane");

									} else if (this.VehInfo.m_class.m_subService == ItemClass.SubService.PublicTransportShip) {

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";

										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Ship");

									} else if (this.VehInfo.m_class.m_subService == ItemClass.SubService.PublicTransportTrain) {

										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";

										if (this.CitizenVehicleName == "VEHICLE_TITLE[Train Passenger]:0")
											this.CitizenVehicleName = Locale.Get ("VEHICLE_TITLE", "Train Engine");

										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get ("SUBSERVICE_DESC", "Train");
									}
									*/
								}
							} else {
								if (GoingOutside)
									LeaveCity = true;

								this.FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
								this.FavCimsLastActivityVehicleButton.isEnabled = false;
								this.FavCimsLastActivityVehicleButton.tooltip = FavCimsLang.text ("Vehicle_on_foot");
							}
						} else {

							this.FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
							this.FavCimsLastActivityVehicleButton.isEnabled = false;
							this.FavCimsLastActivityVehicleButton.tooltip = null;
						}

						//Citizen Status
						this.CitizenStatus = citizenInfo.m_citizenAI.GetLocalizedStatus (citizen, ref this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index], out this.MyTargetID);
						this.CitizenTarget = this.MyBuilding.GetBuildingName (this.MyTargetID.Building, this.MyInstanceID);
						this.FavCimsLastActivity.text = this.CitizenStatus + " " + this.CitizenTarget;

						if (!this.MyTargetID.IsEmpty) {

							//District
							this.TargetDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [MyTargetID.Index].m_position);
							
							if (this.TargetDistrict == 0) {
								this.FavCimsLastActivity.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
							} else {
								this.FavCimsLastActivity.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.TargetDistrict);
							}
						}

						//Now in this District
						this.CitizenDistrict = (int)MyDistrict.GetDistrict (citizenInstance.GetSmoothPosition (InstanceCitizenID));

						//Il carro funebre lo ha caricato
						//if (((citizen != 0) && MyCitizen.m_citizens.m_buffer[citizen].Dead) && (MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Moving)) {
						if(this.CitizenIsDead) {
							//try 
							//{
							this.FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
							this.FavCimsHappyIcon.tooltip = FavCimsLang.text ("People_Life_Status_Dead");
							
							if(this.CitizenRowData ["deathrealage"] == "0")
								this.CitizenRowData ["deathrealage"] = this.RealAge.ToString();
							
							this.FavCimsRealAge.text = this.CitizenRowData ["deathrealage"];
							
							if (this.DeathDate == null) {
								this.DeathDate = GameTime.FavCimsDate (FavCimsLang.text ("time_format"),"n/a");
								this.DeathTime = GameTime.FavCimsTime ();
							}
							this.FavCimsCitizenName.tooltip = 
								FavCimsLang.text ("People_Life_Status_Dead") + " " + 
									FavCimsLang.text ("People_Life_Status_Dead_date") + " " + this.DeathDate + " " + 
									FavCimsLang.text ("People_Life_Status_Dead_time") + " " + this.DeathTime;

							if(MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Moving) {
								this.hearse = this.MyCitizen.m_citizens.m_buffer [citizen].m_vehicle;
								
								if(hearse != 0) {
									this.CitizenDead.Citizen = citizen;
									this.MyVehicleID.Vehicle = hearse;
									
									this.FavCimsLastActivityVehicleButton.normalBgSprite = "NotificationIconVerySick";
									this.FavCimsLastActivityVehicleButton.isEnabled = true;
									this.FavCimsLastActivityVehicleButton.playAudioEvents = true;
									this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Hearse");
									this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_on_hearse");
								}
							} else if(MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation != Citizen.Location.Moving && this.hearse == 0) { //Verificare con unspawn
								//aspetta il carro funebre
								this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_wait_hearse");
								this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
								this.FavCimsLastActivityVehicleButton.isEnabled = false;
							} else {
								//lo stanno seppellendo
								this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_hisfuneral");
								this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconVerySick";
								this.FavCimsLastActivityVehicleButton.isEnabled = false;
							}
							//}catch (Exception e) {
							//	Debug.Log("error + " + e.ToString());
							//}
						}

					} else {
						if(this.rowLang == null || this.rowLang != FavCimsLang.GameLanguage)
							this.DeadOrGone = false;

						if (!this.DeadOrGone) {
							this.rowLang = FavCimsLang.GameLanguage;
							this.DeadOrGone = true;

							if (this.FavCimsCitizenSingleRowPanel != null && FavCimsCore.RowID.ContainsKey (citizenINT) && this.MyInstancedName.Length > 0) { 

								if(this.DeathDate != null) {
									this.DeathDate = GameTime.FavCimsDate (FavCimsLang.text ("time_format"), this.DeathDate);
								}

								if (this.DeathDate == null) {
									this.DeathDate = GameTime.FavCimsDate (FavCimsLang.text ("time_format"),"n/a");
									this.DeathTime = GameTime.FavCimsTime ();
								}

								//if(this.citflags != null)
									//Debug.Log(this.citflags);

								//Debug.Log(this.FavCimsCitizenName.text + " is dead on date " + this.DeathDate + " at time " + this.DeathTime);

								this.FavCimsCitizenName.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
								this.FavCimsCitizenName.isEnabled = false;
								this.FavCimsEducation.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
								this.FavCimsRealAge.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a
								this.FavCimsAgePhase.textColor = new Color32 (51, 51, 51, 160); //r,g,b,a

								if (!LeaveCity && (this.CitizenIsDead || this.RealAge > 65)) { //Dead Peoples (this.CitizenDead == this.MyInstanceID || this.RealAge > 65)

									try
									{
										//Citizen Life Status
										//this.CitizenRowData ["lifestatus"] = "People_Life_Status_Dead";

										//Happiness x.x
										//this.CitizenRowData ["happiness_icon"] = "NotificationIconDead";
										this.FavCimsHappyIcon.normalBgSprite = "NotificationIconDead";
										this.FavCimsHappyIcon.tooltip = FavCimsLang.text ("People_Life_Status_Dead");

										//Name
										//this.CitizenRowData ["name"] = this.MyInstancedName;
										this.FavCimsCitizenName.text = this.MyInstancedName;
										this.FavCimsCitizenName.tooltip = 
											FavCimsLang.text ("People_Life_Status_Dead") + " " + 
											FavCimsLang.text ("People_Life_Status_Dead_date") + " " + this.DeathDate + " " + 
											FavCimsLang.text ("People_Life_Status_Dead_time") + " " + this.DeathTime;

										//Info Button
										this.OtherInfoButton.isEnabled = false;

										//AgePhase
										//this.CitizenRowData ["agegroup"] = "Dead";
										this.FavCimsAgePhase.text = FavCimsLang.text ("AgePhaseDead_" + this.CitizenRowData ["gender"]);

										//Home
										this.FavCimsCitizenHome.text = FavCimsLang.text ("Home_Location_Dead");
										this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTextureDead;
										this.FavCimsCitizenHome.tooltip = null;
										this.FavCimsCitizenHome.isEnabled = false;
										this.FavCimsCitizenResidentialLevelSprite.texture = null;
										this.FavCimsCitizenHomeSprite.tooltip = null;

										//Working Place
										this.FavCimsWorkingPlace.isEnabled = false;
										this.FavCimsWorkingPlace.tooltip = null;
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
										this.FavCimsWorkingPlaceSprite.tooltip = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
										
										//Last Activity
										this.FavCimsLastActivity.isEnabled = false;
										this.FavCimsLastActivityVehicleButton.isEnabled = false;
										this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
										this.FavCimsLastActivityVehicleButton.tooltip = null;
										this.FavCimsLastActivity.tooltip = null;
										this.FavCimsLastActivity.text = FavCimsLang.text("Citizen_buried");

										this.CitizenRowData.Clear ();
									}catch (Exception e) {
										Debug.Error("error " + e.ToString());
									}

								} else {

									//Citizen Life Status
									//this.CitizenRowData ["lifestatus"] = "People_Life_Status_IsGone";
									
									//Happiness ==>
									//this.CitizenRowData ["happiness_icon"] = "";
									this.FavCimsHappyIcon.normalBgSprite = "";
									this.FavCimsHappyIcon.tooltip = null;

									//Happiness Icon Override (For leaving people)
									this.FavCimsHappyOverride.texture = TextureDB.FavCimsHappyOverride_texture;
									this.FavCimsHappyOverride.relativePosition = new Vector3 (0, 0);
									this.FavCimsHappyOverride.tooltip = FavCimsLang.text ("People_Life_Status_IsGone");

									//Name
									//this.CitizenRowData ["name"] = this.MyInstancedName;
									this.FavCimsCitizenName.text = this.MyInstancedName;
									this.FavCimsCitizenName.tooltip = 
										FavCimsLang.text ("People_Life_Status_IsGone") + " " + 
										FavCimsLang.text ("People_Life_Status_Dead_date") + " " + this.DeathDate + " " + 
										FavCimsLang.text ("People_Life_Status_Dead_time") + " " + this.DeathTime;

									//Info Button
									this.OtherInfoButton.isEnabled = false;

									//Home
									this.FavCimsCitizenHome.text = FavCimsLang.text ("HomeOutsideTheCity");
									this.FavCimsCitizenHomeSprite.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
									this.FavCimsCitizenHome.tooltip = null;
									this.FavCimsCitizenHome.isEnabled = false;
									this.FavCimsCitizenResidentialLevelSprite.texture = null;
									this.FavCimsCitizenHomeSprite.tooltip = null;

									//Working Place
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsWorkingPlace.tooltip = null;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;

									//Last Activity
									//Ha lasciato la citta'.
									this.FavCimsLastActivity.isEnabled = false;
									this.FavCimsLastActivityVehicleButton.isEnabled = false;
									this.FavCimsLastActivityVehicleButton.disabledBgSprite = "NotificationIconDead";
									this.FavCimsLastActivity.tooltip = null;
									 
									this.CitizenRowData.Clear ();
								}

							} else { 
							
								try {
									if(this.MyFamily != null) {
										this.MyFamily.Hide();
										this.MyFamily.MyInstanceID = InstanceID.Empty;
										this.MyFamily = null;
									}
									GameObject.Destroy (this.gameObject);
								} catch { /*(Exception e)*/
									//Debug.Log ("CitizenName Error Destroy Object" + e.ToString ());
								}
							}
						}
					}
				} catch { /*(Exception e)*/
					//Debug.Error ("Update Row Error" + e.ToString ());
				}
			}
			return;
		}
	}
}
