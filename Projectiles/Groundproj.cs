using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Groundproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Groundproj");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            projectile.velocity.X = 0;
            projectile.velocity.Y = 500;
        }
        public override void Kill(int timeLeft)
        {
            Player p = Main.player[projectile.owner];
            if (projectile.ai[0] == 0)
            {
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 21, 0, 0, mod.ProjectileType("Zamorakflame"), p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, projectile.owner, 0, 0);
                Main.PlaySound(SoundID.Item72, projectile.position);
            }
            if (projectile.ai[0] == 1)
            {
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 32, 0, 0, mod.ProjectileType("Guthixclaw"), p.GetWeaponDamage(p.inventory[p.selectedItem]), 0f, projectile.owner, 0, 0);
                Main.PlaySound(SoundID.Item72, projectile.position);
            }
        }
    }
}