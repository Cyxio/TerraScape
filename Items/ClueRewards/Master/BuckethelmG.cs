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
            item.vanity = true;
            item.rare = 2;
            item.width = 24;
            item.height = 28;
        }
    }
}