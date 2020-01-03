using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class aaRuneessence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Essence");
            Tooltip.SetDefault("An uncharged rune stone");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddIngredient(ItemID.StoneBlock, 5);
            recipe.SetResult(this, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock);
            recipe.SetResult(this, 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddRecipe();
        }
    }
}
