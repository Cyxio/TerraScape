using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Bluedhide : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Dragonhide");
            Tooltip.SetDefault("The scaly rough hide from a blue dragon");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 0, 10);
        }
    }
}