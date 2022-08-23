﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Emeraldbolttips : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Emerald Bolt Tips");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.maxStack = 999;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(null, "Enchantemerald");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
