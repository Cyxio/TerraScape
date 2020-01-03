using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    [AutoloadEquip(EquipType.Head)]
    public class Bowlwig : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bowl Wig");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 28;
            item.height = 24;
        }
    }
}