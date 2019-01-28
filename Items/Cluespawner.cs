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
        public override string Texture
        {
            get
            {
                return "OldSchoolRuneScape/Items/ClueScroll/Clue";
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ClueSpawner");
        }
        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 54;
            item.rare = 3;
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
        }
        public override bool UseItem(Player player)
        {
            int ch = Main.rand.Next(5);
            if (ch == 0)
            {
                Item.NewItem(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), mod.ItemType<MasterClue>());
            }
            if (ch == 1)
            {
                Item.NewItem(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), mod.ItemType<EliteClue>());
            }
            if (ch == 2)
            {
                Item.NewItem(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), mod.ItemType<HardClue>());
            }
            if (ch == 3)
            {
                Item.NewItem(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), mod.ItemType<MediumClue>());
            }
            if (ch == 4)
            {
                Item.NewItem(new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1), mod.ItemType<EasyClue>());
            }
            return true;
        }
    }
}
