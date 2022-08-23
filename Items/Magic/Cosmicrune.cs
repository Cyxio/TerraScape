using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Cosmicrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Rune");
            Tooltip.SetDefault("'Used for enchant spells'");
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
