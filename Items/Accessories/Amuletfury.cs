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
            Item.accessory = true;
            Item.width = 18;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.14f;
            player.GetDamage(DamageClass.Summon) += 0.14f;
            player.GetDamage(DamageClass.Melee) += 0.14f;
            player.GetDamage(DamageClass.Magic) += 0.14f;
            player.GetDamage(DamageClass.Ranged) += 0.14f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Enchantonyx");
            recipe.AddIngredient(null, "Onyx");
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}
