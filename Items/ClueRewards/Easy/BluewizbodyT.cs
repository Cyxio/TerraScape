using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Body)]
    public class BluewizbodyT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Wizard Robe (t)");
            Tooltip.SetDefault("15% decreased mana usage");
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.15f;
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.defense = 3;
            item.rare = 1;
        }
    }
}