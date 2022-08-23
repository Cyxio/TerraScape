using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Head)]
    public class Greendragonmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Dragon Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
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