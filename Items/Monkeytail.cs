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
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
        }
    }
}