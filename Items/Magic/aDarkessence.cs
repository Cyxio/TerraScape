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
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
            Item.rare = ItemRarityID.Pink;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(null, "abPureessence", 10);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
