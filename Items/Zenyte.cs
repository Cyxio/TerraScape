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
            Item.maxStack = 999;
            Item.width = 20;
            Item.height = 22;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Zenyteshard");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
