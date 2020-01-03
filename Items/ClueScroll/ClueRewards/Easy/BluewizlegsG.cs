using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class BluewizlegsG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Skirt (g)");
            Tooltip.SetDefault("10% increased magic critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 10;
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 1, 50);
            item.defense = 2;
            item.rare = 1;
        }
    }
}