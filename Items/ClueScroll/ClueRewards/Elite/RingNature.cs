using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    public class Ringnature : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of Nature");
            Tooltip.SetDefault("Turns you into a bush");
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
                player.GetModPlayer<OSRSplayer>().RingNature = true;
            }
        }
    }
}
