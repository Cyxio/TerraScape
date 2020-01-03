using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Body)]
    public class IronbodyG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Platebody (g)");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.defense = 2;
        }
    }
}