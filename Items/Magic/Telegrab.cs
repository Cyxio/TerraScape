using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Telegrab : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Telekinetic grab");
            Tooltip.SetDefault("Casts a ball that grabs items from a distance");
        }
        public override void SetDefaults()
        {
            item.mana = 20;
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = 1;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Telegrab>();
            item.shootSpeed = 7.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Airrune", 15);
            recipe.AddIngredient(null, "Lawrune", 15);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}