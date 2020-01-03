using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    public class Ringcoins : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Coins");
            Tooltip.SetDefault("Turns you into a stack of coins");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.rare = 8;
            item.accessory = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!hideVisual)
            {
                player.GetModPlayer<OSRSplayer>().RingCoins = true;
            }
        }
    }
}
