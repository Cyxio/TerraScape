using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Dragonfire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonbreath");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.damage = 0;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.scale = 0.6f;
            projectile.tileCollide = true;
            projectile.timeLeft = 360;
        }
        public override void AI()
        {
            if (projectile.timeLeft == 360)
            {
                Main.PlaySound(SoundID.DD2_BetsyFlameBreath.WithVolume(0.5f), projectile.Center);
            }
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(255 * 0.005f, 124 * 0.005f, 0));
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 127);
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            if (projectile.ai[0] == 1 && projectile.timeLeft < 350)
            {
                Player target = Main.player[0];
                for (int k = 0; k < Main.ActivePlayersCount; k++)
                {
                    if (projectile.Distance(Main.player[k].position) < projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                if (projectile.Distance(target.position) > 150)
                {
                    float speedX = target.MountedCenter.X - projectile.Center.X;
                    float speedY = target.MountedCenter.Y - projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    projectile.velocity = spd * 12f;
                }
                else
                {
                    projectile.ai[0] = 0;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(projectile.Center, 0, 0, 127, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                Main.PlaySound(SoundID.Item20.WithPitchVariance(0.5f), projectile.position);
            }
        }
    }
}
