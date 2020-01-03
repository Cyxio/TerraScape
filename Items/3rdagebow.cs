using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class _3rdagebow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd Age Bow\n-Cheat Weapon-");
        }
        public override void SetDefaults()
        {
            item.damage = 280;
            item.crit = 21;
            item.ranged = true;
            item.scale = 0.75f;
            item.width = 38;
            item.height = 102;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = Item.sellPrice(1);
            item.rare = 10;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = ModContent.ProjectileType<Projectiles.Thirdagerange>();
            item.shootSpeed = 30f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.Thirdagerange>(), damage, knockBack, player.whoAmI, 0, 0);
            return false;
        }
    }
}