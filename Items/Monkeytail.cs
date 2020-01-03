using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Monkeytail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monkey Tail");
            Tooltip.SetDefault("'Smells like the south end of a northbound monkey, because it is'");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 2);
        }
    }
}