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
            Item.accessory = true;
            Item.width = 18;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.09f;
            player.GetDamage(DamageClass.Summon) += 0.09f;
            player.GetDamage(DamageClass.Melee) += 0.09f;
            player.GetDamage(DamageClass.Magic) += 0.09f;
            player.GetDamage(DamageClass.Ranged) += 0.09f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddIngredient(null, "Dragonstone");
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantdragonstn");
            recipe.AddIngredient(null, "Dragonstone");
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
