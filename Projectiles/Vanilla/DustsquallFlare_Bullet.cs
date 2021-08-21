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
	class DustsquallFlare_Bullet : ModProjectile
	{
		public float rotationspeed = 0.0f;
		public int state = 0;
		Random rnd = new Random();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dustsquall Flare");
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
				// Sand Droplets
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 32, rnd.Next(-10, 11) / 10, rnd.Next(-10, 11) / 10);
				// Stationary Spark
			if (state == 2)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 232, rnd.Next(-4, 5), rnd.Next(-4, 5));
				// Falling Spark
			if (state == 3)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 232, rnd.Next(-5, 6) / 20, rnd.Next(-30, -15) / 5);
			// Rising Storm
			if (state == 4)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 232, rnd.Next(-5, 6) / 20, rnd.Next(15, 30) / 5);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, rnd.Next(-60, 60) / 100, rnd.Next(-10, 10) / 10, ModContent.ProjectileType<DustsquallFlare_Whirl>(), 0, 3f, Main.myPlayer);
			}

			// Initial Slowdown
			if (projectile.ai[0] <= 40f)
			{
				state = 1;
				projectile.velocity.X *= 0.92f;
				projectile.velocity.Y *= 0.92f;
				projectile.velocity.Y -= 0.2f;
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
				state = 2;
				rotationspeed -= 0.1f;
			}

			// Downwards Movement
			if (projectile.ai[0] == 61f)
			{
				state = 3;
				Main.PlaySound(SoundID.Item32, projectile.position);
				projectile.velocity.X = 0f;
				projectile.velocity.Y = 20f;
				rotationspeed = 0.0f;
				projectile.rotation = 0.0f;
			}

			// Storm Explosion + Rain Toggle
			if (projectile.ai[0] >= 65f && projectile.position.Y - 200 >= player.Center.Y && state == 3)
			{
				// Storm
				projectile.velocity.Y = -12f;
				state = 4;

				// Rain Toggle
				if (!Sandstorm.Happening)
				{
					Main.NewText(string.Format("[i/s1:848] [c/B36741:Vicious Storms] [c/909090:sweep across the Desert.]"));
					Sandstorm.Happening = true;
					Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
					Main.PlaySound(SoundID.Item74, projectile.position);
				}
				else
				if (Sandstorm.Happening && ModContent.GetInstance<MainConfig>().WeatherToggle)
				{
					Main.NewText(string.Format("[i/s1:848] [c/909090:The Desert starts to calm down.]"));
					Sandstorm.Happening = false;
					Sandstorm.TimeLeft = 0;
					Main.PlaySound(SoundID.Item74, projectile.position);
				}
				else
				if (Sandstorm.Happening && !ModContent.GetInstance<MainConfig>().WeatherToggle)
				{
					Main.PlaySound(SoundID.Item16, projectile.position);
				}

			}

			if (projectile.ai[0] >= 70f && projectile.position.Y + 400 <= player.Center.Y)
            {
				// Evict Projectile
				projectile.Kill();
			}

			projectile.rotation += rotationspeed;
			projectile.ai[0] += 1f;
		}
	}
}