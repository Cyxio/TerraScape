using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class BlacklegsG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Platelegs (g)");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 1, 50);
            item.defense = 2;
        }
    }
}