using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class GHealthPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pack of Greater Healing Potions");
            Tooltip.SetDefault("A collection of greater healing potions");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 42;
            item.maxStack = 999;
            item.rare = 5;
            item.value = Item.sellPrice(gold: 1, silver: 50);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.GreaterHealingPotion, 15);
        }
    }
}