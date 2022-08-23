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
            Item.width = 22;
            Item.height = 28;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
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
