using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class DubiousGaze : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dubious Gaze");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/D7D7D7:Eye of Cthulhu]\n[c/606060:Usable at Night]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1005;
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 36;

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
			// Limit Breaker
			if (player.GetModPlayer<MIBPlayer>().BuffLimitBreaker == true)
			{
				if (ModContent.GetInstance<MainConfig>().UndefeatedLimit)
					return NPC.downedBoss1;
				else
					return true;
			}

			// If its Nighttime, and no Eye of Cthulhu is alive
			else
				return !Main.dayTime && !NPC.AnyNPCs(NPCID.EyeofCthulhu);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Eye of Cthulhu
			Main.NewText(string.Format("[i/s1:2112] [c/D7D7D7:Eye of Cthulhu] [c/909090:is drawing near.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 1200, (int)player.position.Y - Main.rand.Next(200, 400), NPCID.EyeofCthulhu);
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
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Custom Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddRecipeGroup("MomlobInfBoss:Lens", 25);
				recipe.AddRecipeGroup("MomlobInfBoss:EvilIngreds", 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("BloodOrb"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("UnholyShards"), 25);
				recipe.AddIngredient(ItemID.SharkToothNecklace);

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
				recipe.AddRecipeGroup("MomlobInfBoss:Lens", 6);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal")
					recipe.AddTile(TileID.DemonAltar);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.Anvils);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Summons Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.SuspiciousLookingEye, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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