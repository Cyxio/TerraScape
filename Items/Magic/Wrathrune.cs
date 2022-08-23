using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Wrathrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Rune");
            Tooltip.SetDefault("'Used for very high level missile spells'");
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
