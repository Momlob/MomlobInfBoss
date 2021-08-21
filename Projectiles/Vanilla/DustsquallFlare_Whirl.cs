using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;
using Config;
using MomlobInfBoss.Dusts;

namespace MomlobInfBoss.Projectiles.Vanilla
{
	class DustsquallFlare_Whirl : ModProjectile
	{
		float originX = 0f;
		float targetX = 0f;
		Random rnd = new Random();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dustsquall Flare");
		}

		public override void SetDefaults()
		{
			projectile.width = 27;
			projectile.height = 27;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.ranged = false;
			projectile.alpha = 250 + rnd.Next(-30, 30);
		}



		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			// Initiation
			projectile.alpha = 100;
			if (projectile.ai[0] == 0f)
			{
				originX = projectile.position.X;
				targetX = originX + rnd.Next(-100, 100);
				projectile.scale = rnd.Next(30, 60) / 10;
			}

			// Target
			projectile.ai[0] += 1;
			if (projectile.ai[0] == 10)
				targetX = originX + 60 + projectile.ai[1] * 2;
			if (projectile.ai[0] == 15)
			{
				targetX = originX - 60 - projectile.ai[1] * 2;
				projectile.ai[0] = 5;
			}

			// Motion
			projectile.velocity.X += ((targetX - projectile.position.X) / 30) * ((projectile.ai[1] + 50) / 150);

			// Rotation
			projectile.rotation += rnd.Next(-10, 20) / 10;

			// Alpha / Age
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 300f)
			{
				// Evict Projectile
				projectile.Kill();
			}
		}
	}
}