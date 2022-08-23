using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Shoes)]
    public class Wizardboots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wizard Boots");
            Tooltip.SetDefault("20% increased magic damage");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.2f;
        }
    }
}