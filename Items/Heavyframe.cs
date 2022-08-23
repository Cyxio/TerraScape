using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Heavyframe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Frame");
            Tooltip.SetDefault("'A heavy wooden frame'");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
        }
    }
}