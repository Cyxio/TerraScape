using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Ballistalimbs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballista Limbs");
            Tooltip.SetDefault("'Sturdy struts'");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 18;
            item.maxStack = 999;
            item.rare = 8;
            item.value = Item.sellPrice(0, 2);
        }
    }
}