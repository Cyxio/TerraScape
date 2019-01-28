using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Smokebattlestaff : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.timeLeft = 840;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.alpha = 80;
        }
        public override void AI()
        {
            float distance = projectile.Distance(new Vector2(projectile.ai[0], projectile.ai[1]));
            float slowDistance = 200f;
            float shootSpeed = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed;
            if (distance < slowDistance)
            {
                if (projectile.velocity.Length() > 0.01f)
                {
                    projectile.velocity.Normalize();
                    projectile.velocity *= shootSpeed * (distance / slowDistance);
                }
                else
                {
                    projectile.velocity *= 0;
                }
            }
            projectile.rotation += MathHelper.ToRadians(2);
            int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, -2f, 100, default(Color), 1f);
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
        }
    }
}
