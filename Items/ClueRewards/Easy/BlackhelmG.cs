using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class BlackhelmG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Full Helm (g)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BlackbodyG") && legs.type == mod.ItemType("BlacklegsG");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "3 defense";
            player.statDefense += 3;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.defense = 3;
        }
    }
}