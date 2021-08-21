using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class HiveRegalia : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hive Regalia");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/EFAC10:Queen Bee]\n[c/606060:Usable in the Jungle]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1010;
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
					return NPC.downedQueenBee;
				else
					return true;
			}

			// If in Jungle, and no Queen Bee is alive
			else
				return player.ZoneJungle && !NPC.AnyNPCs(NPCID.QueenBee);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Queen Bee
			Main.NewText(string.Format("[i/s1:2108] [c/EFAC10:Queen Bee] [c/909090:was provoked.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 1200, (int)player.position.Y - Main.rand.Next(200, 400), NPCID.QueenBee);
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
				recipe.AddIngredient(ItemID.Hive, 250);
				recipe.AddRecipeGroup("MomlobInfBoss:HoneyBlocks", 50);
				recipe.AddIngredient(ItemID.JungleSpores, 25);
				recipe.AddIngredient(ItemID.Stinger, 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("Petal"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x && !thorium_x)
					recipe.AddIngredient(calamity.ItemType("MurkyPaste"), 10);
				recipe.AddIngredient(ItemID.AnkletoftheWind);

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
				recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.Hive, 5);
				recipe.AddRecipeGroup("MomlobInfBoss:HoneyBlocks", 5);
				recipe.AddIngredient(ItemID.BottledHoney, 1);
				recipe.AddIngredient(ItemID.Stinger, 1);

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
				recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.Abeemination, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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