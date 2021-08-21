using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class DoombringersRelic : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doombringers Relic");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/A7F5E3:Moon Lord]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1035;
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
					return NPC.downedMoonlord;
				else
					return true;
			}

			// If neither Cultist, the Pillars nor Moonlord himself are alive
			else
				return !NPC.AnyNPCs(NPCID.CultistBoss) && Main.CanStartInvasion() && !NPC.AnyNPCs(NPCID.MoonLordCore) && !NPC.AnyNPCs(NPCID.LunarTowerSolar) && !NPC.AnyNPCs(NPCID.LunarTowerVortex) && !NPC.AnyNPCs(NPCID.LunarTowerNebula) && !NPC.AnyNPCs(NPCID.LunarTowerStardust);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Moonlord
			Main.NewText(string.Format("[i/s1:3373] [c/A7F5E3:Moon Lord] [c/909090:has decended.]"));
			NPC.NewNPC((int)player.position.X, (int)player.position.Y - 1200, NPCID.MoonLordCore);
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

			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Custom Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddRecipeGroup("MomlobInfBoss:Fragments", 100);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && (thorium_x || calamity_x)))
					recipe.AddIngredient(ItemID.LunarTabletFragment, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("MeldiateBar"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("BarofLife"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("TerrariumCore"), 10);
				recipe.AddIngredient(ItemID.AnkhCharm);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.LunarCraftingStation);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Vanilla Recipes")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.FragmentSolar, 20);
				recipe.AddIngredient(ItemID.FragmentVortex, 20);
				recipe.AddIngredient(ItemID.FragmentNebula, 20);
				recipe.AddIngredient(ItemID.FragmentStardust, 20);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.LunarCraftingStation);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}

			if (ModContent.GetInstance<MainConfig>().RecipeMode == "Multiple Vanilla Summons")
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.CelestialSigil, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
					recipe.AddTile(TileID.LunarCraftingStation);
				if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
					recipe.AddTile(TileID.Benches);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}