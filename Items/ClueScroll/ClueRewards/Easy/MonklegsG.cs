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
            Item.width = 18;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.defense = 6;
            Item.rare = ItemRarityID.Blue;
        }
    }
}