using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Smokebarrage : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke Barrage");
            Tooltip.SetDefault("Has a chance to poison and venom the enemy");
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 18;
            Item.height = 15;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Smokeburst>();
            Item.shootSpeed = 9;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 pos = new Vector2(Main.MouseWorld.X, Main.screenPosition.Y);
            velocity = new Vector2(0, 14);
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(source, pos + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-100, 0)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI, 1, 0);
            }
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bloodrune", 25);
            recipe.AddIngredient(null, "Soulrune", 25);
            recipe.AddIngredient(null, "Deathrune", 25);
            recipe.AddIngredient(null, "Firerune", 25);
            recipe.AddIngredient(null, "Airrune", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}