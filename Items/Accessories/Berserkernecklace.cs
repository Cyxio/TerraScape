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
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            if (player.statDefense < 100)
            {
                player.GetDamage(DamageClass.Melee) += (100 - player.statDefense) / 100f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
