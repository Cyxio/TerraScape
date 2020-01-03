using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Reddhide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Dragonhide");
            Tooltip.SetDefault("The scaly rough hide from a red dragon");
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