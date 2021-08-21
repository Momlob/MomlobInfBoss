using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class BurdenedSkull : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burdened Skull");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/CCCC9F:Skeletron]\n[c/606060:Usable at Night]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1011;
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
			// Limit Breaker
			if (player.GetModPlayer<MIBPlayer>().BuffLimitBreaker == true)
			{
				if (ModContent.GetInstance<MainConfig>().UndefeatedLimit)
					return NPC.downedBoss3;
				else
					return true;
			}

			// If its Nighttime, and no Skeletron or Dungeon Guardian is alive
			else
				return !Main.dayTime && !NPC.AnyNPCs(NPCID.SkeletronHead) && !NPC.AnyNPCs(NPCID.DungeonGuardian);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Skeletron
			Main.NewText(string.Format("[i/s1:1281] [c/CCCC9F:Skeletron] [c/909090:is drawing near.]"));
			NPC.NewNPC((int)player.position.X + Main.rand.Next(-400, 400), (int)player.position.Y - 1200, NPCID.SkeletronHead);
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
				recipe.AddIngredient(ItemID.Obsidian, 100);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddRecipeGroup("MomlobInfBoss:EvilCalamityBossDrops", 25);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x))
					recipe.AddRecipeGroup("MomlobInfBoss:EvilBossDrops", 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("AncientBoneDust"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("MagmaCore"), 10);
				recipe.AddRecipeGroup("MomlobInfBoss:DungeonBricks", 10);
				recipe.AddRecipeGroup("MomlobInfBoss:EvilAmulets");

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal")
					recipe.AddTile(TileID.DemonAltar);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.Anvils);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Vanilla / Summons Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Vanilla Recipes" || ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.ClothierVoodooDoll, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal")
					recipe.AddTile(TileID.DemonAltar);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.Anvils);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}