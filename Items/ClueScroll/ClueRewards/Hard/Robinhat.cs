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
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.25f;
        }
    }
}