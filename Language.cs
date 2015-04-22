using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.Globalization;
using System;
//using System.Collections;
using System.Collections.Generic;

namespace FavoriteCims
{
	public class FavCimsLang : MonoBehaviour {

		public static string GameLanguage;
		public static Dictionary<string, string> it = new Dictionary<string, string>();
		public static Dictionary<string, string> en = new Dictionary<string, string> ();

		public static string text (string index) {

			if (GameLanguage == null || GameLanguage != Locale.Get ("LANGUAGE_ENGLISH")) {

				try {
					//string GameLanguage = LocaleFormatter.FormatGeneric(LocaleID.LANGUAGE_ENGLISH);
					GameLanguage = Locale.Get ("LANGUAGE_ENGLISH");

					if (GameLanguage == "(ITALIAN)") {
					
						it = new Dictionary<string, string> ();

						///////NON MODIFICABILE//////////////////////MODIFICABILE
						it ["FavCimsButton_tooltip"] = "I Miei Cims Preferiti";
						it ["FavCimsBCMenuButton_text"] = "Abitanti";
						it ["FavCimsBCMenuButton_tooltip"] = "Persone che segui";
						it ["FavCimsBBMenuButton_text"] = "Edifici";
						it ["FavCimsBBMenuButton_tooltip"] = "Edifici Preferiti";
						it ["FavCimsBSMenuButton_text"] = "Stats";
						it ["FavCimsBSMenuButton_tooltip"] = "Info e Statistiche";
						it ["FavStarButton_enable_tooltip"] = "Clicca per aggiungere ai preferiti!";
						it ["FavStarButton_disable_tooltip"] = "Clicca per rimuovere dai preferiti :(";
						it ["FavCimsHappinesColText_text"] = "Stato";
						it ["FavCimsHappinesColText_tooltip"] = "Felicita' cittadino";
						it ["FavCimsNameColText_text"] = "Nome Abitante";
						it ["FavCimsNameColText_tooltip"] = "Nome del Cittadino";
						it ["FavCimsEduColText_text"] = "Istruzione";
						it ["FavCimsEduColText_tooltip"] = "Livello di educazione del Cittadino";
						it ["FavCimsWorkingPlaceColText_text"] = "Posto di Lavoro / Studio";
						it ["FavCimsWorkingPlaceColText_tooltip"] = "Edificio in cui lavora o studia il Cittadino";
						it ["FavCimsAgePhaseColText_text"] = "Fase di vita";
						it ["FavCimsAgePhaseColText_tooltip"] = "Fase della vita in cui si trova il cittadino";
						it ["FavCimsAgeColText_text"] = "Eta'";
						it ["FavCimsAgeColText_tooltip"] = "L'Eta' del Cittadino";
						it ["FavCimsHomeColText_text"] = "Abitazione";
						it ["FavCimsHomeColText_tooltip"] = "Il cittadino abita in questa casa";
						it ["FavCimsLastActColText_text"] = "Ultima Attivita'";
						it ["FavCimsLastActColText_tooltip"] = "Quello che sta' facendo il cittadino in questo momento";
						it ["FavCimsCloseButtonCol_text"] = "Chiudi";
						it ["FavCimsCloseButtonCol_tooltip"] = "Clicca sul pulsante chiudi per rimuovere il cittadino dai preferiti :(";
						it ["Male"] = "Maschio";
						it ["Female"] = "Femmina";
						it ["AgePhase_Child_Male"] = "Bambino";
						it ["AgePhase_Teen_Male"] = "Ragazzino";
						it ["AgePhase_Young_Male"] = "Ragazzo";
						it ["AgePhase_Adult_Male"] = "Adulto";
						it ["AgePhase_Senior_Male"] = "Anziano";
						it ["Education_Uneducated_Male"] = "Non Istruito";
						it ["Education_OneSchool_Male"] = "Istruito";
						it ["Education_TwoSchools_Male"] = "Ben Istruito";
						it ["Education_ThreeSchools_Male"] = "Molto Istruito";
						it ["AgePhase_Child_Female"] = "Bambina";
						it ["AgePhase_Teen_Female"] = "Ragazzina";
						it ["AgePhase_Young_Female"] = "Ragazza";
						it ["AgePhase_Adult_Female"] = "Adulta";
						it ["AgePhase_Senior_Female"] = "Anziana";
						it ["Education_Uneducated_Female"] = "Non Istruita";
						it ["Education_OneSchool_Female"] = "Istruita";
						it ["Education_TwoSchools_Female"] = "Ben Istruita";
						it ["Education_ThreeSchools_Female"] = "Molto Istruita";
						it ["AgePhaseDead_Male"] = "Morto";
						it ["AgePhaseDead_Female"] = "Morta";
						it ["People_Life_Status_Alive"] = "Il Cittadino e' vivo";
						it ["People_Life_Status_Dead"] = "Il Cittadino e' morto";
						it ["People_Life_Status_Dead_date"] = "il giorno";
						it ["People_Life_Status_Dead_time"] = "alle ore";
						it ["NotificationIconVeryUnhappy"] = "Molto Infelice";
						it ["NotificationIconUnhappy"] = "Infelice";
						it ["NotificationIconHappy"] = "Felice";
						it ["NotificationIconVeryHappy"] = "Veramente Felice";
						it ["NotificationIconExtremelyHappy"] = "Estremamente Felice";
						it ["time_format"] = "dd-mm-yyyy";
						it ["People_Life_Status_IsGone"] = "Questo cittadino ha abbandonato la citta'";
						it ["Citizen_HomeLess"] = "Senzatetto";
						it ["Citizen_HomeLess_tooltip"] = "Questo cittadino non ha una casa";
						it ["HomeOutsideTheCity"] = "Molto molto lontano";
						it ["Home_Location_Dead"] = "Al Cimitero";
						it ["Citizen_Retired"] = "In Pensione";
						it ["Citizen_Retired_tooltip"] = "Il cittadino si e' ritirato dal lavoro";
						it ["Unemployed_tooltip"] = "Il cittadino ha motlo tempo libero al momento...";
						it ["Government_job"] = "Edificio governativo";
						it ["Electricity_job"] = "Centrale Elettrica";
						it ["Water_job"] = "Centrale Idrica";
						it ["Generic_job_place"] = "Il cittadino lavora in questo edificio";
						it ["Vehicle_on_foot"] = "Il cittadino sta camminando";
						it ["Citizen_Details"] = "Dettagli del cittadino";
						it ["Citizen_Details_fullTemplate"] = "Troppe finestre dettagli aperte, chiudine una prima di aprirne una nuova";
						it ["Status_static_label"] = "Stato : ";
						it ["Citizen_Family_unit"] = "Famiglia";
						it ["NowInThisDistrict"] = "Attualmente si trova nel distretto: ";
						it ["DistrictLabel"] = "Distretto: ";
						it ["DistrictNameNoDistrict"] = "Nessuno";
						it ["Citizen_Details_No_Partner"] = "Nessun Partner";
						it ["Citizen_Details_No_Childs"] = "Nessun Figlio";
						it ["Right_click_to_swith_tooltip"] = "Tasto destro per invertire";
						it ["Home_Bubble_panel_Label"] = "Casa : ";
						it ["Low_Wealth_Male"] = "Povero";
						it ["Mid_Wealth_Male"] = "Benestante";
						it ["High_Wealth_Male"] = "Ricco";
						it ["Low_Wealth_Female"] = "Povera";
						it ["Mid_Wealth_Female"] = "Benestante";
						it ["High_Wealth_Female"] = "Ricca";
						it ["WellBeingLabel"] = "Benessere: ";
						it ["HappinessLabel"] = "Felicita' complessiva: ";
						it ["Wealth_Label"] = "Ricchezza";
						it ["Health_Level_VerySick_Male"] = "Molto malato";
						it ["Health_Level_Sick_Male"] = "Malato";
						it ["Health_Level_PoorHealth_Male"] = "Salute scarsa";
						it ["Health_Level_Healthy_Male"] = "In salute";
						it ["Health_Level_VeryHealthy_Male"] = "Buona salute";
						it ["Health_Level_ExcellentHealth_Male"] = "Ottima salute";
						it ["Health_Level_VerySick_Female"] = "Molto malata";
						it ["Health_Level_Sick_Female"] = "Malata";
						it ["Health_Level_PoorHealth_Female"] = "Salute scarsa";
						it ["Health_Level_Healthy_Female"] = "In salute";
						it ["Health_Level_VeryHealthy_Female"] = "Buona salute";
						it ["Health_Level_ExcellentHealth_Female"] = "Ottima salute";
						it ["District_Label"] = "Distretto: ";
						it ["District_Label_tooltip"] = "Attualmente si trova in questo distretto";
						it ["Citizen_Details_NoUnit"] = "Il cittadino e' morto o non ha una casa";
						it ["Citizen_wait_hearse"] 	  = "Aspetta il carro funebre";
						it ["Citizen_on_hearse"]	  = "Sul carro funebre";
						it ["Citizen_hisfuneral"]	  = "Cerimonia funebre";
						it ["Citizen_buried"]		  = "Sepolto";
						//it["Family_no_pets"]					 = "Nessun animale domestico";

						return it [index];

					} else {

						en = new Dictionary<string, string> ();

						//////////NOT EDITABLE////////////////////////EDITABLE
						en ["FavCimsButton_tooltip"] = "My Favorite Cims";
						en ["FavCimsBCMenuButton_text"] = "Citizens";
						en ["FavCimsBCMenuButton_tooltip"] = "Followed People";
						en ["FavCimsBBMenuButton_text"] = "Buildings";
						en ["FavCimsBBMenuButton_tooltip"] = "Favorite Buildings";
						en ["FavCimsBSMenuButton_text"] = "Stats";
						en ["FavCimsBSMenuButton_tooltip"] = "Info and Stats";
						en ["FavStarButton_enable_tooltip"] = "Click here to add to Favorites!";
						en ["FavStarButton_disable_tooltip"] = "Click here to remove from Favorites :(";
						en ["FavCimsHappinesColText_text"] = "Status";
						en ["FavCimsHappinesColText_tooltip"] = "Citizen Happiness";
						en ["FavCimsNameColText_text"] = "Citizen Name";
						en ["FavCimsNameColText_tooltip"] = "Citizen Name";
						en ["FavCimsEduColText_text"] = "Education";
						en ["FavCimsEduColText_tooltip"] = "Citizen Education Level";
						en ["FavCimsWorkingPlaceColText_text"] = "Occupation";
						en ["FavCimsWorkingPlaceColText_tooltip"] = "Place where the Citizen Works or Study";
						en ["FavCimsAgePhaseColText_text"] = "Age Phase";
						en ["FavCimsAgePhaseColText_tooltip"] = "Citizen life Age Phase";
						en ["FavCimsAgeColText_text"] = "Age";
						en ["FavCimsAgeColText_tooltip"] = "Citizen Age";
						en ["FavCimsHomeColText_text"] = "Citizen Home";
						en ["FavCimsHomeColText_tooltip"] = "Citizen Living in this House";
						en ["FavCimsLastActColText_text"] = "Last Activity";
						en ["FavCimsLastActColText_tooltip"] = "What the Citizen is doing at the moment";
						en ["FavCimsCloseButtonCol_text"] = "Close";
						en ["FavCimsCloseButtonCol_tooltip"] = "Click on close button for remove citizen from favorites :(";
						en ["Male"] = "Male";
						en ["Female"] = "Female";
						en ["AgePhase_Child_Male"] = "Child";
						en ["AgePhase_Teen_Male"] = "Boy";
						en ["AgePhase_Young_Male"] = "Young Man";
						en ["AgePhase_Adult_Male"] = "Man";
						en ["AgePhase_Senior_Male"] = "Old Man";
						en ["Education_Uneducated_Male"] = "Uneducated";
						en ["Education_OneSchool_Male"] = "Educated";
						en ["Education_TwoSchools_Male"] = "Well Educated";
						en ["Education_ThreeSchools_Male"] = "Highly Educated";
						en ["AgePhase_Child_Female"] = "Child";
						en ["AgePhase_Teen_Female"] = "Girl";
						en ["AgePhase_Young_Female"] = "Young Woman";
						en ["AgePhase_Adult_Female"] = "Woman";
						en ["AgePhase_Senior_Female"] = "Old Woman";
						en ["Education_Uneducated_Female"] = "Uneducated";
						en ["Education_OneSchool_Female"] = "Educated";
						en ["Education_TwoSchools_Female"] = "Well Educated";
						en ["Education_ThreeSchools_Female"] = "Highly Educated";
						en ["AgePhaseDead_Male"] = "Dead";
						en ["AgePhaseDead_Female"] = "Dead";
						en ["People_Life_Status_Alive"] = "Citizen is alive";
						en ["People_Life_Status_Dead"] = "Citizen is dead";
						en ["People_Life_Status_Dead_date"] = "on this date";
						en ["People_Life_Status_Dead_time"] = "at time";
						en ["NotificationIconVeryUnhappy"] = "Very Unhappy";
						en ["NotificationIconUnhappy"] = "Unhappy";
						en ["NotificationIconHappy"] = "Happy";
						en ["NotificationIconVeryHappy"] = "Very Happy";
						en ["NotificationIconExtremelyHappy"] = "Extremely Happy";
						en ["time_format"] = "mm-dd-yyyy";
						en ["People_Life_Status_IsGone"] = "Citizen is gone away from the city";
						en ["Citizen_HomeLess"] = "Homeless";
						en ["Citizen_HomeLess_tooltip"] = "Citizen has no home";
						en ["HomeOutsideTheCity"] = "Far far away";
						en ["Home_Location_Dead"] = "At Cemetery";
						en ["Citizen_Retired"] = "Retired";
						en ["Citizen_Retired_tooltip"] = "Citizen retired from work";
						en ["Unemployed_tooltip"] = "Citizen has much free time at the moment...";
						en ["Government_job"] = "Government Building";
						en ["Electricity_job"] = "Electricity Plant";
						en ["Water_job"] = "Water Plant";
						en ["Generic_job_place"] = "Citizen Works in this building";
						en ["Vehicle_on_foot"] = "Citizen walking on foot";
						en ["Citizen_Details"] = "Citizen Detail";
						en ["Citizen_Details_fullTemplate"] = "Too many Family Windows opened, close one before open another";
						en ["Status_static_label"] = "Status : ";
						en ["Citizen_Family_unit"] = "Family";
						en ["NowInThisDistrict"] = "In this moment is in District: ";
						en ["DistrictLabel"] = "District: ";
						en ["DistrictNameNoDistrict"] = "None";
						en ["Citizen_Details_No_Partner"] = "No Partner";
						en ["Citizen_Details_No_Childs"] = "No Childs";
						en ["Right_click_to_swith_tooltip"] = "Right Click to Switch Citizen";
						en ["Home_Bubble_panel_Label"] = "Home : ";
						en ["Low_Wealth_Male"] = "Poor";
						en ["Mid_Wealth_Male"] = "Well-off";
						en ["High_Wealth_Male"] = "Rich";
						en ["Low_Wealth_Female"] = "Poor";
						en ["Mid_Wealth_Female"] = "Well-off";
						en ["High_Wealth_Female"] = "Rich";
						en ["WellBeingLabel"] = "Wellbeing: ";
						en ["HappinessLabel"] = "Happiness : ";
						en ["Wealth_Label"] = "Wealth";
						en ["Health_Level_VerySick_Male"] = "Very Sick";
						en ["Health_Level_Sick_Male"] = "Sick";
						en ["Health_Level_PoorHealth_Male"] = "Poor Health";
						en ["Health_Level_Healthy_Male"] = "Healthy";
						en ["Health_Level_VeryHealthy_Male"] = "Very Healthy";
						en ["Health_Level_ExcellentHealth_Male"] = "Excellent Health";
						en ["Health_Level_VerySick_Female"] = "Very Sick";
						en ["Health_Level_Sick_Female"] = "Sick";
						en ["Health_Level_PoorHealth_Female"] = "Poor Health";
						en ["Health_Level_Healthy_Female"] = "Healthy";
						en ["Health_Level_VeryHealthy_Female"] = "Very Healthy";
						en ["Health_Level_ExcellentHealth_Female"] = "Excellent Health";
						en ["District_Label"] = "District: ";
						en ["District_Label_tooltip"] = "In this District at the moment";
						en ["Citizen_Details_NoUnit"] = "Citizen is dead or homeless";
						en ["Citizen_wait_hearse"] 	  = "Waiting for funeral car";
						en ["Citizen_on_hearse"]	  = "On funeral car";
						en ["Citizen_hisfuneral"]	  = "Funeral ceremony";
						en ["Citizen_buried"]		  = "Six Feet Under";
						//en["Family_no_pets"] = "No pets";

						return en [index];

					}
				} catch (Exception e) {
					Debug.Error (" Language File Error " + e.ToString ());
					return "Error";
				}
			
			} else {

				if (GameLanguage == "(ITALIAN)") {
					return it [index];
				}

				return en [index];
			}
		}
	}
}
