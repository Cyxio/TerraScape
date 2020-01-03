using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Reddcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Reddbody") && legs.type == mod.ItemType("Reddchaps");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased ranged damage\n20% chance not to consume ammo";
            player.rangedDamage += 0.1f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Dragonhide Coif");
            Tooltip.SetDefault("10% increased ranged critical strike chance\n20% increased bullet damage");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 4;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 10;
            player.bulletDamage += 0.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Reddhide", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}