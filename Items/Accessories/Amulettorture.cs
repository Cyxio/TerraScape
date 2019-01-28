using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amulettorture : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Torture");
            Tooltip.SetDefault("18% increased melee damage\nFalling below 150 health heals you up to full\nWhile this effect is on cooldown, you gain increased damage & critical strike chance");
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
            player.meleeDamage += 0.18f;
            player.GetModPlayer<OSRSplayer>().Amulettorture = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantzenyte");
            recipe.AddIngredient(null, "Zenyte");
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddIngredient(ItemID.BeetleHusk, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
