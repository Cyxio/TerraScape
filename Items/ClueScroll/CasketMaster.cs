﻿using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class CasketMaster : ModItem
    {
        public override string Texture
        {
            get
            {
                return "OldSchoolRuneScape/Items/ClueScroll/Casket";
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Casket (Master)");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 999;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            if (!ClueRewardUI.visible)
            {
                for (int i = 0; i < 9; i++)
                {
                    ClueRewardUI.rewards[i] = 0;
                }
                Main.playerInventory = false;
                ClueRewardUI.texture = "OldSchoolRuneScape/Items/ClueScroll/ClueReward";
                ClueRewardUI.GetRewards(5);
                ClueRewardUI.visible = true;
            }
        }
    }
}
