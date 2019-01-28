using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Emeraldbolttips : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Emerald Bolt Tips");
        }
        public override void SetDefaults()
        {
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.value = 10;
            item.rare = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(null, "Enchantemerald");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
        }
    }
}
