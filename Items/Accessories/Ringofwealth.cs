using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    public class Ringofwealth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Wealth");
            Tooltip.SetDefault("Doubles the chances of hitting the rare drop table");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>().RingofWealth = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 4);
            recipe.AddIngredient(null, "Dragonstone", 1);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBar, 4);
            recipe.AddIngredient(null, "Dragonstone", 1);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}