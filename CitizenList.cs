using ColossalFramework;
using ColossalFramework.UI;
//using ColossalFramework.IO;
//using ColossalFramework.Globalization;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ICities;
using System.Reflection;
using System.Threading;
//using System.Threading.Tasks;
namespace FavoriteCims
{
	public class FavCimsCore : MonoBehaviour
	{
		public static Dictionary<int, int> RowID = new Dictionary<int, int> ();

		/* Thx to mabako for this piece of code */
		public static T GetPrivateVariable<T>(object obj, string fieldName)
		{
			return (T)obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
		}
		/* End */

		public static Dictionary<InstanceID, string> FavoriteCimsList() {

			object L = GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
			do { }
			while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));
			
			try
			{	
				//Dictionary<InstanceID, string> favlist = (Dictionary<InstanceID, string>)InstanceManager.instance.GetType().GetField("m_names", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(InstanceManager.instance);
				Dictionary<InstanceID, string> favlist = GetPrivateVariable<Dictionary<InstanceID, string>>(InstanceManager.instance, "m_names");
				return (favlist);
			}
			finally
			{
				Monitor.Exit(L);
			}
		}

		public static void AddToFavorites(InstanceID MyInstanceID) { //Toggler for family panel

			if (MyInstanceID.IsEmpty) {
				return;
			}

			object L = GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
			do { }
			while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));

			try
			{
				InstanceManager MyInstance = Singleton<InstanceManager>.instance;
				CitizenManager MyCitizen = Singleton<CitizenManager>.instance;

				uint citizen = MyInstanceID.Citizen;
				string Name = MyCitizen.GetCitizenName (citizen);
				int citizenID = (int)((UIntPtr)citizen);

				if (Name != null && Name.Length > 0) {

					if(!FavCimsCore.RowID.ContainsKey (citizenID)) {
						if(!FavoriteCimsMainPanel.RowsAlreadyExist(MyInstanceID)) {
							try {
								MyInstance.SetName(MyInstanceID,Name);
								CitizenRow FavCimsCitizenSingleRowPanel = FavoriteCimsMainPanel.FavCimsCitizenRowsPanel.AddUIComponent(typeof(CitizenRow)) as CitizenRow;
								if(FavCimsCitizenSingleRowPanel != null) {
									FavCimsCitizenSingleRowPanel.MyInstanceID = MyInstanceID;
									FavCimsCitizenSingleRowPanel.MyInstancedName = Name;
								}
							}catch (Exception e) {
								Debug.Error("Add To Favorites Fail : " + e.ToString());
							}
						}else{
							return;
						}
					}else{
						RemoveRowAndRemoveFav(MyInstanceID, citizenID);
					}
				}
			}
			finally
			{
				Monitor.Exit(L);
			}
			return;
		}

		public static void RemoveRowAndRemoveFav (InstanceID citizenInstanceID, int citizenID) {

			object L = FavCimsCore.GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
			do { }
			while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));
			
			try
			{	
				InstanceManager MyInstance = Singleton<InstanceManager>.instance;

				if(!citizenInstanceID.IsEmpty) {
					string CitizenName = MyInstance.GetName (citizenInstanceID);

					if (CitizenName != null && CitizenName.Length > 0) {
						MyInstance.SetName(citizenInstanceID,null);
					}
				}

				RemoveIdFromArray(citizenID);
			}
			finally
			{
				Monitor.Exit(L);
			}
		}
		
		public static void UpdateMyCitizen(string action) {

			object L = FavCimsCore.GetPrivateVariable<object>(InstanceManager.instance, "m_lock");
			do { }
			while (!Monitor.TryEnter(L, SimulationManager.SYNCHRONIZE_TIMEOUT));
			
			try
			{	
				InstanceManager MyInstance = Singleton<InstanceManager>.instance;
				FavCimsMainClass.FavCimsHumanPanel.SimulateClick();
				InstanceID ThisHuman = HumanWorldInfoPanel.GetCurrentInstanceID();
				
				string CitizenName = MyInstance.GetName (ThisHuman);
				int citizenID = (int)((UIntPtr)ThisHuman.Citizen);

				if (action == "toggle" && CitizenName != null) { //remove
					try {
							MyInstance.SetName(ThisHuman, null);
							RemoveIdFromArray(citizenID);

					} catch (Exception e) {
						Debug.Error("Toggle Remove Fail : " + e.ToString());
					}
				} else {
					try { //add favorite by clicking the Star Button.			
						UITextField DefaultName = FavCimsMainClass.FavCimsHumanPanel.GetComponentInChildren<UITextField>();
						MyInstance.SetName(ThisHuman,DefaultName.text);
					} catch (Exception e) {
						Debug.Error("Toggle Add Fail : " + e.ToString());
					}
				}
			}
			finally
			{
				Monitor.Exit(L);
			}
		}

		public static void InsertIdIntoArray(int citID) {
			RowID [citID] = citID;
		}

		public static void RemoveIdFromArray(int citID) {
			RowID.Remove(citID);
		}

		public static void ClearIdArray() {
			RowID.Clear ();
		}

		//Calculate Real Citizen Age
		public static int CalculateCitizenAge(int GameAge) {

			if (GameAge <= 0) {
				return 0;
			}

			int Real_Age = 0;
			int RealMinAge;
			int RealMaxAge;
			double AgeStep;
			double GameAge_percent;

			if (GameAge > 0 && GameAge <= Citizen.AGE_LIMIT_CHILD) { 
				GameAge_percent = ((double)GameAge / Citizen.AGE_LIMIT_CHILD) * 100;
				RealMinAge = 1;
				RealMaxAge = 12;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);

				return Real_Age;

			} else if (GameAge <= Citizen.AGE_LIMIT_TEEN) { 
				GameAge_percent = (((double)GameAge - Citizen.AGE_LIMIT_CHILD) / (Citizen.AGE_LIMIT_TEEN - Citizen.AGE_LIMIT_CHILD)) * 100;
				RealMinAge = 13;
				RealMaxAge = 19;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);

				return Real_Age;

			} else if (GameAge <= Citizen.AGE_LIMIT_YOUNG) { 
				GameAge_percent = (((double)GameAge - Citizen.AGE_LIMIT_TEEN) / (Citizen.AGE_LIMIT_YOUNG - Citizen.AGE_LIMIT_TEEN)) * 100;
				RealMinAge = 20;
				RealMaxAge = 25;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);

				return Real_Age;

			} else if (GameAge <= Citizen.AGE_LIMIT_ADULT) { 
				GameAge_percent = (((double)GameAge - Citizen.AGE_LIMIT_YOUNG) / (Citizen.AGE_LIMIT_ADULT - Citizen.AGE_LIMIT_YOUNG)) * 100;
				RealMinAge = 26;
				RealMaxAge = 65;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);

				return Real_Age;

			} else if (GameAge <= Citizen.AGE_LIMIT_SENIOR) { 
				
				GameAge_percent = (((double)GameAge - Citizen.AGE_LIMIT_ADULT) / (Citizen.AGE_LIMIT_SENIOR - Citizen.AGE_LIMIT_ADULT)) * 100;
				RealMinAge = 66;
				RealMaxAge = 90;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);

				return Real_Age;

			} else { 

				GameAge_percent = (((double)GameAge - Citizen.AGE_LIMIT_SENIOR) / (Citizen.AGE_LIMIT_FINAL - Citizen.AGE_LIMIT_SENIOR)) * 100;
				RealMinAge = 91;
				RealMaxAge = 105;
				AgeStep = (((double)RealMaxAge - RealMinAge) / 100) * GameAge_percent;
				
				Real_Age = (RealMinAge + (int)AgeStep);
			
				return Real_Age;
			}
		}
	}
}
