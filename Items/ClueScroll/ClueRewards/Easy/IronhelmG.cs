using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class IronhelmG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Full Helm (g)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("IronbodyG").Type && legs.type == Mod.Find<ModItem>("IronlegsG").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "2 defense";
            player.statDefense += 2;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.defense = 2;
        }
    }
}