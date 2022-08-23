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
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1200;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 6f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(Projectile.Center, 124, new Vector2(0f, 0f), 0, Color.White, 1f);
            Dust.NewDustPerfect(Projectile.Center - Projectile.velocity * 0.5f, 124, new Vector2(0f, 0f), 0, Color.Black, 1f);
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
            Lighting.AddLight(Projectile.Center - Projectile.velocity, new Vector3(0.5f, 0.5f, 0.5f));
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(0.5f, 0.5f, 0.5f));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] > 1)
            {
                Projectile.ai[0]--;
                Projectile.timeLeft = 90;
                Vector2 pos = new Vector2(400).RotateRandom(2 * Math.PI);
                Vector2 spd = -pos;
                spd.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + pos, spd * Projectile.velocity.Length(), Projectile.type, Projectile.damage, knockback, Projectile.owner, Projectile.ai[0], 0);
            }
            else
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center - (Projectile.velocity * 0.1f * i), 0, 0, DustID.SandstormInABottle, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, 0, Color.White, 1f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
