using Microsoft.Xna.Framework;
using Terraria;
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
            item.damage = 35;
            item.magic = true;
            item.mana = 16;
            item.width = 18;
            item.height = 15;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Shadowburst>();
            item.shootSpeed = 9;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int ch = Main.rand.Next(10);
            Vector2 velocity = new Vector2(speedX, speedY);
            if (ch >= 0)
            {
                Projectile.NewProjectile(position, velocity, type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position - (velocity * 5).RotatedBy(MathHelper.ToRadians(-20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position - (velocity * 5).RotatedBy(MathHelper.ToRadians(20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
            }
            if (ch >= 5)
            {
                Projectile.NewProjectile(position - velocity * 5, velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
            }
            if (ch >= 8)
            {
                Projectile.NewProjectile(position - (velocity * 10).RotatedBy(MathHelper.ToRadians(20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position - (velocity * 10).RotatedBy(MathHelper.ToRadians(-20)), velocity * Main.rand.NextFloat(0.8f, 1.2f), type, damage, knockBack, player.whoAmI);
            }
            player.reuseDelay = 10;
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bloodrune", 25);
            recipe.AddIngredient(null, "Chaosrune", 25);
            recipe.AddIngredient(null, "Airrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipe();
        }
    }
}