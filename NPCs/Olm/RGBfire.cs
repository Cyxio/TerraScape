using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class RGBfire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Firebreath");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.ai[0] == 0)
            {
                return new Color(150, 0, 0, 0);
            }
            if (Projectile.ai[0] == 1)
            {
                return new Color(0, 150, 0, 0);
            }
            if (Projectile.ai[0] == 2)
            {
                return new Color(0, 0, 150, 0);
            }
            return base.GetAlpha(lightColor);
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, Projectile.position);
                Projectile.alpha = 1;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2);
            if (Projectile.ai[0] == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Red);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Green);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[0] == 2)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Blue);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[1] == 0)
            {
                Player target = Main.player[0];
                for (int k = 0; k < Main.CurrentFrameFlags.ActivePlayersCount; k++)
                {
                    if (Projectile.Distance(Main.player[k].position) < Projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                if (Projectile.Distance(target.position) > 200)
                {
                    float speedX = target.MountedCenter.X - Projectile.Center.X;
                    float speedY = target.MountedCenter.Y - Projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    Projectile.velocity = spd * 20f;
                }
                else
                {
                    Projectile.ai[1] = 1;
                }
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Projectile.ai[0] == 0)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (Projectile.ai[0] == 1)
            {
                target.AddBuff(BuffID.Venom, 300);
            }
            if (Projectile.ai[0] == 2)
            {
                target.AddBuff(BuffID.Blackout, 300);
            }
        }
    }
}
