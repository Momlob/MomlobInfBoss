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
	class Hourglass_Active : ModProjectile
	{
		int timeMode = 0;
		float timeSpeed = 1f;

		int projectileState = 0;
		float rotationSpeed = 0.005f;


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aethers Hourglass");
		}

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.rotation = -MathHelper.PiOver4;
			projectile.alpha = 255;

			projectile.aiStyle = 0;
			projectile.timeLeft = 30000;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			projectile.friendly = false;
			projectile.hostile = false;
			projectile.penetrate = 1;

			projectile.ranged = false;
		}



		public override void AI()
		{
			// Motion Controller

			// Initial Motion
			if (projectileState == 0)
				projectile.velocity.Y = -8;

			// Slowdown & Rotation
			if (projectileState == 1)
			{
				// Motion
				projectile.velocity.Y *= 0.95f;

				// Rotation
				if (rotationSpeed <= 0.25f)
					rotationSpeed += 0.0005f;
				projectile.rotation += rotationSpeed;

				// Alpha
				if (projectile.alpha >= 20)
					projectile.alpha -= 5;
			}

			// Motion Stop
			if (projectileState == 3)
			{
				// Motion Stop
				projectile.velocity.Y = 0;

				// Rotation Stop Point
				if (timeMode == 1)
					projectile.rotation = -MathHelper.PiOver4;
				else
					projectile.rotation = -MathHelper.PiOver4 + MathHelper.Pi;
			}

			// Fade out
			if (projectileState == 4)
			{
				// Alpha
				if (projectile.alpha <= 255 && projectile.timeLeft <= 30)
					projectile.alpha += 10;
			}



			// Particles

			// Glow
			if (projectileState == 1 && Main.rand.Next(20 - (int)rotationSpeed * 20) == 0)
				Dust.NewDust(projectile.Center, 0, 0, 15, Main.rand.Next(-20, 21) / 10, Main.rand.Next(-20, 21) / 10);
			// Glow Explosion
			if (projectileState == 3)
			{
				for (int i = 0; i < 20; i++)
					Dust.NewDust(projectile.Center, 0, 0, 15, Main.rand.Next(-40, 41) / 10, Main.rand.Next(-40, 41) / 10);
				projectileState = 4;
			}
				


			// Time Controll

			// Initialization
			if (projectileState == 0)
			{
				// Initial Time Mode
				if ((Main.dayTime && Main.time <= 54000 - 600) || (!Main.dayTime && Main.time >= 32400 - 600))
					timeMode = 0;
				else
					timeMode = 1;

				projectileState = 1;
			}

			// Time Speed up
			if (projectileState == 1)
			{
				// Time Speed up
				if (timeSpeed < 300)
					timeSpeed += 0.25f;
				Main.time += (int)timeSpeed;

				// Time Speed up Stop
				if (timeMode == 0 && Main.dayTime && Main.time >= 54000 - 300)
				{
					projectileState = 2;
					Main.time = 54000 - 300;
				}
				if (timeMode == 1 && !Main.dayTime && Main.time >= 32400 - 300)
				{
					projectileState = 2;
					Main.time = 32400 - 300;
				}
			}

			// Kill off Projectile
			if (projectileState == 2)
            {
				projectile.timeLeft = 60;
				//Main.PlaySound(SoundID.Item107, player.position, 0);

				projectileState = 3;
			}
		}
	}
}