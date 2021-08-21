using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Event
{
	public class GhoulishHex : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableInvasion;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghoulish Hex");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/E07F14:Pumpkin Moon]\n[c/606060:Usable at Night]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1028;
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
			// If its Nighttime, and there is currently no Moon Event
			return !Main.dayTime && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon;
		}

		public override bool UseItem(Player player)
		{
			// Summon Pumpkin Moon
			Main.NewText(string.Format("[i/s1:1857] [c/909090:The Moon turned the World into a] [c/E07F14:Nightmare]"));
			Main.startPumpkinMoon();
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
				recipe.AddIngredient(ItemID.Pumpkin, 100);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && (thorium_x || calamity_x)))
					recipe.AddIngredient(ItemID.Ectoplasm, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("DarkMatter"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("CursedCloth"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("CoreofChaos"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x && !thorium_x)
					recipe.AddIngredient(calamity.ItemType("DraedonBar"), 10);
				recipe.AddIngredient(ItemID.SoulofFright, 10);
				recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
				recipe.AddIngredient(ItemID.MoonCharm);

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
				recipe.AddIngredient(ItemID.Pumpkin, 30);
				recipe.AddIngredient(ItemID.HallowedBar, 10);
				recipe.AddIngredient(ItemID.Ectoplasm, 5);

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
				recipe.AddIngredient(ItemID.PumpkinMoonMedallion, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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