using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Body)]
    public class AdamantbodyG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamant Platebody (g)");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 12, 0);
            item.defense = 6;
        }
    }
}