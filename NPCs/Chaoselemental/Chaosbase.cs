using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaosbase : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos disc");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.damage = 0;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.scale = 0.6f;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 360;
            Projectile.alpha = 1;
        }
        public override void AI()
        {
            if (Projectile.alpha == 1)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
                Projectile.alpha -= 1;
            }
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch);
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(1f, 1f, 1f));
            Projectile.rotation += 0.3f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.velocity.Y += 0.2f;
            }
            if (Projectile.ai[0] == 2)
            {
                Projectile.velocity.Y -= 0.2f;
            }
            Projectile.velocity *= 1.01f;
        }
    }
}