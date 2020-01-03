using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class BaconPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bacon Pack");
            Tooltip.SetDefault("A collection of the finest bacon");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 42;
            item.maxStack = 999;
            item.rare = 4;
            item.value = Item.sellPrice(silver: 60);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.Bacon, 30);
        }
    }
}