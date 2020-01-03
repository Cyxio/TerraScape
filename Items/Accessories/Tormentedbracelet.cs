using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Tormentedbracelet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tormented Bracelet");
            Tooltip.SetDefault("18% increased magic damage\nMagical attacks have a chance to unleash tormented souls\nAmount of souls increases with missing health");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 22;
            item.height = 31;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.18f;
            player.GetModPlayer<OSRSplayer>().Tormentedbracelet = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantzenyte");
            recipe.AddIngredient(null, "Zenyte");
            recipe.AddIngredient(ItemID.SpectreBar, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
