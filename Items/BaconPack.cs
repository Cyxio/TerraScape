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
            Item.width = 48;
            Item.height = 42;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(silver: 60);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.Bacon, 30);
        }
    }
}