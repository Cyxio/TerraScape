using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class aDarkessence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Essence");
            Tooltip.SetDefault("A dark power infests this dense essence block");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
            item.rare = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "abPureessence", 10);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.SetResult(this, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();
        }
    }
}
