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
		public static Dictionary<string, string> nl = new Dictionary<string, string> ();
		public static Dictionary<string, string> rus = new Dictionary<string, string> ();

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
						//v0.2.1
						it ["Citizen_wait_hearse"] 	  = "Aspetta il carro funebre";
						it ["Citizen_on_hearse"]	  = "Sul carro funebre";
						it ["Citizen_hisfuneral"]	  = "Cerimonia funebre";
						it ["Citizen_buried"]		  = "Sepolto";
						//v0.3
						it ["Citizen_Tourist_tooltip"] = "Questo cittadino e' in visita nella nostra citta'";
						it ["View_PassengersList"]	  = "Guarda lista passeggeri";
						it ["View_NoPassengers"]	  = "Nessun passeggero";
						it ["Vehicle_Passengers"]	  = "Passeggeri nel veicolo";
						it ["Vehicle_DriverIconText"]	  = "Al volante";
						it ["Vehicle_PasssengerIconText"] = "Passeggeri";
						it ["Citizens_HouseHolds"]	= "Guarda Inquilini";
						it ["Citizens_HouseHoldsTitle"]	= "Inquilini";
						it ["CitizenOnBuilding"]	= "Lista Dipendenti e Visitatori";
						it ["CitizenOnBuildingTitle"]	= "Dipendenti e Visitatori";
						it ["OnBuilding_Residential"] = "Appartamento ";
						it ["View_List"] = "Guarda ";
						it ["OnBuilding_Guests"]  = "Visitatori";
						it ["OnBuilding_Workers"]  = "Dipendenti al lavoro";
						it ["WorkersOnBuilding"]   = "Lavoratori nell'edificio";
						it ["OnBuilding_NoWorkers"]	= "Nessuno lavora oggi";
						it ["OnBuilding_NoGuests"] = "Nessun visitatore";
						it ["BuildingIsEmpty"] 	= "Nessuno vive o lavora in questo edificio";
						it ["OnBuilding_TotalWorkers"] = "Totale :";
						//v0.3b
						it ["Buildings_Type_CommercialEco"] = "Edificio Commerciale Ecologico";
						it ["Buildings_Type_CommercialLeisure"] = "Edificio Commerciale d'intrattenimento.";
						it ["Buildings_Type_CommercialTourist"] = "Edificio Turistico o adibito al turismo.";
						//v0.3.1
						it ["Citizen_Arrested"] = "Questo cittadino e' in prigione!";
						it ["Transported_to_Prison"] = "Scortato in Prigione!";
						it ["Jailed_into"] = "Incarcerato presso ";
						it ["Citizen_Under_Arrest"] = "Cittadini in arresto";
						it ["OnBuilding_noArrested"] = "Nessun cittadino in arresto";
						it ["OnPolice_Building_Service"] = "Secondini e Carcerati";
						it ["OnEducation_Building_Service"] = "Insegnanti e Studenti";
						it ["OnMedical_Building_Service"] = "Medici e Pazienti";
						it ["Citizen_at_School"] = "Studenti a lezione";
						it ["Citizen_on_Clinic"] = "Pazienti in cura";

						return it [index];
					
					} else if (GameLanguage == "(DUTCH)") {
						
						nl = new Dictionary<string, string> ();

						///////NIET BEWERKBAAR///////////////////////BEWERKBAAR
						nl ["FavCimsButton_tooltip"] = "Mijn favoriete cims";
						nl ["FavCimsBCMenuButton_text"] = "Burgers";
						nl ["FavCimsBCMenuButton_tooltip"] = "Gevolgde personen";
						nl ["FavCimsBBMenuButton_text"] = "Gebouwen";
						nl ["FavCimsBBMenuButton_tooltip"] = "Favoriete gebouwen";
						nl ["FavCimsBSMenuButton_text"] = "Stats";
						nl ["FavCimsBSMenuButton_tooltip"] = "Info en statistieken";
						nl ["FavStarButton_enable_tooltip"] = "Klik hier om aan favorieten toe te voegen!";
						nl ["FavStarButton_disable_tooltip"] = "Klik hier om uit favorieten te verwijderen :(";
						nl ["FavCimsHappinesColText_text"] = "Status";
						nl ["FavCimsHappinesColText_tooltip"] = "Burgergeluk";
						nl ["FavCimsNameColText_text"] = "Burgernaam";
						nl ["FavCimsNameColText_tooltip"] = "De naam van de burger";
						nl ["FavCimsEduColText_text"] = "Opleiding";
						nl ["FavCimsEduColText_tooltip"] = "Opleidingsniveau van de burger";
						nl ["FavCimsWorkingPlaceColText_text"] = "Beroep";
						nl ["FavCimsWorkingPlaceColText_tooltip"] = "Plaats waar de burger werkt of studeert";
						nl ["FavCimsAgePhaseColText_text"] = "Levensfase";
						nl ["FavCimsAgePhaseColText_tooltip"] = "Levensfase van de burger";
						nl ["FavCimsAgeColText_text"] = "Leeftijd";
						nl ["FavCimsAgeColText_tooltip"] = "Leeftijd van de burger";
						nl ["FavCimsHomeColText_text"] = "Woning van burger";
						nl ["FavCimsHomeColText_tooltip"] = "Burger woont in dit huis";
						nl ["FavCimsLastActColText_text"] = "Laatste activiteit";
						nl ["FavCimsLastActColText_tooltip"] = "Wat de burger op dit moment aan het doen is";
						nl ["FavCimsCloseButtonCol_text"] = "Sluiten";
						nl ["FavCimsCloseButtonCol_tooltip"] = "Klik op de knop \"Sluiten\" om de burger uit de favorieten te verwijderen :(";
						nl ["Male"] = "Mannelijk";
						nl ["Female"] = "Vrouwelijk";
						nl ["AgePhase_Child_Male"] = "Jongetje";
						nl ["AgePhase_Teen_Male"] = "Jongen";
						nl ["AgePhase_Young_Male"] = "Jonge man";
						nl ["AgePhase_Adult_Male"] = "Man";
						nl ["AgePhase_Senior_Male"] = "Oude man";
						nl ["Education_Uneducated_Male"] = "Ongeschoold";
						nl ["Education_OneSchool_Male"] = "Laaggeschoold";
						nl ["Education_TwoSchools_Male"] = "Middengeschoold";
						nl ["Education_ThreeSchools_Male"] = "Hooggeschoold";
						nl ["AgePhase_Child_Female"] = "Meisje";
						nl ["AgePhase_Teen_Female"] = "Meid";
						nl ["AgePhase_Young_Female"] = "Jonge vrouw";
						nl ["AgePhase_Adult_Female"] = "Vrouw";
						nl ["AgePhase_Senior_Female"] = "Oude vrouw";
						nl ["Education_Uneducated_Female"] = "Ongeschoold";
						nl ["Education_OneSchool_Female"] = "Laaggeschoold";
						nl ["Education_TwoSchools_Female"] = "Middengeschoold";
						nl ["Education_ThreeSchools_Female"] = "Hooggeschoold";
						nl ["AgePhaseDead_Male"] = "Dood";
						nl ["AgePhaseDead_Female"] = "Dood";
						nl ["People_Life_Status_Alive"] = "Burger leeft";
						nl ["People_Life_Status_Dead"] = "Burger is dood";
						nl ["People_Life_Status_Dead_date"] = "op deze datum";
						nl ["People_Life_Status_Dead_time"] = "om";
						nl ["NotificationIconVeryUnhappy"] = "Zeer ongelukkig";
						nl ["NotificationIconUnhappy"] = "Ongelukkig";
						nl ["NotificationIconHappy"] = "Gelukkig";
						nl ["NotificationIconVeryHappy"] = "Zeer gelukkig";
						nl ["NotificationIconExtremelyHappy"] = "Buitengewoon gelukkig";
						nl ["time_format"] = "dd-mm-yyyy";
						nl ["People_Life_Status_IsGone"] = "Burger is momenteel niet in de stad";
						nl ["Citizen_HomeLess"] = "Dakloos";
						nl ["Citizen_HomeLess_tooltip"] = "Burger heeft geen woning";
						nl ["HomeOutsideTheCity"] = "Erg ver weg";
						nl ["Home_Location_Dead"] = "Op de begraafplaats";
						nl ["Citizen_Retired"] = "Gepensioneerd";
						nl ["Citizen_Retired_tooltip"] = "Burger is met pensioen";
						nl ["Unemployed_tooltip"] = "Burger heeft momenteel veel vrije tijd...";
						nl ["Government_job"] = "Overheidsgebouw";
						nl ["Electricity_job"] = "Electriciteitscentrale";
						nl ["Water_job"] = "Waterbedrijf";
						nl ["Generic_job_place"] = "Burger werkt in dit gebouw";
						nl ["Vehicle_on_foot"] = "Burger is te voet";
						nl ["Citizen_Details"] = "Burgerdetails";
						nl ["Citizen_Details_fullTemplate"] = "Te veel gezinvensters geopend, sluit er alvorens andere te openen";
						nl ["Status_static_label"] = "Status: ";
						nl ["Citizen_Family_unit"] = "Gezin";
						nl ["NowInThisDistrict"] = "Is op dit moment in het district: ";
						nl ["DistrictLabel"] = "District: ";
						nl ["DistrictNameNoDistrict"] = "Geen";
						nl ["Citizen_Details_No_Partner"] = "Geen partner";
						nl ["Citizen_Details_No_Childs"] = "Geen kinderen";
						nl ["Right_click_to_swith_tooltip"] = "Rechtermuisknop om van burger te wisselen";
						nl ["Home_Bubble_panel_Label"] = "Woning: ";
						nl ["Low_Wealth_Male"] = "Arm";
						nl ["Mid_Wealth_Male"] = "Welgesteld";
						nl ["High_Wealth_Male"] = "Rijk";
						nl ["Low_Wealth_Female"] = "Arm";
						nl ["Mid_Wealth_Female"] = "Welgesteld";
						nl ["High_Wealth_Female"] = "Rijk";
						nl ["WellBeingLabel"] = "Welzijn: ";
						nl ["HappinessLabel"] = "Geluk: ";
						nl ["Wealth_Label"] = "Rijkdom";
						nl ["Health_Level_VerySick_Male"] = "Zeer ziek";
						nl ["Health_Level_Sick_Male"] = "Ziek";
						nl ["Health_Level_PoorHealth_Male"] = "Zwakke gezondheid";
						nl ["Health_Level_Healthy_Male"] = "Normale gezondheid";
						nl ["Health_Level_VeryHealthy_Male"] = "Goede gezondheid";
						nl ["Health_Level_ExcellentHealth_Male"] = "Uitmuntende gezondheid";
						nl ["Health_Level_VerySick_Female"] = "Zeer ziek";
						nl ["Health_Level_Sick_Female"] = "Ziek";
						nl ["Health_Level_PoorHealth_Female"] = "Zwakke gezondheid";
						nl ["Health_Level_Healthy_Female"] = "Normale gezondheid";
						nl ["Health_Level_VeryHealthy_Female"] = "Goede gezondheid";
						nl ["Health_Level_ExcellentHealth_Female"] = "Uitmuntende gezondheid";
						nl ["District_Label"] = "District: ";
						nl ["District_Label_tooltip"] = "Momenteel in de district";
						nl ["Citizen_Details_NoUnit"] = "Burger is dood of dakloos";
						//v0.2.1
						nl ["Citizen_wait_hearse"] = "Wacht op een lijkwagen";
						nl ["Citizen_on_hearse"] = "In lijkwagen";
						nl ["Citizen_hisfuneral"] = "Begravenisceremonie";
						nl ["Citizen_buried"] = "Onder de groene zoden";
						//v0.3
						nl ["Citizen_Tourist_tooltip"] = "Deze burger bezoekt onze stad"; //locstatus
						nl ["View_PassengersList"] = "Passagierslijst bekijken";
						nl ["View_NoPassengers"] = "Geen passagiers";
						nl ["Vehicle_Passengers"] = "Passagiers aan boord";
						nl ["Vehicle_DriverIconText"] = "Voertuigbestuurder";
						nl ["Vehicle_PasssengerIconText"] = "Passagiers";
						nl ["Citizens_HouseHolds"] = "Bewoners bekijken";
						nl ["Citizens_HouseHoldsTitle"] = "Bewoners";
						nl ["CitizenOnBuilding"] = "Werknemers- en gastenlijst";
						nl ["CitizenOnBuildingTitle"] = "Werknemers en gasten";
						nl ["OnBuilding_Residential"] = "Appartement ";
						nl ["View_List"] = "Bekijk ";
						nl ["OnBuilding_Guests"] = "Gasten";
						nl ["OnBuilding_Workers"] = "Werknemers op het werk";
						nl ["WorkersOnBuilding"] = "Lijst van werknemers aan het werk";
						nl ["OnBuilding_NoWorkers"] = "Niemand aan het werk";
						nl ["OnBuilding_NoGuests"] = "Geen gasten binnen";
						nl ["BuildingIsEmpty"] = "Niemand werkt of leeft hier";
						nl ["OnBuilding_TotalWorkers"] = "Totaal: ";
						//v0.3b
						nl ["Buildings_Type_CommercialEco"] = "Ecologisch commercieel gebouw";
						nl ["Buildings_Type_CommercialLeisure"] = "Recreatief commercieel gebouw";
						nl ["Buildings_Type_CommercialTourist"] = "Touristisch commercieel gebouw";
						//v0.3.1
						nl ["Citizen_Arrested"] = "Deze burger zit in de gevangenis!";
						nl ["Transported_to_Prison"] = "Getransporteerd naar de gevangenis";
						nl ["Jailed_into"] = "Gevangen in ";
						nl ["Citizen_Under_Arrest"] = "Burgers onder arrest";
						nl ["OnBuilding_noArrested"] = "Niemand gevangen hier";
						nl ["OnPolice_Building_Service"] = "Bewakers en gevangenen";
						nl ["OnEducation_Building_Service"] = "Leerkrachten en studenten";
						nl ["OnMedical_Building_Service"] = "Dokters en patiënten";
						nl ["Citizen_at_School"] = "Studenten op school";
						nl ["Citizen_on_Clinic"] = "Patiënten die worden behandeld";

						return nl [index];

					} else if (GameLanguage == "(RUSSIAN)") {

						rus = new Dictionary<string, string> ();

						///////NON MODIFICABILE//////////////////////MODIFICABILE
						rus ["FavCimsButton_tooltip"] = "My Favorite Cims";
						rus ["FavCimsBCMenuButton_text"] = "Жители";
						rus ["FavCimsBCMenuButton_tooltip"] = "Люди";
						rus ["FavCimsBBMenuButton_text"] = "Здания";
						rus ["FavCimsBBMenuButton_tooltip"] = "Избранные здания";
						rus ["FavCimsBSMenuButton_text"] = "Показатели";
						rus ["FavCimsBSMenuButton_tooltip"] = "Инфо и показатели";
						rus ["FavStarButton_enable_tooltip"] = "Кликни, чтобы добавить в Избранные!";
						rus ["FavStarButton_disable_tooltip"] = "Кликни, чтобы удалить из Избранных :(";
						rus ["FavCimsHappinesColText_text"] = "Статус";
						rus ["FavCimsHappinesColText_tooltip"] = "Статус счастья";
						rus ["FavCimsNameColText_text"] = "Имя жителя";
						rus ["FavCimsNameColText_tooltip"] = "Имя жителя";
						rus ["FavCimsEduColText_text"] = "Образование";
						rus ["FavCimsEduColText_tooltip"] = "Уровень образования";
						rus ["FavCimsWorkingPlaceColText_text"] = "Место учебы/работы";
						rus ["FavCimsWorkingPlaceColText_tooltip"] = "Место где учится/работает житель";
						rus ["FavCimsAgePhaseColText_text"] = "Возрастная категория";
						rus ["FavCimsAgePhaseColText_tooltip"] = "Категория возраста жителя";
						rus ["FavCimsAgeColText_text"] = "Возраст";
						rus ["FavCimsAgeColText_tooltip"] = "Возраст жителя";
						rus ["FavCimsHomeColText_text"] = "Место жительства";
						rus ["FavCimsHomeColText_tooltip"] = "Горожанин живет здесь";
						rus ["FavCimsLastActColText_text"] = "Последняя активность";
						rus ["FavCimsLastActColText_tooltip"] = "Чем житель занят в данный момент";
						rus ["FavCimsCloseButtonCol_text"] = "Закрыть";
						rus ["FavCimsCloseButtonCol_tooltip"] = "Нажми Закрыть, чтобы убрать жителя из Избранных :(";
						rus ["Male"] = "Мужчина";
						rus ["Female"] = "Женщина";
						rus ["AgePhase_Child_Male"] = "Ребенок";
						rus ["AgePhase_Teen_Male"] = "Подросток";
						rus ["AgePhase_Young_Male"] = "Молодой";
						rus ["AgePhase_Adult_Male"] = "Взрослый";
						rus ["AgePhase_Senior_Male"] = "Старый";
						rus ["Education_Uneducated_Male"] = "Необразован";
						rus ["Education_OneSchool_Male"] = "Начальная школа";
						rus ["Education_TwoSchools_Male"] = "Средняя школа";
						rus ["Education_ThreeSchools_Male"] = "Университет";
						rus ["AgePhase_Child_Female"] = "Ребенок";
						rus ["AgePhase_Teen_Female"] = "Подросток";
						rus ["AgePhase_Young_Female"] = "Молодая";
						rus ["AgePhase_Adult_Female"] = "Взрослая";
						rus ["AgePhase_Senior_Female"] = "Старая";
						rus ["Education_Uneducated_Female"] = "Необразована";
						rus ["Education_OneSchool_Female"] = "Начальная школа";
						rus ["Education_TwoSchools_Female"] = "Средняя школа";
						rus ["Education_ThreeSchools_Female"] = "Университет";
						rus ["AgePhaseDead_Male"] = "Умер";
						rus ["AgePhaseDead_Female"] = "Умерла";
						rus ["People_Life_Status_Alive"] = "Горожанин жив";
						rus ["People_Life_Status_Dead"] = "Горожанин умер";
						rus ["People_Life_Status_Dead_date"] = "в этот день";
						rus ["People_Life_Status_Dead_time"] = "в это время";
						rus ["NotificationIconVeryUnhappy"] = "Горемыка";
						rus ["NotificationIconUnhappy"] = "Грустный(ая)";
						rus ["NotificationIconHappy"] = "Радостный(ая)";
						rus ["NotificationIconVeryHappy"] = "Счастливец(а)";
						rus ["NotificationIconExtremelyHappy"] = "На седьмом небе";
						rus ["time_format"] = "мм-дд-гггг";
						rus ["People_Life_Status_IsGone"] = "Житель уехал из города";
						rus ["Citizen_HomeLess"] = "Бомж";
						rus ["Citizen_HomeLess_tooltip"] = "У жителя нет дома";
						rus ["HomeOutsideTheCity"] = "Далеко-далеко";
						rus ["Home_Location_Dead"] = "На кладбище";
						rus ["Citizen_Retired"] = "Не работает";
						rus ["Citizen_Retired_tooltip"] = "Житель больше не работает";
						rus ["Unemployed_tooltip"] = "У жителя полно свободного времени...";
						rus ["Government_job"] = "Административное Здание";
						rus ["Electricity_job"] = "Электростанция";
						rus ["Water_job"] = "Гидростанция";
						rus ["Generic_job_place"] = "Житель работает здесь";
						rus ["Vehicle_on_foot"] = "Житель идет пешком";
						rus ["Citizen_Details"] = "Подробности";
						rus ["Citizen_Details_fullTemplate"] = "Открыто слишком много Панелей Семей";
						rus ["Status_static_label"] = "Статус : ";
						rus ["Citizen_Family_unit"] = "Семья";
						rus ["NowInThisDistrict"] = "Находится в районе: ";
						rus ["DistrictLabel"] = "Район: ";
						rus ["DistrictNameNoDistrict"] = "Отсутсвует";
						rus ["Citizen_Details_No_Partner"] = "Одинок(а)";
						rus ["Citizen_Details_No_Childs"] = "Детей нет";
						rus ["Right_click_to_swith_tooltip"] = "Правая кнопка - сменить жителя";
						rus ["Home_Bubble_panel_Label"] = "Дом : ";
						rus ["Low_Wealth_Male"] = "Бедный";
						rus ["Mid_Wealth_Male"] = "Обеспеченный";
						rus ["High_Wealth_Male"] = "Богатый";
						rus ["Low_Wealth_Female"] = "Бедная";
						rus ["Mid_Wealth_Female"] = "Обеспеченная";
						rus ["High_Wealth_Female"] = "Богатая";
						rus ["WellBeingLabel"] = "Благополучие: ";
						rus ["HappinessLabel"] = "Счастье : ";
						rus ["Wealth_Label"] = "Благосостояние";
						rus ["Health_Level_VerySick_Male"] = "Тяжело болен";
						rus ["Health_Level_Sick_Male"] = "Болен";
						rus ["Health_Level_PoorHealth_Male"] = "Хворает";
						rus ["Health_Level_Healthy_Male"] = "В норме";
						rus ["Health_Level_VeryHealthy_Male"] = "Здоров";
						rus ["Health_Level_ExcellentHealth_Male"] = "До безобразия здоров";
						rus ["Health_Level_VerySick_Female"] = "Тяжело больна";
						rus ["Health_Level_Sick_Female"] = "Больна";
						rus ["Health_Level_PoorHealth_Female"] = "Хворает";
						rus ["Health_Level_Healthy_Female"] = "В норме";
						rus ["Health_Level_VeryHealthy_Female"] = "Здорова";
						rus ["Health_Level_ExcellentHealth_Female"] = "До неприличия здорова";
						rus ["District_Label"] = "Район: ";
						rus ["District_Label_tooltip"] = "Сейчас в этом районе";
						rus ["Citizen_Details_NoUnit"] = "Житель умер или бомж";
						rus ["Citizen_wait_hearse"] = "Ждет катафалк";
						rus ["Citizen_on_hearse"] = "В катафалке";
						rus ["Citizen_hisfuneral"]	= "Похороны";
						rus ["Citizen_buried"] = "Погребен(а)";
						//v0.3
						rus ["Citizen_Tourist_tooltip"] = "Этот турист посещает ваш город"; 
						rus ["View_PassengersList"] = "Просмотр пассажиров";
						rus ["View_NoPassengers"] = "Пассажиров нет";
						rus ["Vehicle_Passengers"] = "Пассажиры в салоне";
						rus ["Vehicle_DriverIconText"]	= "Водитель";
						rus ["Vehicle_PasssengerIconText"] = "Пассажиры";
						rus ["Citizens_HouseHolds"] = "Просмотр жильцов";
						rus ["Citizens_HouseHoldsTitle"] = "Жильцы";
						rus ["CitizenOnBuilding"]	= "Список кадров и гостей";
						rus ["CitizenOnBuildingTitle"]	= "Сотрудники и Гости"; 
						rus ["OnBuilding_Residential"] = "Квартира ";
						rus ["View_List"] = "Просмотр ";
						rus ["OnBuilding_Guests"] = "Посетители";
						rus ["OnBuilding_Workers"] = "Сотрудники";
						rus ["WorkersOnBuilding"] = "Список сотрудников"; 
						rus ["OnBuilding_NoWorkers"] = "На работе никого";
						rus ["OnBuilding_NoGuests"] = "Посетителей нет";
						rus ["BuildingIsEmpty"] = "Жителей/работников нет";
						rus ["OnBuilding_TotalWorkers"] = "Всего :";
						//v0.3b
						rus ["Buildings_Type_CommercialEco"] = "Коммер. здание (Экология)"; 
						rus ["Buildings_Type_CommercialLeisure"] = "Коммер. здание (Досуг и отдых)";
						rus ["Buildings_Type_CommercialTourist"] = "Коммер. здание (Туризм)";
						//v0.3.1 MANCANTE !!
						rus ["Citizen_Arrested"] = "This Citizen is in Jail!";
						rus ["Transported_to_Prison"] = "Transported to Prison";
						rus ["Jailed_into"] = "Jailed into ";
						rus ["Citizen_Under_Arrest"] = "Citizens under arrest";
						rus ["OnBuilding_noArrested"] = "Nobody jailed here";
						rus ["OnPolice_Building_Service"] = "Guards and Prisoners";
						rus ["OnEducation_Building_Service"] = "Teachers and Students";
						rus ["OnMedical_Building_Service"] = "Doctors and Patients";
						rus ["Citizen_at_School"] = "Students at school";
						rus ["Citizen_on_Clinic"] = "Patients being treated";

						return rus [index];

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
						//v0.2.1
						en ["Citizen_wait_hearse"] 	  = "Waiting for funeral car";
						en ["Citizen_on_hearse"]	  = "On funeral car";
						en ["Citizen_hisfuneral"]	  = "Funeral ceremony";
						en ["Citizen_buried"]		  = "Six Feet Under";
						//v0.3
						en ["Citizen_Tourist_tooltip"] = "This citizen is visiting our City"; //locstatus
						en ["View_PassengersList"]	   = "View passengers list";
						en ["View_NoPassengers"]	   = "No passengers";
						en ["Vehicle_Passengers"]	   = "Passengers on board";
						en ["Vehicle_DriverIconText"]	  = "Vehicle Driver";
						en ["Vehicle_PasssengerIconText"] = "Passengers";
						en ["Citizens_HouseHolds"]	= "View Residents";
						en ["Citizens_HouseHoldsTitle"]	= "Residents";
						en ["CitizenOnBuilding"]	= "Employees and Guests List";
						en ["CitizenOnBuildingTitle"]	= "Employees and Guests";
						en ["OnBuilding_Residential"] = "Apartment ";
						en ["View_List"] = "View ";
						en ["OnBuilding_Guests"]  = "Guests";
						en ["OnBuilding_Workers"]  = "Employees at work";
						en ["WorkersOnBuilding"]   = "List of employees at work";
						en ["OnBuilding_NoWorkers"]	= "Nobody at work";
						en ["OnBuilding_NoGuests"] = "No guests inside";
						en ["BuildingIsEmpty"] 	= "Nobody works or lives here";
						en ["OnBuilding_TotalWorkers"] = "Tot :";
						//v0.3b
						en ["Buildings_Type_CommercialEco"] = "Ecologic Commercial Building";
						en ["Buildings_Type_CommercialLeisure"] = "Leisure Commercial Building";
						en ["Buildings_Type_CommercialTourist"] = "Tourism Commercial Building";
						//v0.3.1
						en ["Citizen_Arrested"] = "This Citizen is in Jail!";
						en ["Transported_to_Prison"] = "Transported to Prison";
						en ["Jailed_into"] = "Jailed into ";
						en ["Citizen_Under_Arrest"] = "Citizens under arrest";
						en ["OnBuilding_noArrested"] = "Nobody jailed here";
						en ["OnPolice_Building_Service"] = "Guards and Prisoners";
						en ["OnEducation_Building_Service"] = "Teachers and Students";
						en ["OnMedical_Building_Service"] = "Doctors and Patients";
						en ["Citizen_at_School"] = "Students at school";
						en ["Citizen_on_Clinic"] = "Patients being treated";

						return en [index];

					}
				} catch /*(Exception e)*/ {
					//Debug.Error (" Language File Error " + e.ToString ());
					return "language Error";
				}
			
			} else {

				if (GameLanguage == "(ITALIAN)") {
					return it [index];
				}


				if (GameLanguage == "(DUTCH)") {
					return nl [index];
				}

				if (GameLanguage == "(RUSSIAN)") {
					return rus [index];
				}
					
				return en [index];
			}
		}
	}
}
