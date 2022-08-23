using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Kodaiwand : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kodai energy");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(0.4f, 0.1f, 1f));
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.timeLeft >= 580)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(Projectile.ai[0]));
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 3)
                {
                    Projectile.frame++;
                    if (Projectile.frame >= Main.projFrames[Projectile.type])
                    {
                        Projectile.frame = 0;
                    }
                    Projectile.frameCounter = 0;
                }
            }
            else if (Projectile.timeLeft > 10)
            {
                Explode();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 10)
            {
                Explode();
            }
        }
        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.Item88, Projectile.position);
            Projectile.velocity *= 0f;
            Projectile.position -= new Vector2(32, 32);
            Projectile.Size = new Vector2(80, 80);
            Projectile.alpha = 255;
            Projectile.timeLeft = 10;
            Dust.NewDust(Projectile.position, 0, 0, ModContent.DustType<Dusts.KodaiExplode>());
        }
    }
}