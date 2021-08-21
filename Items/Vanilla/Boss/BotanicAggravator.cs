using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class BotanicAggravator : ModItem
	{
		// public bool limitBroken = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Botanic Aggravator");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/E180CE:Plantera]\n[c/606060:Usable in the Jungle]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1026;
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
					return NPC.downedPlantBoss;
				else
					return true;
			}

			// If in Jungle, and no Plantera is alive
			else
				return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Plantera
			Main.NewText(string.Format("[i/s1:2109] [c/E180CE:Plantera] [c/909090:was angered.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 1200, (int)player.position.Y - Main.rand.Next(-800, 800), NPCID.Plantera);
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
				recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("Petal"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("BioMatter"), 25);
				recipe.AddIngredient(ItemID.JungleSpores, 25);
				recipe.AddIngredient(ItemID.Vine, 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("TrapperBulb"), 5);
				recipe.AddIngredient(ItemID.AvengerEmblem, 1);

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