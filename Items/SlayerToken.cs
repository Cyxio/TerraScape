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
            item.maxStack = 999;
            item.width = 52;
            item.height = 52;
            item.rare = -11;
            item.value = 0;
        }
    }
}
