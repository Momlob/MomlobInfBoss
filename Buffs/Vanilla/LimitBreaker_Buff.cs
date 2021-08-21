using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomInfBossPlayer;

namespace MomlobInfBoss.Buffs.Vanilla
{
	public class LimitBreaker_Buff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Limit Breaker");
			Description.SetDefault("Allows Infinite Summons to spawn Multiple Bosses.");

			Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MIBPlayer modPlayer = player.GetModPlayer<MIBPlayer>();
			modPlayer.BuffLimitBreaker = true;
		}
	}
}