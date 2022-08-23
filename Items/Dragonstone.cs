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
            Item.maxStack = 999;
            Item.width = 20;
            Item.height = 22;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(0, 5, 0, 0);
        }
    }
}
