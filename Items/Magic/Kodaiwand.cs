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
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 290;
            item.crit = 21;
            item.magic = true;
            item.mana = 9;
            item.width = 62;
            item.height = 62;
            item.useTime = 9;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 25);
            item.rare = 10;
            item.UseSound = SoundID.Item60;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Kodaiwand>();
            item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 toMouse = Main.MouseWorld - position;
            toMouse /= 20f;
            int rotation = Main.rand.Next(-40, 40);
            Projectile.NewProjectile(position, toMouse.RotatedBy(MathHelper.ToRadians(rotation)), type, damage, knockBack, player.whoAmI, (-rotation / 10f), 0);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Kodaiinsignia");
            recipe.AddIngredient(null, "Masterwand");
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}