using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Accessories
{
    public class Boltenchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kandarin Sigil");
            Tooltip.SetDefault("Increases the chance of a bolt special occuring");
        }
        public override void SetDefaults()
        {
            item.accessory = true;
            item.width = 18;
            item.height = 29;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>(mod).Boltenchant = true;
        }
    }
}
