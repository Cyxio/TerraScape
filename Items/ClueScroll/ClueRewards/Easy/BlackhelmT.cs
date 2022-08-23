using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class BlackhelmT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Full Helm (t)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("BlackbodyT").Type && legs.type == Mod.Find<ModItem>("BlacklegsT").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "3 defense";
            player.statDefense += 3;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.defense = 3;
        }
    }
}