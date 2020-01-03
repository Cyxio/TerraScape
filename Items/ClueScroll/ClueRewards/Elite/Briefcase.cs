using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class Briefcase : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Briefcase");
            Tooltip.SetDefault("For your first day in the big city.");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 28;
            item.height = 26;
            item.accessory = true;
        }
    }
}