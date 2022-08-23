using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Lightframe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Frame");
            Tooltip.SetDefault("'A light wooden frame'");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
        }
    }
}