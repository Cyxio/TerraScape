using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Shoes)]
    public class Rangerboots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ranger Boots");
            Tooltip.SetDefault("20% increased ranged damage");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.accessory = true;
            item.rare = 3;
            item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.2f;
        }
    }
}
