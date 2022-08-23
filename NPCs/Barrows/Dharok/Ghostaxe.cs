using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Dharok
{
    public class Ghostaxe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dharok's Axe");
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 72;
            Projectile.height = 62;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.scale = 1.2f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 480;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
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
                Projectile.rotation += MathHelper.ToRadians(12 * Projectile.direction);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Projectile.velocity.Y += 0.3f;
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.rotation += MathHelper.ToRadians(20 - Projectile.velocity.Length());
                if (Projectile.timeLeft > 130)
                {
                    Projectile.timeLeft = 130;
                }
                Projectile.velocity *= 0.98f;
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
