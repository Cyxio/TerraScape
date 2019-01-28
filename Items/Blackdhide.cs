using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Blackdhide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Dragonhide");
            Tooltip.SetDefault("The scaly rough hide from a black dragon");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.maxStack = 999;
            item.rare = 6;
            item.value = Item.sellPrice(0, 0, 50);
        }
    }
}