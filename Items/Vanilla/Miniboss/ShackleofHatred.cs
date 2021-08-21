using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Miniboss
{
	public class ShackleofHatred : ModItem
	{
		// public bool limitBroken = false;

		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<MainConfig>().EnableMiniboss;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shackle of Hatred");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/101010:???]\n[c/606060:Usable in the Dungeon]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1040;
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 34;

			item.maxStack = 1;
			item.consumable = false;
			item.rare = ItemRarityID.Gray;

			item.useAnimation = 15;
			item.useTime = 15;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingUp;
		}



		public override bool CanUseItem(Player player)
		{
			// Limit Breaker
			if (player.GetModPlayer<MIBPlayer>().BuffLimitBreaker == true)
				return true;

			// If in Dungeon, and no Guardian is alive
			else
				return player.ZoneDungeon && !NPC.AnyNPCs(NPCID.DungeonGuardian);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Plantera
			Main.NewText(string.Format("[i/s1:3309] [c/606060:Dungeon Guardian] [c/909090:answered your Deathwish.]"));
			NPC.NewNPC((int)player.position.X + Main.rand.Next(-800, 800), (int)player.position.Y + 1200, NPCID.DungeonGuardian);
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 250);
			recipe.AddIngredient(ItemID.Ectoplasm, 50);
			if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
				recipe.AddIngredient(thorium.ItemType("DarkMatter"), 25);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ItemID.Shackle);

			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Normal" || ModContent.GetInstance<MainConfig>().RecipeStation == "No Demon Altar")
				recipe.AddTile(TileID.LunarCraftingStation);
			if (ModContent.GetInstance<MainConfig>().RecipeStation == "Crafting Bench Only")
				recipe.AddTile(TileID.Benches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}