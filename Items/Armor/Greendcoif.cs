using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Greendcoif : ModItem
    {
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Greendbody") && legs.type == mod.ItemType("Greendchaps");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "15% increased ranged damage\n20% chance not to consume ammo";
            player.rangedDamage += 0.15f;
            player.ammoCost80 = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragonhide Coif");
            Tooltip.SetDefault("4% increased ranged critical strike chance");
        }
        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 30;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Greendhide", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}