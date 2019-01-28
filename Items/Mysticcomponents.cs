using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class Mysticcomponents : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Components");
            Tooltip.SetDefault("Materials for creating magical robes");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 7, 0);
        }
    }
}
