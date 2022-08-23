using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Karil
{
    public class Ghostbolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's Bolt");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (Projectile.alpha < 10)
            {
                Projectile.ai[1] = 9;
            }
            if (Projectile.alpha > 200)
            {
                Projectile.ai[1] = -9;
            }
            Projectile.alpha += (int)Projectile.ai[1];
            if (Projectile.ai[0] == 0)
            {
                Projectile.velocity.Y += 0.1f;
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                }               
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.velocity *= 0.99f;
                if (Projectile.timeLeft > 4 && Projectile.velocity.Length() < 1f && Projectile.velocity.Length() > -1f)
                {
                    Projectile.position -= new Vector2(Projectile.width * 2, Projectile.height * 2);
                    Projectile.width *= 4;
                    Projectile.height *= 4;
                    Projectile.timeLeft = 4;
                    Projectile.alpha = 255;
                }
            }
            if (Projectile.ai[0] == 2)
            {
                
            }
            if (Projectile.ai[0] == 3)
            {
                
            }
            if (Projectile.ai[0] == 4)
            {
                
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Projectile.ai[0] == 1)
            {
                for (int i = 0; i < 36; i++)
                {
                    Vector2 rotate = new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(i * 10));
                    Dust.NewDust(Projectile.Center + rotate, 0, 0, DustID.Enchanted_Pink, rotate.X * 0.001f, rotate.Y * 0.001f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                }
            }         
        }
    }
}
