using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Medium
{
    [AutoloadEquip(EquipType.Head)]
    public class Greaterdemonmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Demon Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 32;
            Item.height = 24;
        }
    }
}