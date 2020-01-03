using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Necklaceanguish : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necklace of anguish");
            Tooltip.SetDefault("18% increased ranged damage\nDealing damage grants increased ranged damage and crit\nGetting hit will reset bonus damage");
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
            player.rangedDamage += 0.18f;
            player.GetModPlayer<OSRSplayer>().Necklaceanguish = true;
            player.rangedDamage += player.GetModPlayer<OSRSplayer>().AnguishTime / 400f;
            player.rangedCrit += player.GetModPlayer<OSRSplayer>().AnguishTime / 4;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantzenyte");
            recipe.AddIngredient(null, "Zenyte");
            recipe.AddIngredient(ItemID.ShroomiteBar, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
