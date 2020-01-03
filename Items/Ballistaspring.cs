using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Ballistaspring : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballista Spring");
            Tooltip.SetDefault("'A spring-loaded mechanism'");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 2);
        }
    }
}