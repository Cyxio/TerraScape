using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Body)]
    public class MonkrobeG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monk's Robe (g)");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.value = Item.sellPrice(0, 0, 2, 50);
            item.defense = 6;
            item.rare = 1;
        }
    }
}