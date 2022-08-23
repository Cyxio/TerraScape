using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class Cluespawner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ClueSpawner");
        }
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 54;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }
        public override bool? UseItem(Player player)
        {
            int ch = Main.rand.Next(5);
            var source = player.GetSource_ItemUse(Item);
            if (ch == 0)
            {
                Item.NewItem(source, new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), ModContent.ItemType<MasterClue>());
            }
            if (ch == 1)
            {
                Item.NewItem(source, new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), ModContent.ItemType<EliteClue>());
            }
            if (ch == 2)
            {
                Item.NewItem(source, new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), ModContent.ItemType<HardClue>());
            }
            if (ch == 3)
            {
                Item.NewItem(source, new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), ModContent.ItemType<MediumClue>());
            }
            if (ch == 4)
            {
                Item.NewItem(source, new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), ModContent.ItemType<EasyClue>());
            }
            return true;
        }
    }
}
