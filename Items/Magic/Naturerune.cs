using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Naturerune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Rune");
            Tooltip.SetDefault("'Used for alchemy spells'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
            item.rare = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("abPureessence"));
            recipe.SetResult(this);
            recipe.AddTile(null, "ARunealtar");
            recipe.AddRecipe();
        }
    }
}
