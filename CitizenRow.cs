using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using UnityEngine;
using System.Threading;

namespace FavoriteCims
{
	public static class MyStringExtensions
	{
		public static bool Like(this string toSearch, string toFind)
		{
			return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
		}
	}

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

		private UIButton FavCimsCitizenHomeButton;
		private UIPanel FavCimsWorkingPlacePanel;

		private UITextureSprite FavCimsWorkingPlaceSprite;
		private UIButton FavCimsWorkingPlace;
		private UIPanel FavCimsLastActivityPanel;
		private UIButton FavCimsLastActivity;
		private UIButton FavCimsLastActivityVehicleButton;
		private UIPanel FavCimsCloseRowPanel;

		private UITextureSprite FavCimsCitizenResidentialLevelSprite;
		private UITextureSprite FavCimsCitizenWorkPlaceLevelSprite;

		private UIButton FavCimsWorkingPlaceButton;

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
		//string VehicleStatus;
		//string VehicleTarget;

		ushort CitizenHome;
		ushort CitizenVehicle;
		ushort WorkPlace;
		ushort InstanceCitizenID;
	
		bool GoingOutside;
		bool LeaveCity = false;
		bool DeadOrGone = false;
		bool HomeLess = false;
		bool CitizenIsDead = false;
        bool isStudent = false;

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
		ushort policeveh;

