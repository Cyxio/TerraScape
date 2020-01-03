using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class HealthPack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pack of Healing Potions");
            Tooltip.SetDefault("A collection of healing potions");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 42;
            item.maxStack = 999;
            item.rare = 3;
            item.value = Item.sellPrice(silver: 30);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.HealingPotion, 15);
        }
    }
}