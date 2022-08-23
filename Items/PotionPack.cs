using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class PotionPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pack of Potions");
            Tooltip.SetDefault("A collection of basic potions");
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 42;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 30);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.RegenerationPotion, 5);
            player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.IronskinPotion, 5);
            player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.SwiftnessPotion, 5);
        }
    }
}