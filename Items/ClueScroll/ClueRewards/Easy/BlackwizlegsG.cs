using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class BlackwizlegsG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Skirt (g)");
            Tooltip.SetDefault("10% increased magic critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 10;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.defense = 2;
            Item.rare = ItemRarityID.Blue;
        }
    }
}