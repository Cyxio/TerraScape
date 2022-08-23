using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Kodaiinsignia : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kodai Insignia");
            Tooltip.SetDefault("An insignia representing Kodai, Xeric's fearsome wizards.");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 20);
            Item.rare = ItemRarityID.Red;
        }
    }
}
