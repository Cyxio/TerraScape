using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Dragonbolttips : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Dragon Bolt Tips");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.maxStack = 999;
            Item.value = 10;
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(null, "Dragonstone");
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
