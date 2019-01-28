using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Verac
{
    public class Ghostball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's ball");
        }
        public override void SetDefaults()
        {
            projectile.hostile = true;
            projectile.width = 42;
            projectile.height = 42;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
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
                projectile.rotation += projectile.velocity.X * 0.025f;
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                projectile.velocity.Y += 0.15f;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.rotation += projectile.velocity.X * 0.025f;
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58);
                projectile.velocity.Y += 0.15f;
            }
            if (projectile.ai[0] == 2)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(5));
                projectile.rotation = projectile.velocity.ToRotation();
                projectile.position.X += projectile.velocity.X * 2f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.ai[0] == 0)
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.9f;
                Main.PlaySound(SoundID.Item10, projectile.position);
                return false;
            }
            if (projectile.ai[0] == 1)
            {
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(oldVelocity.X, -oldVelocity.Y * 0.75f), projectile.type, projectile.damage, projectile.knockBack);
                    Projectile.NewProjectile(projectile.Center, new Vector2(-oldVelocity.X, -oldVelocity.Y * 0.75f), projectile.type, projectile.damage, projectile.knockBack);
                }
                return true;
            }
            return true;
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
