using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MomlobInfBoss.Dusts.Vanilla
{
	public class Drizzleseed_Cloud : ModDust
	{
		Random rnd = new Random();

		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.alpha = 100 + rnd.Next(0, 60);
			dust.frame = new Rectangle(0, rnd.Next(0, 6) * 54, 54, 54);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.velocity.Y -= 0.08f;

			dust.scale += 0.05f;
			if (dust.scale <= 2.0f)
			dust.scale += 0.2f;

			dust.alpha += 2;
			if (dust.alpha >= 250)
			{
				dust.active = false;
			}
			return false;
		}
	}
}