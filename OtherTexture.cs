using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System;

namespace FavoriteCims
{
	public class MyAtlas
	{
		public static UITextureAtlas FavCimsAtlas;
		
		public void LoadAtlasIcons() {
			
			string[] sPritesNames = {
				
				"FavoriteCimsButton",
				"FavoriteCimsButtonHovered",
				"FavoriteCimsButtonPressed",
				"FavoriteCimsButtonFocused",
				"icon_fav_subscribed",
				"icon_fav_unsubscribed"
			};
			
			FavCimsAtlas = CreateMyAtlas ("FavCimsAtlas", UIView.GetAView ().defaultAtlas.material, sPritesNames);
		}
		
		UITextureAtlas CreateMyAtlas(string AtlasName, Material BaseMat, string[] sPritesNames){
			
			var size = 1024;
			Texture2D atlasTex = new Texture2D(size, size, TextureFormat.ARGB32, false);
			
			Texture2D[] textures = new Texture2D[sPritesNames.Length];
			Rect[] rects = new Rect[sPritesNames.Length];
			
			for(int i = 0; i < sPritesNames.Length; i++)
			{
				textures[i] = ResourceLoader.loadTexture(0, 0, sPritesNames[i] + ".png");
			}
			
			rects = atlasTex.PackTextures(textures, 2, size);
			
			UITextureAtlas atlas = ScriptableObject.CreateInstance<UITextureAtlas>();
			
			Material material = Material.Instantiate(BaseMat);
			material.mainTexture = atlasTex;
			atlas.material = material;
			atlas.name = AtlasName;
			
			for (int i = 0; i < sPritesNames.Length; i++)
			{
				var spriteInfo = new UITextureAtlas.SpriteInfo()
				{
					name = sPritesNames[i],
					texture = atlasTex,
					region = rects[i]
				};
				atlas.AddSprite(spriteInfo);
			}
			return atlas;
		}
	}

	public class TextureDB : Texture
	{
		//BubblePanel
		public static Texture FavCimsOtherInfoTexture;
		public static Texture BubbleHeaderIconSpriteTextureFemale;
		public static Texture BubbleHeaderIconSpriteTextureMale;
		public static Texture BubbleFamPortBgSpriteTexture;
		public static Texture BubbleFamPortBgSpriteBackTexture;
		public static Texture BubbleDetailsBgSprite;
		public static Texture BubbleDetailsBgSpriteProblems;
		public static Texture BubbleBgBar1;
		public static Texture BubbleBgBar2;
		public static Texture BubbleBgBar1Big;
		public static Texture BubbleBgBar2Big;
		public static Texture BubbleBgBar1Small;
		public static Texture BubbleBgBar2Small;
		public static Texture BubbleBg1Special;
		public static Texture BubbleBg1Special2;
		public static Texture BubbleBgBarHover;
		public static Texture BubbleDog;
		public static Texture BubbleDogDisabled;
		public static Texture BubbleCar;
		public static Texture BubbleCarDisabled;
		public static Texture LittleStarGrey;
		public static Texture LittleStarGold;

		//CitizenRow Textures//
		//Row Separator Texture
		public static Texture FavCimsSeparator;

		//Happiness overried texture
		public static Texture FavCimsHappyOverride_texture;

		//Row Icons
		public static Texture FavCimsCitizenHomeTexture;
		public static Texture FavCimsCitizenHomeTextureHigh;
		public static Texture FavCimsWorkingPlaceTexture;
		public static Texture FavCimsCitizenHomeTextureDead;
		public static Texture FavCimsCitizenHomeTextureHomeless;
		public static Texture FavCimsWorkingPlaceTextureStudent;
		public static Texture FavCimsWorkingPlaceTextureRetired;
		public static Texture FavCimsCitizenCommercialLowTexture;
		public static Texture FavCimsCitizenCommercialHighTexture;
		public static Texture FavCimsCitizenIndustrialGenericTexture;
		//public static Texture FavCimsWorkingPlaceTextureWorkerGeneric;
		public static Texture FavCimsCitizenOfficeTexture;

