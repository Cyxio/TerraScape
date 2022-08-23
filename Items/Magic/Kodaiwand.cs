using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Kodaiwand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kodai Wand");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 290;
            Item.crit = 21;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.width = 62;
            Item.height = 62;
            Item.useTime = 9;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 25);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item60;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Kodaiwand>();
            Item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 toMouse = Main.MouseWorld - position;
            toMouse /= 20f;
            int rotation = Main.rand.Next(-40, 40);
            Projectile.NewProjectile(source, position, toMouse.RotatedBy(MathHelper.ToRadians(rotation)), type, damage, knockback, player.whoAmI, (-rotation / 10f), 0);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Kodaiinsignia");
            recipe.AddIngredient(null, "Masterwand");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}