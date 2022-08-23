using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.Head)]
    public class Buckethelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bucket Helm");
            Tooltip.SetDefault("A helm made from a bucket.");
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