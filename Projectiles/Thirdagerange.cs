using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Thirdagerange : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd age arrow");
        }
        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.arrow = true;
            projectile.penetrate = -1;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.timeLeft = 1200;
            aiType = ProjectileID.BoneArrow;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(projectile.Center, 124, new Vector2(0f, 0f), 0, Color.White, 0.5f);
            Dust.NewDustPerfect(projectile.Center + projectile.velocity * 0.5f, 124, new Vector2(0f, 0f), 0, Color.Black, 0.5f);
            Lighting.AddLight(projectile.Center, new Vector3(0.4f, 0.4f, 0.4f));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0)
            {
                Vector2 pos = new Vector2(0, Main.rand.Next(-1000, -500));
                Vector2 spd = -pos;
                spd.Normalize();
                Projectile.NewProjectile(target.Center + pos, spd * projectile.velocity.Length(), projectile.type, projectile.damage, knockback, projectile.owner, 1, 0);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.Center - (projectile.velocity * 0.1f * i), 0, 0, 124, projectile.velocity.X * 0.3f, projectile.velocity.Y * 0.3f, 0, Color.White, 0.5f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