		internal static int GetTemplate() {

			for (int i = 0; i < FavCimsMainClass.MaxTemplates; i++) {

				if(FavCimsMainClass.Templates[i].MyInstanceID.IsEmpty) {
					return i;
				}

			}

			return -1;
		}
        /*
        internal void getCimOccupation(Citizen.Education educationLevel, ushort workplaceID, uint citizenID, Citizen.Gender gender, out string CimOccupation)
        {

            CimOccupation = string.Empty;

            System.Random rnd = new System.Random();
            int num = rnd.Next(1, 5);

            switch (educationLevel)
            {
                case Citizen.Education.Uneducated:
                    CimOccupation = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", num.ToString());
                    break;
                case Citizen.Education.OneSchool:
                    CimOccupation = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", num.ToString());
                    break;
                case Citizen.Education.TwoSchools:
                    CimOccupation = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", num.ToString());
                    break;
                case Citizen.Education.ThreeSchools:
                    CimOccupation = Locale.Get(gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", num.ToString());
                    break;
            }
        }
        */
        public void GoToCitizen(InstanceID Target, UIMouseEventParameter eventParam)
		{
			if (Target.IsEmpty)
				return;

			try {
				if (MyInstance.SelectInstance (Target)) {
					if (eventParam.buttons == UIMouseButton.Middle) {

						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						if(this.citizenInfo.m_class.m_service == ItemClass.Service.Tourism) {
							WorldInfoPanel.Show<TouristWorldInfoPanel> (this.position, Target);
						} else {
							WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
						}

					} else if (eventParam.buttons == UIMouseButton.Right) {

						FavCimsMainClass.FavCimsPanel.Hide ();
						ToolsModifierControl.cameraController.SetTarget (Target, ToolsModifierControl.cameraController.transform.position, true);
						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						if(this.citizenInfo.m_class.m_service == ItemClass.Service.Tourism) {
							WorldInfoPanel.Show<TouristWorldInfoPanel> (this.position, Target);
						} else {
							WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
						}

					} else {

						ToolsModifierControl.cameraController.SetTarget (Target, ToolsModifierControl.cameraController.transform.position, true);
						//DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
						if(this.citizenInfo.m_class.m_service == ItemClass.Service.Tourism) {
							WorldInfoPanel.Show<TouristWorldInfoPanel> (this.position, Target);
						} else {
							WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
						}
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
				if (p.buttons == UIMouseButton.Middle) {

					//DefaultTool.OpenWorldInfoPanel(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position);
					//ToolsModifierControl.cameraController.SetTarget(this.CitizenHomeID, ToolsModifierControl.cameraController.transform.position, false);
					WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.CitizenHomeID);

				} else if (p.buttons == UIMouseButton.Right) {
					
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
				Debug.Error("Can't find the House " + e.ToString());
			}
		}

		private void GoToWork(UIComponent component, UIMouseEventParameter p)
		{

			if (this.WorkPlaceID.IsEmpty)
				return;

			try {	
				if (p.buttons == UIMouseButton.Middle) {
					
					//ToolsModifierControl.cameraController.SetTarget(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position, false);
					DefaultTool.OpenWorldInfoPanel(this.WorkPlaceID, ToolsModifierControl.cameraController.transform.position);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.WorkPlaceID);
					
				} else if (p.buttons == UIMouseButton.Right) {
					
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
				if (p.buttons == UIMouseButton.Middle) {

					//ToolsModifierControl.cameraController.SetTarget(this.MyTargetID, ToolsModifierControl.cameraController.transform.position, false);
					//WorldInfoPanel.Show<ZonedBuildingWorldInfoPanel> (this.position, this.MyTargetID);
					DefaultTool.OpenWorldInfoPanel(this.MyTargetID, ToolsModifierControl.cameraController.transform.position);

				} else if (p.buttons == UIMouseButton.Right) {
					
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
				if (p.buttons == UIMouseButton.Middle) {
					
					DefaultTool.OpenWorldInfoPanel(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position);
					//ToolsModifierControl.cameraController.SetTarget(this.MyVehicleID, ToolsModifierControl.cameraController.transform.position, false);
					//WorldInfoPanel.Show<CitizenVehicleWorldInfoPanel> (this.position, this.MyVehicleID);
					
				} else if (p.buttons == UIMouseButton.Right) {
					
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
					this.FavCimsCitizenName.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(MyInstanceID, eventParam);

					this.FavCimsNameColText_EmptySprite = this.FavCimsCitizenNamePanel.AddUIComponent<UITextureSprite> ();
					this.FavCimsNameColText_EmptySprite.width = this.FavCimsCitizenName.width;
					this.FavCimsNameColText_EmptySprite.height = this.FavCimsCitizenName.height;
					this.FavCimsNameColText_EmptySprite.relativePosition = new Vector3 (0, 0);

					if(FavoriteCimsMainPanel.ColumnSpecialBackground == true) {
						this.FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
						this.FavCimsNameColText_EmptySprite.opacity = 0.7f;
					}

					FavoriteCimsMainPanel.FavCimsNameColText.eventClick += delegate {
						
						if (this.FavCimsNameColText_EmptySprite.texture == null) {
							this.FavCimsNameColText_EmptySprite.texture = TextureDB.FavCimsNameBgOverride_texture;
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
					this.FavCimsCitizenHome.eventMouseUp += new MouseEventHandler(this.GoToHome);

					//Printing
					this.FavCimsCitizenHome.relativePosition = new Vector3 (0, 0);

					//Home sprites

					this.FavCimsCitizenHomeButton = this.FavCimsCitizenHomePanel.AddUIComponent<UIButton> ();
					this.FavCimsCitizenHomeButton.name = "FavCimsCitizenHomeButton";
					this.FavCimsCitizenHomeButton.atlas = MyAtlas.FavCimsAtlas;
					this.FavCimsCitizenHomeButton.size = new Vector2(20,40);
					this.FavCimsCitizenHomeButton.relativePosition = new Vector3 (10, 0);
					this.FavCimsCitizenHomeButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

					this.FavCimsCitizenResidentialLevelSprite = this.FavCimsCitizenHomeButton.AddUIComponent<UITextureSprite> ();
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
					this.FavCimsWorkingPlace.eventMouseUp += new MouseEventHandler(this.GoToWork);
					
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
					this.FavCimsWorkingPlaceButton = this.FavCimsWorkingPlaceSprite.AddUIComponent<UIButton> ();
					this.FavCimsWorkingPlaceButton.name = "FavCimsWorkingPlaceButton";
					this.FavCimsWorkingPlaceButton.width = 20;
					this.FavCimsWorkingPlaceButton.height = 20;
					this.FavCimsWorkingPlaceButton.relativePosition = new Vector3 (0, 10);
					this.FavCimsWorkingPlaceButton.isInteractive = false;
					this.FavCimsWorkingPlaceButton.tooltipBox = UIView.GetAView().defaultTooltipBox;

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
					this.FavCimsLastActivity.font.size = 14;
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
					this.FavCimsLastActivity.eventMouseUp += new MouseEventHandler(this.GoToTarget);
					
					//Printing
					this.FavCimsLastActivity.relativePosition = new Vector3 (40, 0);

					//Last Activity Button
					this.FavCimsLastActivityVehicleButton = this.FavCimsLastActivityPanel.AddUIComponent<UIButton> ();
					this.FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
					this.FavCimsLastActivityVehicleButton.width = 26;
					this.FavCimsLastActivityVehicleButton.height = 26;
					this.FavCimsLastActivityVehicleButton.relativePosition = new Vector3 (5, 7);
					this.FavCimsLastActivityVehicleButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
					this.FavCimsLastActivityVehicleButton.eventMouseUp += new MouseEventHandler(this.GoToVehicle);

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
			//uint vehicle = this.MyInstanceID.Vehicle;

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
                        var tmp_gender = Citizen.GetGender(citizen);
                        this.CitizenRowData ["gender"] = tmp_gender.ToString ();
						
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
							this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
							this.HomeInfo = this.MyBuilding.m_buildings.m_buffer [CitizenHomeID.Index].Info;

							if (this.HomeInfo.m_class.m_service == ItemClass.Service.Residential) {

								this.FavCimsCitizenHome.tooltip = null;

								if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
									this.FavCimsCitizenHome.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialHigh.ToString ());
								} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHighEco) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconHigh";
									this.FavCimsCitizenHome.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialHigh.ToString ()) + " Eco";
								} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLowEco) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 153, 0, 80); //r,g,b,a
									this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
									this.FavCimsCitizenHome.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialLow.ToString ()) + " Eco";
								} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow) {
									this.FavCimsCitizenHome.textColor = new Color32 (0, 153, 0, 80); //r,g,b,a
									this.FavCimsCitizenHomeButton.normalBgSprite = "homeIconLow";
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
									this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
								} else {
									this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.HomeDistrict);
								}
							}
						} else {

							this.FavCimsCitizenHome.text = FavCimsLang.text ("Citizen_HomeLess");
							this.FavCimsCitizenHome.isEnabled = false;
							this.FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
							this.FavCimsCitizenHomeButton.tooltip = FavCimsLang.text ("DistrictNameNoDistrict");
							this.FavCimsCitizenHome.tooltip = FavCimsLang.text ("Citizen_HomeLess_tooltip");
							this.FavCimsCitizenResidentialLevelSprite.texture = null;
							this.HomeLess = true;
						}

						//Working Place
						this.WorkPlace = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_workBuilding;
						if (this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen) != ItemClass.Level.None) {
                            this.isStudent = true;
                            this.FavCimsWorkingPlaceButton.normalBgSprite = null;
							this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
							this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
							this.FavCimsWorkingPlace.tooltip = Locale.Get ("CITIZEN_SCHOOL_LEVEL", this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen).ToString ()) + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
						} else if (this.WorkPlace == 0) {
							this.FavCimsWorkingPlaceButton.normalBgSprite = null;

                            if ((this.MyCitizen.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                            {

                                string str = string.Empty;

                                if (SteamHelper.IsDLCOwned(SteamHelper.DLC.CampusDLC))
                                {
                                    float num = Singleton<ImmaterialResourceManager>.instance.CheckExchangeStudentAttractivenessBonus() * 100f;
                                    Randomizer m_randomizer = new Randomizer(citizen);
                                    str = (double)m_randomizer.Int32(0, 100) >= (double)num ? Locale.Get("CITIZEN_OCCUPATION_TOURIST") : Locale.Get("CITIZEN_OCCUPATION_EXCHANGESTUDENT");
                                }
                                else
                                    str = Locale.Get("CITIZEN_OCCUPATION_TOURIST");
                            //}

                            //if (this.citizenInfo.m_class.m_service == ItemClass.Service.Tourism) {

								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture; //unemployed
								this.FavCimsWorkingPlace.text = str;
								this.FavCimsWorkingPlace.isEnabled = false;
								this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Citizen_Tourist_tooltip");
								this.FavCimsWorkingPlaceSprite.tooltip = null;
								this.FavCimsWorkingPlaceButton.tooltip = null;
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;

							} else {
								if (this.tmp_age >= 180) {
									//In Pensione
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
									this.FavCimsWorkingPlace.text = FavCimsLang.text ("Citizen_Retired");
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Citizen_Retired_tooltip");
									this.FavCimsWorkingPlaceSprite.tooltip = null;
									this.FavCimsWorkingPlaceButton.tooltip = null;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								} else { 
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture; //unemployed
									this.FavCimsWorkingPlace.text = Locale.Get ("CITIZEN_OCCUPATION_UNEMPLOYED");
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Unemployed_tooltip");
									this.FavCimsWorkingPlaceSprite.tooltip = null;
									this.FavCimsWorkingPlaceButton.tooltip = null;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								}
							}
						}

						if (this.WorkPlace != 0) {

                            string str = string.Empty;
                            if(!isStudent)
                            {
                                CommonBuildingAI buildingAi = this.MyBuilding.m_buildings.m_buffer[(int)this.WorkPlace].Info.m_buildingAI as CommonBuildingAI;

                                if (buildingAi != null)
                                    str = buildingAi.GetTitle(tmp_gender, tmp_education, this.WorkPlace, citizen);
                                if (str == string.Empty)
                                {
                                    int num = new Randomizer((uint)this.WorkPlace + citizen).Int32(1, 5);
                                    switch (tmp_education)
                                    {
                                        case Citizen.Education.Uneducated:
                                            str = Locale.Get(tmp_gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_UNEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                            break;
                                        case Citizen.Education.OneSchool:
                                            str = Locale.Get(tmp_gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_EDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_EDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                            break;
                                        case Citizen.Education.TwoSchools:
                                            str = Locale.Get(tmp_gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_WELLEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                            break;
                                        case Citizen.Education.ThreeSchools:
                                            str = Locale.Get(tmp_gender != Citizen.Gender.Female ? "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED" : "CITIZEN_OCCUPATION_PROFESSION_HIGHLYEDUCATED_FEMALE", num.ToString()) + " " + Locale.Get("CITIZEN_OCCUPATION_LOCATIONPREPOSITION");
                                            break;
                                    }
                                }
                            }

                            this.WorkPlaceID.Building = this.WorkPlace;
							this.FavCimsWorkingPlace.text = str + " " + this.MyBuilding.GetBuildingName (this.WorkPlace, this.MyInstanceID);
							this.FavCimsWorkingPlace.isEnabled = true;
							this.WorkInfo = this.MyBuilding.m_buildings.m_buffer [WorkPlaceID.Index].Info;

                            this.FavCimsWorkingPlaceSprite.texture = null;

							if (this.WorkInfo.m_class.m_service == ItemClass.Service.Commercial) {

								this.FavCimsWorkingPlaceButton.normalBgSprite = null;

								if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh) {
									this.FavCimsWorkingPlace.textColor = new Color32 (0, 51, 153, 147); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.CommercialHigh.ToString ());
								
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialEco) {

									this.FavCimsWorkingPlace.textColor = new Color32 (0, 150, 136, 116); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Buildings_Type_CommercialEco");

								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLeisure) {

									this.FavCimsWorkingPlace.textColor = new Color32 (219, 68, 55, 3); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Buildings_Type_CommercialLeisure");

								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialTourist) {

									this.FavCimsWorkingPlace.textColor = new Color32 (156, 39, 176, 194); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialHighTexture;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Buildings_Type_CommercialTourist");

								} else {
									this.FavCimsWorkingPlace.textColor = new Color32 (0, 153, 204, 130); //r,g,b,a
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenCommercialLowTexture;
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.CommercialLow.ToString ());
								}

								if(this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh || this.WorkInfo.m_class.m_subService == ItemClass.SubService.CommercialLow) {
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
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
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
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
										break;
                                    case ItemClass.SubService.PlayerIndustryFarming: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
                                        break;
                                    case ItemClass.SubService.IndustrialForestry:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
										break;
                                    case ItemClass.SubService.PlayerIndustryForestry: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
                                        break;
                                    case ItemClass.SubService.IndustrialOil:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
										break;
                                    case ItemClass.SubService.PlayerIndustryOil: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
                                        break;
                                    case ItemClass.SubService.IndustrialOre:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
										break;
                                    case ItemClass.SubService.PlayerIndustryOre: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
                                        break;
                                    default:
										this.FavCimsWorkingPlaceButton.normalBgSprite = null;
										this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
										break;
								}
								/*
								if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialFarming) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFarming";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialForestry) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButton.normalBgSprite = "ResourceForestry";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialOil) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOil";
								} else if (this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialOre) {
									this.FavCimsWorkingPlaceSprite.texture = null;
									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyOre";
								} else {
									this.FavCimsWorkingPlaceButton.normalBgSprite = null;
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
								}
								*/

								if(this.WorkInfo.m_class.m_subService == ItemClass.SubService.IndustrialGeneric) {
									switch (this.WorkInfo.m_class.m_level)
									{
										case ItemClass.Level.Level3:
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [3];
											break;
										case ItemClass.Level.Level2:
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [2];
											break;
										default:
											this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsIndustrialLevel [1];
											break;
									}
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
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
								this.FavCimsWorkingPlaceButton.normalBgSprite = null;
								this.FavCimsWorkingPlace.textColor = new Color32 (0, 204, 255, 128); //r,g,b,a
								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenOfficeTexture;

								switch (this.WorkInfo.m_class.m_subService) 
								{
									case ItemClass.SubService.OfficeHightech:
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", "Office") + " Eco";
									break;

									default:
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", "Office");
									break;
								}

								switch (this.WorkInfo.m_class.m_level) 
								{
									case ItemClass.Level.Level3:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel [2];
										break;
									default:
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = TextureDB.FavCimsOfficeLevel [1];
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
										this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconFireSafety";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "FireDepartment");
										break;
									case ItemClass.Service.HealthCare:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconHealthcareFocused";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Healthcare");
										break;
									case ItemClass.Service.PoliceDepartment:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconPolice";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Police");
										break;
									case ItemClass.Service.Garbage:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyRecycling";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Garbage");
										break;
									case ItemClass.Service.Electricity:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyPowerSaving";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Electricity_job");
										break;
									case ItemClass.Service.Education:
										this.FavCimsWorkingPlace.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
										this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconEducationPressed";
										break;
									case ItemClass.Service.Beautification:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarBeautificationParksnPlazas";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Beautification");
										break;
                                    //case ItemClass.Service.Government:
                                    //this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconGovernment";
                                    //this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Government_job");
                                    //break;
                                    case ItemClass.Service.Water:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyWaterSaving";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Water_job");
										break;
									case ItemClass.Service.PublicTransport:

                                        switch(this.WorkInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.PublicTransportPost:
                                                this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarPublicTransportPost";
                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                                break;

                                            default:
                                                this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFreePublicTransport";
                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                                break;
                                        }
										
										break;
									case ItemClass.Service.Monument:
										this.FavCimsWorkingPlaceButton.normalBgSprite = "FeatureMonumentLevel6";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Monuments");
										break;
                                    case ItemClass.Service.Fishing: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarIndustryFishing";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                        break;
                                    case ItemClass.Service.Disaster: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarFireDepartmentDisaster";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                        break;
                                    case ItemClass.Service.Museums: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaMuseums";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                        break;
                                    case ItemClass.Service.VarsitySports: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                        break;
                                    default:
										this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
										this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyNone";
										this.FavCimsWorkingPlace.tooltip = null;
										break;
								}

								/*
								if (this.WorkInfo.m_class.m_service == ItemClass.Service.FireDepartment) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconFireSafety";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "FireDepartment");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.HealthCare) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconHealthcareFocused";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Healthcare");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.PoliceDepartment) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconPolice";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Police");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Garbage) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyRecycling";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Garbage");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Electricity) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyPowerSaving";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Electricity_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Education) {

									this.FavCimsWorkingPlace.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									this.FavCimsWorkingPlaceButton.normalBgSprite = "InfoIconEducationPressed";

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Beautification) { //parchi

									this.FavCimsWorkingPlaceButton.normalBgSprite = "SubBarBeautificationParksnPlazas";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Beautification");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Government) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "ToolbarIconGovernment";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Government_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Water) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyWaterSaving";
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Water_job");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.PublicTransport) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyFreePublicTransport";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "PublicTransport");

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Monument) {

									this.FavCimsWorkingPlaceButton.normalBgSprite = "FeatureMonumentLevel6";
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Monuments");
								
								} else if (this.WorkInfo.m_class.m_service != ItemClass.Service.None) {
									
									this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
									this.FavCimsWorkingPlaceButton.normalBgSprite = "IconPolicyNone";
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
							this.FavCimsWorkingPlaceButton.tooltip = null;
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

								/*FIX for Train Passenger Warning (Game Bug?) -> Update 29/03/2020, Maybe is fixed.*/
								/*
								[Warning] : Error with VEHICLE_TITLE[Train Passenger]:0: 
								The id was not found in the localization files.
								[Localization - Internal] ()
								*/
								string name_key = PrefabCollection<VehicleInfo>.PrefabName((uint) this.VehInfo.m_prefabDataIndex);
								if(name_key == "Train Passenger") { // Train Passenger key not exists on locale files!
									this.CitizenVehicleName = Locale.Get ("VEHICLE_TITLE", "Train Engine"); 
								} else {
									this.CitizenVehicleName = this.MyVehicle.GetVehicleName (this.CitizenVehicle);
								}

								if (this.VehInfo.m_class.m_service == ItemClass.Service.Residential) {

                                    if (this.CitizenVehicleName.Like("Bicycle"))
                                    {
                                        //sta usando una bicicletta
                                        this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                        this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconTouristBicycleVehicle";
                                        this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristBicycleVehicle";
                                        this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("PROPS_DESC", "bicycle01");
                                    }
                                    else if(this.CitizenVehicleName.Like("Scooter")) //New code 0.3.x
                                    {
                                        //sta usando uno scooter
                                        this.FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
                                        this.FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsIconScooter";
                                        this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "FavCimsIconScooter";
                                        this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName;
                                    }
                                    else
                                    {
                                        //sta usando una macchina
                                        this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
                                        this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconCitizenVehicle";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconTouristVehicle";
										this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName;
									}

									if (this.VehInfo.m_vehicleAI.GetOwnerID (this.CitizenVehicle, ref MyVehicle.m_vehicles.m_buffer [this.CitizenVehicle]).Citizen == citizen) {
										//sta usando la sua macchina/scooter/bicicletta.
										if (GoingOutside)
											LeaveCity = true;
									}

								} else if (this.VehInfo.m_class.m_service == ItemClass.Service.PublicTransport) {

                                    this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;

                                    //sta usando un mezzo pubblico
                                    if (GoingOutside)
										LeaveCity = true;

									switch (this.VehInfo.m_class.m_subService)
									{
										case ItemClass.SubService.PublicTransportCableCar:
										
											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportCableCar";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get ("SUBSERVICE_DESC", "CableCar");

										break;

										case ItemClass.SubService.PublicTransportMonorail:
											
										this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMonorail";
										this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
										this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
										this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
										this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get ("SUBSERVICE_DESC", "Monorail");

										break;

										case ItemClass.SubService.PublicTransportTaxi:
											
											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTaxi";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get ("SUBSERVICE_DESC", "Taxi");

										break;

										case ItemClass.SubService.PublicTransportTram:
											
											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTram";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTramHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTramFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTramPressed";
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Tram") + " - " + Locale.Get ("SUBSERVICE_DESC", "Tram");

										break;

										case ItemClass.SubService.PublicTransportBus:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportBus";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportBusHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportBusFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportBusPressed";
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Bus") + " - " + Locale.Get ("SUBSERVICE_DESC", "Bus");

											break;

										case ItemClass.SubService.PublicTransportMetro:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportMetro";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportMetroFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportMetroPressed";
											
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Metro") + " - " + Locale.Get ("SUBSERVICE_DESC", "Metro");

											break;

										case ItemClass.SubService.PublicTransportPlane:
											
											if(this.CitizenVehicleName.Like("Blimp")) {
												
												this.FavCimsLastActivityVehicleButton.normalBgSprite = "IconPolicyEducationalBlimps";
												this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
												this.FavCimsLastActivityVehicleButton.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
												this.FavCimsLastActivityVehicleButton.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";

												this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get ("FEATURES_DESC", "Blimp");

											} else {

												this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPlane";
												this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
												this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
												this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPlanePressed";

												this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get ("SUBSERVICE_DESC", "Plane");

											}
											
											break;
								
										case ItemClass.SubService.PublicTransportShip:

											if(this.CitizenVehicleName.Like("Ferry")) {

												this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
												this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
												this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
												this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";

												this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get ("FEATURES_DESC", "Ferry");

											} else {
											
												this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportShip";
												this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportShipHovered";
												this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportShipFocused";
												this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportShipPressed";

												this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get ("SUBSERVICE_DESC", "Ship");

											}


											break;
										case ItemClass.SubService.PublicTransportTrain:

											this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrain";
											this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
											this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrainFocused";
											this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrainPressed";
																						
											this.FavCimsLastActivityVehicleButton.tooltip = Locale.Get ("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get ("SUBSERVICE_DESC", "Train");

											break;

                                        case ItemClass.SubService.PublicTransportPost: //New code 0.3.x

                                            this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportPost";
                                            this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportPostHovered";
                                            this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportPostFocused";
                                            this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportPostPressed";

                                            this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Post");

                                            break;
                                        case ItemClass.SubService.PublicTransportTours: //New code 0.3.x

                                            this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTours";
                                            this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportToursHovered";
                                            this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportToursFocused";
                                            this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportToursPressed";

                                            this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Tours");

                                            break;
                                        case ItemClass.SubService.PublicTransportTrolleybus:  //New code 0.3.x

                                            this.FavCimsLastActivityVehicleButton.normalBgSprite = "SubBarPublicTransportTrolleybus";
                                            this.FavCimsLastActivityVehicleButton.hoveredBgSprite = "SubBarPublicTransportTrolleybusHovered";
                                            this.FavCimsLastActivityVehicleButton.focusedBgSprite = "SubBarPublicTransportTrolleybusFocused";
                                            this.FavCimsLastActivityVehicleButton.pressedBgSprite = "SubBarPublicTransportTrolleybusPressed";

                                            this.FavCimsLastActivityVehicleButton.tooltip = this.CitizenVehicleName + " - " + Locale.Get("SUBSERVICE_DESC", "Trolleybus");
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

                                this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;

                                //Sta andando a piedi.
                                if (GoingOutside)
									LeaveCity = true;

								this.FavCimsLastActivityVehicleButton.disabledBgSprite = "InfoIconPopulationDisabled";
								this.FavCimsLastActivityVehicleButton.isEnabled = false;
								this.FavCimsLastActivityVehicleButton.tooltip = FavCimsLang.text ("Vehicle_on_foot");
							}
						} else {

                            this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;

                            //è fermo o non possiede un veicolo.
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

						//Gestione Citizen Criminali o in Prigione.
						if(this.MyCitizen.m_citizens.m_buffer[(int)(IntPtr) citizen].Arrested && this.MyCitizen.m_citizens.m_buffer[(int)(IntPtr) citizen].Criminal) {
							this.FavCimsHappyIcon.atlas = MyAtlas.FavCimsAtlas;
							this.FavCimsHappyIcon.normalBgSprite = "FavCimsCrimeArrested";
							this.FavCimsHappyIcon.tooltip = FavCimsLang.text ("Citizen_Arrested");

							if(MyCitizen.m_citizens.m_buffer[citizen].CurrentLocation == Citizen.Location.Moving) {

								this.policeveh = this.MyCitizen.m_citizens.m_buffer [citizen].m_vehicle;

								if(policeveh != 0) {
									this.MyVehicleID.Vehicle = policeveh;

									this.FavCimsLastActivityVehicleButton.atlas = MyAtlas.FavCimsAtlas;
									this.FavCimsLastActivityVehicleButton.normalBgSprite = "FavCimsPoliceVehicle";
									this.FavCimsLastActivityVehicleButton.isEnabled = true;
									this.FavCimsLastActivityVehicleButton.playAudioEvents = true;
									this.FavCimsLastActivityVehicleButton.tooltip = this.MyVehicle.GetVehicleName (policeveh) + " - " + Locale.Get ("VEHICLE_STATUS_PRISON_RETURN");

									this.FavCimsLastActivity.isEnabled = false;
									this.FavCimsLastActivity.text = FavCimsLang.text ("Transported_to_Prison");
								}
							} else {
								this.FavCimsLastActivity.isEnabled = true;
								this.FavCimsLastActivity.text = FavCimsLang.text ("Jailed_into") + this.CitizenTarget;
								this.FavCimsLastActivityVehicleButton.atlas = UIView.GetAView().defaultAtlas;
							}
						} else {
							this.FavCimsHappyIcon.atlas = UIView.GetAView().defaultAtlas;
							this.FavCimsHappyIcon.normalBgSprite = this.CitizenRowData ["happiness_icon"];
							this.FavCimsHappyIcon.tooltip = FavCimsLang.text (this.CitizenRowData ["happiness_icon"]);
						}

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

								//Randomize output for instances deleted by the game in simultation mode.
								System.Random r = new System.Random();
								int rInt = r.Next(0, 100);
								string tmp_status;

								if(this.RealAge >= 85) {
									if(rInt >= 99) {
										tmp_status = "goneaway";
									}else{
										tmp_status = "dead";
									}
								} else if(this.RealAge >= 65) {
									if(rInt >= 70) {
										tmp_status = "goneaway";
									}else{
										tmp_status = "dead";
									}
								} else if (this.RealAge >= 45) {
									if(rInt >= 50) {
										tmp_status = "goneaway";
									}else{
										tmp_status = "dead";
									}
								} else if (this.RealAge >= 20) {
									if(rInt >= 30) {
										tmp_status = "goneaway";
									}else{
										tmp_status = "dead";
									}
								} else {
									if(rInt >= 2) {
										tmp_status = "goneaway";
									}else{
										tmp_status = "dead";
									}
								}

								if (!LeaveCity && (this.CitizenIsDead || tmp_status == "dead")) { 

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
										this.FavCimsCitizenHomeButton.normalBgSprite = "houseofthedead";
										this.FavCimsCitizenHome.tooltip = null;
										this.FavCimsCitizenHome.isEnabled = false;
										this.FavCimsCitizenResidentialLevelSprite.texture = null;
										this.FavCimsCitizenHomeButton.tooltip = null;

										//Working Place
										this.FavCimsWorkingPlace.isEnabled = false;
										this.FavCimsWorkingPlace.tooltip = null;
										this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
										this.FavCimsWorkingPlaceSprite.tooltip = null;
										this.FavCimsWorkingPlaceButton.tooltip = null;
										
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
									this.FavCimsCitizenHomeButton.normalBgSprite = "homelessIcon";
									this.FavCimsCitizenHome.tooltip = null;
									this.FavCimsCitizenHome.isEnabled = false;
									this.FavCimsCitizenResidentialLevelSprite.texture = null;
									this.FavCimsCitizenHomeButton.tooltip = null;

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