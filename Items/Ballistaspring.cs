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
            Item.width = 28;
            Item.height = 24;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
        }
    }
}