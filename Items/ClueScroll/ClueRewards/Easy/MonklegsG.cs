using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class MonklegsG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monk's Robe Bottom (g)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<MonkrobeG>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10 defense";
            player.statDefense += 10;
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 1, 50);
            item.defense = 6;
            item.rare = 1;
        }
    }
}