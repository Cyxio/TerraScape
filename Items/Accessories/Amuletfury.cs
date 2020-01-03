using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amuletfury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Fury");
            Tooltip.SetDefault("14% increased damage");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 18;
            item.height = 29;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
            item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage += 0.14f;
            player.minionDamage += 0.14f;
            player.meleeDamage += 0.14f;
            player.magicDamage += 0.14f;
            player.rangedDamage += 0.14f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
