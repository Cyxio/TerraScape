using Terraria.DataStructures;
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
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 350;
            Item.crit = 21;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.width = 62;
            Item.height = 62;
            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(1);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thirdagemage>();
            Item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 toMouse = Main.MouseWorld - position;
            toMouse /= 30f;
            int rotation = Main.rand.Next(-30, 30);
            Projectile.NewProjectile(source, position, toMouse.RotatedBy(MathHelper.ToRadians(rotation)), type, damage, knockback, player.whoAmI, (-rotation / 15f), 0);
            return false;
        }
    }
}