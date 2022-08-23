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
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 28;
            Item.height = 24;
        }
    }
}