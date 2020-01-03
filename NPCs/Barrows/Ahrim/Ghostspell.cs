using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Ahrim
{
    public class Ghostspell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's spell");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 480;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item20.WithPitchVariance(0.5f), projectile.position);
            }
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
                if (projectile.timeLeft > 408)
                {
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                }
            }
            if (projectile.ai[0] == 1)
            {
                projectile.scale = 1.5f;
                if (Main.rand.Next(projectile.timeLeft) == 0)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(90)) * 1.5f, ModContent.ProjectileType<Ghostspell>(), projectile.damage, 0f, 0, 99, 0);
                    Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)) * 1.5f, ModContent.ProjectileType<Ghostspell>(), projectile.damage, 0f, 0, 99, 0);
                    projectile.Kill();
                }
            }
            if (projectile.ai[0] == 2)
            {
                if (projectile.timeLeft > 461)
                {
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                }
            }
            if (projectile.ai[0] == 3)
            {
                if (projectile.timeLeft > 461)
                {
                    projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-5));
                }                
            }
            if (projectile.ai[0] == 4)
            {
                projectile.tileCollide = false;
            }  
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (projectile.ai[0] == 2 || projectile.ai[0] == 3)
            {
                damage = (int)((10 + (target.statDefense * 0.75f)) / 4f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.ai[0] == 2 || projectile.ai[0] == 3)
            {
                target.immuneTime = 0;
            }
        }
    }
}