using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Head)]
    public class AdamanthelmG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamant Full Helm (g)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("AdamantbodyG").Type && legs.type == Mod.Find<ModItem>("AdamantlegsG").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "4 defense";
            player.statDefense += 4;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 15, 0);
            Item.defense = 5;
        }
    }
}