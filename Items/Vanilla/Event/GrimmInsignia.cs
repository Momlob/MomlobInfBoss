using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;

namespace MomlobInfBoss.Items.Vanilla.Event
{
	public class GrimmInsignia : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableInvasion;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grimm Insignia");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/BC3434:Blood Moon]\n[c/606060:Usable at Night]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1004;
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;

			item.maxStack = 1;
			item.consumable = false;
			item.rare = ItemRarityID.Blue;

			item.useAnimation = 15;
			item.useTime = 15;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingUp;
		}



		public override bool CanUseItem(Player player)
		{
			// If it is Nighttime, and there is currently no other Moon Event.
			return !Main.dayTime && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon;
		}

		public override bool UseItem(Player player)
		{
			// Spawn Bloodmoon.
			Main.NewText(string.Format("[i/s1:1819] [c/909090:The Moon] [c/BC3434:twists] [c/909090:upon your Command.]"));
			Main.bloodMoon = true;
			Main.PlaySound(SoundID.ForceRoar, player.position, 0);
			return true;
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
			recipe.AddRecipeGroup("MomlobInfBoss:EvilWoods", 100);
			recipe.AddRecipeGroup("MomlobInfBoss:EvilPowders", 50);
			recipe.AddRecipeGroup("MomlobInfBoss:SilverBars", 25);
			if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
				recipe.AddIngredient(thorium.ItemType("Blood"), 10);
			recipe.AddIngredient(ItemID.FallenStar, 10);

			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal")
				recipe.AddTile(TileID.DemonAltar);
			if (ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
				recipe.AddTile(TileID.Anvils);
			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
				recipe.AddTile(TileID.Benches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}