using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Torag
{
    public class Ghosthammer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's hammer");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 64;
            projectile.height = 64;
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
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item1.WithPitchVariance(0.5f), projectile.position);
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
            if(projectile.ai[0] == 0)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                projectile.velocity.Y += 0.2f;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.rotation += MathHelper.ToRadians(12 * projectile.direction);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                projectile.velocity.Y += 0.3f;
            }
            if (projectile.ai[0] == 2)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                if (projectile.timeLeft > 390)
                {
                    projectile.velocity *= 0.97f;
                }
                else if (projectile.timeLeft > 372)
                {
                    Dust.NewDust(projectile.Center, 0, 0, 58, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(10));
                }
                else
                {
                    projectile.velocity *= 1.03f;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Slow, 900);
            }
            else
            {
                target.velocity *= Main.rand.NextFloat(0.5f);
                target.wingTime = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(projectile.Center, 0, 0, 58, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
            }
        }
    }
}
