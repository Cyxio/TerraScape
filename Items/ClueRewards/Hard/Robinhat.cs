using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Head)]
    public class Robinhat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Robin Hood Hat");
            Tooltip.SetDefault("25% increased ranged damage");
        }
        public override void SetDefaults()
        {
            item.rare = 3;
            item.width = 30;
            item.height = 18;
            item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.25f;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }
    }
}