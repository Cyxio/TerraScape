using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.Head)]
    public class Deerstalker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deerstalker");
            Tooltip.SetDefault("Elementary!");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 24;
            item.height = 28;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }
    }
}