using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class SeethingIdol : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seething Idol");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/964A10:Golem]\n[c/606060:Usable in the Lihzahrds Temple]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1030;
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
					return NPC.downedGolemBoss;
				else
					return true;
			}

			// If in Jungle Temple, and no Golem is alive	(Because im a clueless Idiot, the Temple Check uses Music instead)
			else
				return Main.curMusic == 26 && !NPC.AnyNPCs(NPCID.Golem);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Golem
			Main.NewText(string.Format("[i/s1:2110] [c/964A10:Golem] [c/909090:has been reactivated.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 100, (int)player.position.Y - Main.rand.Next(100, 200), NPCID.Golem);
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
				recipe.AddIngredient(ItemID.LunarTabletFragment, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("SolarPebble"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("DraedonBar"), 25);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && (thorium_x || calamity_x)))
					recipe.AddIngredient(ItemID.HellstoneBar, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("MoltenResidue"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && my_materials_x)
					recipe.AddIngredient(my_materials.ItemType("VenusPetal"), 5);
				recipe.AddIngredient(ItemID.TurtleShell, 5);
				recipe.AddIngredient(ItemID.PhilosophersStone);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.MythrilAnvil);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			// Vanilla / Summons Recipe
			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons" || ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.LihzahrdPowerCell, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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