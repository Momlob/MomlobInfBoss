using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Event
{
	public class ExtraterrestrialSignal : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableInvasion;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Xenoprobe Signal");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/90B14B:Martian Madness]");
			// Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 4));
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1031;
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
			// Spawn Martian Invasion
			Main.NewText(string.Format("[i/s1:2806] [c/90B14B:Martians] [c/909090:are invading.]"));
			Main.PlaySound(SoundID.ForceRoar, player.position, 0);
			Main.invasionType = 4;
			Main.StartInvasion();
			Main.invasionX = (double)(Main.spawnTileX - 1);
			Main.invasionWarn = 2;
			Main.invasionType = 4;
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
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && (thorium_x || calamity_x)))
					recipe.AddIngredient(ItemID.Wire, 100);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(ItemID.MartianConduitPlating, 100);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x && !thorium_x)
					recipe.AddIngredient(calamity.ItemType("MysteriousCircuitry"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("StrangePlating"), 25);
				recipe.AddIngredient(ItemID.MeteoriteBar, 25);
				recipe.AddIngredient(ItemID.ShroomiteBar, 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("AlienTech"), 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && my_materials_x)
					recipe.AddIngredient(my_materials.ItemType("AncientGear"), 5);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && my_materials_x))
					recipe.AddIngredient(ItemID.EyeoftheGolem);

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