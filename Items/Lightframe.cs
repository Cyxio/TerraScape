using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Lightframe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Frame");
            Tooltip.SetDefault("'A light wooden frame'");
        }
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 56;
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 2);
        }
    }
}