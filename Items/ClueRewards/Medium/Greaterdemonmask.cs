using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Head)]
    public class Greaterdemonmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Demon Mask");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 32;
            item.height = 24;
        }
        public override bool DrawHead()
        {
            return false;
        }
    }
}