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
            Item.accessory = true;
            Item.width = 18;
            Item.height = 29;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Boltenchant = true;
        }
    }
}
