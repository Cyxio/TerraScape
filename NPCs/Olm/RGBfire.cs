using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class RGBfire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Firebreath");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.ai[0] == 0)
            {
                return new Color(150, 0, 0, 0);
            }
            if (projectile.ai[0] == 1)
            {
                return new Color(0, 150, 0, 0);
            }
            if (projectile.ai[0] == 2)
            {
                return new Color(0, 0, 150, 0);
            }
            return base.GetAlpha(lightColor);
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.DD2_BetsyFlameBreath, projectile.position);
                projectile.alpha = 1;
            }
            projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2);
            if (projectile.ai[0] == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 263, 0, 0, 0, Color.Red);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 263, 0, 0, 0, Color.Green);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[0] == 2)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 263, 0, 0, 0, Color.Blue);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[1] == 0)
            {
                Player target = Main.player[0];
                for (int k = 0; k < Main.ActivePlayersCount; k++)
                {
                    if (projectile.Distance(Main.player[k].position) < projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                if (projectile.Distance(target.position) > 200)
                {
                    float speedX = target.MountedCenter.X - projectile.Center.X;
                    float speedY = target.MountedCenter.Y - projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    projectile.velocity = spd * 20f;
                }
                else
                {
                    projectile.ai[1] = 1;
                }
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[0] == 0)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.Venom, 300);
            }
            if (projectile.ai[0] == 2)
            {
                target.AddBuff(BuffID.Blackout, 300);
            }
        }
    }
}
