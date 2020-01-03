using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Entangle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entangle");
            Tooltip.SetDefault("Casts a bolt that slows the enemy for three seconds");
        }
        public override void SetDefaults()
        {
            item.mana = 80;
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 7;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Entangle>();
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earthrune", 150);
            recipe.AddIngredient(null, "Waterrune", 150);
            recipe.AddIngredient(null, "Naturerune", 100);
            recipe.AddIngredient(null, "Soulrune", 100);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}