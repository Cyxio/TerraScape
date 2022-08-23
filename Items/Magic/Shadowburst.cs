using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Shadowburst : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Burst");
            Tooltip.SetDefault("Has a chance to confuse the enemy");
        }
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 16;
            Item.width = 18;
            Item.height = 15;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Shadowburst>();
            Item.shootSpeed = 9;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int ch = Main.rand.Next(10);
            if (ch >= 0)
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position - (velocity * 5).RotatedBy(MathHelper.ToRadians(-20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position - (velocity * 5).RotatedBy(MathHelper.ToRadians(20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI);
            }
            if (ch >= 5)
            {
                Projectile.NewProjectile(source, position - velocity * 5, velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI);
            }
            if (ch >= 8)
            {
                Projectile.NewProjectile(source, position - (velocity * 10).RotatedBy(MathHelper.ToRadians(20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position - (velocity * 10).RotatedBy(MathHelper.ToRadians(-20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockback, player.whoAmI);
            }
            player.reuseDelay = 10;
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Bloodrune", 25);
            recipe.AddIngredient(null, "Chaosrune", 25);
            recipe.AddIngredient(null, "Airrune", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}