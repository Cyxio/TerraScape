using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    public class Enchantdragonstn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonstone Enchantment Tablet");
            Tooltip.SetDefault("Used to enchant dragonstone jewellery");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.rare = 1;
            item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Cosmicrune", 10);
            recipe.AddIngredient(null, "Waterrune", 15);
            recipe.AddIngredient(null, "Earthrune", 15);
            recipe.AddIngredient(ItemID.ClayBlock);
            recipe.AddTile(null, "Lectern");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
