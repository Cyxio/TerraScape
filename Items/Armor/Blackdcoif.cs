using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Blackdcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Blackdbody") && legs.type == mod.ItemType("Blackdchaps");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "18% increased ranged damage\n20% chance not to consume ammo";
            player.rangedDamage += 0.18f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide Coif");
            Tooltip.SetDefault("10% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 6;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Blackdhide", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}