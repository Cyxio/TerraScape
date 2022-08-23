using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Head)]
    public class Blackdemonmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Demon Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
            Item.width = 36;
            Item.height = 28;
        }
    }
}