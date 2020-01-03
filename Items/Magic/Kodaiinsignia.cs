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
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 20);
            item.rare = 10;
        }
    }
}
