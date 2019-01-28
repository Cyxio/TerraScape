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
            item.width = 56;
            item.height = 56;
            item.maxStack = 999;
            item.rare = 2;
            item.value = Item.sellPrice(0, 5);
        }
    }
}