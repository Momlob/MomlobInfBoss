using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Projectiles.Vanilla;
using MomlobInfBoss.Buffs.Vanilla;

namespace MomlobInfBoss.Items.Vanilla.Other
{
	public class LimitBreaker : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableOther;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Limit Breaker");
			Tooltip.SetDefault("[c/909090:Allows Summons to spawn Multiple Bosses.]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 999;
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;

			item.maxStack = 1;
			item.rare = ItemRarityID.Blue;
		}



        public override void UpdateInventory(Player player)
        {
			// Apply Limit Breaker Buff
			player.AddBuff(ModContent.BuffType<LimitBreaker_Buff>(), 10, true);
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
			recipe.AddRecipeGroup("MomlobInfBoss:SilverBars", 25);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddRecipeGroup("MomlobInfBoss:Counterweights");

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