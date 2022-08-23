using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.arrow = true;
            Projectile.penetrate = -1;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 1200;
            AIType = ProjectileID.BoneArrow;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Dust.NewDustPerfect(Projectile.Center, 124, new Vector2(0f, 0f), 0, Color.White, 0.5f);
            Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * 0.5f, 124, new Vector2(0f, 0f), 0, Color.Black, 0.5f);
            Lighting.AddLight(Projectile.Center, new Vector3(0.4f, 0.4f, 0.4f));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] == 0)
            {
                Vector2 pos = new Vector2(0, Main.rand.Next(-1000, -500));
                Vector2 spd = -pos;
                spd.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center + pos, spd * Projectile.velocity.Length(), Projectile.type, Projectile.damage, knockback, Projectile.owner, 1, 0);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.Center - (Projectile.velocity * 0.1f * i), 0, 0, DustID.SandstormInABottle, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, 0, Color.White, 0.5f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
