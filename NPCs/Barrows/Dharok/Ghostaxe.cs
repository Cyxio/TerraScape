using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Dharok
{
    public class Ghostaxe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dharok's Axe");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 72;
            projectile.height = 62;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.scale = 1.2f;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
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
                projectile.rotation += MathHelper.ToRadians(12 * projectile.direction);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                projectile.velocity.Y += 0.3f;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.rotation += MathHelper.ToRadians(20 - projectile.velocity.Length());
                if (projectile.timeLeft > 130)
                {
                    projectile.timeLeft = 130;
                }
                projectile.velocity *= 0.98f;
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
