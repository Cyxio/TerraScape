using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amuletstrength : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of Strength");
            Tooltip.SetDefault("Increases melee damage by 8%");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 18;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantruby");
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantruby");
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
