using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class Tribalmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Mask");
            Tooltip.SetDefault("'A ceremonial wooden mask'");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.value = Item.sellPrice(0, 0, 20);
            item.rare = 2;
            item.defense = 3;
        }
    }
}