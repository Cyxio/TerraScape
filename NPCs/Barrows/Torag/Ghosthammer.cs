using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Torag
{
    public class Ghosthammer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's hammer");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }

        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
            }
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (Projectile.alpha < 10)
            {
                Projectile.ai[1] = 9;
            }
            if (Projectile.alpha > 200)
            {
                Projectile.ai[1] = -9;
            }
            Projectile.alpha += (int)Projectile.ai[1];
            if(Projectile.ai[0] == 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Projectile.velocity.Y += 0.2f;
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.rotation += MathHelper.ToRadians(12 * Projectile.direction);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Projectile.velocity.Y += 0.3f;
            }
            if (Projectile.ai[0] == 2)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
                if (Projectile.timeLeft > 390)
                {
                    Projectile.velocity *= 0.97f;
                }
                else if (Projectile.timeLeft > 372)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                    Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(10));
                }
                else
                {
                    Projectile.velocity *= 1.03f;
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.NextBool(5))
            {
                target.AddBuff(BuffID.Slow, 900);
            }
            else
            {
                target.velocity *= Main.rand.NextFloat(0.5f);
                target.wingTime = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
            }
        }
    }
}
