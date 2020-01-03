using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class _3rdagewand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd Age Wand\n-Cheat Weapon-");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 350;
            item.crit = 21;
            item.magic = true;
            item.mana = 15;
            item.width = 62;
            item.height = 62;
            item.useTime = 7;
            item.useAnimation = 21;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = Item.sellPrice(1);
            item.rare = 10;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Thirdagemage>();
            item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 toMouse = Main.MouseWorld - position;
            toMouse /= 30f;
            int rotation = Main.rand.Next(-30, 30);
            Projectile.NewProjectile(position, toMouse.RotatedBy(MathHelper.ToRadians(rotation)), type, damage, knockBack, player.whoAmI, (-rotation / 15f), 0);
            return false;
        }
    }
}