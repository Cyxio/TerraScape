using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    [AutoloadEquip(EquipType.Head)]
    public class BuckethelmG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bucket Helm (g)");
            Tooltip.SetDefault("A helm made from a golden bucket.");
        }
        public override void SetDefaults()
        {
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 24;
            Item.height = 28;
        }
    }
}