		//Building Level Icons
		public static Texture[] FavCimsResidentialLevel = new Texture[6];
		public static Texture[] FavCimsIndustrialLevel = new Texture[6];
		public static Texture[] FavCimsCommercialLevel = new Texture[6];
		public static Texture[] FavCimsOfficeLevel = new Texture[6];
		
		public static void LoadFavCimsTextures () {

			try { //Building Level Icons
				
				FavCimsResidentialLevel[0] = null;
				
				for(int i = 1; i <= 5; i++) {
					
					FavCimsResidentialLevel[i] = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.levels.ResidentialLevel" + i.ToString() + ".png");
					FavCimsResidentialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsResidentialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsResidentialLevel[i].mipMapBias = -0.5f;
					FavCimsResidentialLevel[i].name = "FavCimsResidentialLevel" + i;
					
					FavCimsIndustrialLevel[i] = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.levels.IndustrialLevel" + i.ToString() + ".png");
					FavCimsIndustrialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsIndustrialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsIndustrialLevel[i].mipMapBias = -0.5f;
					FavCimsIndustrialLevel[i].name = "FavCimsIndustrialLevel" + i;
					
					FavCimsCommercialLevel[i] = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.levels.CommercialLevel" + i.ToString() + ".png");
					FavCimsCommercialLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsCommercialLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsCommercialLevel[i].mipMapBias = -0.5f;
					FavCimsCommercialLevel[i].name = "FavCimsCommercialLevel" + i;
					
					FavCimsOfficeLevel[i] = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.levels.OfficeLevel" + i.ToString() + ".png");
					FavCimsOfficeLevel[i].wrapMode = TextureWrapMode.Clamp;
					FavCimsOfficeLevel[i].filterMode = FilterMode.Bilinear;
					FavCimsOfficeLevel[i].mipMapBias = -0.5f;
					FavCimsOfficeLevel[i].name = "FavCimsOfficeLevel" + i;
					
				}
			}catch(Exception e) {
				Debug.Error("Can't load level icons : " + e.ToString());
			}

			try { //Citizen row list textures

				//Icon Separator_texture
				FavCimsSeparator = ResourceLoader.loadTexture (1, 40, "UIMainPanel.Rows.col_separator.png");
				FavCimsSeparator.wrapMode = TextureWrapMode.Clamp;
				FavCimsSeparator.filterMode = FilterMode.Bilinear;
				FavCimsSeparator.name = "FavCimsSeparator";

				//Happiness Override Texture
				FavCimsHappyOverride_texture = ResourceLoader.loadTexture (30, 30, "UIMainPanel.Rows.icon_citisenisgone.png");
				FavCimsHappyOverride_texture.wrapMode = TextureWrapMode.Clamp;
				FavCimsHappyOverride_texture.filterMode = FilterMode.Bilinear;
				FavCimsHappyOverride_texture.name = "FavCimsHappyOverride_texture";
				FavCimsHappyOverride_texture.mipMapBias = -0.5f;

				//Row Icons texture

				//Home
				FavCimsCitizenHomeTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.homeIconLow.png");
				FavCimsCitizenHomeTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTexture.mipMapBias = -0.5f;
				FavCimsCitizenHomeTexture.name = "FavCimsCitizenHomeTexture";
				
				FavCimsCitizenHomeTextureHigh = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.homeIconHigh.png");
				FavCimsCitizenHomeTextureHigh.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureHigh.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureHigh.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureHigh.name = "FavCimsCitizenHomeTexture";

				FavCimsCitizenHomeTextureDead = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.houseofthedead.png");
				FavCimsCitizenHomeTextureDead.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureDead.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureDead.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureDead.name = "FavCimsCitizenHomeTextureDead";
				
				FavCimsCitizenHomeTextureHomeless = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.homelessIcon.png");
				FavCimsCitizenHomeTextureHomeless.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenHomeTextureHomeless.filterMode = FilterMode.Bilinear;
				FavCimsCitizenHomeTextureHomeless.mipMapBias = -0.5f;
				FavCimsCitizenHomeTextureHomeless.name = "FavCimsCitizenHomeTextureHomeless";

				//Work
				FavCimsWorkingPlaceTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.nojob.png");
				//FavCimsWorkingPlaceTextureWorkerGeneric = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.workIndustry.png");
				FavCimsWorkingPlaceTextureStudent = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.workstudy.png");
				FavCimsWorkingPlaceTextureRetired = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.workretired.png");
				FavCimsCitizenCommercialLowTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.CommercialLow.png");
				FavCimsCitizenCommercialHighTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.CommercialHigh.png");
				FavCimsCitizenIndustrialGenericTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.IndustrialIcon.png");
				FavCimsCitizenOfficeTexture = ResourceLoader.loadTexture (20, 40, "UIMainPanel.Rows.OfficeIcon.png");
				FavCimsWorkingPlaceTexture.wrapMode = TextureWrapMode.Clamp;

				FavCimsWorkingPlaceTextureStudent.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTextureRetired.wrapMode = TextureWrapMode.Clamp;
				FavCimsWorkingPlaceTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialLowTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenCommercialHighTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenIndustrialGenericTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsCitizenOfficeTexture.wrapMode = TextureWrapMode.Clamp;

				FavCimsWorkingPlaceTextureStudent.filterMode = FilterMode.Bilinear;
				FavCimsWorkingPlaceTextureRetired.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialLowTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenCommercialHighTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenIndustrialGenericTexture.filterMode = FilterMode.Bilinear;
				FavCimsCitizenOfficeTexture.filterMode = FilterMode.Bilinear;

				FavCimsWorkingPlaceTextureStudent.mipMapBias = -0.5f;
				FavCimsWorkingPlaceTextureRetired.mipMapBias = -0.5f;
				FavCimsCitizenCommercialLowTexture.mipMapBias = -0.5f;
				FavCimsCitizenCommercialHighTexture.mipMapBias = -0.5f;
				FavCimsCitizenIndustrialGenericTexture.mipMapBias = -0.5f;
				FavCimsCitizenOfficeTexture.mipMapBias = -0.5f;

				FavCimsWorkingPlaceTexture.name = "FavCimsWorkingPlaceTexture";
				//FavCimsWorkingPlaceTextureWorkerGeneric.name = "FavCimsWorkingPlaceTextureWorker";
				FavCimsWorkingPlaceTextureStudent.name = "FavCimsWorkingPlaceTextureStudent";
				FavCimsWorkingPlaceTextureRetired.name = "FavCimsWorkingPlaceTextureRetired";
				FavCimsCitizenCommercialLowTexture.name = "FavCimsCitizenCommercialLowTexture";
				FavCimsCitizenCommercialHighTexture.name = "FavCimsCitizenCommercialHighTexture";
				FavCimsCitizenIndustrialGenericTexture.name = "FavCimsCitizenIndustrialHighTexture";
				FavCimsCitizenOfficeTexture.name = "FavCimsCitizenOfficeTexture";

				//BubblePanel
				BubbleHeaderIconSpriteTextureFemale = ResourceLoader.loadTexture (28, 26, "UIMainPanel.BubblePanel.Female.png");
				BubbleHeaderIconSpriteTextureFemale.wrapMode = TextureWrapMode.Clamp;
				BubbleHeaderIconSpriteTextureFemale.filterMode = FilterMode.Bilinear;
				BubbleHeaderIconSpriteTextureFemale.mipMapBias = -0.5f;
				BubbleHeaderIconSpriteTextureFemale.name = "BubbleHeaderIconSpriteTextureFemale";
				BubbleHeaderIconSpriteTextureMale = ResourceLoader.loadTexture (28, 26, "UIMainPanel.BubblePanel.Male.png");
				BubbleHeaderIconSpriteTextureMale.wrapMode = TextureWrapMode.Clamp;
				BubbleHeaderIconSpriteTextureMale.filterMode = FilterMode.Bilinear;
				BubbleHeaderIconSpriteTextureMale.mipMapBias = -0.5f;
				BubbleHeaderIconSpriteTextureMale.name = "BubbleHeaderIconSpriteTextureFemale";
				BubbleFamPortBgSpriteTexture = ResourceLoader.loadTexture (238, 151, "UIMainPanel.BubblePanel.camBg.png");
				BubbleFamPortBgSpriteTexture.wrapMode = TextureWrapMode.Clamp;
				BubbleFamPortBgSpriteTexture.filterMode = FilterMode.Bilinear;
				BubbleFamPortBgSpriteTexture.name = "BubbleCamBgSpriteTexture";
				BubbleFamPortBgSpriteBackTexture = ResourceLoader.loadTexture (234, 147, "UIMainPanel.BubblePanel.backgroundBack.jpg");
				BubbleFamPortBgSpriteBackTexture.wrapMode = TextureWrapMode.Clamp;
				BubbleFamPortBgSpriteBackTexture.filterMode = FilterMode.Bilinear;
				BubbleFamPortBgSpriteBackTexture.name = "BubbleCamBgSpriteBackTexture";
				BubbleDetailsBgSprite = ResourceLoader.loadTexture (238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSprite.png");
				BubbleDetailsBgSprite.wrapMode = TextureWrapMode.Clamp;
				BubbleDetailsBgSprite.filterMode = FilterMode.Bilinear;
				BubbleDetailsBgSprite.name = "BubbleDetailsBgSprite";
				BubbleDetailsBgSpriteProblems = ResourceLoader.loadTexture (238, 151, "UIMainPanel.BubblePanel.BubbleDetailsBgSpriteProblems.png");
				BubbleDetailsBgSpriteProblems.wrapMode = TextureWrapMode.Clamp;
				BubbleDetailsBgSpriteProblems.filterMode = FilterMode.Bilinear;
				BubbleDetailsBgSpriteProblems.name = "BubbleDetailsBgSpriteProblems";

				BubbleBgBar1 = ResourceLoader.loadTexture (236, 26, "UIMainPanel.BubblePanel.BubbleBg1.png");
				BubbleBgBar1.name = "BubbleBgBar1";
				BubbleBgBar1.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1.filterMode = FilterMode.Bilinear;
				BubbleBgBar1.mipMapBias = -0.5f;
				BubbleBgBar2 = ResourceLoader.loadTexture (236, 26, "UIMainPanel.BubblePanel.BubbleBg2.png");
				BubbleBgBar2.name = "BubbleBgBar1";
				BubbleBgBar2.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar2.filterMode = FilterMode.Bilinear;
				BubbleBgBar2.mipMapBias = -0.5f;

				BubbleBgBar1Big = ResourceLoader.loadTexture (198, 40, "UIMainPanel.BubblePanel.BubbleBg1Big.png");
				BubbleBgBar1Big.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1Big.filterMode = FilterMode.Bilinear;
				BubbleBgBar1Big.name = "BubbleBgBar1Big";

				BubbleBgBar1Small = ResourceLoader.loadTexture (198, 15, "UIMainPanel.BubblePanel.BubbleBg1Small.png");
				BubbleBgBar1Small.name = "BubbleBgBar1Small";
				BubbleBgBar1Small.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar1Small.filterMode = FilterMode.Bilinear;
				BubbleBgBar2Small = ResourceLoader.loadTexture (198, 15, "UIMainPanel.BubblePanel.BubbleBg2Small.png");
				BubbleBgBar2Small.name = "BubbleBgBar1Small";
				BubbleBgBar2Small.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBar2Small.filterMode = FilterMode.Bilinear;

				BubbleBg1Special = ResourceLoader.loadTexture (236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special.png");
				BubbleBg1Special.wrapMode = TextureWrapMode.Clamp;
				BubbleBg1Special.filterMode = FilterMode.Bilinear;
				BubbleBg1Special.name = "BubbleBg1Special";
				BubbleBg1Special2 = ResourceLoader.loadTexture (236, 26, "UIMainPanel.BubblePanel.BubbleBg1Special2.png");
				BubbleBg1Special2.wrapMode = TextureWrapMode.Clamp;
				BubbleBg1Special2.filterMode = FilterMode.Bilinear;
				BubbleBg1Special2.name = "BubbleBg1Special2";

				BubbleBgBarHover = ResourceLoader.loadTexture (236, 20, "UIMainPanel.BubblePanel.BubbleBgHeader.png");
				BubbleBgBarHover.name = "BubbleBgBar1";
				BubbleBgBarHover.wrapMode = TextureWrapMode.Clamp;
				BubbleBgBarHover.filterMode = FilterMode.Bilinear;

				BubbleDog = ResourceLoader.loadTexture (14, 20, "UIMainPanel.BubblePanel.Dog.png");
				BubbleDog.name = "BubbleDog";
				BubbleDog.wrapMode = TextureWrapMode.Clamp;
				BubbleDog.filterMode = FilterMode.Bilinear;
				BubbleDog.mipMapBias = -0.5f;

				BubbleDogDisabled = ResourceLoader.loadTexture (14, 20, "UIMainPanel.BubblePanel.DogDisabled.png");
				BubbleDogDisabled.name = "BubbleDogDisabled";
				BubbleDogDisabled.wrapMode = TextureWrapMode.Clamp;
				BubbleDogDisabled.filterMode = FilterMode.Bilinear;
				BubbleDogDisabled.mipMapBias = -0.5f;

				BubbleCar = ResourceLoader.loadTexture (31, 20, "UIMainPanel.BubblePanel.Car.png");
				BubbleCar.name = "BubbleCar";
				BubbleCar.wrapMode = TextureWrapMode.Clamp;
				BubbleCar.filterMode = FilterMode.Bilinear;
				BubbleCar.mipMapBias = -0.5f;

				BubbleCarDisabled = ResourceLoader.loadTexture (31, 20, "UIMainPanel.BubblePanel.CarDisabled.png");
				BubbleCarDisabled.name = "BubbleCarDisabled";
				BubbleCarDisabled.wrapMode = TextureWrapMode.Clamp;
				BubbleCarDisabled.filterMode = FilterMode.Bilinear;
				BubbleCarDisabled.mipMapBias = -0.5f;

				LittleStarGrey = ResourceLoader.loadTexture (20, 20, "UIMainPanel.BubblePanel.LittleStarGrey.png");
				LittleStarGrey.name = "LittleStarGrey";
				LittleStarGrey.wrapMode = TextureWrapMode.Clamp;
				LittleStarGrey.filterMode = FilterMode.Bilinear;
				LittleStarGrey.mipMapBias = -0.5f;

				LittleStarGold = ResourceLoader.loadTexture (20, 20, "UIMainPanel.BubblePanel.LittleStarGold.png");
				LittleStarGold.name = "LittleStarGold";
				LittleStarGold.wrapMode = TextureWrapMode.Clamp;
				LittleStarGold.filterMode = FilterMode.Bilinear;
				LittleStarGold.mipMapBias = -0.5f;

				FavCimsOtherInfoTexture = ResourceLoader.loadTexture (250, 500, "UIMainPanel.panel_middle.png");
				FavCimsOtherInfoTexture.wrapMode = TextureWrapMode.Clamp;
				FavCimsOtherInfoTexture.filterMode = FilterMode.Bilinear;
				FavCimsOtherInfoTexture.name = "FavCimsOtherInfoTexture";

			}catch(Exception e) {
				Debug.Error("Can't load row icons : " + e.ToString());
			}
		}	
	}
}
