using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amuletglory : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Glory");
            Tooltip.SetDefault("9% increased damage");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 18;
            item.height = 29;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;
            item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage += 0.09f;
            player.minionDamage += 0.09f;
            player.meleeDamage += 0.09f;
            player.magicDamage += 0.09f;
            player.rangedDamage += 0.09f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddIngredient(null, "Dragonstone");
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddIngredient(null, "Dragonstone");
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
