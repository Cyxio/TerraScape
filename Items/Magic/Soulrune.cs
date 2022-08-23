using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Soulrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Rune");
            Tooltip.SetDefault("'Used for high level curse spells'");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.width = 28;
            Item.height = 28;
            Item.value = 10;
            Item.rare = ItemRarityID.Pink;
        }
    }
}
