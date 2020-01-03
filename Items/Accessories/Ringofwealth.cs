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
            item.accessory = true;
            item.width = 22;
            item.height = 31;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>().RingofWealth = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 4);
            recipe.AddIngredient(null, "Dragonstone", 1);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 4);
            recipe.AddIngredient(null, "Dragonstone", 1);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}