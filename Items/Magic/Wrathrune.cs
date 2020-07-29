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
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
        }
    }
}
