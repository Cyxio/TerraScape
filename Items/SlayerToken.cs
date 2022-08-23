using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class SlayerToken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slayer Token");
            Tooltip.SetDefault("Currency for trading with slayer masters");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 52;
            Item.height = 52;
            Item.rare = -11;
            Item.value = 0;
        }
    }
}
