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
            item.width = 48;
            item.height = 42;
            item.maxStack = 999;
            item.rare = 2;
            item.value = Item.sellPrice(silver: 30);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.RegenerationPotion, 5);
            player.QuickSpawnItem(ItemID.IronskinPotion, 5);
            player.QuickSpawnItem(ItemID.SwiftnessPotion, 5);
        }
    }
}