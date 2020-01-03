using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Bluedhide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Dragonhide");
            Tooltip.SetDefault("The scaly rough hide from a blue dragon");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 999;
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 10);
        }
    }
}