using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using Config;

namespace MomlobInfBoss.Items.Vanilla.Weather
{

	public class DrizzleseedFlare : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableWeather;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Drizzleseed Flare");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/55668C:Rain]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1001;
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
			item.shoot = ModContent.ProjectileType<Projectiles.Vanilla.DrizzleseedFlare_Bullet>();
			item.shootSpeed = 16f;
		}



		public override bool CanUseItem(Player player)
		{
			// If there is currently no Rain, dependent on Config.
			return !(Main.raining && !ModContent.GetInstance<MainConfig>().WeatherToggle);
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
			recipe.AddIngredient(ItemID.Gel, 100);
			recipe.AddIngredient(ItemID.Waterleaf, 25);
			recipe.AddRecipeGroup("MomlobInfBoss:Corals", 10);
			if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
				recipe.AddIngredient(thorium.ItemType("FishScale"), 10);
			recipe.AddIngredient(ItemID.FlareGun);

			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
				recipe.AddTile(TileID.Anvils);
			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
				recipe.AddTile(TileID.Benches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}