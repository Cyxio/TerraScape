using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Mystichat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Hat");
            Tooltip.SetDefault("Increases maximum mana by 20");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Mystictop") && legs.type == mod.ItemType("Mysticbottom");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased magic damage\n18% reduced mana usage";
            player.magicDamage += 0.1f;
            player.manaCost -= 0.18f;
        }

        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 30;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Mysticcomponents>(), 4);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}