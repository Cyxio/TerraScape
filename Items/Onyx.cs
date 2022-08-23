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
            Item.maxStack = 999;
            Item.width = 42;
            Item.height = 46;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}
