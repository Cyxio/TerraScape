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
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.defense = 6;
            Item.rare = ItemRarityID.Blue;
        }
    }
}