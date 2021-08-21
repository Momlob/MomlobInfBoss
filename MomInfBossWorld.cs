using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MomInfBossPlayer;

namespace MomInfBossWorld
{
    class MIBWorld : ModWorld 
    {
		public static int BiomeTemple;
		public static int BiomeMarble;
		public static int BiomeGranite;

		public override void ResetNearbyTileEffects()
		{
			BiomeGranite = 0;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			BiomeTemple = tileCounts[TileID.LihzahrdAltar];
			BiomeMarble = tileCounts[TileID.Marble];
			BiomeGranite = tileCounts[TileID.Granite];
		}

	}
}
