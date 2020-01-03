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
            projectile.aiStyle = -1;
            projectile.width = 10;
            projectile.height = 10;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position, 0, 0, 5, 0, 0, 0, Color.Black);
            Main.dust[dust].noGravity = true;
            projectile.rotation += 0.2f;
            Player target = Main.player[projectile.owner];
            float speedX = target.MountedCenter.X - projectile.Center.X;
            float speedY = target.MountedCenter.Y - projectile.Center.Y;
            Vector2 spd = new Vector2(speedX, speedY);
            spd.Normalize();
            spd *= 10;
            projectile.velocity = spd;
            if (projectile.Distance(target.MountedCenter) < 10)
            {
                target.statLife += projectile.damage;
                target.HealEffect(projectile.damage);
                projectile.active = false;
            }
        }
    }
}
