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
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 1);
        }
    }
}