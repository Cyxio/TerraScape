using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class Amuletdamned : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amulet of the Damned");
            Tooltip.SetDefault("Increases the effectiveness of barrows weapons\nAdds unique effects to barrows armor sets");
        }
        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.width = 22;
            Item.height = 31;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Amuletdamned = true;
        }
    }
}
