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
            Item.width = 16;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 0, 20);
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }
    }
}