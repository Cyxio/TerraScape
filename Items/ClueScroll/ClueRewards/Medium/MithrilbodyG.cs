using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Body)]
    public class MithrilbodyG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mithril Platebody (g)");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 12, 0);
            Item.defense = 5;
        }
    }
}