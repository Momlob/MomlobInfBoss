using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Config;
using MomlobInfBoss.Dusts;

namespace MomlobInfBoss.Projectiles.Vanilla
{
	class DrizzleseedFlare_Bullet : ModProjectile
	{
		public float rotationspeed = 0.0f;
		Random rnd = new Random();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Drizzleseed Flare");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.friendly = false;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.ranged = false;
			projectile.alpha = 0;
		}



		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			// Some Particles
				// Water Droplets
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, rnd.Next(-10, 11) / 10, rnd.Next(-10, 11) / 10);
				// Stationary Spark
			if (projectile.ai[0] >= 51f && projectile.ai[0] <= 60f)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, rnd.Next(-4, 5), rnd.Next(-4, 5));
				// Rising Spark
			if (projectile.ai[0] >= 60f)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, rnd.Next(-5, 6) / 20, rnd.Next(15, 30) / 5);


			// Initial Slowdown
			if (projectile.ai[0] <= 40f) {
				projectile.velocity.X *= 0.92f;
				projectile.velocity.Y *= 0.92f;
				if (rotationspeed <= 0.5f)
					rotationspeed += 0.02f; 
			}

			// Standstill
			if (projectile.ai[0] == 41f)
			{
				projectile.velocity.X = 0f;
				projectile.velocity.Y = 0f;
				rotationspeed = 1.0f;
			}

			// Rotation Slowdown
			if (projectile.ai[0] >= 51f && projectile.ai[0] <= 60f)
			{
				rotationspeed -= 0.1f;
			}

			// Upwards Movement
			if (projectile.ai[0] == 61f)
			{
				Main.PlaySound(SoundID.Item32, projectile.position);
				projectile.velocity.X = 0f;
				projectile.velocity.Y = -16f;
				rotationspeed = 0.0f;
				projectile.rotation = 0f;

			}

			// Raincloud Explosion + Rain Toggle
			if (projectile.ai[0] >= 65f && projectile.position.Y + 500 <= player.Center.Y)
			{
				// Rain Toggle
				if (!Main.raining)
				{
					Main.NewText(string.Format("[i/s1:2498] [c/55668C:Rainclouds] [c/909090:darken the Sky.]"));
					Main.raining = true;
					Main.rainTime = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
					Main.PlaySound(SoundID.Item74, projectile.position);
				}
				else
				if (Main.raining && ModContent.GetInstance<MainConfig>().WeatherToggle)
				{
					Main.NewText(string.Format("[i/s1:2498] [c/909090:The sky is clearing.]"));
					Main.raining = false;
					Main.rainTime = 0;
					Main.PlaySound(SoundID.Item74, projectile.position);
				}
				else
				if (Main.raining && !ModContent.GetInstance<MainConfig>().WeatherToggle)
				{
					Main.PlaySound(SoundID.Item16, projectile.position);
				}

				// Cloud Explosion
				int dustType = ModContent.DustType<Dusts.Vanilla.Drizzleseed_Cloud> ();
				int repeat = 20;
				for (int i = 0; i < repeat; i++)
					Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, rnd.Next(-160, 160) / 10, rnd.Next(-20, 20) / 10);

				// Evict Projectile
				projectile.Kill();
			}


			projectile.rotation += rotationspeed;
			projectile.ai[0] += 1f;
		}
	}
}