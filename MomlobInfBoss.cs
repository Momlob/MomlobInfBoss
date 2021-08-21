using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Config;

namespace MomlobInfBoss
{
	class MomlobInfBoss : Mod
	{
		public MomlobInfBoss()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
		}



		public override void AddRecipeGroups()
		{
			//   Mod Checks
			Mod calamity = ModLoader.GetMod("CalamityMod");
			Mod thorium = ModLoader.GetMod("ThoriumMod");



			//   Recipe Group :   Wood
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Wood", new int[] {
				ItemID.Wood,
				ItemID.BorealWood,
				ItemID.RichMahogany,
				ItemID.PalmWood,
				ItemID.Ebonwood,
				ItemID.Shadewood,
				ItemID.Pearlwood,
				ItemID.DynastyWood,
				ItemID.SpookyWood
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Woods", group);
			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Woods"]].ValidItems.Add(calamity.ItemType("Acidwood"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Woods"]].ValidItems.Add(calamity.ItemType("AstralMonolith"));
			}
			if (thorium != null && ModContent.GetInstance<MainConfig>().EnableThorium)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Woods"]].ValidItems.Add(thorium.ItemType("YewWood"));
			}

			//   Recipe Group :   Evil Wood
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Wood", new int[] {
				ItemID.Ebonwood,
				ItemID.Shadewood
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilWoods", group);

			//   Recipe Group :   Sand
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Sand", new int[] {
				ItemID.SandBlock,
				ItemID.HardenedSand,
				ItemID.EbonsandBlock,
				ItemID.CorruptHardenedSand,
				ItemID.CrimsandBlock,
				ItemID.CrimsonHardenedSand,
				ItemID.PearlsandBlock,
				ItemID.HallowHardenedSand
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Sands", group);
			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sands"]].ValidItems.Add(calamity.ItemType("SulphurousSand"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sands"]].ValidItems.Add(calamity.ItemType("AstralSand"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sands"]].ValidItems.Add(calamity.ItemType("HardenedAstralSand"));
			}

			//   Recipe Group :   Sandstone
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Sandstone", new int[] {
				ItemID.Sandstone,
				ItemID.SandstoneBrick,
				ItemID.SandstoneSlab,
				ItemID.CorruptSandstone,
				ItemID.CrimsonSandstone,
				ItemID.HallowSandstone
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Sandstones", group);
			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sandstones"]].ValidItems.Add(calamity.ItemType("SulphurousSandstone"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sandstones"]].ValidItems.Add(calamity.ItemType("HardenedSulphurousSandstone"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Sandstones"]].ValidItems.Add(calamity.ItemType("AstralSandstone"));
			}

			//   Recipe Group :   Snow
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Snow", new int[] {
				ItemID.SnowBlock
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Snow", group);
			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Snow"]].ValidItems.Add(calamity.ItemType("AstralSnow"));
			}

			//   Recipe Group :   Honey
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Honey", new int[] {
				ItemID.HoneyBlock,
				ItemID.CrispyHoneyBlock
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:HoneyBlocks", group);

			//   Recipe Group :   Ocean
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Ocean Forage", new int[] {
				ItemID.Coral,
				ItemID.Seashell,
				ItemID.Starfish
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Corals", group);

			//   Recipe Group :   Dungeon Brick
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dungeon Brick", new int[] {
				ItemID.GreenBrick,
				ItemID.BlueBrick,
				ItemID.PinkBrick
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:DungeonBricks", group);

			//   Recipe Group :   Hell Brick
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Underworld Brick", new int[] {
				ItemID.ObsidianBrick,
				ItemID.HellstoneBrick
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:HellBricks", group);



			//   Recipe Group :   Copper / Tin Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[] {
				ItemID.CopperBar,
				ItemID.TinBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:CopperBars", group);

			//   Recipe Group :   Iron / Lead Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Iron Bar", new int[] {
				ItemID.IronBar,
				ItemID.LeadBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:IronBars", group);

			//   Recipe Group :   Silver / Tungsten Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[] {
				ItemID.SilverBar,
				ItemID.TungstenBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:SilverBars", group);

			//   Recipe Group :   Gold / Platinum Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[] {
				ItemID.GoldBar,
				ItemID.PlatinumBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:GoldBars", group);

			//   Recipe Group :   Demonite / Crimtane Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Bar", new int[] {
				ItemID.DemoniteBar,
				ItemID.CrimtaneBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilBars", group);

			//   Recipe Group :   Cobalt / Palladium Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Bar", new int[] {
				ItemID.CobaltBar,
				ItemID.PalladiumBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:CobaltBars", group);

			//   Recipe Group :   Mythril / Orichalcum Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Mythril Bar", new int[] {
				ItemID.MythrilBar,
				ItemID.OrichalcumBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:MythrilBars", group);

			//   Recipe Group :   Adamantite / Titanium Bar
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Adamantite Bar", new int[] {
				ItemID.AdamantiteBar,
				ItemID.TitaniumBar
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:AdamantiteBars", group);

			//   Recipe Group :   Gem
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gems", new int[] {
				ItemID.Amethyst,
				ItemID.Topaz,
				ItemID.Sapphire,
				ItemID.Emerald,
				ItemID.Ruby,
				ItemID.Diamond,
				ItemID.Amber
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Gems", group);
			if (thorium != null && ModContent.GetInstance<MainConfig>().EnableThorium)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Gems"]].ValidItems.Add(thorium.ItemType("Pearl"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Gems"]].ValidItems.Add(thorium.ItemType("Opal"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Gems"]].ValidItems.Add(thorium.ItemType("Onyx"));
			}



			//   Recipe Group :   Lens
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Lens", new int[] {
				ItemID.Lens,
				ItemID.BlackLens
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Lens", group);
			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Lens"]].ValidItems.Add(calamity.ItemType("BlightedLens"));
			}

			//   Recipe Group :   Powder
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Powder", new int[] {
				ItemID.VilePowder,
				ItemID.ViciousPowder
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilPowders", group);

			//   Recipe Group :   Dark Material
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Ingredient", new int[] {
				ItemID.RottenChunk,
				ItemID.Vertebrae
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilIngreds", group);

			//   Recipe Group :   Dark Boss Material
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Boss Material", new int[] {
				ItemID.ShadowScale,
				ItemID.TissueSample
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilBossDrops", group);

			//   Recipe Group :   Dark Hardmode Material
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Hardmode Ingredient", new int[] {
				ItemID.CursedFlame,
				ItemID.Ichor
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilHardIngreds", group);

			//   Recipe Group :   Soul
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Base Soul", new int[] {
				ItemID.SoulofLight,
				ItemID.SoulofNight
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:BaseSouls", group);

			//   Recipe Group :   Lunar Fragment
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Lunar Fragment", new int[] {
				ItemID.FragmentSolar,
				ItemID.FragmentVortex,
				ItemID.FragmentNebula,
				ItemID.FragmentStardust
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Fragments", group);
			if (thorium != null && ModContent.GetInstance<MainConfig>().EnableThorium)
			{
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Fragments"]].ValidItems.Add(thorium.ItemType("CometFragment"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Fragments"]].ValidItems.Add(thorium.ItemType("CelestialFragment"));
				RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["MomlobInfBoss:Fragments"]].ValidItems.Add(thorium.ItemType("WhiteDwarfFragment"));
			}



			//   Recipe Group :   Glowstick
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Glowstick", new int[] {
					ItemID.Glowstick,
					ItemID.StickyGlowstick,
					ItemID.BouncyGlowstick,
					ItemID.SpelunkerGlowstick
				});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Glowsticks", group);

			//   Recipe Group :   Bottle
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Water Bottles", new int[] {
				ItemID.UnholyWater,
				ItemID.BloodWater
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilFlasks", group);

			//   Recipe Group :   Dark Amulet
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Amulet", new int[] {
				ItemID.BandofStarpower,
				ItemID.PanicNecklace
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:EvilAmulets", group);

			//   Recipe Group :   Counterweight
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Counterweight", new int[] {
				ItemID.BlackCounterweight,
				ItemID.YellowCounterweight,
				ItemID.RedCounterweight,
				ItemID.PurpleCounterweight,
				ItemID.BlueCounterweight,
				ItemID.GreenCounterweight
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Counterweights", group);

			//   Recipe Group :   Life Heart
			group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Life Heart", new int[] {
				ItemID.LifeCrystal,
				ItemID.LifeFruit
			});
			RecipeGroup.RegisterGroup("MomlobInfBoss:Hearts", group);



			if (calamity != null && ModContent.GetInstance<MainConfig>().EnableCalamity)
			{
				//   Recipe Group :   Dark Calamity Boss Material
				group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Dark Calamity Boss Material", new int[] {
					calamity.ItemType("TrueShadowScale"),
					calamity.ItemType("BloodSample")
				});
				RecipeGroup.RegisterGroup("MomlobInfBoss:EvilCalamityBossDrops", group);
			}



			if (thorium != null && ModContent.GetInstance<MainConfig>().EnableThorium)
			{
				//   Recipe Group :   Thorium Beholder Bar
				group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Beholder Bar", new int[] {
					thorium.ItemType("LodeStoneIngot"),
					thorium.ItemType("ValadiumIngot")
				});
				RecipeGroup.RegisterGroup("MomlobInfBoss:BeholderBars", group);
			}

			/*
			if (calamity != null || thorium != null)
			{
					group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Blood Moon Relic", new int[] {
						calamity.ItemType("BloodIdol"),
						thorium.ItemType("BloodMoonMedallion"),
					});
					RecipeGroup.RegisterGroup("MomlobInfBoss:BloodSpawn", group);

					group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Portable Bulb", new int[] {
						calamity.ItemType("BulbofDoom"),
						thorium.ItemType("PlantBulb"),
					});
					RecipeGroup.RegisterGroup("MomlobInfBoss:PlanteraSpawn", group);

					group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Jellyfish Drops", new int[] {
						ItemID.JellyfishNecklace,
						calamity.ItemType("LifeJelly"),
						calamity.ItemType("ManaJelly"),
						calamity.ItemType("VitalJelly")
					});
					RecipeGroup.RegisterGroup("MomlobInfBoss:JellyAcc", group);
			}
			*/
		}
	}
}
