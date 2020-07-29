using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Smokedevil : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 28;
            projectile.height = 28;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
            projectile.alpha = 100;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity *= 0f;
            return false;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
            Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
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
            projectile.velocity *= 0.995f;
        }
    }
}