using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class RoyalGunk : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Royal Sludge");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/5481E0:King Slime]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1003;
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
					return NPC.downedSlimeKing;
				else
					return true;
			}

			// If no King Slime is alive
			else
				return !NPC.AnyNPCs(NPCID.KingSlime);
		}

		public override bool UseItem(Player player)
		{
			// Spawn King Slime
			Main.NewText(string.Format("[i/s1:2493] [c/5481E0:King Slime] [c/909090:was provoked.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 1200, (int)player.position.Y - 100, NPCID.KingSlime);
			Main.PlaySound(SoundID.ForceRoar, player.Center, 0);
			return true;
		}



		/* Fuck this...
		public override void UpdateInventory(Player player)
		{
			// 
			if (player.GetModPlayer<MIBPlayer>().BuffLimitBreaker == true)
				limitBroken = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (limitBroken == true)
            {
				var brokenTooltip = new TooltipLine(mod, "LimitBroken", $"[c/E28AF8:Limit Broken]");
				tooltips.Add(brokenTooltip);
			}
		}
		*/



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
				recipe.AddIngredient(ItemID.Gel, 100);
				recipe.AddRecipeGroup("MomlobInfBoss:GoldBars", 25);
				recipe.AddRecipeGroup("MomlobInfBoss:Gems", 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("WulfrumShard"), 10);
				recipe.AddIngredient(ItemID.Shackle);

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
				recipe.AddIngredient(ItemID.Gel, 20);
				recipe.AddRecipeGroup("MomlobInfBoss:GoldBars", 5);
				recipe.AddRecipeGroup("MomlobInfBoss:Gems", 1);

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
				recipe.AddIngredient(ItemID.SlimeCrown, ModContent.GetInstance<MainConfig>().RecipeMultiplier);

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