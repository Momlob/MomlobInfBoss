using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using Config;

namespace MomlobInfBoss.Items.Vanilla.Weather
{

	public class DustsquallFlare : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableWeather;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dustsquall Flare");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/B36741:Sandstorm]\n[c/606060:Usable in the Desert]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1002;
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;

			item.maxStack = 1;
			item.consumable = false;
			item.rare = ItemRarityID.Blue;

			item.useAnimation = 45;
			item.useTime = 45;
			item.autoReuse = false;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.UseSound = SoundID.Item41;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<Projectiles.Vanilla.DustsquallFlare_Bullet>();
			item.shootSpeed = 20f;
		}



		public override bool CanUseItem(Player player)
		{
			// If in Desert, and there is currently no Sandstorm dependent on Config. 
			return player.ZoneDesert && !(Sandstorm.Happening && !ModContent.GetInstance<MainConfig>().WeatherToggle);
		}



		public override void AddRecipes()
		{
			// Mod Calls
			Mod my_materials = ModLoader.GetMod("MomlobBossMat");
			bool my_materials_x = (my_materials != null && ModContent.GetInstance<MainConfig>().EnableMyIng);
			Mod calamity = ModLoader.GetMod("CalamityMod");
			bool calamity_x = (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity);
			Mod thorium = ModLoader.GetMod("ThoriumMod");
			bool thorium_x = (thorium != null && ModContent.GetInstance<MainConfig>().EnableThorium);

			// Base Recipe
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("MomlobInfBoss:Sands", 100);
			recipe.AddIngredient(ItemID.AntlionMandible, 10);
			recipe.AddIngredient(ItemID.Amber, 10);
			if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
				recipe.AddIngredient(thorium.ItemType("BirdTalon"), 10);
			if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x && !thorium_x)
				recipe.AddIngredient(calamity.ItemType("DesertFeather"), 10);
			recipe.AddIngredient(ItemID.IllegalGunParts);

			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
				recipe.AddTile(TileID.Anvils);
			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
				recipe.AddTile(TileID.Benches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}