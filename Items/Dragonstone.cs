using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class Dragonstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonstone");
            Tooltip.SetDefault("'A powerful purple gem'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 20;
            item.height = 22;
            item.rare = 5;
            item.value = Item.buyPrice(0, 5, 0, 0);
        }
    }
}
