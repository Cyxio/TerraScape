using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class Onyx : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Onyx");
            Tooltip.SetDefault("'A rare black gem'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 42;
            item.height = 46;
            item.rare = 8;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}
