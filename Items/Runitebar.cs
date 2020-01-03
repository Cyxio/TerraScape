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
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.useTurn = true;
            item.rare = 3;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.value = 20000;
            item.useStyle = 1;
            item.consumable = false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Runiteore", 4);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}