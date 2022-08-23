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
            Item.width = 26;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 5);
            Item.defense = 10;
        }
    }
}