using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Airrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Rune");
            Tooltip.SetDefault("'One of the 4 basic elemental Runes'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
        }
    }
}
