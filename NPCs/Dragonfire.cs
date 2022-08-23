using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Dragonfire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonbreath");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.damage = 0;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.scale = 0.6f;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 360;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 360)
            {
                SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath.WithVolumeScale(0.5f), Projectile.Center);
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(255 * 0.005f, 124 * 0.005f, 0));
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare);
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
            if (Projectile.ai[0] == 1 && Projectile.timeLeft < 350)
            {
                Player target = Main.player[0];
                for (int k = 0; k < Main.CurrentFrameFlags.ActivePlayersCount; k++)
                {
                    if (Projectile.Distance(Main.player[k].position) < Projectile.Distance(target.position))
                    {
                        target = Main.player[k];
                    }
                }
                if (Projectile.Distance(target.position) > 150)
                {
                    float speedX = target.MountedCenter.X - Projectile.Center.X;
                    float speedY = target.MountedCenter.Y - Projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    Projectile.velocity = spd * 12f;
                }
                else
                {
                    Projectile.ai[0] = 0;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
            }
        }
    }
}
