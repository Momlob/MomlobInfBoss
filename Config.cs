using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace Config 
{
	public class MainConfig : ModConfig 
	{
		public const string ConfigName = "Let Me Retry";
		public override ConfigScope Mode => ConfigScope.ServerSide;



		[Header("Recipes")]

		[DrawTicks]
		[ReloadRequired]
		[Label("Recipe Type")]
		[Tooltip("1: Infinite Boss Summons use Custom more expensive Recipes.\n2: Infinite Boss Summons use Vanilla Recipes.\n3: Infinite Boss Summons are crafted from multiple Vanilla Boss Summons.")]
		[OptionStrings(new string[] {  "Custom Recipes", "Vanilla Recipes","Multiple Vanilla Summons" })]
		[DefaultValue("Custom Recipes")]
		public string RecipeMode { get; set; }

		[DrawTicks]
		[ReloadRequired]
		[Label("Crafting Station")]
		[Tooltip("1: Items are crafted at their usual Stations.\n2: Items crafted at a Demon Altar use an Anvil instead.\n3: All Items are crafted at a Crafting Bench.")]
		[OptionStrings(new string[] { "Normal", "No Demon Altar", "Crafting Bench Only" })]
		[DefaultValue("Normal")]
		public string RecipeStation { get; set; }

		[Slider]
		[ReloadRequired]
		[Label("Vanilla Summon Ammount")]
		[Tooltip("If Recipes are switched to ´Multiple Vanilla Summons´\nDictates Ammount of Vanilla Summons Required.")]
		[Increment(1)]
		[Range(1, 20)]
		[DefaultValue(10)]
		public int RecipeMultiplier;

		internal void OnDeserializedMethod(StreamingContext context)
		{
			RecipeMultiplier = Utils.Clamp(RecipeMultiplier, 1, 20);
		}

		/*
		[ReloadRequired]
		[Label("Remove Vanilla Summons")]
		[Tooltip("Wether Ingredients from other Mods should be added to Recipes.")]
		[DefaultValue(true)]
		public bool RemoveVanilla;
		*/

		[ReloadRequired]
		[Label("Use Mod Ingredients")]
		[Tooltip("Wether Ingredients from other Mods should be added to Recipes.")]
		[DefaultValue(true)]
		public bool ModdedIngredients;



		[Header("Enabled Summons")]

		[ReloadRequired]
		[Label("Weather Summons")]
		[Tooltip("Enables Infinite Summons for Weather Events.")]
		[DefaultValue(true)]
		public bool EnableWeather;

		[ReloadRequired]
		[Label("Event Summons")]
		[Tooltip("Enables Infinite Summons for General Events.")]
		[DefaultValue(true)]
		public bool EnableInvasion;

		[ReloadRequired]
		[Label("Subboss Summons")]
		[Tooltip("Enables Infinite Summons for ceartain smaller Bosses.")]
		[DefaultValue(true)]
		public bool EnableMiniboss;

		[ReloadRequired]
		[Label("Other")]
		[Tooltip("Enables other Miscellaneous Items.")]
		[DefaultValue(true)]
		public bool EnableOther;



		[Header("Features")]

		[Label("Weather Toggle")]
		[Tooltip("Weather Summons can turn off Weather when used again.")]
		[DefaultValue(true)]
		public bool WeatherToggle;

		[Label("Limit Break before Defeat")]
		[Tooltip("The Limit Breaker only allows for multiple Spawns after first Defeat.")]
		[DefaultValue(true)]
		public bool UndefeatedLimit;



		[Header("Enabled Mods")]

		[ReloadRequired]
		[Label("Other Mod Support")]
		[Tooltip("Enables Compatability with my oter Mod, Crafty Boss Drops.")]
		[DefaultValue(true)]
		public bool EnableMyIng;

		[ReloadRequired]
		[Label("Calamity Support")]
		[Tooltip("Enables Compatability with the Calamity Mod.")]
		[DefaultValue(true)]
		public bool EnableCalamity;

		[ReloadRequired]
		[Label("Thorium Support")]
		[Tooltip("Enables Compatability with the Thorium Mod.")]
		[DefaultValue(true)]
		public bool EnableThorium;

	}
}