using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Greendhide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragonhide");
            Tooltip.SetDefault("The scaly rough hide from a green dragon");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 999;
            item.rare = 3;
            item.value = Item.sellPrice(0, 0, 1);
        }
    }
}