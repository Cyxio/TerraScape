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
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.sellPrice(0, 0, 50);
        }
    }
}