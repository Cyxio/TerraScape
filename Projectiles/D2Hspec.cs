using Terraria.Audio;
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
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].type == ModContent.ItemType<Items.Weapons.Melee.Dragon2h>())
            {
                player.position = Projectile.position - new Vector2(player.width / 2, player.height);
                player.itemRotation = MathHelper.ToRadians(135 * player.direction);
                player.itemLocation = new Vector2(player.MountedCenter.X + player.direction * 10, player.Center.Y);
                player.itemAnimation = 2;
                Dust.NewDustPerfect(player.Center + new Vector2(10 * player.direction, 75), 55, null, 0, new Color(255, 0, 0), 1f);
                Projectile.velocity.X = 0;
                if (Projectile.velocity.Y > 15)
                {
                    Projectile.velocity.Y = 15;
                }
                Projectile.velocity.Y *= 1.03f;
                Dust.NewDustPerfect(player.Center + new Vector2(10 * player.direction, 75), 55, null, 0, new Color(255, 0, 0), 1f);
            }           
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
            Lighting.AddLight(Projectile.position, new Vector3(4, 0, 0));
            var source = Projectile.GetSource_FromThis();
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, 5.5f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, -5.5f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, 6f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, -6f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, 6.5f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            Projectile.NewProjectile(source, Projectile.position.X, Projectile.position.Y - 15, -6.5f, 0, Mod.Find<ModProjectile>("D2Hspec2").Type, Projectile.damage, 0, Projectile.owner, 0, 0);
            base.Kill(timeLeft);
        }
    }
}