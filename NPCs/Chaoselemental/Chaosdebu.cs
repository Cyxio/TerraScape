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
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.damage = 0;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.damage = 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(0, 255 * 0.005f, 0));
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenFairy);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            Player target = Main.player[0];
            if (Projectile.ai[0] == 0)
            {
                for (int k = 0; k < Main.CurrentFrameFlags.ActivePlayersCount; k++)
                {
                    if (Projectile.Distance(Main.player[k].position) < Projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                float speedX = target.MountedCenter.X - Projectile.Center.X;
                float speedY = target.MountedCenter.Y - Projectile.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                Projectile.velocity = spd * 12f;

                if (Projectile.Distance(target.MountedCenter) < 75)
                {
                    Projectile.ai[0] = 1;
                }
            }
            if (Projectile.Colliding(Projectile.Hitbox, target.Hitbox))
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
                Projectile.active = false;
            }
        }
    }
}