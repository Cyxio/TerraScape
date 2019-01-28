using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Body)]
    public class RunebodyGX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthix Platebody");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.defense = 8;
        }
    }
}