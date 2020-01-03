using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Guthan
{
    public class Ghostspear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Spear");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 20;
            projectile.height = 20;
            projectile.scale = 1.5f;
            projectile.damage = 0;
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
                Main.PlaySound(SoundID.Item20.WithPitchVariance(0.5f), projectile.position);
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
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
            }
            if (projectile.ai[0] == 1)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                projectile.height = 120;
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
            }
            if (projectile.ai[0] == 2)
            {
                // useless lmao
                projectile.ai[0] = 0;
            }
            if (projectile.ai[0] == 3)
            {
                projectile.width = 120;
                projectile.height = 120;
                projectile.rotation += 0.25f;
                if (projectile.timeLeft < 200)
                {
                    projectile.width = 30;
                    projectile.height = 30;
                    Player target = Main.player[0];
                    for (int k = 0; k < Main.ActivePlayersCount; k++)
                    {
                        if (projectile.Distance(Main.player[k].position) < projectile.Distance(target.position))
                        {
                            target = Main.player[k];
                        }
                    }
                    float speedX = target.MountedCenter.X - projectile.Center.X;
                    float speedY = target.MountedCenter.Y - projectile.Center.Y;
                    Vector2 spd = new Vector2(speedX, speedY);
                    spd.Normalize();
                    projectile.velocity = spd * 16f;
                    projectile.ai[0] = 0;
                }
            }
            if (projectile.ai[0] == 4)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                projectile.timeLeft = 36;
                projectile.ai[0] = 0;
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
