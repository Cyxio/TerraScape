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
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 28;
            Item.height = 26;
            Item.accessory = true;
        }
    }
}