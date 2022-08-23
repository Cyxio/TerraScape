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
                player.GetModPlayer<OSRSplayer>().RingNature = true;
            }
        }
    }
}
