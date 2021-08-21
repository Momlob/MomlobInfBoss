using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Event
{
	public class CryolexWreath : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableInvasion;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cryolex Wreath");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/D9EFEE:Frost Legion]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1020;
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
			// If there is currently no Invasion
			return Main.CanStartInvasion();
		}

		public override bool UseItem(Player player)
		{
			// Summon Frost Legion
			Main.NewText(string.Format("[i/s1:1940] [c/D9EFEE:Frost Legion] [c/909090:is invading.]"));
			Main.PlaySound(SoundID.ForceRoar, player.position, 0);
			Main.invasionType = 2;
			Main.StartInvasion();
			Main.invasionX = (double)(Main.spawnTileX - 1);
			Main.invasionWarn = 2;
			Main.invasionType = 2;
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
				recipe.AddIngredient(ItemID.SnowBlock, 100);
				recipe.AddIngredient(ItemID.BorealWood, 50);
				recipe.AddIngredient(ItemID.Shiverthorn, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("EssenceofEleum"), 25);
				recipe.AddIngredient(ItemID.IceFeather);

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
				recipe.AddIngredient(ItemID.SnowGlobe, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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