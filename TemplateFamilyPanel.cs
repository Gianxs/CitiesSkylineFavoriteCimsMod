using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
{
	public class FamilyPanelTemplate : UIPanel
	{
		private const float Run = 0.3f;
		private float seconds = Run;
		private bool FirstRun = true;
		private bool execute = false;
		
		private CitizenManager MyCitizen = Singleton<CitizenManager>.instance;
		private BuildingManager MyBuilding = Singleton<BuildingManager>.instance;
		private InstanceManager MyInstance = Singleton<InstanceManager>.instance;
		private VehicleManager MyVehicle = Singleton<VehicleManager>.instance;
		private DistrictManager MyDistrict  = Singleton<DistrictManager>.instance;
		
		private static readonly string[] sHappinessLevels = new string[] { "VeryUnhappy", "Unhappy", "Happy", "VeryHappy", "ExtremelyHappy" };
		private static readonly string[] sHealthLevels = new string[] { "VerySick", "Sick", "PoorHealth", "Healthy", "VeryHealthy", "ExcellentHealth" };
		
		public InstanceID MyInstanceID;
		private InstanceID PrevMyInstanceID;
		private InstanceID MyPetID;
		private InstanceID MyVehicleID;
		private InstanceID MyTargetID;
		private InstanceID FamilyVehicleID;
		private InstanceID PersonalVehicleID;
		private InstanceID WorkPlaceID;
		
		uint DogOwner = 0;
		//uint CarOwner = 0;
		
		int RealAge;
		int HomeDistrict;
		int WorkDistrict;
		int CitizenDistrict;
		
		//Activity
		InstanceID CitizenInstanceID;
		
		uint citizen;
		uint MyCitizenUnit;
		Citizen CitizenData; 
		CitizenUnit Family;
		uint CitizenPartner;
		uint CitizenParent2;
		uint CitizenParent3;
		uint CitizenParent4;
		
		InstanceID PartnerID;
		InstanceID PartnerVehID;
		InstanceID PartnerTarget;
		InstanceID Parent1ID;
		InstanceID Parent1VehID;
		InstanceID Parent1Target;
		InstanceID Parent2ID;
		InstanceID Parent2VehID;
		InstanceID Parent2Target;
		InstanceID Parent3ID;
		InstanceID Parent3VehID;
		InstanceID Parent3Target;
		InstanceID Parent4ID;
		InstanceID Parent4VehID;
		InstanceID Parent4Target;
		
		InstanceID CitizenHomeID;
		
		ushort WorkPlace;
		ushort CitizenInstance;
		ushort CitizenHome;
		BuildingInfo HomeInfo;
		BuildingInfo WorkInfo;
		ushort MainCitizenInstance;
		ushort policeveh;
        bool isStudent;
		
		ushort Pet;
		CitizenInstance PetInstance;
		
		WindowController PanelMover;
		
		UITextureSprite FavCimsOtherInfoSprite;
		
		//Header Panel
		UIPanel BubbleHeaderPanel;
		//Icon Sprite
		UITextureSprite BubbleHeaderIconSprite;
		//UILabel Name
		UIButton BubbleHeaderCitizenName;
		//UIButton Close Panel
		UIButton BubbleCloseButton;
		//FamilyPortrait
		UIPanel BubbleFamilyPortraitPanel;
		//Sprite little Screen
		UITextureSprite BubbleFamPortBgSprite;
		UITextureSprite BubbleFamPortBgSpriteBack;
		UIPanel BubbleRow1Panel;
		UIPanel BubbleRow1HappyPanel;
		UIButton BubbleRow1HappyIcon;
		UIPanel BubbleRow1TextPanel;
		UITextureSprite BubbleRow1LabelsSprite;
		UIPanel BubbleRow1AgeLabelPanel;
		UIButton BubbleCitizenAge;
		UIPanel BubbleRow1AgePhaseLabelPanel;
		UIButton BubbleCitizenAgePhase;
		UIPanel BubbleRow1EducationLabelPanel;
		UIButton BubbleCitizenEducation;
		
		UIButton BubbleEduLevel1;
		UIButton BubbleEduLevel2;
		UIButton BubbleEduLevel3;
		UIPanel BubbleRow1EducationTooltipArea;
		
		UITextureSprite BubbleWealthHealthSprite;
		UIPanel BubbleWealthSpritePanel;
		UIButton BubbleWealthSprite;
		
		UIPanel BubbleHealthSpritePanel;
		UIButton BubbleHealthSprite;
		UIButton BubbleHealthValue;
		
		UIPanel BubbleRow1ValuesPanel;
		UIPanel BubbleRow1AgeValuePanel;
		UIButton BubbleCitizenAgeVal;
		UIPanel BubbleRow1AgePhaseValuePanel;
		UIButton BubbleCitizenAgePhaseVal;
		UIPanel BubbleRow1EducationValuePanel;
		
		UIPanel BubbleRow2Panel;
		UIButton BubbleRow2WealthValueVal;
		UIButton BubbleRow2WellbeingIcon;
		
		UIPanel BubbleTargetPanel;
		UIButton BubbleTargetIcon;
		
		UIPanel WorkBuildingPanel;
		UITextureSprite BubbleWorkBuildingSprite;
		UIPanel BubbleActivityPanel;
		UITextureSprite BubbleActivitySprite;
		UIPanel BubbleDistrictPanel;
		UITextureSprite BubbleDistrictSprite;
		
		UIButton FavCimsWorkingPlace;
		UIPanel BubbleWorkIconPanel;
		UITextureSprite FavCimsWorkingPlaceSprite;
		UIButton FavCimsWorkingPlaceButtonGamDefImg;
		UITextureSprite FavCimsCitizenWorkPlaceLevelSprite;
		
		UIPanel BubbleActivityVehiclePanel;
		UIButton FavCimsLastActivityVehicleButton;
		UIButton FavCimsLastActivity;
		
		UIButton FavCimsDistrictLabel;
		UIButton FavCimsDistrictValue;
		
		//Details & Problems Panel
		UIPanel BubbleDetailsPanel;
		//Details & Problems Background Sprite
		UITextureSprite BubbleDetailsBgSprite;
		
		//Home Icon
		UITextureSprite BubbleHomeIcon;
		//Home Level
		UITextureSprite BubbleHomeLevel;
		
		//Home Panel
		UIPanel BubbleHomePanel;
		//Home Name
		UIButton BubbleHomeName;
		//Details & Problems Icons Panel
		UIPanel BubbleDetailsIconsPanel;
		
		//Details & Problems Icons Buttons
		UIButton BubbleDetailsLandValue;
		UIButton BubbleDetailsCrime;
		UIButton BubbleDetailsNoise;
		UIButton BubbleDetailsWater;
		UIButton BubbleDetailsElettricity;
		UIButton BubbleDetailsGarbage;
		UIButton BubbleDetailsDeath;
		UIButton BubbleDetailsFire;
		UIButton BubbleDetailsPollution;
		
		//Header Family Bar Panel
		UIPanel BubbleFamilyBarPanel;
		//Header Family Bar Bg
		UITextureSprite BubbleFamilyBarPanelBg;
		//Header Family Label
		UILabel BubbleFamilyBarLabel;
		//Header Family Dog SpriteButton
		UITextureSprite BubbleFamilyBarDogButton;
		//Header Family Car SpriteButton
		UITextureSprite BubbleFamilyBarCarButton;
		//Family Panel
		UIPanel BubbleFamilyPanel;

		//Personal Vehicle Button
		UITextureSprite BubblePersonalCarButton;

		//No partner panel
		UIPanel NoPartnerPanel;
		//No partner button sprite
		UIButton NoPartnerBSprite;
		//No partner fake button
		UIButton NoPartnerFButton;
		
		//No Child Panel
		UIPanel NoChildsPanel;
		//No Child button sprite
		UIButton NoChildsBSprite;
		//No Child fake button
		UIButton NoChildsFButton;
		
		//Parent1 Panel
		UIPanel PartnerPanel;
		//Partner Background Bar
		UITextureSprite BubblePartnerBgBar;
		//Partner icon Sprite
		UIButton BubblePartnerLove;
		//Partner Name Button
		UIButton BubblePartnerName;
		//Partner Real Age Button
		UIButton BubbleParnerAgeButton;
		//Partner Follow Texture Sprite
		UIButton BubblePartnerFollowToggler;
		//Partner Activity Background Bar
		UITextureSprite BubblePartnerActivityBar;
		//Partner Activity Vehicle Button
		UIButton BubblePartnerVehicleButton;
		//Partner Activity Destination
		UIButton BubblePartnerDestination;
		
		//Parent1 Panel
		UIPanel Parent1Panel;
		//Partner Background Bar
		UITextureSprite BubbleParent1BgBar;
		//Partner icon Sprite
		UIButton BubbleParent1Love;
		//Partner Name Button
		UIButton BubbleParent1Name;
		//Partner Real Age Button
		UIButton BubbleParent1AgeButton;
		//Partner Follow Texture Sprite
		UIButton BubbleParent1FollowToggler;
		//Partner Activity Background Bar
		UITextureSprite BubbleParent1ActivityBar;
		//Partner Activity Vehicle Button
		UIButton BubbleParent1VehicleButton;
		//Partner Activity Destination
		UIButton BubbleParent1Destination;
		
		//Family 2 Panel
		UIPanel FamilyMember2Panel;
		//Family 2 Background bar
		UITextureSprite BubbleFamilyMember2BgBar;
		//Family 2 icon sprite
		UITextureSprite BubbleFamilyMember2IconSprite;
		//Family 2 name button
		UIButton BubbleFamilyMember2Name;
		//Family Member2 Real Age Button
		UIButton BubbleFamilyMember2AgeButton;
		//Family 2 Follow Texture Sprite
		UIButton BubbleFamilyMember2FollowToggler;
		//Family 2 Activity background
		UITextureSprite BubbleFamilyMember2ActivityBgBar;
		//Family 2 Activity Vehicle Button
		UIButton BubbleFamilyMember2ActivityVehicleButton;
		//Family 2 Activity Destination
		UIButton BubbleFamilyMember2ActivityDestination;
		
		//Family 3 Panel
		UIPanel FamilyMember3Panel;
		//Family 3 Background bar
		UITextureSprite BubbleFamilyMember3BgBar;
		//Family 3 icon sprite
		UITextureSprite BubbleFamilyMember3IconSprite;
		//Family 3 name button
		UIButton BubbleFamilyMember3Name;
		//Family Member3 Real Age Button
		UIButton BubbleFamilyMember3AgeButton;
		//Family 3 Follow Texture Sprite
		UIButton BubbleFamilyMember3FollowToggler;
		//Family 3 Activity background
		UITextureSprite BubbleFamilyMember3ActivityBgBar;
		//Family 3 Activity Vehicle Button
		UIButton BubbleFamilyMember3ActivityVehicleButton;
		//Family 3 Activity Destination
		UIButton BubbleFamilyMember3ActivityDestination;
		
		//Family 4 Panel
		UIPanel FamilyMember4Panel;
		//Family 4 Background bar
		UITextureSprite BubbleFamilyMember4BgBar;
		//Family 4 icon sprite
		UITextureSprite BubbleFamilyMember4IconSprite;
		//Family 4 name button
		UIButton BubbleFamilyMember4Name;
		//Family Member4 Real Age Button
		UIButton BubbleFamilyMember4AgeButton;
		//Family 4 Follow Texture Sprite
		UIButton BubbleFamilyMember4FollowToggler;
		//Family 4 Activity background
		UITextureSprite BubbleFamilyMember4ActivityBgBar;
		//Family 4 Activity Vehicle Button
		UIButton BubbleFamilyMember4ActivityVehicleButton;
		//Family 4 Activity Destination
		UIButton BubbleFamilyMember4ActivityDestination;

		///////////////End Bubble///////////////
		
		public void GoToCitizen(InstanceID Target, UIMouseEventParameter eventParam)
		{
			if (Target.IsEmpty)
				return;
			
			try {
				if (MyInstance.SelectInstance (Target)) {
					if (eventParam.buttons == UIMouseButton.Middle) {//Input.GetMouseButton (2)) {
						WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
					} else if (eventParam.buttons == UIMouseButton.Right) {//Input.GetMouseButton (1)) {
						//Switch Citizen
						this.MyInstanceID = Target;
						this.execute = true;
						LateUpdate();
					} else {
						
						ToolsModifierControl.cameraController.SetTarget (Target, ToolsModifierControl.cameraController.transform.position, true);
						WorldInfoPanel.Show<CitizenWorldInfoPanel> (this.position, Target);
					}
				}
			} catch(Exception e) {
				Debug.Error("Can't find the Citizen " + e.ToString());
			}
		}
		
		public void GoToInstance(InstanceID Target, UIMouseEventParameter eventParam) {
			
			if (Target.IsEmpty)
				return;
			
			try {
				if (MyInstance.SelectInstance (Target)) {
					if (eventParam.buttons == UIMouseButton.Middle) {
						//ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, false);
						DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
					} else {
						ToolsModifierControl.cameraController.SetTarget(Target, ToolsModifierControl.cameraController.transform.position, true);
						DefaultTool.OpenWorldInfoPanel(Target, ToolsModifierControl.cameraController.transform.position);
					}
				}
			} catch/*(Exception e)*/ {
				//Debug.Error("Can't find Target " + e.ToString());
			}
			
		}
		
		internal void FamilyVehicle(uint m_citizen, UITextureSprite sPrite, out InstanceID MyCitVeh) {
			
			MyCitVeh = InstanceID.Empty;

			if (m_citizen == 0)
				return;

			try
			{
				ushort FamVehicle = this.MyCitizen.m_citizens.m_buffer [m_citizen].m_vehicle;
				ushort FamVehicleParked = this.MyCitizen.m_citizens.m_buffer [m_citizen].m_parkedVehicle;
				
				VehicleInfo VehInfo;
				VehicleParked VehParked;
				
				if (FamVehicle != 0) {
					MyCitVeh.Vehicle = FamVehicle;
					VehInfo = this.MyVehicle.m_vehicles.m_buffer [FamVehicle].Info;
					
					if (VehInfo.m_vehicleAI.GetOwnerID (FamVehicle, ref MyVehicle.m_vehicles.m_buffer [FamVehicle]).Citizen == m_citizen) {
						//sta usando la sua macchina
						//this.CarOwner = m_citizen;
						sPrite.texture = TextureDB.BubbleCar;
						sPrite.playAudioEvents = true;
						sPrite.tooltip = this.MyVehicle.GetVehicleName (FamVehicle) + " - " + Locale.Get("VEHICLE_OWNER") + " " + this.MyCitizen.GetCitizenName (m_citizen);
					}
				} else if (FamVehicleParked != 0) {
					MyCitVeh.ParkedVehicle = FamVehicleParked;
					//VehInfo = this.MyVehicle.m_parkedVehicles.m_buffer[FamVehicleParked].Info;
					VehParked = this.MyVehicle.m_parkedVehicles.m_buffer[FamVehicleParked];
					
					if (VehParked.m_ownerCitizen == m_citizen) {
						//sta usando la sua macchina
						//this.CarOwner = m_citizen;
						sPrite.texture = TextureDB.BubbleCar;
						sPrite.playAudioEvents = true;
						sPrite.tooltip = this.MyVehicle.GetParkedVehicleName (FamVehicleParked) + " (" + Locale.Get("VEHICLE_STATUS_PARKED") + ")" + 
							" - " + Locale.Get("VEHICLE_OWNER") + " " + this.MyCitizen.GetCitizenName (m_citizen);
					}
				} else {
					//this.CarOwner = 0;
					sPrite.texture = TextureDB.BubbleCarDisabled;
					sPrite.playAudioEvents = false;
					sPrite.tooltip = null;
					//this.FamilyVehicleID.Vehicle = 0;
					//this.FamilyVehicleID.ParkedVehicle = 0;
				}
			} catch /*(Exception e)*/ {
				//Debug.Error("Family Car Error - " + e.ToString());
			}
		}
		
		internal void Activity(uint m_citizen, UIButton ButtVehicle, UIButton ButtDestination, out InstanceID VehID, out InstanceID MyTargetID) {
			
			VehID = InstanceID.Empty;
			MyTargetID = InstanceID.Empty;
			
			if (m_citizen == 0)
				return;
			
			ushort instance = this.MyCitizen.m_citizens.m_buffer[m_citizen].m_instance;
			CitizenInstance Cinstance = this.MyCitizen.m_instances.m_buffer [instance];
			
			if (Cinstance.m_targetBuilding != 0) {
				
				ushort veh = this.MyCitizen.m_citizens.m_buffer [m_citizen].m_vehicle;
				
				//bool GoingOutside = (MyBuilding.m_buildings.m_buffer [this.citizenInstance.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None;
				
				if (veh != 0) {
					
					VehID.Vehicle = veh;
					
					ButtVehicle.isEnabled = true;
					
					VehicleInfo VehInfo = this.MyVehicle.m_vehicles.m_buffer [veh].Info;
					
					string CitizenVehicleName = this.MyVehicle.GetVehicleName (veh);
					
					if (VehInfo.m_class.m_service == ItemClass.Service.Residential) {
						//if (VehInfo.m_vehicleAI.GetOwnerID (veh, ref MyVehicle.m_vehicles.m_buffer [veh]).Citizen == m_citizen) {
						if (CitizenVehicleName.Like ("Bicycle")) {
							//sta usando una bicicletta
							ButtVehicle.normalBgSprite = "IconTouristBicycleVehicle";
							ButtVehicle.hoveredBgSprite = "IconTouristBicycleVehicle";
							ButtVehicle.tooltip = CitizenVehicleName + " - " + Locale.Get ("PROPS_DESC", "bicycle01");
						} else {
							ButtVehicle.normalBgSprite = "IconCitizenVehicle";
							ButtVehicle.hoveredBgSprite = "IconTouristVehicle";
							ButtVehicle.tooltip = CitizenVehicleName;
						}

						//}
					} else if (VehInfo.m_class.m_service == ItemClass.Service.PublicTransport) {
						
						switch (VehInfo.m_class.m_subService)
						{

						case ItemClass.SubService.PublicTransportCableCar:

							ButtVehicle.normalBgSprite = "SubBarPublicTransportCableCar";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportCableCarHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportCableCarFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportCableCarPressed";
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Cable Car") + " - " + Locale.Get ("SUBSERVICE_DESC", "CableCar");

							break;

						case ItemClass.SubService.PublicTransportMonorail:

							ButtVehicle.normalBgSprite = "SubBarPublicTransportMonorail";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportMonorailHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportMonorailFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportMonorailPressed";
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Monorail Front") + " - " + Locale.Get ("SUBSERVICE_DESC", "Monorail");

							break;

						case ItemClass.SubService.PublicTransportTaxi:

							ButtVehicle.normalBgSprite = "SubBarPublicTransportTaxi";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTaxiHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportTaxiFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportTaxiPressed";
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Taxi") + " - " + Locale.Get ("SUBSERVICE_DESC", "Taxi");

							break;

						case ItemClass.SubService.PublicTransportTram:

							ButtVehicle.normalBgSprite = "SubBarPublicTransportTram";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTramHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportTramFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportTramPressed";
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Tram") + " - " + Locale.Get ("SUBSERVICE_DESC", "Tram");

							break;


						case ItemClass.SubService.PublicTransportBus:
							
							ButtVehicle.normalBgSprite = "SubBarPublicTransportBus";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportBusHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportBusFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportBusPressed";
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Bus") + " - " + Locale.Get ("SUBSERVICE_DESC", "Bus");
							
							break;
							
						case ItemClass.SubService.PublicTransportMetro:
							
							ButtVehicle.normalBgSprite = "SubBarPublicTransportMetro";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportMetroHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportMetroFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportMetroPressed";
							
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Metro") + " - " + Locale.Get ("SUBSERVICE_DESC", "Metro");
							
							break;
							
						case ItemClass.SubService.PublicTransportPlane:

							if(CitizenVehicleName.Like("Blimp")) {

								ButtVehicle.normalBgSprite = "IconPolicyEducationalBlimps";
								ButtVehicle.hoveredBgSprite = "IconPolicyEducationalBlimpsHovered";
								ButtVehicle.focusedBgSprite = "IconPolicyEducationalBlimpsFocused";
								ButtVehicle.pressedBgSprite = "IconPolicyEducationalBlimpsPressed";

								ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Blimp") + " - " + Locale.Get ("FEATURES_DESC", "Blimp");

							} else {

								ButtVehicle.normalBgSprite = "SubBarPublicTransportPlane";
								ButtVehicle.hoveredBgSprite = "SubBarPublicTransportPlaneHovered";
								ButtVehicle.focusedBgSprite = "SubBarPublicTransportPlaneFocused";
								ButtVehicle.pressedBgSprite = "SubBarPublicTransportPlanePressed";

								ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Aircraft Passenger") + " - " + Locale.Get ("SUBSERVICE_DESC", "Plane");
							}
							
							break;
						case ItemClass.SubService.PublicTransportShip:

							if (CitizenVehicleName.Like ("Ferry")) {

								ButtVehicle.normalBgSprite = "SubBarPublicTransportShip";
								ButtVehicle.hoveredBgSprite = "SubBarPublicTransportShipHovered";
								ButtVehicle.focusedBgSprite = "SubBarPublicTransportShipFocused";
								ButtVehicle.pressedBgSprite = "SubBarPublicTransportShipPressed";
								
								ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Ferry") + " - " + Locale.Get ("FEATURES_DESC", "Ferry");

							} else {

								ButtVehicle.normalBgSprite = "SubBarPublicTransportShip";
								ButtVehicle.hoveredBgSprite = "SubBarPublicTransportShipHovered";
								ButtVehicle.focusedBgSprite = "SubBarPublicTransportShipFocused";
								ButtVehicle.pressedBgSprite = "SubBarPublicTransportShipPressed";

								ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Ship Passenger") + " - " + Locale.Get ("SUBSERVICE_DESC", "Ship");

							}
								
							break;
						case ItemClass.SubService.PublicTransportTrain:
							
							ButtVehicle.normalBgSprite = "SubBarPublicTransportTrain";
							ButtVehicle.hoveredBgSprite = "SubBarPublicTransportTrainHovered";
							ButtVehicle.focusedBgSprite = "SubBarPublicTransportTrainFocused";
							ButtVehicle.pressedBgSprite = "SubBarPublicTransportTrainPressed";
							
							if (CitizenVehicleName == "VEHICLE_TITLE[Train Passenger]:0")
								CitizenVehicleName = Locale.Get ("VEHICLE_TITLE", "Train Engine");
							
							ButtVehicle.tooltip = Locale.Get ("VEHICLE_TITLE", "Train Engine") + " - " + Locale.Get ("SUBSERVICE_DESC", "Train");
							
							break;
						}					
					}
				} else {
					ButtVehicle.disabledBgSprite = "InfoIconPopulationDisabled";
					ButtVehicle.isEnabled = false;
					ButtVehicle.tooltip = FavCimsLang.text ("Vehicle_on_foot");
				}
			} else {
				ButtVehicle.disabledBgSprite = "InfoIconPopulationDisabled";
				ButtVehicle.isEnabled = false;
				ButtVehicle.tooltip = null;
			}
			
			//Citizen Status
			CitizenInstanceID.Citizen = m_citizen;
			
			CitizenInfo citizenInfo = this.MyCitizen.m_citizens.m_buffer [m_citizen].GetCitizenInfo (m_citizen);

			string CitizenStatus = citizenInfo.m_citizenAI.GetLocalizedStatus (m_citizen, ref this.MyCitizen.m_citizens.m_buffer [m_citizen], out MyTargetID);
			
			string CitizenTarget = this.MyBuilding.GetBuildingName (MyTargetID.Building, CitizenInstanceID);
			ButtDestination.text = CitizenStatus + " " + CitizenTarget;
			
			if (!MyTargetID.IsEmpty) {
				
				//District
				int TargetDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [MyTargetID.Index].m_position);
				
				if (TargetDistrict == 0) {
					ButtDestination.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
				} else {
					ButtDestination.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (TargetDistrict);
				}
			}

			if (this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)m_citizen].Arrested && this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)m_citizen].Criminal) {
				if (this.MyCitizen.m_citizens.m_buffer [m_citizen].CurrentLocation == Citizen.Location.Moving) {

					this.policeveh = this.MyCitizen.m_citizens.m_buffer [m_citizen].m_vehicle;

					if (this.policeveh != 0) {
						VehID.Vehicle = policeveh;

						ButtVehicle.atlas = MyAtlas.FavCimsAtlas;
						ButtVehicle.normalBgSprite = "FavCimsPoliceVehicle";
						ButtVehicle.isEnabled = true;
						ButtVehicle.playAudioEvents = true;
						ButtVehicle.tooltip = this.MyVehicle.GetVehicleName (policeveh) + " - " + Locale.Get ("VEHICLE_STATUS_PRISON_RETURN");

						ButtDestination.isEnabled = false;
						ButtDestination.text = FavCimsLang.text ("Transported_to_Prison");
					}
				} else {
					ButtDestination.isEnabled = true;
					ButtDestination.text = FavCimsLang.text ("Jailed_into") + CitizenTarget;
					ButtVehicle.atlas = UIView.GetAView().defaultAtlas;
				}
			}			
		}
		
		internal void FamilyPet(uint m_citizen) {
			
			if (m_citizen == 0)
				return;
			
			try
			{
				//Family Pets
				this.CitizenInstance = this.MyCitizen.m_citizens.m_buffer[m_citizen].m_instance;
				
				//if (this.CitizenInstance != 0) {
				this.Pet = (ushort)Array.FindIndex (MyCitizen.m_instances.m_buffer, element => element.m_targetBuilding == this.CitizenInstance);
				
				this.PetInstance = this.MyCitizen.m_instances.m_buffer [this.Pet];

				//if (this.PetInstance.Info.m_citizenAI.m_info.GetService() == h

				//Citizen.Flags.DummyTraffic
				//Vehicle.Flags.WaitingTarget

				if (this.PetInstance.Info.m_citizenAI.IsAnimal ()) {
					
					this.DogOwner = m_citizen;
					
					this.MyPetID.CitizenInstance = this.Pet;
					
					if (!this.MyPetID.IsEmpty) {
						
						InstanceID MyPetTargetID;
						
						string petname = MyCitizen.GetInstanceName (Pet);
						CitizenInfo petinfo = PetInstance.Info;
						string petstatus = petinfo.m_citizenAI.GetLocalizedStatus (Pet, ref PetInstance, out MyPetTargetID);
						
						this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDog;
						this.BubbleFamilyBarDogButton.tooltip = petname + " - " + petstatus + " " + this.MyCitizen.GetCitizenName (m_citizen);
						this.BubbleFamilyBarDogButton.playAudioEvents = true;
					}
				} else {
					
					this.DogOwner = 0;
					
					this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
					this.BubbleFamilyBarDogButton.tooltip = null;
					this.BubbleFamilyBarDogButton.playAudioEvents = false;
					this.MyPetID = InstanceID.Empty;
				}
				//} else {
				//return;
				//}
			} catch /*(Exception e)*/ {
				//Debug.Log("Family Dog Error - " + e.ToString());
			}
		}
		
		internal static string GetHappinessString(Citizen.Happiness happinessLevel)
		{
			return ("NotificationIcon" + sHappinessLevels[(int) happinessLevel]);
		}
		
		internal static string GetHealthString(Citizen.Health healthLevel)
		{
			return ("NotificationIcon" + sHealthLevels[(int) healthLevel]);
		}
		
		public override void Start() {

			//Atlas
			UITextureAtlas m_atlas = MyAtlas.FavCimsAtlas;

			//////////Main Family Panel////////////
			this.width = 250;
			this.height = 500;
			this.clipChildren = true;
			//this.backgroundSprite = "CitizenBackground";
			
			int RandXMin = 30;
			int RandXMax = Screen.width / 4;
			
			int RandYMin = 100;
			int RandYMax = (Screen.height - ((int)this.height*2)) - RandYMin;
			
			System.Random rnd = new System.Random ();
			
			//Family Panel Bg Sprite
			this.FavCimsOtherInfoSprite = this.AddUIComponent<UITextureSprite> ();
			this.FavCimsOtherInfoSprite.name = "FavCimsOtherInfoSprite";
			this.FavCimsOtherInfoSprite.texture = TextureDB.FavCimsOtherInfoTexture;
			this.FavCimsOtherInfoSprite.width = this.width;
			this.FavCimsOtherInfoSprite.height = this.height;
			this.FavCimsOtherInfoSprite.relativePosition =  Vector3.zero;
			
			//Header Panel
			this.BubbleHeaderPanel = this.AddUIComponent<UIPanel>();
			this.BubbleHeaderPanel.name = "BubbleHeaderPanel";
			this.BubbleHeaderPanel.width = 250;
			this.BubbleHeaderPanel.height = 41;
			this.BubbleHeaderPanel.relativePosition = new Vector3(0,0);
			//Icon Sprite
			this.BubbleHeaderIconSprite = this.BubbleHeaderPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleHeaderIconSprite.name = "BubbleHeaderIconSprite";
			this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
			this.BubbleHeaderIconSprite.relativePosition = new Vector3 (9, this.BubbleHeaderPanel.relativePosition.y+9);
			//UIButton Name
			this.BubbleHeaderCitizenName = this.BubbleHeaderPanel.AddUIComponent<UIButton> ();
			this.BubbleHeaderCitizenName.name = "BubbleHeaderCitizenName";
			this.BubbleHeaderCitizenName.width = this.BubbleHeaderPanel.width;
			this.BubbleHeaderCitizenName.height = this.BubbleHeaderPanel.height;
			this.BubbleHeaderCitizenName.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleHeaderCitizenName.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleHeaderCitizenName.playAudioEvents = false;
			this.BubbleHeaderCitizenName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleHeaderCitizenName.font.size = 15;
			this.BubbleHeaderCitizenName.textScale = 1f;
			this.BubbleHeaderCitizenName.wordWrap = true;
			this.BubbleHeaderCitizenName.textPadding.left = 5;
			this.BubbleHeaderCitizenName.textPadding.right = 5;
			this.BubbleHeaderCitizenName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleHeaderCitizenName.hoveredTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleHeaderCitizenName.pressedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleHeaderCitizenName.focusedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleHeaderCitizenName.useDropShadow = true;
			this.BubbleHeaderCitizenName.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleHeaderCitizenName.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleHeaderCitizenName.relativePosition =  Vector3.zero;
			//Panel Mover
			this.BubbleHeaderCitizenName.eventMouseDown += delegate {
				if(Input.GetMouseButton(0)) {
					if(this.GetComponentInChildren<WindowController>() != null) {
						this.PanelMover = this.GetComponentInChildren<WindowController>();
						this.PanelMover.ComponentToMove = this;
						this.PanelMover.Stop = false;
						this.PanelMover.Start();
					} else {
						this.PanelMover = this.AddUIComponent(typeof(WindowController)) as WindowController;
						this.PanelMover.ComponentToMove = this;
					}
					this.opacity = 0.5f;
				}
			};
			//Panel Mover Close Event
			this.BubbleHeaderCitizenName.eventMouseUp += delegate {
				if(this.PanelMover != null) {
					this.PanelMover.Stop = true;
					this.PanelMover.ComponentToMove = null;
					this.PanelMover = null;
				}
				this.opacity = 1f;
			};
			
			//Bubble Close Panel
			this.BubbleCloseButton = this.AddUIComponent<UIButton> ();
			this.BubbleCloseButton.name = "BubbleCloseButton";
			this.BubbleCloseButton.width = 26;
			this.BubbleCloseButton.height = 26;
			this.BubbleCloseButton.normalBgSprite = "buttonclose";
			this.BubbleCloseButton.hoveredBgSprite = "buttonclosehover";
			this.BubbleCloseButton.pressedBgSprite = "buttonclosepressed";
			this.BubbleCloseButton.opacity = 0.9f;
			//this.BubbleCloseButton.useOutline = true;
			this.BubbleCloseButton.playAudioEvents = true;
			this.BubbleCloseButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
			
			this.BubbleCloseButton.eventClick += delegate {
				try {
					//GameObject.Destroy(this.gameObject);
					this.Hide();
					this.MyInstanceID = InstanceID.Empty;
				}catch(Exception e) {
					Debug.Error("Can't remove family panel " + e.ToString());
				}
			};
			
			//Printing
			this.BubbleCloseButton.relativePosition = new Vector3 (this.BubbleHeaderPanel.width - 36, 7);
			
			//FamilyPortrait
			this.BubbleFamilyPortraitPanel = this.AddUIComponent<UIPanel>();
			this.BubbleFamilyPortraitPanel.name = "BubbleFamilyPortraitPanel";
			this.BubbleFamilyPortraitPanel.width = 242;
			this.BubbleFamilyPortraitPanel.height = 156;
			this.BubbleFamilyPortraitPanel.relativePosition = new Vector3(4,this.BubbleHeaderPanel.relativePosition.y+this.BubbleHeaderPanel.height);
			
			//Sprite CitizenDetail Background
			this.BubbleFamPortBgSpriteBack = this.BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamPortBgSpriteBack.name = "BubbleFamPortBgSpriteBack";
			this.BubbleFamPortBgSpriteBack.texture = TextureDB.BubbleFamPortBgSpriteBackTexture;
			this.BubbleFamPortBgSpriteBack.relativePosition = new Vector3 (4, 4);
			
			//Sprite CitizenDetail Foreground
			this.BubbleFamPortBgSprite = this.BubbleFamilyPortraitPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamPortBgSprite.name = "BubbleFamPortBgSprite";
			this.BubbleFamPortBgSprite.texture = TextureDB.BubbleFamPortBgSpriteTexture;
			this.BubbleFamPortBgSprite.relativePosition =  Vector3.zero;
			
			//Panel (Happiness Icon + Age + Age Phase + Education (Label & Value))
			this.BubbleRow1Panel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow1Panel.name = "BubbleRow1Panel";
			this.BubbleRow1Panel.width = 234;
			this.BubbleRow1Panel.height = 36;
			this.BubbleRow1Panel.relativePosition = new Vector3 (4, 4);
			
			//Happiness Container Panel
			this.BubbleRow1HappyPanel = this.BubbleRow1Panel.AddUIComponent<UIPanel> ();
			this.BubbleRow1HappyPanel.name = "BubbleRow1Panel";
			this.BubbleRow1HappyPanel.width = 36;
			this.BubbleRow1HappyPanel.height = 36;
			this.BubbleRow1HappyPanel.relativePosition =  Vector3.zero;
			
			//Happiness Icon
			this.BubbleRow1HappyIcon = this.BubbleRow1HappyPanel.AddUIComponent<UIButton> ();
			this.BubbleRow1HappyIcon.width = 26;
			this.BubbleRow1HappyIcon.height = 26;
			this.BubbleRow1HappyIcon.isEnabled = false;
			this.BubbleRow1HappyIcon.playAudioEvents = false;
			this.BubbleRow1HappyIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleRow1HappyIcon.relativePosition = new Vector3 (4, 5);
			
			//Wellbeing Icon
			this.BubbleRow2WellbeingIcon = this.BubbleRow1HappyPanel.AddUIComponent<UIButton> ();
			this.BubbleRow2WellbeingIcon.width = 11;
			this.BubbleRow2WellbeingIcon.height = 11;
			this.BubbleRow2WellbeingIcon.isEnabled = false;
			this.BubbleRow2WellbeingIcon.playAudioEvents = false;
			this.BubbleRow2WellbeingIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleRow2WellbeingIcon.relativePosition = new Vector3 (24, 5);
			
			//Labels & Values Container Panel
			this.BubbleRow1TextPanel = this.BubbleRow1Panel.AddUIComponent<UIPanel> ();
			this.BubbleRow1TextPanel.name = "BubbleRow1TextPanel";
			this.BubbleRow1TextPanel.width = 198;
			this.BubbleRow1TextPanel.height = 37;
			this.BubbleRow1TextPanel.relativePosition = new Vector3 (36, 0);
			
			//Labels Container Sprite
			this.BubbleRow1LabelsSprite = this.BubbleRow1TextPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleRow1LabelsSprite.name = "BubbleRow1LabelsSprite";
			this.BubbleRow1LabelsSprite.width = 198;
			this.BubbleRow1LabelsSprite.height = 34;
			this.BubbleRow1LabelsSprite.texture = TextureDB.BubbleBgBar1Big;
			this.BubbleRow1LabelsSprite.relativePosition = new Vector3 (0, 3);
			
			//Age Label Panel
			this.BubbleRow1AgeLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow1AgeLabelPanel.name = "BubbleRow1AgeLabelPanel";
			this.BubbleRow1AgeLabelPanel.width = 32;
			this.BubbleRow1AgeLabelPanel.height = 17;
			this.BubbleRow1AgeLabelPanel.relativePosition =  Vector3.zero;
			
			//Age Button (Label) Text
			this.BubbleCitizenAge = this.BubbleRow1AgeLabelPanel.AddUIComponent<UIButton> ();
			this.BubbleCitizenAge.name = "BubbleCitizenAge";
			this.BubbleCitizenAge.width = this.BubbleRow1AgeLabelPanel.width;
			this.BubbleCitizenAge.height = this.BubbleRow1AgeLabelPanel.height;
			this.BubbleCitizenAge.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleCitizenAge.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleCitizenAge.font.size = 15;
			this.BubbleCitizenAge.textScale = 0.80f;
			this.BubbleCitizenAge.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAge.outlineSize = 1;
			this.BubbleCitizenAge.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleCitizenAge.isInteractive = false;
			this.BubbleCitizenAge.useDropShadow = true;
			this.BubbleCitizenAge.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleCitizenAge.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAge.relativePosition = new Vector3 (0, 1);
			
			//Age Phase Label Panel
			this.BubbleRow1AgePhaseLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow1AgePhaseLabelPanel.name = "BubbleRow1AgePhaseLabelPanel";
			this.BubbleRow1AgePhaseLabelPanel.width = 100;
			this.BubbleRow1AgePhaseLabelPanel.height = 17;
			this.BubbleRow1AgePhaseLabelPanel.relativePosition = new Vector3 (32, 0);
			
			//Age Phase Button (Label) Text
			this.BubbleCitizenAgePhase = this.BubbleRow1AgePhaseLabelPanel.AddUIComponent<UIButton> ();
			this.BubbleCitizenAgePhase.name = "BubbleCitizenAgePhase";
			this.BubbleCitizenAgePhase.width = this.BubbleRow1AgePhaseLabelPanel.width;
			this.BubbleCitizenAgePhase.height = this.BubbleRow1AgePhaseLabelPanel.height;
			this.BubbleCitizenAgePhase.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleCitizenAgePhase.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleCitizenAgePhase.font.size = 15;
			this.BubbleCitizenAgePhase.textScale = 0.8f;
			this.BubbleCitizenAgePhase.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgePhase.outlineSize = 1;
			this.BubbleCitizenAgePhase.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleCitizenAgePhase.isInteractive = false;
			this.BubbleCitizenAgePhase.useDropShadow = true;
			this.BubbleCitizenAgePhase.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleCitizenAgePhase.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgePhase.relativePosition = new Vector3 (0, 1);
			
			//Education Label Panel
			this.BubbleRow1EducationLabelPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow1EducationLabelPanel.name = "BubbleRow1LabelsPanel";
			this.BubbleRow1EducationLabelPanel.width = 66;
			this.BubbleRow1EducationLabelPanel.height = 17;
			this.BubbleRow1EducationLabelPanel.relativePosition = new Vector3 (132, 0);
			
			//Education Button (Label) Text
			this.BubbleCitizenEducation = this.BubbleRow1EducationLabelPanel.AddUIComponent<UIButton> ();
			this.BubbleCitizenEducation.name = "BubbleCitizenEducation";
			this.BubbleCitizenEducation.width = this.BubbleRow1EducationLabelPanel.width;
			this.BubbleCitizenEducation.height = this.BubbleRow1EducationLabelPanel.height;
			//this.BubbleCitizenEducation.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleCitizenEducation.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleCitizenEducation.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleCitizenEducation.font.size = 15;
			this.BubbleCitizenEducation.textScale = 0.80f;
			//this.BubbleCitizenEducation.wordWrap = true;
			//this.BubbleCitizenEducation.textPadding.left = 2;
			//this.BubbleCitizenEducation.textPadding.right = 2;
			this.BubbleCitizenEducation.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenEducation.outlineSize = 1;
			this.BubbleCitizenEducation.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleCitizenEducation.isInteractive = false;
			this.BubbleCitizenEducation.useDropShadow = true;
			this.BubbleCitizenEducation.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleCitizenEducation.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenEducation.relativePosition = new Vector3 (0, 1);
			
			//Values Container Sprite
			this.BubbleRow1ValuesPanel = this.BubbleRow1LabelsSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow1ValuesPanel.name = "BubbleRow1ValuesPanel";
			this.BubbleRow1ValuesPanel.width = 198;
			this.BubbleRow1ValuesPanel.height = 17;
			//this.BubbleRow1ValuesPanel.texture = TextureDB.BubbleBgBar1Small;
			this.BubbleRow1ValuesPanel.relativePosition = new Vector3 (0, 17);
			
			//Age Value Panel
			this.BubbleRow1AgeValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel> ();
			this.BubbleRow1AgeValuePanel.name = "BubbleRow1AgeValuePanel";
			this.BubbleRow1AgeValuePanel.width = 32;
			this.BubbleRow1AgeValuePanel.height = 17;
			this.BubbleRow1AgeValuePanel.relativePosition =  Vector3.zero;
			
			//Age Button (Value) Text
			this.BubbleCitizenAgeVal = this.BubbleRow1AgeValuePanel.AddUIComponent<UIButton> ();
			this.BubbleCitizenAgeVal.name = "BubbleCitizenAgeVal";
			this.BubbleCitizenAgeVal.width = this.BubbleRow1AgeValuePanel.width;
			this.BubbleCitizenAgeVal.height = this.BubbleRow1AgeValuePanel.height;
			//this.BubbleCitizenAgeVal.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleCitizenAgeVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleCitizenAgeVal.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleCitizenAgeVal.font.size = 15;
			this.BubbleCitizenAgeVal.textScale = 0.85f;
			//this.BubbleCitizenAgeVal.wordWrap = true;
			//this.BubbleCitizenAgeVal.textPadding.left = 2;
			//this.BubbleCitizenAgeVal.textPadding.right = 2;
			this.BubbleCitizenAgeVal.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgeVal.outlineSize = 1;
			this.BubbleCitizenAgeVal.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleCitizenAgeVal.isInteractive = false;
			this.BubbleCitizenAgeVal.useDropShadow = true;
			this.BubbleCitizenAgeVal.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleCitizenAgeVal.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgeVal.relativePosition = new Vector3 (0, 0);
			
			//Age Phase Value Panel
			this.BubbleRow1AgePhaseValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel> ();
			this.BubbleRow1AgePhaseValuePanel.name = "BubbleRow1AgePhaseValuePanel";
			this.BubbleRow1AgePhaseValuePanel.width = 100;
			this.BubbleRow1AgePhaseValuePanel.height = 17;
			this.BubbleRow1AgePhaseValuePanel.relativePosition = new Vector3 (32, 0);
			
			//Age Phase Button (Value) Text
			this.BubbleCitizenAgePhaseVal = this.BubbleRow1AgePhaseValuePanel.AddUIComponent<UIButton> ();
			this.BubbleCitizenAgePhaseVal.name = "BubbleCitizenAgePhaseVal";
			this.BubbleCitizenAgePhaseVal.width = this.BubbleRow1AgePhaseValuePanel.width;
			this.BubbleCitizenAgePhaseVal.height = this.BubbleRow1AgePhaseValuePanel.height;
			this.BubbleCitizenAgePhaseVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleCitizenAgePhaseVal.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleCitizenAgePhaseVal.font.size = 15;
			this.BubbleCitizenAgePhaseVal.textScale = 0.85f;
			this.BubbleCitizenAgePhaseVal.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgePhaseVal.outlineSize = 1;
			this.BubbleCitizenAgePhaseVal.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleCitizenAgePhaseVal.isInteractive = false;
			this.BubbleCitizenAgePhaseVal.useDropShadow = true;
			this.BubbleCitizenAgePhaseVal.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleCitizenAgePhaseVal.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleCitizenAgePhaseVal.relativePosition = new Vector3 (0, 0);
			
			//Education Value Panel
			this.BubbleRow1EducationValuePanel = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel> ();
			this.BubbleRow1EducationValuePanel.name = "BubbleRow1LabelsPanel";
			this.BubbleRow1EducationValuePanel.width = 66;
			this.BubbleRow1EducationValuePanel.height = 17;
			//this.BubbleRow1EducationValuePanel.padding.top = 1;
			this.BubbleRow1EducationValuePanel.relativePosition = new Vector3 (132, 0);
			
			//Education Level 1
			this.BubbleEduLevel1 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton> ();
			this.BubbleEduLevel1.width = 18;
			this.BubbleEduLevel1.height = 17;
			this.BubbleEduLevel1.normalBgSprite = "InfoIconEducation";
			this.BubbleEduLevel1.disabledBgSprite = "InfoIconEducationDisabled";
			this.BubbleEduLevel1.isEnabled = false;
			this.BubbleEduLevel1.playAudioEvents = false;
			this.BubbleEduLevel1.relativePosition = new Vector3 (2, 0);
			
			//Education Level 2
			this.BubbleEduLevel2 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton> ();
			this.BubbleEduLevel2.width = this.BubbleEduLevel1.width;
			this.BubbleEduLevel2.height = this.BubbleEduLevel1.height;
			this.BubbleEduLevel2.normalBgSprite = "InfoIconEducation";
			this.BubbleEduLevel2.disabledBgSprite = "InfoIconEducationDisabled";
			this.BubbleEduLevel2.isEnabled = false;
			this.BubbleEduLevel2.playAudioEvents = false;
			this.BubbleEduLevel2.relativePosition = new Vector3 (24, 0);
			
			//Education Level 3
			this.BubbleEduLevel3 = this.BubbleRow1EducationValuePanel.AddUIComponent<UIButton> ();
			this.BubbleEduLevel3.width = this.BubbleEduLevel1.width;
			this.BubbleEduLevel3.height = this.BubbleEduLevel1.height;
			this.BubbleEduLevel3.normalBgSprite = "InfoIconEducation";
			this.BubbleEduLevel3.disabledBgSprite = "InfoIconEducationDisabled";
			this.BubbleEduLevel3.isEnabled = false;
			this.BubbleEduLevel3.playAudioEvents = false;
			this.BubbleEduLevel3.relativePosition = new Vector3 (46, 0);
			
			//Education Tooltip Area
			this.BubbleRow1EducationTooltipArea = this.BubbleRow1ValuesPanel.AddUIComponent<UIPanel> ();
			this.BubbleRow1EducationTooltipArea.name = "BubbleRow1EducationTooltipArea";
			this.BubbleRow1EducationTooltipArea.width = this.BubbleRow1EducationValuePanel.width;
			this.BubbleRow1EducationTooltipArea.height = this.BubbleRow1EducationValuePanel.height;
			this.BubbleRow1EducationTooltipArea.absolutePosition = BubbleRow1EducationValuePanel.absolutePosition;
			this.BubbleRow1EducationTooltipArea.tooltipBox = UIView.GetAView().defaultTooltipBox;
			
			//Target Panel
			this.BubbleTargetPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleTargetPanel.name = "BubbleTargetPanel";
			this.BubbleTargetPanel.width = 58;
			this.BubbleTargetPanel.height = 36;
			this.BubbleTargetPanel.relativePosition = new Vector3 (4, 35);
			//Target Button 
			this.BubbleTargetIcon = this.BubbleTargetPanel.AddUIComponent<UIButton> ();
			this.BubbleTargetIcon.width = 28;
			this.BubbleTargetIcon.height = 28;
			this.BubbleTargetIcon.normalBgSprite = "LocationMarkerNormal";
			this.BubbleTargetIcon.hoveredBgSprite = "LocationMarkerHovered";
			this.BubbleTargetIcon.focusedBgSprite = "LocationMarkerFocused";
			this.BubbleTargetIcon.pressedBgSprite = "LocationMarkerPressed";
			this.BubbleTargetIcon.disabledBgSprite = "LocationMarkerDisabled";
			this.BubbleTargetIcon.playAudioEvents = true;
			this.BubbleTargetIcon.relativePosition = new Vector3 (4, 0);
			this.BubbleTargetIcon.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToCitizen (MyInstanceID, eventParam);

			//Wealth + Health Panel
			this.BubbleRow2Panel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleRow2Panel.name = "BubbleRow2Panel";
			this.BubbleRow2Panel.width = 198;
			this.BubbleRow2Panel.height = 34;
			this.BubbleRow2Panel.relativePosition = new Vector3 (40, 44);
			
			//Wealth + Health Bg Sprite
			this.BubbleWealthHealthSprite = this.BubbleRow2Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleWealthHealthSprite.name = "BubbleWealthHealthSprite";
			this.BubbleWealthHealthSprite.width = 198;
			this.BubbleWealthHealthSprite.height = 34;
			this.BubbleWealthHealthSprite.texture = TextureDB.BubbleBgBar1Big;
			this.BubbleWealthHealthSprite.relativePosition = Vector3.zero;
			
			//Wealth Sprite Panel
			this.BubbleWealthSpritePanel = this.BubbleWealthHealthSprite.AddUIComponent<UIPanel> ();
			this.BubbleWealthSpritePanel.name = "BubbleWealthSpritePanel";
			this.BubbleWealthSpritePanel.width = 37;
			this.BubbleWealthSpritePanel.height = 34;
			this.BubbleWealthSpritePanel.relativePosition = new Vector3 (0, 0);
			
			//Wealth Button Sprite
			this.BubbleWealthSprite = this.BubbleWealthSpritePanel.AddUIComponent<UIButton> ();
			this.BubbleWealthSprite.name = "BubbleWealthSprite";
			this.BubbleWealthSprite.width = 25;
			this.BubbleWealthSprite.height = 25;
			this.BubbleWealthSprite.normalBgSprite = "MoneyThumb";
			this.BubbleWealthSprite.playAudioEvents = false;
			this.BubbleWealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleWealthSprite.relativePosition = new Vector3(10,5);
			
			//Weatlh Button Value
			this.BubbleRow2WealthValueVal = this.BubbleWealthHealthSprite.AddUIComponent<UIButton> ();
			this.BubbleRow2WealthValueVal.name = "BubbleRow2WealthValueVal";
			this.BubbleRow2WealthValueVal.width = 70;
			this.BubbleRow2WealthValueVal.height = 34;
			this.BubbleRow2WealthValueVal.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleRow2WealthValueVal.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleRow2WealthValueVal.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			//this.BubbleRow2WealthValueVal.textPadding.left = 2;
			//this.BubbleRow2WealthValueVal.textPadding.right = 2;
			this.BubbleRow2WealthValueVal.textPadding.top = 1;
			this.BubbleRow2WealthValueVal.font.size = 15;
			this.BubbleRow2WealthValueVal.textScale = 0.80f;
			this.BubbleRow2WealthValueVal.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleRow2WealthValueVal.outlineSize = 1;
			this.BubbleRow2WealthValueVal.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleRow2WealthValueVal.isInteractive = false;
			this.BubbleRow2WealthValueVal.useDropShadow = true;
			this.BubbleRow2WealthValueVal.wordWrap = true;
			this.BubbleRow2WealthValueVal.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleRow2WealthValueVal.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleRow2WealthValueVal.relativePosition = new Vector3(37,0);
			
			//Health Sprite Panel
			this.BubbleHealthSpritePanel = this.BubbleWealthHealthSprite.AddUIComponent<UIPanel> ();
			this.BubbleHealthSpritePanel.name = "BubbleHealthSpritePanel";
			this.BubbleHealthSpritePanel.width = 26;
			this.BubbleHealthSpritePanel.height = 34;
			this.BubbleHealthSpritePanel.relativePosition = new Vector3 (107, 0);
			
			//Health Button Sprite
			this.BubbleHealthSprite = this.BubbleHealthSpritePanel.AddUIComponent<UIButton> ();
			this.BubbleHealthSprite.name = "BubbleWealthSprite";
			this.BubbleHealthSprite.width = 26;
			this.BubbleHealthSprite.height = 26;
			this.BubbleHealthSprite.playAudioEvents = false;
			this.BubbleHealthSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleHealthSprite.relativePosition = new Vector3(0,4);
			
			//Heatlh Button Value
			this.BubbleHealthValue = this.BubbleWealthHealthSprite.AddUIComponent<UIButton> ();
			this.BubbleHealthValue.name = "BubbleHealthValue";
			this.BubbleHealthValue.width = 65;
			this.BubbleHealthValue.height = 34;
			this.BubbleHealthValue.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleHealthValue.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleHealthValue.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleHealthValue.textPadding.left = 5;
			this.BubbleHealthValue.textPadding.right = 5;
			this.BubbleHealthValue.textPadding.top = 1;
			this.BubbleHealthValue.font.size = 15;
			this.BubbleHealthValue.textScale = 0.85f;
			this.BubbleHealthValue.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleHealthValue.outlineSize = 1;
			this.BubbleHealthValue.textColor = new Color32 (0, 51, 102, 140); //r,g,b,a
			this.BubbleHealthValue.isInteractive = false;
			this.BubbleHealthValue.useDropShadow = true;
			this.BubbleHealthValue.wordWrap = true;
			this.BubbleHealthValue.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleHealthValue.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleHealthValue.relativePosition = new Vector3(133,0);
			
			//Work Building Panel
			this.WorkBuildingPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.WorkBuildingPanel.name = "WorkBuildingPanel";
			this.WorkBuildingPanel.width = 234;
			this.WorkBuildingPanel.height = 25;
			this.WorkBuildingPanel.relativePosition = new Vector3 (4, 82);
			
			//Work Building Background
			this.BubbleWorkBuildingSprite = this.WorkBuildingPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleWorkBuildingSprite.name = "BubbleWorkBuildingSprite";
			this.BubbleWorkBuildingSprite.width = this.WorkBuildingPanel.width;
			this.BubbleWorkBuildingSprite.height = this.WorkBuildingPanel.height;
			this.BubbleWorkBuildingSprite.texture = TextureDB.BubbleBg1Special;
			this.BubbleWorkBuildingSprite.relativePosition = Vector3.zero;
			this.BubbleWorkBuildingSprite.clipChildren = true;
			
			this.FavCimsWorkingPlace = this.BubbleWorkBuildingSprite.AddUIComponent<UIButton> ();
			this.FavCimsWorkingPlace.name = "FavCimsWorkingPlace";
			this.FavCimsWorkingPlace.width = this.BubbleWorkBuildingSprite.width;
			this.FavCimsWorkingPlace.height = this.BubbleWorkBuildingSprite.height;
			this.FavCimsWorkingPlace.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.FavCimsWorkingPlace.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.FavCimsWorkingPlace.playAudioEvents = true;
			this.FavCimsWorkingPlace.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.FavCimsWorkingPlace.font.size = 15;
			this.FavCimsWorkingPlace.textScale = 0.85f;
			//this.FavCimsWorkingPlace.wordWrap = true;
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
			this.FavCimsWorkingPlace.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.FavCimsWorkingPlace.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance (WorkPlaceID, eventParam);
			this.FavCimsWorkingPlace.relativePosition = new Vector3 (0, 1);
			
			//Work Panel
			this.BubbleWorkIconPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleWorkIconPanel.name = "BubbleRow2Panel";
			this.BubbleWorkIconPanel.width = 36;
			this.BubbleWorkIconPanel.height = 40;
			this.BubbleWorkIconPanel.absolutePosition = new Vector3 (BubbleFamPortBgSprite.absolutePosition.x + 4, BubbleFamPortBgSprite.absolutePosition.y + 71); //70
			
			//Work sprites
			this.FavCimsWorkingPlaceSprite = this.BubbleWorkIconPanel.AddUIComponent<UITextureSprite> ();
			this.FavCimsWorkingPlaceSprite.name = "FavCimsWorkingPlaceSprite";
			this.FavCimsWorkingPlaceSprite.width = 20;
			this.FavCimsWorkingPlaceSprite.height = 40;
			this.FavCimsWorkingPlaceSprite.relativePosition = new Vector3 (9, 3);
			this.FavCimsWorkingPlaceSprite.tooltipBox = UIView.GetAView().defaultTooltipBox;
			
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

			//Citizen Personal Vehicle
			this.BubblePersonalCarButton = this.BubbleFamPortBgSprite.AddUIComponent<UITextureSprite> ();
			this.BubblePersonalCarButton.name = "BubblePersonalCarButton";
			this.BubblePersonalCarButton.width = 30;
			this.BubblePersonalCarButton.height = 20;
			this.BubblePersonalCarButton.texture = TextureDB.BubbleCarDisabled;
			this.BubblePersonalCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubblePersonalCarButton.absolutePosition = new Vector3(BubbleTargetIcon.absolutePosition.x, this.BubbleTargetIcon.absolutePosition.y + this.BubbleTargetIcon.height);
			this.BubblePersonalCarButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToInstance(this.PersonalVehicleID, eventParam);
			this.BubblePersonalCarButton.BringToFront ();

			//Activity Panel
			this.BubbleActivityPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleActivityPanel.name = "BubbleActivityPanel";
			this.BubbleActivityPanel.width = 234;
			this.BubbleActivityPanel.height = 18; //15
			this.BubbleActivityPanel.relativePosition = new Vector3 (4, this.WorkBuildingPanel.relativePosition.y + 31); //34
			
			//Activity Background
			this.BubbleActivitySprite = this.BubbleActivityPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleActivitySprite.name = "BubbleActivitySprite";
			this.BubbleActivitySprite.width = this.BubbleActivityPanel.width;
			this.BubbleActivitySprite.height = this.BubbleActivityPanel.height;
			this.BubbleActivitySprite.texture = TextureDB.BubbleBg1Special2;
			this.BubbleActivitySprite.relativePosition = Vector3.zero;
			
			//Citizen Vehicle Panel
			this.BubbleActivityVehiclePanel = this.BubbleActivitySprite.AddUIComponent<UIPanel> ();
			this.BubbleActivityVehiclePanel.name = "BubbleActivityVehiclePanel";
			this.BubbleActivityVehiclePanel.width = 234;
			this.BubbleActivityVehiclePanel.height = 18; //15
			this.BubbleActivityVehiclePanel.relativePosition = new Vector3 (4, 0);
			
			//Citizen Vehicle
			this.FavCimsLastActivityVehicleButton = this.BubbleActivityVehiclePanel.AddUIComponent<UIButton> ();
			this.FavCimsLastActivityVehicleButton.name = "FavCimsLastActivityVehicleButton";
			this.FavCimsLastActivityVehicleButton.width = 18;
			this.FavCimsLastActivityVehicleButton.height = 17;
			this.FavCimsLastActivityVehicleButton.relativePosition = new Vector3 (0, 0);
			this.FavCimsLastActivityVehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.MyVehicleID, eventParam);
			
			//Last Activity
			this.FavCimsLastActivity = this.BubbleActivitySprite.AddUIComponent<UIButton> ();
			this.FavCimsLastActivity.name = "FavCimsLastActivity";
			this.FavCimsLastActivity.width = this.BubbleActivitySprite.width-27;
			this.FavCimsLastActivity.height = this.BubbleActivitySprite.height;
			this.FavCimsLastActivity.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.FavCimsLastActivity.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.FavCimsLastActivity.playAudioEvents = true;
			this.FavCimsLastActivity.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.FavCimsLastActivity.font.size = 15;
			this.FavCimsLastActivity.textScale = 0.75f;
			//this.FavCimsLastActivity.wordWrap = true;
			this.FavCimsLastActivity.textPadding.left = 0;
			this.FavCimsLastActivity.textPadding.right = 5;
			//this.FavCimsLastActivity.textPadding.left = (int)this.FavCimsLastActivitySprite.width + (int)this.FavCimsLastActivitySprite.relativePosition.x + 10;
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
			this.FavCimsLastActivity.maximumSize = new Vector2 (this.BubbleActivitySprite.width-40, this.BubbleActivitySprite.height);
			this.FavCimsLastActivity.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.FavCimsLastActivity.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.MyTargetID, eventParam);
			this.FavCimsLastActivity.relativePosition = new Vector3 (27, 1);
			
			//District Panel
			this.BubbleDistrictPanel = this.BubbleFamPortBgSprite.AddUIComponent<UIPanel> ();
			this.BubbleDistrictPanel.name = "BubbleDistrictPanel";
			this.BubbleDistrictPanel.width = 234;
			this.BubbleDistrictPanel.height = 15;
			this.BubbleDistrictPanel.relativePosition = new Vector3 (4, this.BubbleActivityPanel.relativePosition.y + 21);
			
			//District Background
			this.BubbleDistrictSprite = this.BubbleDistrictPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleDistrictSprite.name = "BubbleDistrictSprite";
			this.BubbleDistrictSprite.width = this.BubbleDistrictPanel.width;
			this.BubbleDistrictSprite.height = this.BubbleDistrictPanel.height;
			this.BubbleDistrictSprite.texture = TextureDB.BubbleBg1Special2;
			this.BubbleDistrictSprite.relativePosition = Vector3.zero;
			
			//District Label
			this.FavCimsDistrictLabel = this.BubbleDistrictSprite.AddUIComponent<UIButton> ();
			this.FavCimsDistrictLabel.name = "FavCimsDistrictLabel";
			this.FavCimsDistrictLabel.width = 60;
			this.FavCimsDistrictLabel.height = 15;
			this.FavCimsDistrictLabel.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.FavCimsDistrictLabel.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.FavCimsDistrictLabel.playAudioEvents = true;
			this.FavCimsDistrictLabel.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.FavCimsDistrictLabel.font.size = 15;
			this.FavCimsDistrictLabel.textScale = 0.70f;
			//this.FavCimsDistrictLabel.wordWrap = true;
			this.FavCimsDistrictLabel.textPadding.left = 0;
			this.FavCimsDistrictLabel.textPadding.right = 5;
			this.FavCimsDistrictLabel.outlineColor = new Color32 (0, 0, 0, 0);
			this.FavCimsDistrictLabel.outlineSize = 1;
			this.FavCimsDistrictLabel.textColor = new Color32 (153, 0, 0, 0);
			this.FavCimsDistrictLabel.isInteractive = false;
			//this.FavCimsDistrictLabel.hoveredTextColor = new Color32 (204, 102, 0, 20);
			//this.FavCimsDistrictLabel.pressedTextColor = new Color32 (153, 0, 0, 0);
			//this.FavCimsDistrictLabel.focusedTextColor = new Color32 (102, 153, 255, 147);
			//this.FavCimsDistrictLabel.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.FavCimsDistrictLabel.useDropShadow = true;
			this.FavCimsDistrictLabel.dropShadowOffset = new Vector2 (1, -1);
			this.FavCimsDistrictLabel.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.FavCimsDistrictLabel.relativePosition = new Vector3 (4, 1);
			
			//Now in this District
			this.FavCimsDistrictValue = this.BubbleDistrictSprite.AddUIComponent<UIButton> ();
			this.FavCimsDistrictValue.name = "FavCimsDistrictValue";
			this.FavCimsDistrictValue.width = this.BubbleDistrictPanel.width - 74;
			this.FavCimsDistrictValue.height = 15;
			this.FavCimsDistrictValue.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.FavCimsDistrictValue.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.FavCimsDistrictValue.playAudioEvents = true;
			this.FavCimsDistrictValue.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.FavCimsDistrictValue.font.size = 15;
			this.FavCimsDistrictValue.textScale = 0.70f;
			//this.FavCimsDistrictValue.wordWrap = true;
			this.FavCimsDistrictValue.textPadding.left = 0;
			this.FavCimsDistrictValue.textPadding.right = 5;
			this.FavCimsDistrictValue.outlineColor = new Color32 (0, 0, 0, 0);
			this.FavCimsDistrictValue.outlineSize = 1;
			this.FavCimsDistrictValue.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			//this.FavCimsDistrictValue.isInteractive = false;
			//this.FavCimsDistrictValue.hoveredTextColor = new Color32 (204, 102, 0, 20);
			//this.FavCimsDistrictValue.pressedTextColor = new Color32 (153, 0, 0, 0);
			//this.FavCimsDistrictValue.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.FavCimsDistrictValue.disabledTextColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.FavCimsDistrictValue.isEnabled = false;
			this.FavCimsDistrictValue.useDropShadow = true;
			this.FavCimsDistrictValue.dropShadowOffset = new Vector2 (1, -1);
			this.FavCimsDistrictValue.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.FavCimsDistrictValue.relativePosition = new Vector3 (64, 1);
			
			//Details & Problems Panel
			this.BubbleDetailsPanel = this.AddUIComponent<UIPanel>();
			this.BubbleDetailsPanel.name = "BubbleDetailsPanel";
			this.BubbleDetailsPanel.width = 235;
			this.BubbleDetailsPanel.height = 60;
			this.BubbleDetailsPanel.relativePosition = new Vector3(7,this.BubbleFamilyPortraitPanel.relativePosition.y+this.BubbleFamilyPortraitPanel.height+1);
			//Details & Problems Background Sprite
			this.BubbleDetailsBgSprite = this.BubbleDetailsPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleDetailsBgSprite.name = "BubbleFamPortBgSprite";
			this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
			this.BubbleDetailsBgSprite.relativePosition = Vector3.zero;
			
			//Home Icon
			this.BubbleHomeIcon = this.BubbleDetailsPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleHomeIcon.name = "FavCimsCitizenHomeSprite";
			this.BubbleHomeIcon.relativePosition = new Vector3 (10,10);
			this.BubbleHomeIcon.tooltipBox = UIView.GetAView().defaultTooltipBox;
			
			//Home Level
			this.BubbleHomeLevel = this.BubbleHomeIcon.AddUIComponent<UITextureSprite> ();
			this.BubbleHomeLevel.name = "FavCimsCitizenResidentialLevelSprite";
			this.BubbleHomeLevel.relativePosition = Vector3.zero;
			
			//Home Name & Level
			this.BubbleHomePanel = this.BubbleDetailsPanel.AddUIComponent<UIPanel>();
			this.BubbleHomePanel.name = "BubbleHomePanel";
			this.BubbleHomePanel.width = 181;
			this.BubbleHomePanel.height = 30;
			this.BubbleHomePanel.maximumSize = new Vector2(181,40);
			this.BubbleHomePanel.autoLayoutDirection = LayoutDirection.Horizontal;
			this.BubbleHomePanel.autoLayout = true;
			this.BubbleHomePanel.relativePosition = new Vector3(40,4);
			
			//Home Name
			this.BubbleHomeName = this.BubbleHomePanel.AddUIComponent<UIButton> ();
			this.BubbleHomeName.name = "BubbleHomeName";
			this.BubbleHomeName.width = this.BubbleHomePanel.width;
			this.BubbleHomeName.height = this.BubbleHomePanel.height;
			this.BubbleHomeName.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleHomeName.textHorizontalAlignment = UIHorizontalAlignment.Left;
			//this.BubbleHomeName.textPadding.left = 5;
			this.BubbleHomeName.playAudioEvents = true;
			this.BubbleHomeName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleHomeName.font.size = 15;
			this.BubbleHomeName.textScale = 0.90f;
			this.BubbleHomeName.wordWrap = true;
			this.BubbleHomeName.textPadding.left = 2;
			this.BubbleHomeName.textPadding.right = 5;
			this.BubbleHomeName.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleHomeName.outlineSize = 1;
			this.BubbleHomeName.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubbleHomeName.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleHomeName.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleHomeName.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleHomeName.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleHomeName.useDropShadow = true;
			this.BubbleHomeName.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleHomeName.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleHomeName.maximumSize = new Vector2 (this.BubbleHomePanel.width, this.BubbleHomePanel.height);
			this.BubbleHomeName.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleHomeName.text = "prova";
			this.BubbleHomeName.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToInstance (this.CitizenHomeID, eventParam);
			this.BubbleHomeName.relativePosition =  Vector3.zero;
			
			//Details & Problems Icons Panel
			this.BubbleDetailsIconsPanel = this.BubbleDetailsPanel.AddUIComponent<UIPanel>();
			this.BubbleDetailsIconsPanel.name = "BubbleDetailsIconsPanel";
			this.BubbleDetailsIconsPanel.width = 181;
			this.BubbleDetailsIconsPanel.height = 20;
			//this.BubbleDetailsIconsPanel.padding.left = 7;
			//this.BubbleDetailsIconsPanel.padding.right = 7;
			this.BubbleDetailsIconsPanel.maximumSize = new Vector2(181,30);
			this.BubbleDetailsIconsPanel.autoLayoutDirection = LayoutDirection.Horizontal;
			this.BubbleDetailsIconsPanel.autoLayout = true;
			this.BubbleDetailsIconsPanel.relativePosition = new Vector3(this.BubbleHomePanel.relativePosition.x, 30);
			
			//Details & Problems Icons Buttons;
			this.BubbleDetailsElettricity = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsElettricity.name = "BubbleDetailsElettricity";
			this.BubbleDetailsElettricity.normalBgSprite = "ToolbarIconElectricity";
			this.BubbleDetailsElettricity.width = 20;
			this.BubbleDetailsElettricity.height = 20;
			this.BubbleDetailsElettricity.playAudioEvents = false;
			this.BubbleDetailsElettricity.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsElettricity.isInteractive = false;
			
			this.BubbleDetailsWater = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsWater.name = "BubbleDetailsWater";
			this.BubbleDetailsWater.normalBgSprite = "IconPolicyWaterSaving";
			this.BubbleDetailsWater.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsWater.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsWater.playAudioEvents = false;
			this.BubbleDetailsWater.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsWater.isInteractive = false;
			
			this.BubbleDetailsLandValue = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsLandValue.name = "BubbleDetailsLandValue";
			this.BubbleDetailsLandValue.normalBgSprite = "InfoIconLandValue";
			this.BubbleDetailsLandValue.width = BubbleDetailsElettricity.width;
			this.BubbleDetailsLandValue.height = BubbleDetailsElettricity.height;
			this.BubbleDetailsLandValue.playAudioEvents = false;
			this.BubbleDetailsLandValue.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsLandValue.isInteractive = false;
			
			this.BubbleDetailsCrime = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsCrime.name = "BubbleDetailsCrime";
			this.BubbleDetailsCrime.normalBgSprite = "InfoIconCrime";
			this.BubbleDetailsCrime.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsCrime.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsCrime.playAudioEvents = false;
			this.BubbleDetailsCrime.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsCrime.isInteractive = false;
			
			this.BubbleDetailsNoise = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsNoise.name = "BubbleDetailsNoise";
			this.BubbleDetailsNoise.normalBgSprite = "InfoIconNoisePollution";
			this.BubbleDetailsNoise.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsNoise.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsNoise.playAudioEvents = false;
			this.BubbleDetailsNoise.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsNoise.isInteractive = false;
			
			this.BubbleDetailsGarbage = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsGarbage.name = "BubbleDetailsGarbage";
			this.BubbleDetailsGarbage.normalBgSprite = "InfoIconGarbage";
			this.BubbleDetailsGarbage.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsGarbage.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsGarbage.playAudioEvents = false;
			this.BubbleDetailsGarbage.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsGarbage.isInteractive = false;
			
			this.BubbleDetailsDeath = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsDeath.name = "BubbleDetailsDeath";
			this.BubbleDetailsDeath.normalBgSprite = "NotificationIconVerySick";
			this.BubbleDetailsDeath.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsDeath.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsDeath.playAudioEvents = false;
			this.BubbleDetailsDeath.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsDeath.isInteractive = false;
			
			this.BubbleDetailsFire = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsFire.name = "BubbleDetailsFire";
			this.BubbleDetailsFire.normalBgSprite = "ToolbarIconFireDepartment";
			this.BubbleDetailsFire.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsFire.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsFire.playAudioEvents = false;
			this.BubbleDetailsFire.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsFire.isInteractive = false;
			
			this.BubbleDetailsPollution = this.BubbleDetailsIconsPanel.AddUIComponent<UIButton> ();
			this.BubbleDetailsPollution.name = "BubbleDetailsPollution";
			this.BubbleDetailsPollution.normalBgSprite = "InfoIconPollution";
			this.BubbleDetailsPollution.width = this.BubbleDetailsElettricity.width;
			this.BubbleDetailsPollution.height = this.BubbleDetailsElettricity.height;
			this.BubbleDetailsPollution.playAudioEvents = false;
			this.BubbleDetailsPollution.tooltipBox = UIView.GetAView().defaultTooltipBox;
			//this.BubbleDetailsPollution.isInteractive = false;
			
			//Header Family Bar Panel
			this.BubbleFamilyBarPanel = this.AddUIComponent<UIPanel>();
			this.BubbleFamilyBarPanel.name = "BubbleFamilyBarPanel";
			this.BubbleFamilyBarPanel.width = 236;
			this.BubbleFamilyBarPanel.height = 20;
			this.BubbleFamilyBarPanel.relativePosition = new Vector3(7,this.BubbleDetailsPanel.relativePosition.y + this.BubbleDetailsPanel.height + 2);
			
			//Header Family Bar Bg
			this.BubbleFamilyBarPanelBg = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyBarPanelBg.name = "BubbleFamilyBarPanelBg";
			this.BubbleFamilyBarPanelBg.width = this.BubbleFamilyBarPanel.width;
			this.BubbleFamilyBarPanelBg.height = this.BubbleFamilyBarPanel.height;
			this.BubbleFamilyBarPanelBg.texture = TextureDB.BubbleBgBarHover;
			this.BubbleFamilyBarPanelBg.relativePosition =  Vector3.zero;
			//Header Family Label
			this.BubbleFamilyBarLabel = this.BubbleFamilyBarPanel.AddUIComponent<UILabel> ();
			this.BubbleFamilyBarLabel.name = "BubbleFamilyBarLabel";
			this.BubbleFamilyBarLabel.height = this.BubbleFamilyBarPanel.height;
			this.BubbleFamilyBarLabel.width = 221;
			this.BubbleFamilyBarLabel.font.size = 11;
			this.BubbleFamilyBarLabel.textScale = 1f;
			this.BubbleFamilyBarLabel.textColor = new Color32 (102, 0, 51, 220);
			this.BubbleFamilyBarLabel.relativePosition = new Vector3(7,2);
			//Header Family Dog SpriteButton
			this.BubbleFamilyBarDogButton = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyBarDogButton.name = "BubbleFamilyBarDogButton";
			this.BubbleFamilyBarDogButton.texture = TextureDB.BubbleDogDisabled;
			this.BubbleFamilyBarDogButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleFamilyBarDogButton.relativePosition = new Vector3(175,0);
			this.BubbleFamilyBarDogButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToInstance(this.MyPetID, eventParam);
			//Header Family Car SpriteButton
			this.BubbleFamilyBarCarButton = this.BubbleFamilyBarPanel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyBarCarButton.name = "BubbleFamilyBarCarButton";
			this.BubbleFamilyBarCarButton.texture = TextureDB.BubbleCarDisabled;
			this.BubbleFamilyBarCarButton.tooltipBox = UIView.GetAView().defaultTooltipBox;
			this.BubbleFamilyBarCarButton.relativePosition = new Vector3(this.BubbleFamilyBarDogButton.relativePosition.x + this.BubbleFamilyBarDogButton.width + 10,0);
			this.BubbleFamilyBarCarButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToInstance(this.FamilyVehicleID, eventParam);
			//Family Panel
			this.BubbleFamilyPanel = this.AddUIComponent<UIPanel>();
			this.BubbleFamilyPanel.name = "BubbleFamilyPanel";
			this.BubbleFamilyPanel.width = 236;
			this.BubbleFamilyPanel.height = 212;
			this.BubbleFamilyPanel.clipChildren = true;
			this.BubbleFamilyPanel.padding = new RectOffset(0,0,0,0);
			this.BubbleFamilyPanel.autoLayout = true;
			this.BubbleFamilyPanel.autoLayoutDirection = LayoutDirection.Vertical;
			this.BubbleFamilyPanel.relativePosition = new Vector3(7,this.BubbleFamilyBarPanel.relativePosition.y + this.BubbleFamilyBarPanel.height);
			
			//No partner panel
			this.NoPartnerPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.NoPartnerPanel.name = "NoPartnerPanel";
			this.NoPartnerPanel.width = this.BubbleFamilyPanel.width;
			this.NoPartnerPanel.height = 52;
			this.NoPartnerPanel.Hide ();
			//No partner button sprite
			this.NoPartnerBSprite = this.NoPartnerPanel.AddUIComponent<UIButton> ();
			this.NoPartnerBSprite.name = "NoPartnerBSprite";
			this.NoPartnerBSprite.normalBgSprite = "InfoIconHealthDisabled";
			this.NoPartnerBSprite.width = 36;
			this.NoPartnerBSprite.height = 36;
			this.NoPartnerBSprite.relativePosition = new Vector3 (7, 5);
			//No partner fake button
			this.NoPartnerFButton = this.NoPartnerPanel.AddUIComponent<UIButton> ();
			this.NoPartnerFButton.name = "NoPartnerFButton";
			this.NoPartnerFButton.width = 155;
			this.NoPartnerFButton.height = this.NoPartnerBSprite.height;
			this.NoPartnerFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.NoPartnerFButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.NoPartnerFButton.playAudioEvents = false;
			this.NoPartnerFButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.NoPartnerFButton.font.size = 16;
			this.NoPartnerFButton.textScale = 0.9f;
			this.NoPartnerFButton.wordWrap = true;
			this.NoPartnerFButton.useDropShadow = true;
			this.NoPartnerFButton.dropShadowOffset = new Vector2 (1, -1);
			this.NoPartnerFButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.NoPartnerFButton.textPadding.left = 5;
			this.NoPartnerFButton.textPadding.right = 5;
			this.NoPartnerFButton.isEnabled = false;
			this.NoPartnerFButton.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.NoPartnerFButton.relativePosition = new Vector3 (this.NoPartnerBSprite.relativePosition.x + this.NoPartnerBSprite.width, this.NoPartnerBSprite.relativePosition.y);
			
			//x 4 Rows
			//Partner Panel
			this.PartnerPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.PartnerPanel.name = "PartnerPanel";
			this.PartnerPanel.width = this.BubbleFamilyPanel.width;
			this.PartnerPanel.height = 52;
			this.PartnerPanel.clipChildren = true;
			this.PartnerPanel.padding = new RectOffset(0,0,0,0);
			this.PartnerPanel.autoLayout = true;
			this.PartnerPanel.autoLayoutDirection = LayoutDirection.Vertical;
			//Partner Background Bar
			this.BubblePartnerBgBar = this.PartnerPanel.AddUIComponent<UITextureSprite> ();
			this.BubblePartnerBgBar.name = "BubblePartnerBgBar";
			this.BubblePartnerBgBar.width = this.PartnerPanel.width;
			this.BubblePartnerBgBar.height = 26;
			this.BubblePartnerBgBar.texture = TextureDB.BubbleBgBar1;
			//Partner Fake Button Hearth
			this.BubblePartnerLove = this.BubblePartnerBgBar.AddUIComponent<UIButton> ();
			this.BubblePartnerLove.name = "BubblePartnerLove";
			this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
			this.BubblePartnerLove.width = 22;
			this.BubblePartnerLove.height = 22;
			this.BubblePartnerLove.isInteractive = false;
			this.BubblePartnerLove.relativePosition = new Vector3 (7, 2);
			//Partner Name Button
			this.BubblePartnerName = this.BubblePartnerBgBar.AddUIComponent<UIButton> ();
			this.BubblePartnerName.name = "BubblePartnerName";
			this.BubblePartnerName.width = 135;
			this.BubblePartnerName.height = this.BubblePartnerBgBar.height;
			this.BubblePartnerName.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubblePartnerName.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubblePartnerName.playAudioEvents = true;
			this.BubblePartnerName.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubblePartnerName.font.size = 15;
			this.BubblePartnerName.textScale = 0.80f;
			this.BubblePartnerName.wordWrap = true;
			this.BubblePartnerName.useDropShadow = true;
			this.BubblePartnerName.dropShadowOffset = new Vector2 (1, -1);
			this.BubblePartnerName.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubblePartnerName.textPadding.left = 5;
			this.BubblePartnerName.textPadding.right = 5;
			this.BubblePartnerName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubblePartnerName.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubblePartnerName.pressedTextColor = new Color32 (102, 153, 255, 147);
			this.BubblePartnerName.focusedTextColor = new Color32 (153, 0, 0, 0);
			this.BubblePartnerName.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubblePartnerName.relativePosition = new Vector3 (this.BubblePartnerLove.relativePosition.x + this.BubblePartnerLove.width, 2);
			this.BubblePartnerName.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(this.PartnerID, eventParam);
			//Partner Real Age Button
			this.BubbleParnerAgeButton = this.BubblePartnerBgBar.AddUIComponent<UIButton> ();
			this.BubbleParnerAgeButton.name = "BubbleParnerAgeButton";
			this.BubbleParnerAgeButton.width = 23;
			this.BubbleParnerAgeButton.height = 18;
			this.BubbleParnerAgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleParnerAgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleParnerAgeButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleParnerAgeButton.textScale = 0.90f;
			this.BubbleParnerAgeButton.font.size = 15;
			this.BubbleParnerAgeButton.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleParnerAgeButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			//this.BubbleParnerAgeButton.text = "99"; //Real Age test
			//this.BubbleParnerAgeButton.normalBgSprite = "GenericPanel";
			this.BubbleParnerAgeButton.isInteractive = false;
			this.BubbleParnerAgeButton.relativePosition = new Vector3 (this.BubblePartnerName.relativePosition.x + this.BubblePartnerName.width + 6, 6);
			//Partner Follow Texture Sprite
			this.BubblePartnerFollowToggler = this.BubblePartnerBgBar.AddUIComponent<UIButton> ();
			this.BubblePartnerFollowToggler.name = "BubblePartnerFollowToggler";
			this.BubblePartnerFollowToggler.atlas = m_atlas;
			this.BubblePartnerFollowToggler.size = new Vector2(18,18);
			this.BubblePartnerFollowToggler.playAudioEvents = true;
			this.BubblePartnerFollowToggler.relativePosition = new Vector3 (this.BubbleParnerAgeButton.relativePosition.x + this.BubbleParnerAgeButton.width + 12, 4);
			this.BubblePartnerFollowToggler.eventClick += (component, eventParam) => { 
				FavCimsCore.AddToFavorites (this.PartnerID);
			};

			//Partner Activity Background Bar
			this.BubblePartnerActivityBar = this.PartnerPanel.AddUIComponent<UITextureSprite> ();
			this.BubblePartnerActivityBar.name = "BubblePartnerActivityBar";
			this.BubblePartnerActivityBar.width = this.PartnerPanel.width;
			this.BubblePartnerActivityBar.height = 26;
			this.BubblePartnerActivityBar.texture = TextureDB.BubbleBgBar2;
			//Partner Activity Vehicle Button
			this.BubblePartnerVehicleButton = this.BubblePartnerActivityBar.AddUIComponent<UIButton> ();
			this.BubblePartnerVehicleButton.name = "BubblePartnerVehicleButton";
			this.BubblePartnerVehicleButton.width = 22;
			this.BubblePartnerVehicleButton.height = 22;
			this.BubblePartnerVehicleButton.relativePosition = new Vector3 (7, 2);
			this.BubblePartnerVehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.PartnerVehID, eventParam);
			//Partner Activity Destination
			this.BubblePartnerDestination = this.BubblePartnerActivityBar.AddUIComponent<UIButton> ();
			this.BubblePartnerDestination.name = "BubblePartnerDestination";
			this.BubblePartnerDestination.width = this.BubblePartnerActivityBar.width-this.BubblePartnerVehicleButton.width;
			this.BubblePartnerDestination.height = this.BubblePartnerActivityBar.height;
			this.BubblePartnerDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubblePartnerDestination.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubblePartnerDestination.playAudioEvents = true;
			this.BubblePartnerDestination.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubblePartnerDestination.font.size = 15;
			this.BubblePartnerDestination.textScale = 0.75f;
			this.BubblePartnerDestination.wordWrap = true;
			this.BubblePartnerDestination.textPadding.left = 0;
			this.BubblePartnerDestination.textPadding.right = 5;
			this.BubblePartnerDestination.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubblePartnerDestination.outlineSize = 1;
			this.BubblePartnerDestination.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubblePartnerDestination.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubblePartnerDestination.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubblePartnerDestination.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubblePartnerDestination.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubblePartnerDestination.useDropShadow = true;
			this.BubblePartnerDestination.dropShadowOffset = new Vector2 (1, -1);
			this.BubblePartnerDestination.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubblePartnerDestination.maximumSize = new Vector2 (this.BubblePartnerDestination.width, this.BubblePartnerActivityBar.height);
			this.BubblePartnerDestination.relativePosition = new Vector3 (this.BubblePartnerVehicleButton.relativePosition.x + this.BubblePartnerVehicleButton.width +5, 2);
			this.BubblePartnerDestination.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.PartnerTarget, eventParam);
			//Parent 1 Panel
			this.Parent1Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.Parent1Panel.name = "PartnerPanel";
			this.Parent1Panel.width = this.BubbleFamilyPanel.width;
			this.Parent1Panel.height = 52;
			this.Parent1Panel.clipChildren = true;
			this.Parent1Panel.padding = new RectOffset(0,0,0,0);
			this.Parent1Panel.autoLayout = true;
			this.Parent1Panel.autoLayoutDirection = LayoutDirection.Vertical;
			this.Parent1Panel.relativePosition = new Vector3(0,0);
			this.Parent1Panel.Hide ();
			//Parent1 Background Bar
			this.BubbleParent1BgBar = this.Parent1Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleParent1BgBar.name = "BubbleParent1BgBar";
			this.BubbleParent1BgBar.width = this.Parent1Panel.width;
			this.BubbleParent1BgBar.height = 26;
			this.BubbleParent1BgBar.texture = TextureDB.BubbleBgBar1;
			//Parent1 Fake Button Hearth
			this.BubbleParent1Love = this.BubbleParent1BgBar.AddUIComponent<UIButton> ();
			this.BubbleParent1Love.name = "BubbleParent1Love";
			this.BubbleParent1Love.normalBgSprite = "InfoIconAge";
			this.BubbleParent1Love.width = 22;
			this.BubbleParent1Love.height = 22;
			this.BubbleParent1Love.isInteractive = false;
			this.BubbleParent1Love.relativePosition = new Vector3 (7, 2);
			//Parent1 Name Button
			this.BubbleParent1Name = this.BubbleParent1BgBar.AddUIComponent<UIButton> ();
			this.BubbleParent1Name.name = "BubbleParent1Name";
			this.BubbleParent1Name.width = 135;
			this.BubbleParent1Name.height = this.BubbleParent1BgBar.height;
			this.BubbleParent1Name.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleParent1Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleParent1Name.playAudioEvents = true;
			this.BubbleParent1Name.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleParent1Name.font.size = 15;
			this.BubbleParent1Name.textScale = 0.80f;
			this.BubbleParent1Name.wordWrap = true;
			this.BubbleParent1Name.useDropShadow = true;
			this.BubbleParent1Name.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleParent1Name.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleParent1Name.textPadding.left = 5;
			this.BubbleParent1Name.textPadding.right = 5;
			this.BubbleParent1Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleParent1Name.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleParent1Name.pressedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleParent1Name.focusedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleParent1Name.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleParent1Name.relativePosition = new Vector3 (this.BubbleParent1Love.relativePosition.x + this.BubbleParent1Love.width, 2);
			this.BubbleParent1Name.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(this.Parent1ID, eventParam);
			//Parent1 Real Age Button
			this.BubbleParent1AgeButton = this.BubbleParent1BgBar.AddUIComponent<UIButton> ();
			this.BubbleParent1AgeButton.name = "BubbleParent1AgeButton";
			this.BubbleParent1AgeButton.width = 23;
			this.BubbleParent1AgeButton.height = 18;
			this.BubbleParent1AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleParent1AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleParent1AgeButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleParent1AgeButton.textScale = 0.90f;
			this.BubbleParent1AgeButton.font.size = 15;
			this.BubbleParent1AgeButton.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleParent1AgeButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			//this.BubbleParent1AgeButton.text = "99"; //Real Age test
			//this.BubbleParent1AgeButton.normalBgSprite = "GenericPanel";
			this.BubbleParent1AgeButton.isInteractive = false;
			this.BubbleParent1AgeButton.relativePosition = new Vector3 (this.BubbleParent1Name.relativePosition.x + this.BubbleParent1Name.width + 6, 6);
			//Parent1 Follow Texture Sprite
			this.BubbleParent1FollowToggler = this.BubbleParent1BgBar.AddUIComponent<UIButton> ();
			this.BubbleParent1FollowToggler.name = "BubbleParent1FollowToggler";
			this.BubbleParent1FollowToggler.atlas = m_atlas;
			this.BubbleParent1FollowToggler.size = new Vector2 (18, 18);
			this.BubbleParent1FollowToggler.playAudioEvents = true;
			this.BubbleParent1FollowToggler.relativePosition = new Vector3 (this.BubbleParent1AgeButton.relativePosition.x + this.BubbleParent1AgeButton.width + 12, 4);
			this.BubbleParent1FollowToggler.eventClick += (component, eventParam) => { 
				FavCimsCore.AddToFavorites (this.Parent1ID);
			};
			//Parent1 Activity Background Bar
			this.BubbleParent1ActivityBar = this.Parent1Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleParent1ActivityBar.name = "BubbleParent1ActivityBar";
			this.BubbleParent1ActivityBar.width = this.Parent1Panel.width;
			this.BubbleParent1ActivityBar.height = 26;
			this.BubbleParent1ActivityBar.texture = TextureDB.BubbleBgBar2;
			//Parent1 Activity Vehicle Button
			this.BubbleParent1VehicleButton = this.BubbleParent1ActivityBar.AddUIComponent<UIButton> ();
			this.BubbleParent1VehicleButton.name = "BubbleParent1VehicleButton";
			this.BubbleParent1VehicleButton.width = 22;
			this.BubbleParent1VehicleButton.height = 22;
			this.BubbleParent1VehicleButton.relativePosition = new Vector3 (7, 2);
			this.BubbleParent1VehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent1VehID, eventParam);
			//Parent1 Activity Destination
			this.BubbleParent1Destination = this.BubbleParent1ActivityBar.AddUIComponent<UIButton> ();
			this.BubbleParent1Destination.name = "BubbleParent1Destination";
			this.BubbleParent1Destination.width = this.BubbleParent1ActivityBar.width-this.BubbleParent1VehicleButton.width;
			this.BubbleParent1Destination.height = this.BubbleParent1ActivityBar.height;
			this.BubbleParent1Destination.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleParent1Destination.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleParent1Destination.playAudioEvents = true;
			this.BubbleParent1Destination.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleParent1Destination.font.size = 15;
			this.BubbleParent1Destination.textScale = 0.75f;
			this.BubbleParent1Destination.wordWrap = true;
			this.BubbleParent1Destination.textPadding.left = 0;
			this.BubbleParent1Destination.textPadding.right = 5;
			this.BubbleParent1Destination.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleParent1Destination.outlineSize = 1;
			this.BubbleParent1Destination.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubbleParent1Destination.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleParent1Destination.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleParent1Destination.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleParent1Destination.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleParent1Destination.useDropShadow = true;
			this.BubbleParent1Destination.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleParent1Destination.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleParent1Destination.maximumSize = new Vector2 (this.BubbleParent1Destination.width, this.BubbleParent1ActivityBar.height);
			this.BubbleParent1Destination.relativePosition = new Vector3 (this.BubblePartnerDestination.relativePosition.x, 2);
			this.BubbleParent1Destination.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent1Target, eventParam);
			
			//No Child Panel
			this.NoChildsPanel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.NoChildsPanel.name = "NoChildsPanel";
			this.NoChildsPanel.width = this.BubbleFamilyPanel.width;
			this.NoChildsPanel.height = 52;
			this.NoChildsPanel.Hide ();
			//No Child button sprite
			this.NoChildsBSprite = this.NoChildsPanel.AddUIComponent<UIButton> ();
			this.NoChildsBSprite.name = "NoChildsBSprite";
			this.NoChildsBSprite.normalBgSprite = "InfoIconHappinessDisabled";
			this.NoChildsBSprite.width = 36;
			this.NoChildsBSprite.height = 36;
			this.NoChildsBSprite.relativePosition = new Vector3 (7, 5);
			//No Childs fake button
			this.NoChildsFButton = this.NoChildsPanel.AddUIComponent<UIButton> ();
			this.NoChildsFButton.name = "NoChildsFButton";
			this.NoChildsFButton.width = 155;
			this.NoChildsFButton.height = this.NoChildsBSprite.height;
			this.NoChildsFButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.NoChildsFButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.NoChildsFButton.playAudioEvents = false;
			this.NoChildsFButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.NoChildsFButton.font.size = 16;
			this.NoChildsFButton.textScale = 0.9f;
			this.NoChildsFButton.wordWrap = true;
			this.NoChildsFButton.useDropShadow = true;
			this.NoChildsFButton.dropShadowOffset = new Vector2 (1, -1);
			this.NoChildsFButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.NoChildsFButton.textPadding.left = 5;
			this.NoChildsFButton.textPadding.right = 5;
			this.NoChildsFButton.isEnabled = false;
			this.NoChildsFButton.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.NoChildsFButton.relativePosition = new Vector3 (this.NoChildsBSprite.relativePosition.x + this.NoChildsBSprite.width, this.NoChildsBSprite.relativePosition.y);
			
			//Parent 2 Panel
			this.FamilyMember2Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.FamilyMember2Panel.name = "FamilyMember2Panel";
			this.FamilyMember2Panel.width = this.BubbleFamilyPanel.width;
			this.FamilyMember2Panel.height = 52;
			this.FamilyMember2Panel.clipChildren = true;
			this.FamilyMember2Panel.padding = new RectOffset(0,0,0,0);
			this.FamilyMember2Panel.autoLayout = true;
			this.FamilyMember2Panel.autoLayoutDirection = LayoutDirection.Vertical;
			this.FamilyMember2Panel.relativePosition = new Vector3(0,0);
			this.FamilyMember2Panel.Hide ();
			//Family 2 Background bar
			this.BubbleFamilyMember2BgBar = this.FamilyMember2Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember2BgBar.name = "BubbleFamilyMember2BgBar";
			this.BubbleFamilyMember2BgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember2BgBar.height = 26;
			this.BubbleFamilyMember2BgBar.texture = TextureDB.BubbleBgBar1;
			//Family 2 icon sprite
			this.BubbleFamilyMember2IconSprite = this.BubbleFamilyMember2BgBar.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember2IconSprite.name = "BubbleFamilyMember2IconSprite";
			this.BubbleFamilyMember2IconSprite.width = 18;
			this.BubbleFamilyMember2IconSprite.height = 18;
			this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
			this.BubbleFamilyMember2IconSprite.relativePosition = new Vector3(7,4);
			//Family 2 name button
			this.BubbleFamilyMember2Name = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember2Name.name = "BubbleFamilyMember2Name";
			this.BubbleFamilyMember2Name.width = 135;
			this.BubbleFamilyMember2Name.height = this.BubbleFamilyMember2BgBar.height;
			this.BubbleFamilyMember2Name.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember2Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember2Name.playAudioEvents = true;
			this.BubbleFamilyMember2Name.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember2Name.font.size = 15;
			this.BubbleFamilyMember2Name.textScale = 0.80f;
			this.BubbleFamilyMember2Name.wordWrap = true;
			this.BubbleFamilyMember2Name.useDropShadow = true;
			this.BubbleFamilyMember2Name.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember2Name.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember2Name.textPadding.left = 5;
			this.BubbleFamilyMember2Name.textPadding.right = 5;
			this.BubbleFamilyMember2Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleFamilyMember2Name.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember2Name.pressedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember2Name.focusedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember2Name.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember2Name.relativePosition = new Vector3 (this.BubbleFamilyMember2IconSprite.relativePosition.x + this.BubbleFamilyMember2IconSprite.width+2, 2);
			this.BubbleFamilyMember2Name.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(this.Parent2ID, eventParam);
			//Family Member2 Real Age Button
			this.BubbleFamilyMember2AgeButton = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember2AgeButton.name = "BubbleFamilyMember2AgeButton";
			this.BubbleFamilyMember2AgeButton.width = 23;
			this.BubbleFamilyMember2AgeButton.height = 18;
			this.BubbleFamilyMember2AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleFamilyMember2AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember2AgeButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember2AgeButton.textScale = 0.90f;
			this.BubbleFamilyMember2AgeButton.font.size = 15;
			this.BubbleFamilyMember2AgeButton.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember2AgeButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			//this.BubbleFamilyMember2AgeButton.text = "99"; //Real Age test
			//this.BubbleFamilyMember2AgeButton.normalBgSprite = "GenericPanel";
			this.BubbleFamilyMember2AgeButton.isInteractive = false;
			this.BubbleFamilyMember2AgeButton.relativePosition = new Vector3 (this.BubbleFamilyMember2Name.relativePosition.x + this.BubbleFamilyMember2Name.width + 6, 6);
			//Family 2 Follow Texture Sprite
			this.BubbleFamilyMember2FollowToggler = this.BubbleFamilyMember2BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember2FollowToggler.name = "BubbleFamilyMember2FollowToggler";
			this.BubbleFamilyMember2FollowToggler.atlas = m_atlas;
			this.BubbleFamilyMember2FollowToggler.size = new Vector2 (18, 18);
			this.BubbleFamilyMember2FollowToggler.playAudioEvents = true;
			this.BubbleFamilyMember2FollowToggler.relativePosition = new Vector3 (this.BubbleFamilyMember2AgeButton.relativePosition.x + this.BubbleFamilyMember2AgeButton.width + 12, 4);
			this.BubbleFamilyMember2FollowToggler.eventClick += (component, eventParam) => { 
				FavCimsCore.AddToFavorites (this.Parent2ID);
			};
			//Family 2 Activity background
			this.BubbleFamilyMember2ActivityBgBar = this.FamilyMember2Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember2ActivityBgBar.name = "BubbleFamilyMember2ActivityBgBar";
			this.BubbleFamilyMember2ActivityBgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember2ActivityBgBar.height = 26;
			this.BubbleFamilyMember2ActivityBgBar.texture = TextureDB.BubbleBgBar2;
			//Family 2 Activity Vehicle Button
			this.BubbleFamilyMember2ActivityVehicleButton = this.BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember2ActivityVehicleButton.name = "BubbleFamilyMember2ActivityVehicleButton";
			this.BubbleFamilyMember2ActivityVehicleButton.width = 22;
			this.BubbleFamilyMember2ActivityVehicleButton.height = 22;
			this.BubbleFamilyMember2ActivityVehicleButton.relativePosition = new Vector3 (7, 2);
			this.BubbleFamilyMember2ActivityVehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent2VehID, eventParam);
			//Family 2 Activity Destination
			this.BubbleFamilyMember2ActivityDestination = this.BubbleFamilyMember2ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember2ActivityDestination.name = "BubbleFamilyMember2ActivityDestination";
			this.BubbleFamilyMember2ActivityDestination.width = this.BubbleFamilyMember2ActivityBgBar.width-this.BubbleFamilyMember2ActivityVehicleButton.width;
			this.BubbleFamilyMember2ActivityDestination.height = this.BubbleFamilyMember2ActivityBgBar.height;
			this.BubbleFamilyMember2ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember2ActivityDestination.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember2ActivityDestination.playAudioEvents = true;
			this.BubbleFamilyMember2ActivityDestination.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember2ActivityDestination.font.size = 15;
			this.BubbleFamilyMember2ActivityDestination.textScale = 0.75f;
			this.BubbleFamilyMember2ActivityDestination.wordWrap = true;
			this.BubbleFamilyMember2ActivityDestination.textPadding.left = 0;
			this.BubbleFamilyMember2ActivityDestination.textPadding.right = 5;
			this.BubbleFamilyMember2ActivityDestination.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember2ActivityDestination.outlineSize = 1;
			this.BubbleFamilyMember2ActivityDestination.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubbleFamilyMember2ActivityDestination.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember2ActivityDestination.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember2ActivityDestination.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember2ActivityDestination.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember2ActivityDestination.useDropShadow = true;
			this.BubbleFamilyMember2ActivityDestination.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember2ActivityDestination.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember2ActivityDestination.maximumSize = new Vector2 (this.BubbleFamilyMember2ActivityDestination.width, this.BubbleFamilyMember2ActivityBgBar.height);
			this.BubbleFamilyMember2ActivityDestination.relativePosition = new Vector3 (this.BubblePartnerDestination.relativePosition.x, 2);
			this.BubbleFamilyMember2ActivityDestination.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent2Target, eventParam);
			
			//Parent 3 Panel
			this.FamilyMember3Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.FamilyMember3Panel.name = "FamilyMember3Panel";
			this.FamilyMember3Panel.width = this.BubbleFamilyPanel.width;
			this.FamilyMember3Panel.height = 52;
			this.FamilyMember3Panel.clipChildren = true;
			this.FamilyMember3Panel.padding = new RectOffset(0,0,0,0);
			this.FamilyMember3Panel.autoLayout = true;
			this.FamilyMember3Panel.autoLayoutDirection = LayoutDirection.Vertical;
			this.FamilyMember3Panel.relativePosition = new Vector3(0,0);
			this.FamilyMember3Panel.Hide ();
			//Family 3 Background bar
			this.BubbleFamilyMember3BgBar = this.FamilyMember3Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember3BgBar.name = "BubbleFamilyMember3BgBar";
			this.BubbleFamilyMember3BgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember3BgBar.height = 26;
			this.BubbleFamilyMember3BgBar.texture = TextureDB.BubbleBgBar1;
			//Family 3 icon sprite
			this.BubbleFamilyMember3IconSprite = this.BubbleFamilyMember3BgBar.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember3IconSprite.name = "BubbleFamilyMember3IconSprite";
			this.BubbleFamilyMember3IconSprite.width = 18;
			this.BubbleFamilyMember3IconSprite.height = 18;
			this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
			this.BubbleFamilyMember3IconSprite.relativePosition = new Vector3(7,4);
			//Family 3 name button
			this.BubbleFamilyMember3Name = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember3Name.name = "BubbleFamilyMember3Name";
			this.BubbleFamilyMember3Name.width = 135;
			this.BubbleFamilyMember3Name.height = this.BubbleFamilyMember3BgBar.height;
			this.BubbleFamilyMember3Name.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember3Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember3Name.playAudioEvents = true;
			this.BubbleFamilyMember3Name.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember3Name.font.size = 15;
			this.BubbleFamilyMember3Name.textScale = 0.80f;
			this.BubbleFamilyMember3Name.wordWrap = true;
			this.BubbleFamilyMember3Name.useDropShadow = true;
			this.BubbleFamilyMember3Name.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember3Name.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember3Name.textPadding.left = 5;
			this.BubbleFamilyMember3Name.textPadding.right = 5;
			this.BubbleFamilyMember3Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleFamilyMember3Name.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember3Name.pressedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember3Name.focusedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember3Name.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember3Name.relativePosition = new Vector3 (this.BubbleFamilyMember3IconSprite.relativePosition.x + this.BubbleFamilyMember3IconSprite.width+2, 2);
			this.BubbleFamilyMember3Name.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(this.Parent3ID, eventParam);
			//Family Member3 Real Age Button
			this.BubbleFamilyMember3AgeButton = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember3AgeButton.name = "BubbleFamilyMember3AgeButton";
			this.BubbleFamilyMember3AgeButton.width = 23;
			this.BubbleFamilyMember3AgeButton.height = 18;
			this.BubbleFamilyMember3AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleFamilyMember3AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember3AgeButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember3AgeButton.textScale = 0.90f;
			this.BubbleFamilyMember3AgeButton.font.size = 15;
			this.BubbleFamilyMember3AgeButton.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember3AgeButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			//this.BubbleFamilyMember3AgeButton.text = "99"; //Real Age test
			//this.BubbleFamilyMember3AgeButton.normalBgSprite = "GenericPanel";
			this.BubbleFamilyMember3AgeButton.isInteractive = false;
			this.BubbleFamilyMember3AgeButton.relativePosition = new Vector3 (this.BubbleFamilyMember3Name.relativePosition.x + this.BubbleFamilyMember3Name.width + 6, 6);
			//Family 3 Follow Texture Sprite
			this.BubbleFamilyMember3FollowToggler = this.BubbleFamilyMember3BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember3FollowToggler.name = "BubbleFamilyMember3FollowToggler";
			this.BubbleFamilyMember3FollowToggler.atlas = m_atlas;
			this.BubbleFamilyMember3FollowToggler.size = new Vector2 (18, 18);
			this.BubbleFamilyMember3FollowToggler.playAudioEvents = true;
			this.BubbleFamilyMember3FollowToggler.relativePosition = new Vector3 (this.BubbleFamilyMember3AgeButton.relativePosition.x + this.BubbleFamilyMember3AgeButton.width + 12, 4);
			this.BubbleFamilyMember3FollowToggler.eventClick += (component, eventParam) => { 
				FavCimsCore.AddToFavorites (this.Parent3ID);
			};
			//Family 3 Activity background
			this.BubbleFamilyMember3ActivityBgBar = this.FamilyMember3Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember3ActivityBgBar.name = "BubbleFamilyMember3ActivityBgBar";
			this.BubbleFamilyMember3ActivityBgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember3ActivityBgBar.height = 26;
			this.BubbleFamilyMember3ActivityBgBar.texture = TextureDB.BubbleBgBar2;
			//Family 3 Activity Vehicle Button
			this.BubbleFamilyMember3ActivityVehicleButton = this.BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember3ActivityVehicleButton.name = "BubbleFamilyMember3ActivityVehicleButton";
			this.BubbleFamilyMember3ActivityVehicleButton.width = 22;
			this.BubbleFamilyMember3ActivityVehicleButton.height = 22;
			this.BubbleFamilyMember3ActivityVehicleButton.relativePosition = new Vector3 (7, 2);
			this.BubbleFamilyMember3ActivityVehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent3VehID, eventParam);
			//Family 3 Activity Destination
			this.BubbleFamilyMember3ActivityDestination = this.BubbleFamilyMember3ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember3ActivityDestination.name = "BubbleFamilyMember3ActivityDestination";
			this.BubbleFamilyMember3ActivityDestination.width = this.BubbleFamilyMember3ActivityBgBar.width-this.BubbleFamilyMember3ActivityVehicleButton.width;
			this.BubbleFamilyMember3ActivityDestination.height = this.BubbleFamilyMember3ActivityBgBar.height;
			this.BubbleFamilyMember3ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember3ActivityDestination.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember3ActivityDestination.playAudioEvents = true;
			this.BubbleFamilyMember3ActivityDestination.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember3ActivityDestination.font.size = 15;
			this.BubbleFamilyMember3ActivityDestination.textScale = 0.75f;
			this.BubbleFamilyMember3ActivityDestination.wordWrap = true;
			this.BubbleFamilyMember3ActivityDestination.textPadding.left = 0;
			this.BubbleFamilyMember3ActivityDestination.textPadding.right = 5;
			this.BubbleFamilyMember3ActivityDestination.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember3ActivityDestination.outlineSize = 1;
			this.BubbleFamilyMember3ActivityDestination.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubbleFamilyMember3ActivityDestination.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember3ActivityDestination.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember3ActivityDestination.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember3ActivityDestination.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember3ActivityDestination.useDropShadow = true;
			this.BubbleFamilyMember3ActivityDestination.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember3ActivityDestination.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember3ActivityDestination.maximumSize = new Vector2 (this.BubbleFamilyMember3ActivityDestination.width, this.BubbleFamilyMember3ActivityBgBar.height);
			this.BubbleFamilyMember3ActivityDestination.relativePosition = new Vector3 (this.BubblePartnerDestination.relativePosition.x, 2);
			this.BubbleFamilyMember3ActivityDestination.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent3Target, eventParam);
			
			//Parent 4 Panel
			this.FamilyMember4Panel = this.BubbleFamilyPanel.AddUIComponent<UIPanel>();
			this.FamilyMember4Panel.name = "FamilyMember4Panel";
			this.FamilyMember4Panel.width = this.BubbleFamilyPanel.width;
			this.FamilyMember4Panel.height = 52;
			this.FamilyMember4Panel.clipChildren = true;
			this.FamilyMember4Panel.padding = new RectOffset(0,0,0,0);
			this.FamilyMember4Panel.autoLayout = true;
			this.FamilyMember4Panel.autoLayoutDirection = LayoutDirection.Vertical;
			this.FamilyMember4Panel.relativePosition = new Vector3(0,0);
			this.FamilyMember4Panel.Hide ();
			//Family 4 Background bar
			this.BubbleFamilyMember4BgBar = this.FamilyMember4Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember4BgBar.name = "BubbleFamilyMember4BgBar";
			this.BubbleFamilyMember4BgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember4BgBar.height = 26;
			this.BubbleFamilyMember4BgBar.texture = TextureDB.BubbleBgBar1;
			//Family 4 icon sprite
			this.BubbleFamilyMember4IconSprite = this.BubbleFamilyMember4BgBar.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember4IconSprite.name = "BubbleFamilyMember4IconSprite";
			this.BubbleFamilyMember4IconSprite.width = 18;
			this.BubbleFamilyMember4IconSprite.height = 18;
			this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
			this.BubbleFamilyMember4IconSprite.relativePosition = new Vector3(7,4);
			//Family 4 name button
			this.BubbleFamilyMember4Name = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember4Name.name = "BubbleFamilyMember4Name";
			this.BubbleFamilyMember4Name.width = 135;
			this.BubbleFamilyMember4Name.height = this.BubbleFamilyMember4BgBar.height;
			this.BubbleFamilyMember4Name.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember4Name.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember4Name.playAudioEvents = true;
			this.BubbleFamilyMember4Name.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember4Name.font.size = 15;
			this.BubbleFamilyMember4Name.textScale = 0.80f;
			this.BubbleFamilyMember4Name.wordWrap = true;
			this.BubbleFamilyMember4Name.useDropShadow = true;
			this.BubbleFamilyMember4Name.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember4Name.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember4Name.textPadding.left = 5;
			this.BubbleFamilyMember4Name.textPadding.right = 5;
			this.BubbleFamilyMember4Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
			this.BubbleFamilyMember4Name.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember4Name.pressedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember4Name.focusedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember4Name.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember4Name.relativePosition = new Vector3 (this.BubbleFamilyMember4IconSprite.relativePosition.x + this.BubbleFamilyMember4IconSprite.width+2, 2);
			this.BubbleFamilyMember4Name.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => this.GoToCitizen(this.Parent4ID, eventParam);
			//Family Member4 Real Age Button
			this.BubbleFamilyMember4AgeButton = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember4AgeButton.name = "BubbleFamilyMember4AgeButton";
			this.BubbleFamilyMember4AgeButton.width = 23;
			this.BubbleFamilyMember4AgeButton.height = 18;
			this.BubbleFamilyMember4AgeButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			this.BubbleFamilyMember4AgeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember4AgeButton.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember4AgeButton.textScale = 0.90f;
			this.BubbleFamilyMember4AgeButton.font.size = 15;
			this.BubbleFamilyMember4AgeButton.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember4AgeButton.dropShadowColor = new Color32 (0, 0, 0, 0);
			//this.BubbleFamilyMember4AgeButton.text = "99"; //Real Age test
			//this.BubbleFamilyMember4AgeButton.normalBgSprite = "GenericPanel";
			this.BubbleFamilyMember4AgeButton.isInteractive = false;
			this.BubbleFamilyMember4AgeButton.relativePosition = new Vector3 (this.BubbleFamilyMember4Name.relativePosition.x + this.BubbleFamilyMember4Name.width + 6, 6);
			//Family 4 Follow Texture Sprite
			this.BubbleFamilyMember4FollowToggler = this.BubbleFamilyMember4BgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember4FollowToggler.name = "BubbleFamilyMember4FollowToggler";
			this.BubbleFamilyMember4FollowToggler.atlas = m_atlas;
			this.BubbleFamilyMember4FollowToggler.width = 18;
			this.BubbleFamilyMember4FollowToggler.height = 18;
			this.BubbleFamilyMember4FollowToggler.playAudioEvents = true;
			this.BubbleFamilyMember4FollowToggler.relativePosition = new Vector3 (this.BubbleFamilyMember4AgeButton.relativePosition.x + this.BubbleFamilyMember4AgeButton.width + 12, 4);
			this.BubbleFamilyMember4FollowToggler.eventClick += (component, eventParam) => { 
				FavCimsCore.AddToFavorites (this.Parent4ID);
			};
			//Family 4 Activity background
			this.BubbleFamilyMember4ActivityBgBar = this.FamilyMember4Panel.AddUIComponent<UITextureSprite> ();
			this.BubbleFamilyMember4ActivityBgBar.name = "BubbleFamilyMember4ActivityBgBar";
			this.BubbleFamilyMember4ActivityBgBar.width = this.BubbleFamilyPanel.width;
			this.BubbleFamilyMember4ActivityBgBar.height = 26;
			this.BubbleFamilyMember4ActivityBgBar.texture = TextureDB.BubbleBgBar2;
			//Family 4 Activity Vehicle Button
			this.BubbleFamilyMember4ActivityVehicleButton = this.BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember4ActivityVehicleButton.name = "BubbleFamilyMember4ActivityVehicleButton";
			this.BubbleFamilyMember4ActivityVehicleButton.width = 22;
			this.BubbleFamilyMember4ActivityVehicleButton.height = 22;
			this.BubbleFamilyMember4ActivityVehicleButton.relativePosition = new Vector3 (7, 2);
			this.BubbleFamilyMember4ActivityVehicleButton.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent4VehID, eventParam);
			//Family 4 Activity Destination
			this.BubbleFamilyMember4ActivityDestination = this.BubbleFamilyMember4ActivityBgBar.AddUIComponent<UIButton> ();
			this.BubbleFamilyMember4ActivityDestination.name = "BubbleFamilyMember4ActivityDestination";
			this.BubbleFamilyMember4ActivityDestination.width = this.BubbleFamilyMember4ActivityBgBar.width-this.BubbleFamilyMember4ActivityVehicleButton.width;
			this.BubbleFamilyMember4ActivityDestination.height = this.BubbleFamilyMember4ActivityBgBar.height;
			this.BubbleFamilyMember4ActivityDestination.textVerticalAlignment = UIVerticalAlignment.Middle;
			this.BubbleFamilyMember4ActivityDestination.textHorizontalAlignment = UIHorizontalAlignment.Left;
			this.BubbleFamilyMember4ActivityDestination.playAudioEvents = true;
			this.BubbleFamilyMember4ActivityDestination.font = UIDynamicFont.FindByName ("OpenSans-Regular");
			this.BubbleFamilyMember4ActivityDestination.font.size = 15;
			this.BubbleFamilyMember4ActivityDestination.textScale = 0.75f;
			this.BubbleFamilyMember4ActivityDestination.wordWrap = true;
			this.BubbleFamilyMember4ActivityDestination.textPadding.left = 0;
			this.BubbleFamilyMember4ActivityDestination.textPadding.right = 5;
			this.BubbleFamilyMember4ActivityDestination.outlineColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember4ActivityDestination.outlineSize = 1;
			this.BubbleFamilyMember4ActivityDestination.textColor = new Color32 (21, 59, 96, 140); //r,g,b,a
			this.BubbleFamilyMember4ActivityDestination.hoveredTextColor = new Color32 (204, 102, 0, 20);
			this.BubbleFamilyMember4ActivityDestination.pressedTextColor = new Color32 (153, 0, 0, 0);
			this.BubbleFamilyMember4ActivityDestination.focusedTextColor = new Color32 (102, 153, 255, 147);
			this.BubbleFamilyMember4ActivityDestination.disabledTextColor = new Color32 (51, 51, 51, 160); //r,g,b,a
			this.BubbleFamilyMember4ActivityDestination.useDropShadow = true;
			this.BubbleFamilyMember4ActivityDestination.dropShadowOffset = new Vector2 (1, -1);
			this.BubbleFamilyMember4ActivityDestination.dropShadowColor = new Color32 (0, 0, 0, 0);
			this.BubbleFamilyMember4ActivityDestination.maximumSize = new Vector2 (this.BubbleFamilyMember4ActivityDestination.width, this.BubbleFamilyMember4ActivityBgBar.height);
			this.BubbleFamilyMember4ActivityDestination.relativePosition = new Vector3 (this.BubblePartnerDestination.relativePosition.x, 2);
			this.BubbleFamilyMember4ActivityDestination.eventMouseUp += (UIComponent component, UIMouseEventParameter eventParam) => GoToInstance(this.Parent4Target, eventParam);

            //this.absolutePosition = new Vector3 ((float)rnd.Next (RandXMin, RandXMax), (float)rnd.Next (RandYMin, RandYMax));
            this.absolutePosition = new Vector3((float)rnd.Next(RandXMin, RandXMax), 200f);
            //this.absolutePosition = new Vector3 (FavCimsMainClass.FavCimsPanel.absolutePosition.x - this.width - 15, (FavCimsMainClass.FavCimsPanel.absolutePosition.y / 2) - (this.height / 2));//(MouseClickPos.y - Screen.height)*-1);
            //this.BringToFront ();

            ///////////////End Bubble///////////////
        }

		public override void Update() {

			if (FavCimsMainClass.UnLoading)
				return;
			
			if (MyInstanceID.IsEmpty) {
				//GameObject.Destroy (this.gameObject);
				return;
			}

			if (this.MyInstanceID != this.PrevMyInstanceID) {  //clear old var;	
				this.DogOwner = 0;
				//this.CarOwner = 0;
				this.FirstRun = true;
				this.PrevMyInstanceID = this.MyInstanceID;
			}
			
			this.seconds -= 1 * Time.deltaTime;
			
			if (this.seconds <= 0 || this.FirstRun) {
				this.execute = true;
				this.seconds = Run;
			} else {
				this.execute = false;
			}
		}
		
		public override void LateUpdate() {

			if (FavCimsMainClass.UnLoading) {
				return;
			}

			if (this.execute || this.FirstRun) {
				
				if(this.isVisible) {
					
					try
					{
						citizen = MyInstanceID.Citizen;
						
						this.CitizenData = this.MyCitizen.m_citizens.m_buffer [citizen];
						
						//Little Screen Labels
						this.BubbleCitizenAge.text = FavCimsLang.text ("FavCimsAgeColText_text");
						this.BubbleCitizenAgePhase.text = FavCimsLang.text ("FavCimsAgePhaseColText_text");
						this.BubbleCitizenEducation.text = FavCimsLang.text ("FavCimsEduColText_text");
						this.BubbleWealthSprite.tooltip = FavCimsLang.text("Wealth_Label");
						this.FavCimsDistrictLabel.text = FavCimsLang.text("District_Label");
						
						//Family Label
						this.BubbleFamilyBarLabel.text = FavCimsLang.text ("Citizen_Family_unit");
						this.NoChildsFButton.text = FavCimsLang.text ("Citizen_Details_No_Childs");
						this.NoPartnerFButton.text = FavCimsLang.text ("Citizen_Details_No_Partner");
						
						///Citizen Family
						this.MyCitizenUnit = this.CitizenData.GetContainingUnit (citizen, MyBuilding.m_buildings.m_buffer [CitizenData.m_homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);

						if (this.MyCitizenUnit != 0) {
							
							this.Family = this.MyCitizen.m_units.m_buffer [this.MyCitizenUnit];
							
							//Selected Citizen Data
							
							//Citizen Name
							this.BubbleHeaderCitizenName.text = this.MyCitizen.GetCitizenName (citizen);

                            //Citizen Genre
                            var tmp_gender = Citizen.GetGender(citizen);
                            if (tmp_gender == Citizen.Gender.Female) {
								this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
								this.BubbleHeaderCitizenName.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
								this.BubbleHeaderCitizenName.hoveredTextColor = new Color32 (255, 102, 204, 213); //r,g,b,a
								this.BubbleHeaderCitizenName.pressedTextColor = new Color32 (255, 102, 204, 213); //r,g,b,a
								this.BubbleHeaderCitizenName.focusedTextColor = new Color32 (255, 102, 204, 213); //r,g,b,a
							}else{
								this.BubbleHeaderIconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
								this.BubbleHeaderCitizenName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
								this.BubbleHeaderCitizenName.hoveredTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
								this.BubbleHeaderCitizenName.pressedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
								this.BubbleHeaderCitizenName.focusedTextColor = new Color32 (204, 204, 51, 40); //r,g,b,a
							}
							
							//Citizen Health
							int tmp_health = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_health;
							string healthIcon = GetHealthString(Citizen.GetHealthLevel (tmp_health));
							this.BubbleHealthSprite.normalBgSprite = healthIcon;
							this.BubbleHealthSprite.tooltip = Locale.Get("INFO_HEALTH_TITLE");
							this.BubbleHealthValue.text = FavCimsLang.text("Health_Level_" + sHealthLevels[(int) Citizen.GetHealthLevel (tmp_health)] + 
							                                               "_" + Citizen.GetGender (citizen));
							
							switch (Citizen.GetHealthLevel (tmp_health)) {
								
							case Citizen.Health.ExcellentHealth:
								BubbleHealthValue.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								break;
								
							case Citizen.Health.VeryHealthy:
								BubbleHealthValue.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								break;
								
							case Citizen.Health.Healthy:
								BubbleHealthValue.textColor = new Color32 (102, 204, 0, 60); //r,g,b,a
								break;
								
							case Citizen.Health.PoorHealth:
								BubbleHealthValue.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								break;
								
							case Citizen.Health.Sick:
								BubbleHealthValue.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								break;
								
							case Citizen.Health.VerySick:
								BubbleHealthValue.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								break;
							}
							
							//Citizen Education
							var tmp_education = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].EducationLevel;
							string education = tmp_education.ToString ();
							this.BubbleRow1EducationTooltipArea.tooltip = FavCimsLang.text ("Education_" + education + "_" + Citizen.GetGender (citizen));
							
							if (education == "ThreeSchools") {
								this.BubbleEduLevel3.isEnabled = true;
								this.BubbleEduLevel2.isEnabled = true;
								this.BubbleEduLevel1.isEnabled = true;
							} else if (education == "TwoSchools") {
								this.BubbleEduLevel3.isEnabled = false;
								this.BubbleEduLevel2.isEnabled = true;
								this.BubbleEduLevel1.isEnabled = true;
							} else if (education == "OneSchool") {
								this.BubbleEduLevel3.isEnabled = false;
								this.BubbleEduLevel2.isEnabled = false;
								this.BubbleEduLevel1.isEnabled = true;
							} else {
								this.BubbleEduLevel3.isEnabled = false;
								this.BubbleEduLevel2.isEnabled = false;
								this.BubbleEduLevel1.isEnabled = false;
							}
							
							//Citizen Wellbeing
							int tmp_wellbeing = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_wellbeing;
							//string wellbeing = Citizen.GetWellbeingLevel (tmp_education, tmp_wellbeing).ToString ();
							string wellbeingIcon = GetHappinessString(Citizen.GetHappinessLevel (tmp_wellbeing));
							this.BubbleRow2WellbeingIcon.normalBgSprite = wellbeingIcon;
							this.BubbleRow2WellbeingIcon.tooltip = FavCimsLang.text("WellBeingLabel") + FavCimsLang.text (wellbeingIcon);
							
							//Citizen Wealth
							var tmp_wealth = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].WealthLevel;
							
							this.BubbleRow2WealthValueVal.tooltip = FavCimsLang.text("Wealth_Label");
							if(tmp_wealth == Citizen.Wealth.Low) {
								this.BubbleRow2WealthValueVal.text = FavCimsLang.text("Low_Wealth_" + Citizen.GetGender (citizen));
								this.BubbleRow2WealthValueVal.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
							}else if (tmp_wealth == Citizen.Wealth.Medium) {
								this.BubbleRow2WealthValueVal.text = FavCimsLang.text("Mid_Wealth_" + Citizen.GetGender (citizen));
								this.BubbleRow2WealthValueVal.textColor = new Color32 (255, 204, 0, 32);
							}else {
								this.BubbleRow2WealthValueVal.text = FavCimsLang.text("High_Wealth_" + Citizen.GetGender (citizen));
								this.BubbleRow2WealthValueVal.textColor = new Color32 (102, 204, 0, 60); //r,g,b,a
							}
							
							//Citizen Happiness
							int tmp_happiness = Citizen.GetHappiness (tmp_health, tmp_wellbeing);
							//string Happiness = Citizen.GetHappinessLevel (tmp_happiness).ToString (); //Bad, Poor, Good, Excellent, Suberb
							string HappinessIcon = GetHappinessString (Citizen.GetHappinessLevel (tmp_happiness));

							if (this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)citizen].Arrested && this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)citizen].Criminal) {
								this.BubbleRow1HappyIcon.atlas = MyAtlas.FavCimsAtlas;
								this.BubbleRow1HappyIcon.normalBgSprite = "FavCimsCrimeArrested";
								this.BubbleRow1HappyIcon.tooltip = FavCimsLang.text ("Citizen_Arrested");
							} else {
								this.BubbleRow1HappyIcon.atlas = UIView.GetAView().defaultAtlas;
								this.BubbleRow1HappyIcon.normalBgSprite = HappinessIcon;
								this.BubbleRow1HappyIcon.tooltip = FavCimsLang.text("HappinessLabel") + FavCimsLang.text (HappinessIcon);
							}
							
							//Age Group (Age Phase)
							int tmp_age = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_age;
							string agegroup = Citizen.GetAgeGroup (tmp_age).ToString ();
							this.BubbleCitizenAgePhaseVal.text = FavCimsLang.text ("AgePhase_" + agegroup + "_" + Citizen.GetGender (citizen));
							
							//Real Age 
							this.RealAge = FavCimsCore.CalculateCitizenAge (tmp_age);
							
							if (this.RealAge <= 12) { //CHILD
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
							} else if (this.RealAge <= 19) { //TEEN
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
							} else if (this.RealAge <= 25) { //YOUNG
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
							} else if (this.RealAge <= 65) { //ADULT
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
							} else if (this.RealAge <= 90) { //SENIOR
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
							} else { //FINAL
								this.BubbleCitizenAgeVal.text = this.RealAge.ToString ();
								this.BubbleCitizenAgeVal.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								this.BubbleCitizenAgePhaseVal.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
							}
							
							//Working Place
							this.WorkPlace = this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].m_workBuilding;
							if (this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen) != ItemClass.Level.None) {
                                this.isStudent = true;
                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
								this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureStudent;
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								//this.CitizenRowData ["workplace"] = "Student";
								this.FavCimsWorkingPlace.tooltip = Locale.Get ("CITIZEN_SCHOOL_LEVEL", this.MyCitizen.m_citizens.m_buffer [MyInstanceID.Index].GetCurrentSchoolLevel (citizen).ToString ()) + " " + this.MyBuilding.GetBuildingName(this.WorkPlace, this.MyInstanceID);
							} else if (this.WorkPlace == 0) {
								this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
								
								if (tmp_age >= 180) {
									//In Pensione
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTextureRetired;
									//this.CitizenRowData ["workplace"] = "Retired";
									this.FavCimsWorkingPlace.text = FavCimsLang.text ("Citizen_Retired");
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Citizen_Retired_tooltip");
									this.FavCimsWorkingPlaceSprite.tooltip = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								} else { 
									this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsWorkingPlaceTexture; //unemployed
									//this.CitizenRowData ["workplace"] = "Unemployed";
									this.FavCimsWorkingPlace.text = Locale.Get ("CITIZEN_OCCUPATION_UNEMPLOYED");
									this.FavCimsWorkingPlace.isEnabled = false;
									this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Unemployed_tooltip");
									this.FavCimsWorkingPlaceSprite.tooltip = null;
									this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								}
							}
							
							//Perchè l'edificio è presente sia che lavori sia che vada a scuola, se lo metto nell'else qui sopra se va a scuola non comparirebbe nulla o dovrei scriverlo 2 volte.
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
								//this.CitizenRowData ["workplace"] = this.MyBuilding.GetBuildingName (this.WorkPlace, this.MyInstanceID);
								this.FavCimsWorkingPlace.text = str + " " + this.MyBuilding.GetBuildingName (this.WorkPlace, this.MyInstanceID);
								this.FavCimsWorkingPlace.isEnabled = true;
								this.WorkInfo = this.MyBuilding.m_buildings.m_buffer [WorkPlaceID.Index].Info;

                                if (this.WorkInfo.m_class.m_service == ItemClass.Service.Commercial) {
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
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

								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Industrial) {
									
									this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
									this.FavCimsWorkingPlace.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", "Industrial");
									
									switch (this.WorkInfo.m_class.m_subService) 
									{
									case ItemClass.SubService.IndustrialFarming:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
										break;
                                    case ItemClass.SubService.PlayerIndustryFarming: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFarming";
                                        break;
                                    case ItemClass.SubService.IndustrialForestry:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
										break;
                                    case ItemClass.SubService.PlayerIndustryForestry: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ResourceForestry";
                                        break;
                                    case ItemClass.SubService.IndustrialOil:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
										break;
                                    case ItemClass.SubService.PlayerIndustryOil: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOil";
                                        break;
                                    case ItemClass.SubService.IndustrialOre:
										this.FavCimsWorkingPlaceSprite.texture = null;
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
										break;
                                    case ItemClass.SubService.PlayerIndustryOre: //New code 0.3.x
                                        this.FavCimsWorkingPlaceSprite.texture = null;
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyOre";
                                        break;
                                    default:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
										this.FavCimsWorkingPlaceSprite.texture = TextureDB.FavCimsCitizenIndustrialGenericTexture;
										break;
									}
									
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
									
								} else if (this.WorkInfo.m_class.m_service == ItemClass.Service.Office) {
									this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = null;
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
									
								} else {
									this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
									this.FavCimsWorkingPlaceSprite.texture = null;
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
									//case ItemClass.Service.Government:
										//this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "ToolbarIconGovernment";
										//this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Government_job");
										//break;
									case ItemClass.Service.Water:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyWaterSaving";
										this.FavCimsWorkingPlace.tooltip = FavCimsLang.text ("Water_job");
										break;
									case ItemClass.Service.PublicTransport:

                                        switch (this.WorkInfo.m_class.m_subService)
                                        {
                                            case ItemClass.SubService.PublicTransportPost:
                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarPublicTransportPost";
                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SUBSERVICE_DESC", "Post");
                                                break;

                                            default:
                                                this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyFreePublicTransport";
                                                this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "PublicTransport");
                                                break;
                                        }
                                        
										break;
									case ItemClass.Service.Monument:
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "FeatureMonumentLevel6";
										this.FavCimsWorkingPlace.tooltip = Locale.Get ("SERVICE_DESC", "Monuments");
										break;
                                    case ItemClass.Service.Fishing: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarIndustryFishing";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "Fishing");
                                        break;
                                    case ItemClass.Service.Disaster: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarFireDepartmentDisaster";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "FireDepartmentDisaster");
                                        break;
                                    case ItemClass.Service.Museums: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaMuseums";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("MAIN_CATEGORY", "CampusAreaMuseums");
                                        break;
                                    case ItemClass.Service.VarsitySports: //New code 0.3.x
                                        this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "SubBarCampusAreaVarsitySports";
                                        this.FavCimsWorkingPlace.tooltip = Locale.Get("SERVICE_DESC", "VarsitySports");
                                        break;
                                    default:
										this.FavCimsWorkingPlace.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
										this.FavCimsWorkingPlaceButtonGamDefImg.normalBgSprite = "IconPolicyNone";
										this.FavCimsWorkingPlace.tooltip = null;
										break;
									}
								}
								
								//District
								this.WorkDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [WorkPlaceID.Index].m_position);
								
								if (this.WorkDistrict == 0) {
									this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
								} else {
									this.FavCimsWorkingPlaceSprite.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.WorkDistrict);
								}
								
							}else{
								this.FavCimsWorkingPlace.isEnabled = false;
								this.FavCimsCitizenWorkPlaceLevelSprite.texture = null;
								this.FavCimsWorkingPlaceButtonGamDefImg.tooltip = null;
								this.FavCimsWorkingPlaceSprite.tooltip = null;
							}
							
							//Family House
							this.CitizenHome = this.MyCitizen.m_citizens.m_buffer [this.MyInstanceID.Index].m_homeBuilding;
							if (this.CitizenHome != 0) {
								this.CitizenHomeID.Building = this.CitizenHome;
								
								this.BubbleHomeName.text = this.MyBuilding.GetBuildingName (this.CitizenHome, this.MyInstanceID);
								this.BubbleHomeName.isEnabled = true;
								this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
								this.HomeInfo = this.MyBuilding.m_buildings.m_buffer [CitizenHomeID.Index].Info;
								
								if (this.HomeInfo.m_class.m_service == ItemClass.Service.Residential) {
									
									this.BubbleHomeName.tooltip = null;
									
									if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHigh) {
										this.BubbleHomeName.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
										this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
										this.BubbleHomeName.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialHigh.ToString ());
									}else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialHighEco) {
										this.BubbleHomeName.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
										this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHigh;
										this.BubbleHomeName.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialHigh.ToString ()) + " Eco";
									} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLowEco) {
										this.BubbleHomeName.textColor = new Color32 (0, 153, 0, 80); //r,g,b,a
										this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
										this.BubbleHomeName.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialLow.ToString ()) + " Eco";
									} else if (this.HomeInfo.m_class.m_subService == ItemClass.SubService.ResidentialLow) {
										this.BubbleHomeName.textColor = new Color32 (0, 153, 0, 80); //r,g,b,a
										this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTexture;
										this.BubbleHomeName.tooltip = Locale.Get ("ZONEDBUILDING_TITLE", ItemClass.SubService.ResidentialLow.ToString ());
									}

									switch (this.HomeInfo.m_class.m_level) 
									{
									case ItemClass.Level.Level5:
										this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel [5];
										break;
									case ItemClass.Level.Level4:
										this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel [4];
										break;
									case ItemClass.Level.Level3:
										this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel [3];
										break;
									case ItemClass.Level.Level2:
										this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel [2];
										break;
									default:
										this.BubbleHomeLevel.texture = TextureDB.FavCimsResidentialLevel [1];
										break;
									}

									//District
									this.HomeDistrict = (int)MyDistrict.GetDistrict (this.MyBuilding.m_buildings.m_buffer [this.CitizenHomeID.Index].m_position);
									
									if (this.HomeDistrict == 0) {
										this.BubbleHomeIcon.tooltip = FavCimsLang.text ("DistrictLabel") + FavCimsLang.text ("DistrictNameNoDistrict");
									} else {
										this.BubbleHomeIcon.tooltip = FavCimsLang.text ("DistrictLabel") + MyDistrict.GetDistrictName (this.HomeDistrict);
									}
									
									//Home Status Problems
									Notification.Problem HomeProblems = MyBuilding.m_buildings.m_buffer[this.CitizenHome].m_problems;
									
									if(HomeProblems != Notification.Problem.None) {
										this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSpriteProblems;
									}else{
										this.BubbleDetailsBgSprite.texture = TextureDB.BubbleDetailsBgSprite;
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Electricity.ToString())) {
										this.BubbleDetailsElettricity.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_TITLE","Electricity");
									}else{
										this.BubbleDetailsElettricity.normalFgSprite = null;
										this.BubbleDetailsElettricity.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Sewage.ToString())) {
										this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE","Sewage");
									} else if(HomeProblems.ToString().Contains(Notification.Problem.DirtyWater.ToString())) {
										this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NORMAL","DirtyWater");
									} else if(HomeProblems.ToString().Contains(Notification.Problem.Water.ToString())) {	
										this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE","Water");
									}else if(HomeProblems.ToString().Contains(Notification.Problem.Flood.ToString())) {
										this.BubbleDetailsWater.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_TITLE","Flood");
									}else{
										this.BubbleDetailsWater.normalFgSprite = null;
										this.BubbleDetailsWater.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Death.ToString())) {
										this.BubbleDetailsDeath.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_TITLE","Death");
									}else{
										this.BubbleDetailsDeath.normalFgSprite = null;
										this.BubbleDetailsDeath.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Fire.ToString())) {
										this.BubbleDetailsFire.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_TITLE","Fire");
									}else{
										this.BubbleDetailsFire.normalFgSprite = null;
										this.BubbleDetailsFire.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Garbage.ToString())) {
										this.BubbleDetailsGarbage.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_TITLE","Garbage");
									}else{
										this.BubbleDetailsGarbage.normalFgSprite = null;
										this.BubbleDetailsGarbage.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.LandValueLow.ToString())) {
										this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE","LandValueLow");
									}else if(HomeProblems.ToString().Contains(Notification.Problem.TooFewServices.ToString())) {
										this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE","ToofewServices");
									}else if(HomeProblems.ToString().Contains(Notification.Problem.TaxesTooHigh.ToString())) {
										this.BubbleDetailsLandValue.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_TITLE","TaxesTooHigh");
									}else{
										this.BubbleDetailsLandValue.normalFgSprite = null;
										this.BubbleDetailsLandValue.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Noise.ToString())) {
										this.BubbleDetailsNoise.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NORMAL","Noise");
									}else{
										this.BubbleDetailsNoise.normalFgSprite = null;
										this.BubbleDetailsNoise.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
									
									if(HomeProblems.ToString().Contains(Notification.Problem.Pollution.ToString())) {
										this.BubbleDetailsPollution.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NORMAL","Pollution");
									}else{
										this.BubbleDetailsPollution.normalFgSprite = null;
										this.BubbleDetailsPollution.tooltip = Locale.Get("NOTIFICATION_NONE");
									}

									if (this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)citizen].Arrested && this.MyCitizen.m_citizens.m_buffer [(int)(IntPtr)citizen].Criminal) {
										this.BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsCrime.tooltip = FavCimsLang.text ("Citizen_Arrested");
									} else if(HomeProblems.ToString().Contains(Notification.Problem.Crime.ToString())) {
										this.BubbleDetailsCrime.normalFgSprite = "TutorialGlow";
										this.BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_TITLE","Crime");
									}else{
										this.BubbleDetailsCrime.normalFgSprite = null;
										this.BubbleDetailsCrime.tooltip = Locale.Get("NOTIFICATION_NONE");
									}
								}
							} else {
								this.BubbleHomeName.text = FavCimsLang.text ("Citizen_HomeLess");
								this.BubbleHomeName.isEnabled = false;
								this.BubbleHomeIcon.texture = TextureDB.FavCimsCitizenHomeTextureHomeless;
								this.BubbleHomeIcon.tooltip = FavCimsLang.text ("DistrictNameNoDistrict");
								this.BubbleHomeName.tooltip = FavCimsLang.text ("Citizen_HomeLess_tooltip");
							}
							
							//Activity
							Activity(citizen, this.FavCimsLastActivityVehicleButton, this.FavCimsLastActivity, out this.MyVehicleID, out this.MyTargetID);
							
							//Now in this District
							this.MainCitizenInstance = MyCitizen.m_citizens.m_buffer[citizen].m_instance;
							this.CitizenDistrict = (int)MyDistrict.GetDistrict(MyCitizen.m_instances.m_buffer[this.MainCitizenInstance].GetSmoothPosition(this.MainCitizenInstance));
							
							if (this.CitizenDistrict == 0) {
								this.FavCimsDistrictValue.tooltip = FavCimsLang.text("District_Label_tooltip");
								this.FavCimsDistrictValue.text = FavCimsLang.text ("DistrictNameNoDistrict");
							} else {
								this.FavCimsDistrictValue.tooltip = FavCimsLang.text("District_Label_tooltip");
								this.FavCimsDistrictValue.text = MyDistrict.GetDistrictName (this.CitizenDistrict);
							}

							//Citizen Have a Car?
							FamilyVehicle(citizen, BubblePersonalCarButton, out this.PersonalVehicleID);
							//Family Have a Car?
							FamilyVehicle(this.Family.m_citizen0, BubbleFamilyBarCarButton, out this.FamilyVehicleID);
							
							bool isSon = false;
							int Sons = 0;
							
							//Partner
							if (this.Family.m_citizen0 != 0 && citizen == this.Family.m_citizen1) { //&& this.Family.m_citizen0 != citizen 
								this.CitizenPartner = this.Family.m_citizen0;
								this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
								//Have Pet?
								if(this.DogOwner != 0) {
									FamilyPet(this.DogOwner);
								}else{
									FamilyPet(this.Family.m_citizen1);
								}
							} else {
								if (this.Family.m_citizen1 != 0 && citizen == this.Family.m_citizen0) {
									this.CitizenPartner = this.Family.m_citizen1;
									this.BubblePartnerLove.normalBgSprite = "InfoIconHealth";
									//Have Pet?
									if(this.DogOwner != 0) {
										FamilyPet(this.DogOwner);
									}else{
										FamilyPet(this.Family.m_citizen0);
									}
								}else if (citizen == this.Family.m_citizen0) {
									//Have Pet?
									if(this.DogOwner != 0) {
										FamilyPet(this.DogOwner);
									}else{
										FamilyPet(citizen);
									}
									this.CitizenPartner = 0;
								} else {
									this.BubblePartnerLove.normalBgSprite = "InfoIconAge";
									this.CitizenPartner = this.Family.m_citizen0;
									isSon = true;
								}
							}
							
							if (this.CitizenPartner != 0) {
								
								this.PartnerID.Citizen = CitizenPartner;
								
								int CitizenPartnerINT = (int)((UIntPtr)this.CitizenPartner);
								
								this.BubblePartnerName.text = this.MyCitizen.GetCitizenName (CitizenPartner);
								if (Citizen.GetGender (CitizenPartner) == Citizen.Gender.Female) {
									this.BubblePartnerName.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
								} else {
									this.BubblePartnerName.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
								}
								
								if (this.PartnerID.IsEmpty) {
									this.BubblePartnerName.tooltip = null;
									this.BubblePartnerName.isEnabled = false;
								} else {
									this.BubblePartnerName.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
									this.BubblePartnerName.isEnabled = true;
								}
								
								if(this.DogOwner != 0) {
									FamilyPet(this.DogOwner);
								}else{
									FamilyPet(this.CitizenPartner);
								}
								
								Activity(this.CitizenPartner, this.BubblePartnerVehicleButton, this.BubblePartnerDestination, out this.PartnerVehID, out this.PartnerTarget);
								
								//Real Age 
								this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [CitizenPartner].m_age);
								
								if (this.RealAge <= 12) { //CHILD
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
								} else if (this.RealAge <= 19) { //TEEN
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								} else if (this.RealAge <= 25) { //YOUNG
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								} else if (this.RealAge <= 65) { //ADULT
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
								} else if (this.RealAge <= 90) { //SENIOR
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								} else { //FINAL
									this.BubbleParnerAgeButton.text = this.RealAge.ToString ();
									this.BubbleParnerAgeButton.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								}
								
								//Partner is in favorites?
								if (FavCimsCore.RowID.ContainsKey (CitizenPartnerINT)) {
									//Yes
									//this.BubblePartnerFollowToggler.texture = TextureDB.LittleStarGold;
									this.BubblePartnerFollowToggler.normalBgSprite = "icon_fav_subscribed";
								} else {
									//No
									//this.BubblePartnerFollowToggler.texture = TextureDB.LittleStarGrey;
									this.BubblePartnerFollowToggler.normalBgSprite = "icon_fav_unsubscribed";
								}
								this.PartnerPanel.Show ();
								if(!isSon)
									this.NoPartnerPanel.Hide();
							} else {
								this.PartnerPanel.Hide ();
								if(!isSon)
									this.NoPartnerPanel.Show();
							}
							
							//Genitore 2 
							if (isSon) {

								this.NoPartnerPanel.Hide();

								if(this.Family.m_citizen1 != 0) {
									
									this.CitizenPartner = this.Family.m_citizen1;
									
									this.Parent1ID.Citizen = CitizenPartner;
									
									int CitizenPartnerINT = (int)((UIntPtr)this.CitizenPartner);
									
									this.BubbleParent1Name.text = this.MyCitizen.GetCitizenName (CitizenPartner);
									if (Citizen.GetGender (CitizenPartner) == Citizen.Gender.Female) {
										this.BubbleParent1Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
									} else {
										this.BubbleParent1Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
									}
									
									if (this.Parent1ID.IsEmpty) {
										this.BubbleParent1Name.isEnabled = false;
										this.BubbleParent1Name.tooltip = null;
									} else {
										this.BubbleParent1Name.isEnabled = true;
										this.BubbleParent1Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
									}
									
									Activity(this.CitizenPartner, this.BubbleParent1VehicleButton, this.BubbleParent1Destination, out this.Parent1VehID, out this.Parent1Target);
									
									//Real Age 
									this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [CitizenPartner].m_age);
									
									if (this.RealAge <= 12) { //CHILD
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
									} else if (this.RealAge <= 19) { //TEEN
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
									} else if (this.RealAge <= 25) { //YOUNG
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
									} else if (this.RealAge <= 65) { //ADULT
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
									} else if (this.RealAge <= 90) { //SENIOR
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
									} else { //FINAL
										this.BubbleParent1AgeButton.text = this.RealAge.ToString ();
										this.BubbleParent1AgeButton.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
									}
									
									//Parent1 is in favorites?
									if (FavCimsCore.RowID.ContainsKey (CitizenPartnerINT)) {
										//Yes
										this.BubbleParent1FollowToggler.normalBgSprite = "icon_fav_subscribed";
									} else {
										//No
										this.BubbleParent1FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
									}
									this.Parent1Panel.Show ();
								}else{
									this.Parent1Panel.Hide ();
								}
							} else {
								this.Parent1Panel.Hide ();
							}
							
							//Parent2
							if (this.Family.m_citizen2 != 0 && this.Family.m_citizen2 != citizen) {
								this.CitizenParent2 = this.Family.m_citizen2;
								
								this.Parent2ID.Citizen = CitizenParent2;
								
								int CitizenParent2INT = (int)((UIntPtr)this.CitizenParent2);
								
								this.BubbleFamilyMember2Name.text = this.MyCitizen.GetCitizenName (CitizenParent2);
								if (Citizen.GetGender (CitizenParent2) == Citizen.Gender.Female) {
									this.BubbleFamilyMember2Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
									this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
								} else {
									this.BubbleFamilyMember2Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
									this.BubbleFamilyMember2IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
								}
								
								if (this.Parent2ID.IsEmpty) {
									this.BubbleFamilyMember2Name.isEnabled = false;
									this.BubbleFamilyMember2Name.tooltip = null;
								} else {
									this.BubbleFamilyMember2Name.isEnabled = true;
									this.BubbleFamilyMember2Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
								}
								
								//Have Pet?
								if(this.DogOwner != 0) {
									FamilyPet(this.DogOwner);
								}else{
									FamilyPet(this.Family.m_citizen2);
								}
								
								Activity(this.CitizenParent2, this.BubbleFamilyMember2ActivityVehicleButton, this.BubbleFamilyMember2ActivityDestination, out this.Parent2VehID, out this.Parent2Target);
								
								//Real Age 
								this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [CitizenParent2].m_age);
								
								if (this.RealAge <= 12) { //CHILD
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
								} else if (this.RealAge <= 19) { //TEEN
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								} else if (this.RealAge <= 25) { //YOUNG
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								} else if (this.RealAge <= 65) { //ADULT
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
								} else if (this.RealAge <= 90) { //SENIOR
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								} else { //FINAL
									this.BubbleFamilyMember2AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember2AgeButton.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								}
								
								//Parent is in favorites?
								if (FavCimsCore.RowID.ContainsKey (CitizenParent2INT)) {
									//Yes
									this.BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_subscribed";
								} else {
									//No
									this.BubbleFamilyMember2FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
								}
								this.FamilyMember2Panel.Show ();
								
								if(!isSon)
									Sons++;
							} else {
								
								if(this.Family.m_citizen2 == citizen) {
									//Have Pet?
									if(this.DogOwner != 0) {
										FamilyPet(this.DogOwner);
									}else{
										FamilyPet(this.Family.m_citizen2);
									}
								}
								
								this.FamilyMember2Panel.Hide ();
							}
							
							//Parent3
							if (this.Family.m_citizen3 != 0 && this.Family.m_citizen3 != citizen) {
								this.CitizenParent3 = this.Family.m_citizen3;
								
								this.Parent3ID.Citizen = CitizenParent3;
								
								int CitizenParent3INT = (int)((UIntPtr)this.CitizenParent3);
								
								this.BubbleFamilyMember3Name.text = this.MyCitizen.GetCitizenName (CitizenParent3);
								if (Citizen.GetGender (CitizenParent3) == Citizen.Gender.Female) {
									this.BubbleFamilyMember3Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
									this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
								} else {
									this.BubbleFamilyMember3Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
									this.BubbleFamilyMember3IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
								}
								
								if (this.Parent3ID.IsEmpty) {
									this.BubbleFamilyMember3Name.isEnabled = false;
									this.BubbleFamilyMember3Name.tooltip = null;
								} else {
									this.BubbleFamilyMember3Name.isEnabled = true;
									this.BubbleFamilyMember3Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
								}
								
								//Have Pet?
								if(this.DogOwner != 0) {
									FamilyPet(this.DogOwner);
								}else{
									FamilyPet(this.Family.m_citizen3);
								}
								
								Activity(this.CitizenParent3, this.BubbleFamilyMember3ActivityVehicleButton, this.BubbleFamilyMember3ActivityDestination, out this.Parent3VehID, out this.Parent3Target);
								
								//Real Age 
								this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [CitizenParent3].m_age);
								
								if (this.RealAge <= 12) { //CHILD
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
								} else if (this.RealAge <= 19) { //TEEN
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								} else if (this.RealAge <= 25) { //YOUNG
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								} else if (this.RealAge <= 65) { //ADULT
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
								} else if (this.RealAge <= 90) { //SENIOR
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								} else { //FINAL
									this.BubbleFamilyMember3AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember3AgeButton.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								}
								
								//Parent is in favorites?
								if (FavCimsCore.RowID.ContainsKey (CitizenParent3INT)) {
									//Yes
									this.BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_subscribed";
								} else {
									//No
									this.BubbleFamilyMember3FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
								}
								this.FamilyMember3Panel.Show ();
								
								if(!isSon)
									Sons++;
							} else {
								
								if(this.Family.m_citizen3 == citizen) {
									//Have Pet?
									if(this.DogOwner != 0) {
										FamilyPet(this.DogOwner);
									}else{
										FamilyPet(this.Family.m_citizen3);
									}
								}
								
								this.FamilyMember3Panel.Hide ();
							}
							
							//Parent4
							if (this.Family.m_citizen4 != 0 && this.Family.m_citizen4 != citizen) {
								this.CitizenParent4 = this.Family.m_citizen4;
								
								this.Parent4ID.Citizen = CitizenParent4;
								
								int CitizenParent4INT = (int)((UIntPtr)this.CitizenParent4);
								
								this.BubbleFamilyMember4Name.text = this.MyCitizen.GetCitizenName (CitizenParent4);
								if (Citizen.GetGender (CitizenParent4) == Citizen.Gender.Female) {
									this.BubbleFamilyMember4Name.textColor = new Color32 (255, 102, 204, 213); //r,g,b,a
									this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureFemale;
								} else {
									this.BubbleFamilyMember4Name.textColor = new Color32 (204, 204, 51, 40); //r,g,b,a
									this.BubbleFamilyMember4IconSprite.texture = TextureDB.BubbleHeaderIconSpriteTextureMale;
								}
								
								if (this.Parent4ID.IsEmpty) {
									this.BubbleFamilyMember4Name.isEnabled = false;
									this.BubbleFamilyMember4Name.tooltip = null;
								} else {
									this.BubbleFamilyMember4Name.isEnabled = true;
									this.BubbleFamilyMember4Name.tooltip = FavCimsLang.text("Right_click_to_swith_tooltip");
								}
								
								//Have Pet?
								if(this.DogOwner != 0) {
									FamilyPet(this.DogOwner);
								}else{
									FamilyPet(this.Family.m_citizen4);
								}
								
								Activity(this.CitizenParent4, this.BubbleFamilyMember4ActivityVehicleButton, this.BubbleFamilyMember4ActivityDestination, out this.Parent4VehID, out this.Parent4Target);
								
								//Real Age 
								this.RealAge = FavCimsCore.CalculateCitizenAge (this.MyCitizen.m_citizens.m_buffer [CitizenParent4].m_age);
								
								if (this.RealAge <= 12) { //CHILD
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (83, 166, 0, 60); //r,g,b,a
								} else if (this.RealAge <= 19) { //TEEN
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (0, 102, 51, 100); //r,g,b,a
								} else if (this.RealAge <= 25) { //YOUNG
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (255, 204, 0, 32); //r,g,b,a
								} else if (this.RealAge <= 65) { //ADULT
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (255, 102, 0, 16); //r,g,b,a
								} else if (this.RealAge <= 90) { //SENIOR
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (153, 0, 0, 0); //r,g,b,a
								} else { //FINAL
									this.BubbleFamilyMember4AgeButton.text = this.RealAge.ToString ();
									this.BubbleFamilyMember4AgeButton.textColor = new Color32 (255, 0, 0, 0); //r,g,b,a
								}
								
								//Parent is in favorites?
								if (FavCimsCore.RowID.ContainsKey (CitizenParent4INT)) {
									//Yes
									this.BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_subscribed";
								} else {
									//No
									this.BubbleFamilyMember4FollowToggler.normalBgSprite = "icon_fav_unsubscribed";
								}
								this.FamilyMember4Panel.Show ();
								
								if(!isSon)
									Sons++;
							} else {
								
								if(this.Family.m_citizen4 == citizen) {
									//Have Pet?
									if(this.DogOwner != 0) {
										FamilyPet(this.DogOwner);
									}else{
										FamilyPet(this.Family.m_citizen4);
									}
								}
								
								this.FamilyMember4Panel.Hide ();
							}
							
							if(Sons == 0 && !isSon) {
								NoChildsPanel.Show();
							}else{
								NoChildsPanel.Hide();
							}
							
						}else{
							//GameObject.Destroy (this.gameObject);
							this.Hide();
							this.MyInstanceID = InstanceID.Empty;
							return;
						}
						
						if(this.FirstRun)
							this.FirstRun = false;
						
					} catch /*(Exception e)*/ {
						//Debug.Error("errore" + e.ToString());
					}
				}
			}
		}
	}
}
