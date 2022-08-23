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
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.18f;
            player.GetModPlayer<OSRSplayer>().Necklaceanguish = true;
            player.GetDamage(DamageClass.Ranged) += player.GetModPlayer<OSRSplayer>().AnguishTime / 400f;
            player.GetCritChance(DamageClass.Ranged) += player.GetModPlayer<OSRSplayer>().AnguishTime / 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantzenyte");
            recipe.AddIngredient(null, "Zenyte");
            recipe.AddIngredient(ItemID.ShroomiteBar, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
