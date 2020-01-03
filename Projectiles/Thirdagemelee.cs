using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Thirdagemelee : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd age longsword");
        }
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.timeLeft = 1200;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = 6f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(projectile.Center, 124, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust.NewDustPerfect(projectile.Center - projectile.velocity * 0.5f, 124, new Vector2(0f, 0f), 0, Color.Black, 1f);
            Lighting.AddLight(projectile.Center, new Vector3(1f, 1f, 1f));
            Lighting.AddLight(projectile.Center - projectile.velocity, new Vector3(0.5f, 0.5f, 0.5f));
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(0.5f, 0.5f, 0.5f));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] > 1)
            {
                projectile.ai[0]--;
                projectile.timeLeft = 90;
                Vector2 pos = new Vector2(400).RotateRandom(2 * Math.PI);
                Vector2 spd = -pos;
                spd.Normalize();
                Projectile.NewProjectile(target.Center + pos, spd * projectile.velocity.Length(), projectile.type, projectile.damage, knockback, projectile.owner, projectile.ai[0], 0);
            }
            else
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.Center - (projectile.velocity * 0.1f * i), 0, 0, 124, projectile.velocity.X * 0.3f, projectile.velocity.Y * 0.3f, 0, Color.White, 1f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
