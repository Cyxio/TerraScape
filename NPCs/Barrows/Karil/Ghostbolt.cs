using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.hostile = true;
            projectile.width = 18;
            projectile.height = 18;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item5.WithPitchVariance(0.5f), projectile.position);
            }
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (projectile.alpha < 10)
            {
                projectile.ai[1] = 9;
            }
            if (projectile.alpha > 200)
            {
                projectile.ai[1] = -9;
            }
            projectile.alpha += (int)projectile.ai[1];
            if (projectile.ai[0] == 0)
            {
                projectile.velocity.Y += 0.1f;
                if (Main.rand.Next(2) == 0)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                }               
            }
            if (projectile.ai[0] == 1)
            {
                projectile.velocity *= 0.99f;
                if (projectile.timeLeft > 4 && projectile.velocity.Length() < 1f && projectile.velocity.Length() > -1f)
                {
                    projectile.position -= new Vector2(projectile.width * 2, projectile.height * 2);
                    projectile.width *= 4;
                    projectile.height *= 4;
                    projectile.timeLeft = 4;
                    projectile.alpha = 255;
                }
            }
            if (projectile.ai[0] == 2)
            {
                
            }
            if (projectile.ai[0] == 3)
            {
                
            }
            if (projectile.ai[0] == 4)
            {
                
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 1)
            {
                for (int i = 0; i < 36; i++)
                {
                    Vector2 rotate = new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(i * 10));
                    Dust.NewDust(projectile.Center + rotate, 0, 0, 58, rotate.X * 0.001f, rotate.Y * 0.001f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(projectile.Center, 0, 0, 58, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                }
            }         
        }
    }
}
