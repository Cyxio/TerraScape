using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Waterrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Rune");
            Tooltip.SetDefault("'One of the 4 basic elemental Runes'");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
        }
    }
}