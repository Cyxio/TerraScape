using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.Head)]
    public class Deerstalker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deerstalker");
            Tooltip.SetDefault("Elementary!");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 24;
            Item.height = 28;
        }
    }
}