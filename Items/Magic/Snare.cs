using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Snare : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snare");
            Tooltip.SetDefault("Casts a bolt that slows the enemy for two seconds");
        }
        public override void SetDefaults()
        {
            item.mana = 40;
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Snare>();
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earthrune", 50);
            recipe.AddIngredient(null, "Waterrune", 50);
            recipe.AddIngredient(null, "Naturerune", 25);
            recipe.AddIngredient(null, "Chaosrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}