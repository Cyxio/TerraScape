using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    public class Enchantonyx : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Onyx Enchantment Tablet");
            Tooltip.SetDefault("Used to enchant onyx jewellery");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Cosmicrune", 10);
            recipe.AddIngredient(null, "Firerune", 20);
            recipe.AddIngredient(null, "Earthrune", 20);
            recipe.AddIngredient(ItemID.ClayBlock);
            recipe.AddTile(null, "Lectern");
            recipe.Register();
        }
    }
}
