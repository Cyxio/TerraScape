using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Sarastaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saradomin Staff");
            Tooltip.SetDefault("'Strike with the power of Saradomin'");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.magic = true;
            item.mana = 16;
            item.crit = 3;
            item.width = 42;
            item.height = 62;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 6;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Sarastrike");
            item.shootSpeed = 10f;
            item.scale = 0.8f;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().GodCharge)
            {
                damage *= 2;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 55), new Vector2(0, 5), mod.ProjectileType("Sarastrike"), damage, knockBack, player.whoAmI);
            Main.PlaySound(SoundID.Item72, Main.MouseWorld);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}