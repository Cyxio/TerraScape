using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.aiStyle = 1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 1500;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.arrow = true;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            if(projectile.timeLeft == 1500)
            {
                projectile.damage = (int)(projectile.damage * 1.3f);
            }
            Lighting.AddLight(projectile.Center, new Vector3(0, 255 * 0.005f, 123 * 0.005f));
            Dust.NewDust(projectile.Center + (projectile.velocity / 2), 0, 0, 74, 0, 0, 0, default(Color), 0.5f);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
        }
    }
}
