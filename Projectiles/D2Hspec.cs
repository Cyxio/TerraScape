using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class D2Hspec : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("D2Hspec");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.inventory[player.selectedItem].type == mod.ItemType<Items.Dragon2h>())
            {
                player.position = projectile.position - new Vector2(player.width / 2, player.height);
                player.itemRotation = MathHelper.ToRadians(135 * player.direction);
                player.itemLocation = new Vector2(player.MountedCenter.X + player.direction * 10, player.Center.Y);
                player.itemAnimation = 2;
                Dust.NewDustPerfect(player.Center + new Vector2(10 * player.direction, 75), 55, null, 0, new Color(255, 0, 0), 1f);
                projectile.velocity.X = 0;
                if (projectile.velocity.Y > 15)
                {
                    projectile.velocity.Y = 15;
                }
                projectile.velocity.Y *= 1.03f;
                Dust.NewDustPerfect(player.Center + new Vector2(10 * player.direction, 75), 55, null, 0, new Color(255, 0, 0), 1f);
            }           
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item70, projectile.position);
            Lighting.AddLight(projectile.position, new Vector3(4, 0, 0));
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, 5.5f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, -5.5f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, 6f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, -6f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, 6.5f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 15, -6.5f, 0, mod.ProjectileType("D2Hspec2"), projectile.damage, 0, projectile.owner, 0, 0);
            base.Kill(timeLeft);
        }
    }
}