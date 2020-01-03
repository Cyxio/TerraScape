using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Head)]
    public class Goblinmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Mask");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 18;
            item.height = 22;
        }
        public override bool DrawHead()
        {
            return false;
        }
    }
}
