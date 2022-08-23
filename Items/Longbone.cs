using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Longbone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Long Bone");
            Tooltip.SetDefault("A rare bone that might sell well");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 5);
        }
    }
}