using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Runitebar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runite Bar");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.value = 20000;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Runiteore", 4);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}