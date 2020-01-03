using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class Zenyte : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenyte");
            Tooltip.SetDefault("'An unstable gem of great power'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 20;
            item.height = 22;
            item.rare = 9;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Zenyteshard");
            recipe.AddIngredient(null, "Onyx");
            recipe.SetResult(this);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}
