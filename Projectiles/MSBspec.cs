using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class MSBspec : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MSBspec");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 1500;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            if(Projectile.timeLeft == 1500)
            {
                Projectile.damage = (int)(Projectile.damage * 1.3f);
            }
            Lighting.AddLight(Projectile.Center, new Vector3(0, 255 * 0.005f, 123 * 0.005f));
            Dust.NewDust(Projectile.Center + (Projectile.velocity / 2), 0, 0, DustID.GreenFairy, 0, 0, 0, default(Color), 0.5f);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
