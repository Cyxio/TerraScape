using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Heavyframe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Frame");
            Tooltip.SetDefault("'A heavy wooden frame'");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 2);
        }
    }
}