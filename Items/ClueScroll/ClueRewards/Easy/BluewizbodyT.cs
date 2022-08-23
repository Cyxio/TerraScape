using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Body)]
    public class BluewizbodyT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Wizard Robe (t)");
            Tooltip.SetDefault("15% decreased mana usage");
        }
        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.15f;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.defense = 3;
            Item.rare = ItemRarityID.Blue;
        }
    }
}