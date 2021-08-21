using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Buffs.Vanilla;
using MomInfBossPlayer;

namespace MomlobInfBoss.Items.Vanilla.Boss
{
	public class ManicialProphecy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Manicial Prophecy");
			Tooltip.SetDefault("[c/909090:Unconsumable]\n[c/909090:Summons:] [c/294295:Lunatic Cultist]");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 1034;
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
			// If all that Crap below is true...
			// Due to multiple spawning Pillars afterwards, this Item cant be Limit Broken!
			return !NPC.AnyNPCs(NPCID.CultistBoss) && Main.CanStartInvasion() && !NPC.AnyNPCs(NPCID.MoonLordCore) && !NPC.AnyNPCs(NPCID.LunarTowerSolar) && !NPC.AnyNPCs(NPCID.LunarTowerVortex) && !NPC.AnyNPCs(NPCID.LunarTowerNebula) && !NPC.AnyNPCs(NPCID.LunarTowerStardust);
		}

		public override bool UseItem(Player player)
		{
			// Spawn Cultist
			Main.NewText(string.Format("[i/s1:3372] [c/294295:Lunatic Cultist] [c/909090:invokes impending Doom.]"));
			NPC.NewNPC((int)player.position.X + player.direction * 100, (int)player.position.Y - Main.rand.Next(40, 60), NPCID.CultistBoss);
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
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && (thorium_x || calamity_x)))
					recipe.AddIngredient(ItemID.SpectreBar, 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x)
					recipe.AddIngredient(calamity.ItemType("Lumenite"), 50);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x)
					recipe.AddIngredient(thorium.ItemType("DarkMatter"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && calamity_x && !thorium_x)
					recipe.AddIngredient(calamity.ItemType("SolarVeil"), 25);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && thorium_x && !calamity_x)
					recipe.AddIngredient(thorium.ItemType("BloomWeave"), 10);
				recipe.AddIngredient(ItemID.BeetleHusk, 10);
				if (ModContent.GetInstance<MainConfig>().ModdedIngredients && my_materials_x)
					recipe.AddIngredient(my_materials.ItemType("FrozenStar"), 5);
				if (!(ModContent.GetInstance<MainConfig>().ModdedIngredients && my_materials_x))
					recipe.AddIngredient(ItemID.AncientCloth, 5);
					recipe.AddIngredient(ItemID.SpellTome);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}