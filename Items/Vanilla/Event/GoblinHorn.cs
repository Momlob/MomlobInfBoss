using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;

namespace MomlobInfBoss.Items.Vanilla.Event
{
	public class GoblinHorn : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableInvasion;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Horn");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/7CA05F:Goblin Army]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1006;
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
			item.useStyle = ItemUseStyleID.HoldingOut;
		}



		public override bool CanUseItem(Player player)
		{
			// If there is currently no Invasion
			return Main.CanStartInvasion();
		}

		public override bool UseItem(Player player)
		{
			// Summon Goblin Invasion
			Main.NewText(string.Format("[i/s1:1742] [c/7CA05F:Goblin Army] [c/909090:is invading.]"));
			Main.PlaySound(SoundID.ForceRoar, player.position, 0);
			Main.invasionType = 1;
			Main.StartInvasion();
			Main.invasionX = (double)(Main.spawnTileX - 1);
			Main.invasionWarn = 2;
			Main.invasionType = 1;
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
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Custom Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddRecipeGroup("MomlobInfBoss:Woods", 100);
				recipe.AddIngredient(ItemID.TatteredCloth, 25);
				recipe.AddIngredient(ItemID.Silk, 25);
				recipe.AddRecipeGroup("MomlobInfBoss:EvilBars", 10);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal")
					recipe.AddTile(TileID.DemonAltar);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.Anvils);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Vanilla Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Vanilla Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddRecipeGroup("MomlobInfBoss:Woods", 5);
				recipe.AddIngredient(ItemID.TatteredCloth, 10);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.Loom);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Summons Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.GoblinBattleStandard, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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
}