using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Shoes)]
    public class Holysandals : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Sandals");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.accessory = true;
            item.rare = 3;
            item.value = Item.sellPrice(0, 5);
            item.defense = 10;
        }
    }
}