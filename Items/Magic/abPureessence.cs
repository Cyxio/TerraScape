using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class abPureessence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pure Essence");
            Tooltip.SetDefault("An uncharged Rune Stone of extra capability");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RuneEssence>())
                .AddCondition(Recipe.Condition.NearLava)
                .Register();
        }
    }
}
