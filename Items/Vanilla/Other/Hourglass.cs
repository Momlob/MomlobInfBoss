using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Projectiles.Vanilla;

namespace MomlobInfBoss.Items.Vanilla.Other
{
	public class Hourglass : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableOther;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aethers Hourglass");
			Tooltip.SetDefault("[c/909090:The Power of Time at your Fingertips.]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 998;
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
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.Item117;
		}



		public override bool CanUseItem(Player player)
		{
			// If no Boss is alive.
			return !NPC.AnyDanger();
		}

		public override bool UseItem(Player player)
		{
			// Spawn Hourglass
			Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, -8, ModContent.ProjectileType<Hourglass_Active>(), 0, 0f, Main.myPlayer);
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
			recipe.AddIngredient(ItemID.Glass, 100);
			recipe.AddRecipeGroup("MomlobInfBoss:Sands", 100);
			recipe.AddIngredient(ItemID.Marble, 50);
			recipe.AddIngredient(ItemID.Granite, 50);
			recipe.AddIngredient(ItemID.SunplateBlock, 25);

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