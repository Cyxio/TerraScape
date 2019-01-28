using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Berserkernecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker Necklace");
            Tooltip.SetDefault("Increases melee damage by 1% for every point of defense under 100");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 22;
            item.height = 31;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
        }
        public override void UpdateEquip(Player player)
        {
            if (player.statDefense < 100)
            {
                player.meleeDamage += (100 - player.statDefense) / 100f;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
