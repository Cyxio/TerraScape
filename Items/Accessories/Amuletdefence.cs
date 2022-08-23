using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amuletdefence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Defense");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 18;
            Item.height = 29;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantemerald");
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantemerald");
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
