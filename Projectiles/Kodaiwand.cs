using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Kodaiwand : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kodai energy");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 32;
            projectile.height = 32;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(0.4f, 0.1f, 1f));
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (projectile.timeLeft >= 580)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(projectile.ai[0]));
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
            }
            else if (projectile.timeLeft > 10)
            {
                Explode();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.timeLeft > 10)
            {
                Explode();
            }
        }
        private void Explode()
        {
            Main.PlaySound(SoundID.Item88, projectile.position);
            projectile.velocity *= 0f;
            projectile.position -= new Vector2(32, 32);
            projectile.Size = new Vector2(80, 80);
            projectile.alpha = 255;
            projectile.timeLeft = 10;
            Dust.NewDust(projectile.position, 0, 0, mod.DustType<Dusts.KodaiExplode>());
        }
    }
}