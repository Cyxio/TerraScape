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
            item.maxStack = 999;
            item.width = 16;
            item.height = 28;
            item.rare = 9;
            item.value = Item.sellPrice(0, 0, 10, 0);
        }
    }
}
