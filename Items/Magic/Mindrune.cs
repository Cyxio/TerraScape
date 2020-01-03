using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Mindrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Rune");
            Tooltip.SetDefault("'Used for basic level missile spells'");
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
            recipe.AddIngredient(mod.ItemType("aaRuneessence"), 1);
            recipe.SetResult(this);
            recipe.AddTile(null, "Runealtar");
            recipe.AddRecipe();
        }
    }
}
