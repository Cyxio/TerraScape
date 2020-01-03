using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class OlmFire : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Flames");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item89, projectile.position);
                projectile.alpha = 20;
                for (int o = 0; o < 12; o++)
                {
                    int dust = Dust.NewDust(projectile.Center, 0, 0, 75);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].velocity = new Vector2(5).RotatedBy(MathHelper.ToRadians(30 * o));
                }
            }
            Lighting.AddLight(projectile.Center, new Vector3(0, 1.2f, 0));
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
            if (projectile.ai[0] == 77 && projectile.timeLeft < 472)
            {
                Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y - projectile.height), Vector2.Zero, projectile.type, projectile.damage, 0f, 0, -1, 0);
                Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y + projectile.height), Vector2.Zero, projectile.type, projectile.damage, 0f, 0, 1, 0);
                projectile.ai[0] = 0;
            }
            if (projectile.timeLeft % 6 == 1)
            {
                if (projectile.ai[0] < 0 && projectile.ai[0] > -15)
                {
                    Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y - projectile.height), Vector2.Zero, projectile.type, projectile.damage, 0f, 0, projectile.ai[0] - 1, 0);
                    projectile.ai[0] = 0;
                }
                if (projectile.ai[0] > 0 && projectile.ai[0] < 15)
                {
                    Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y + projectile.height), Vector2.Zero, projectile.type, projectile.damage, 0f, 0, projectile.ai[0] + 1, 0);
                    projectile.ai[0] = 0;
                }
            }
            projectile.position.Y += Main.player[(int)(projectile.ai[1])].velocity.Y;
        }
        public override void Kill(int timeLeft)
        {
            for (int o = 0; o < 12; o++)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 75);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity = new Vector2(5).RotatedBy(MathHelper.ToRadians(30 * o));
            }
        }
    }
}
