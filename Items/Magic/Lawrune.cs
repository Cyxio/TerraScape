using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Lawrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Law Rune");
            Tooltip.SetDefault("'Used for teleport spells'");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
