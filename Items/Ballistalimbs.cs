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
            Item.width = 24;
            Item.height = 18;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
        }
    }
}