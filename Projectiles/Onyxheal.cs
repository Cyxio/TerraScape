using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Onyxheal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Onyxheal");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, 0, 0, DustID.Blood, 0, 0, 0, Color.Black);
            Main.dust[dust].noGravity = true;
            Projectile.rotation += 0.2f;
            Player target = Main.player[Projectile.owner];
            float speedX = target.MountedCenter.X - Projectile.Center.X;
            float speedY = target.MountedCenter.Y - Projectile.Center.Y;
            Vector2 spd = new Vector2(speedX, speedY);
            spd.Normalize();
            spd *= 10;
            Projectile.velocity = spd;
            if (Projectile.Distance(target.MountedCenter) < 10)
            {
                target.statLife += Projectile.damage;
                target.HealEffect(Projectile.damage);
                Projectile.active = false;
            }
        }
    }
}
