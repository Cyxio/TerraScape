using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class _3rdagebow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd Age Bow\n-Cheat Weapon-");
        }
        public override void SetDefaults()
        {
            Item.damage = 280;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.75f;
            Item.width = 38;
            Item.height = 102;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(1);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thirdagerange>();
            Item.shootSpeed = 30f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Thirdagerange>(), damage, knockback, player.whoAmI, 0, 0);
            return false;
        }
    }
}