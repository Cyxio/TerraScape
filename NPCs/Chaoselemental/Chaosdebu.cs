using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaosdebu : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos debuff");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 24;
            projectile.height = 24;
            projectile.damage = 0;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            projectile.damage = 0;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(0, 255 * 0.005f, 0));
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 74);
            }
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
            Player target = Main.player[0];
            if (projectile.ai[0] == 0)
            {
                for (int k = 0; k < Main.ActivePlayersCount; k++)
                {
                    if (projectile.Distance(Main.player[k].position) < projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                float speedX = target.MountedCenter.X - projectile.Center.X;
                float speedY = target.MountedCenter.Y - projectile.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                projectile.velocity = spd * 12f;

                if (projectile.Distance(target.MountedCenter) < 75)
                {
                    projectile.ai[0] = 1;
                }
            }
            if (projectile.Colliding(projectile.Hitbox, target.Hitbox))
            {
                int ch = Main.rand.Next(10);
                if (ch == 9)
                {
                    target.AddBuff(BuffID.VortexDebuff, 120);
                }
                if (ch == 8)
                {
                    target.AddBuff(BuffID.Confused, 120);
                }
                if (ch == 7)
                {
                    target.AddBuff(BuffID.Featherfall, 600);
                }
                if (ch == 6)
                {
                    target.AddBuff(BuffID.Stoned, 40);
                }
                if (ch < 6)
                {
                    target.AddBuff(BuffID.OnFire, 300);
                }
                if (ch < 3)
                {
                    target.AddBuff(BuffID.CursedInferno, 240);
                }
                if (ch < 1)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }
                projectile.active = false;
            }
        }
    }
}