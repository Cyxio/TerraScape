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
            item.accessory = true;
            item.width = 22;
            item.height = 31;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Amuletdamned = true;
        }
    }
}
