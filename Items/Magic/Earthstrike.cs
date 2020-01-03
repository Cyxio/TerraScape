using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Earthstrike : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Strike");
        }
        public override void SetDefaults()
        {
            item.damage = 11;
            item.magic = true;
            item.mana = 5;
            item.width = 14;
            item.height = 9;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("EarthstrikeP");
            item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Earthrune", 25);
            recipe.AddIngredient(null, "Mindrune", 25);
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}