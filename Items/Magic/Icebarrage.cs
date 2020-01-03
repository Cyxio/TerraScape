using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Icebarrage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Barrage");
            Tooltip.SetDefault("Has a chance to halt the enemy's movement");
        }
        public override void SetDefaults()
        {
            item.damage = 30;
            item.magic = true;
            item.mana = 10;
            item.width = 18;
            item.height = 15;
            item.useTime = 12;
            item.useAnimation = 12;
            item.reuseDelay = 10;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Iceburst>();
            item.shootSpeed = 9;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 pos = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y);
            Vector2 velocity = new Vector2(0, 14);
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(pos + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-100, 0)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI, 1, 0);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bloodrune", 25);
            recipe.AddIngredient(null, "Soulrune", 25);
            recipe.AddIngredient(null, "Deathrune", 25);
            recipe.AddIngredient(null, "Waterrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}