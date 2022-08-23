using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class Zenyteshard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenyte Shard");
            Tooltip.SetDefault("Combine with an onyx to create a zenyte");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 16;
            Item.height = 28;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 0, 10, 0);
        }
    }
}
