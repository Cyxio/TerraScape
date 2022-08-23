using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Legs)]
    public class MithrillegsT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mithril Platelegs (t)");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 9, 0);
            Item.defense = 3;
        }
    }
}