using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class ScourgeCircuitry : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scourge Circuitry");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/A0A0A0:The Destroyer]\n[c/606060:Usable at Night]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1024;
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
					return NPC.downedMechBoss1;
				else
					return true;
			}

			// If its Nighttime, and no Destroyer is alive
			else
				return!Main.dayTime && !NPC.AnyNPCs(NPCID.TheDestroyer);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Destroyer
			Main.NewText(string.Format("[i/s1:2113] [c/A0A0A0:The Destroyer] [c/909090:has been reactivated.]"));
			NPC.NewNPC((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + 1200, NPCID.TheDestroyer);
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
				recipe.AddRecipeGroup("MomlobInfBoss:BaseSouls", 50);
				recipe.AddRecipeGroup("MomlobInfBoss:MythrilBars", 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddRecipeGroup("MomlobInfBoss:BeholderBars", 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("DubiousPlating"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("MysteriousCircuitry"), 10);
				recipe.AddRecipeGroup("MomlobInfBoss:EvilIngreds", 25);
				recipe.AddIngredient(ItemID.SpiderFang, 10);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.MythrilAnvil);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Vanilla Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Vanilla Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.SoulofNight, 6);
				recipe.AddRecipeGroup("MomlobInfBoss:IronBars", 5);
				recipe.AddRecipeGroup("MomlobInfBoss:EvilIngreds", 3);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.MythrilAnvil);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Summons Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.MechanicalWorm, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.MythrilAnvil);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}