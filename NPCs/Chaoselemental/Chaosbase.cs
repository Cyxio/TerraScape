using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaosbase : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos disc");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 28;
            projectile.height = 28;
            projectile.damage = 0;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.scale = 0.6f;
            projectile.tileCollide = true;
            projectile.timeLeft = 360;
            projectile.alpha = 1;
        }
        public override void AI()
        {
            if (projectile.alpha == 1)
            {
                Main.PlaySound(SoundID.Item20, projectile.Center);
                projectile.alpha -= 1;
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 59);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 60);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 61);
            }
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(1f, 1f, 1f));
            projectile.rotation += 0.3f;
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.velocity.Y += 0.2f;
            }
            if (projectile.ai[0] == 2)
            {
                projectile.velocity.Y -= 0.2f;
            }
            projectile.velocity *= 1.01f;
        }
    }
}