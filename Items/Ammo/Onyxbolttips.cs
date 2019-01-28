using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Onyxbolttips : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Onyx Bolt Tips");
        }
        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.value = 10;
            item.rare = 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 150);
            recipe.AddRecipe();
        }
    }
}
