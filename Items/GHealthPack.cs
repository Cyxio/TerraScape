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
            Item.width = 48;
            Item.height = 42;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 1, silver: 50);
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Item.type), ItemID.GreaterHealingPotion, 15);
        }
    }
}