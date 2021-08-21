using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using MomInfBossWorld;
using System.IO;

namespace MomInfBossPlayer
{
    class MIBPlayer : ModPlayer
	{
		public bool BuffLimitBreaker;

		public override void ResetEffects()
		{
			BuffLimitBreaker = false;
		}


		/*
		public bool ZoneTemple;
		public bool ZoneMarble;
		public bool ZoneGranite;

		public override void UpdateBiomes()
		{
			ZoneTemple = MIBWorld.BiomeTemple >= 1;
			ZoneMarble = MIBWorld.BiomeMarble> 100;
			ZoneGranite = MIBWorld.BiomeGranite > 100;
		}

		public override bool CustomBiomesMatch(Player other)
		{
			MIBPlayer modOther = other.GetModPlayer<MIBPlayer>();
			return ZoneGranite == modOther.ZoneGranite;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			MIBPlayer modOther = other.GetModPlayer<MIBPlayer>();
			modOther.ZoneGranite = ZoneGranite;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = ZoneGranite;
			writer.Write(flags);
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			ZoneGranite = flags[0];
		}
		*/
	}
}
