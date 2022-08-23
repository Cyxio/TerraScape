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
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.18f;
            player.GetModPlayer<OSRSplayer>().Tormentedbracelet = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantzenyte");
            recipe.AddIngredient(null, "Zenyte");
            recipe.AddIngredient(ItemID.SpectreBar, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
