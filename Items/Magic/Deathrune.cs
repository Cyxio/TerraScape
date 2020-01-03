using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Deathrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Rune");
            Tooltip.SetDefault("'Used for medium level missile spells'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
            item.rare = 4;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("aDarkessence"));
            recipe.SetResult(this);
            recipe.AddTile(null, "ARunealtar");
            recipe.AddRecipe();
        }
    }
}
