using Terraria;
using Terraria.ID;
using OldSchoolRuneScape.UI;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class EliteClue : Clue
    {
        public override bool CanPickup(Player player)
        {
            return !player.HasItem(ModContent.ItemType<EliteClue>()) && player.HeldItem.type != ModContent.ItemType<EliteClue>() && player.trashItem.type != ModContent.ItemType<EliteClue>();
        }
        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().eliteSteps == 0)
            {
                player.GetModPlayer<OSRSplayer>().eliteSteps = Main.rand.Next(11, 16);
            }
            if (player.GetModPlayer<OSRSplayer>().eliteClue == 0)
            {
                player.GetModPlayer<OSRSplayer>().eliteClue = 1 + Main.rand.Next(OldSchoolRuneScape.eliteClueCount);
                player.GetModPlayer<OSRSplayer>().eliteStage = 0;
            }
            if (player.GetModPlayer<OSRSplayer>().cluestep == 4)
            {
                player.GetModPlayer<OSRSplayer>().eliteStage++;
                if (player.GetModPlayer<OSRSplayer>().eliteStage >= player.GetModPlayer<OSRSplayer>().eliteSteps)
                {
                    item.TurnToAir();
                    player.QuickSpawnItem(ModContent.ItemType<CasketElite>());
                    player.GetModPlayer<OSRSplayer>().ClueReset(ModContent.ItemType<EliteClue>(), player);
                    player.GetModPlayer<OSRSplayer>().CompleteClue(4, player);
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_has_completed_an_Elite_Clue_Scroll! 9");
                    }
                    else
                    {
                        Main.NewText("Elite Clue Scroll complete!", Colors.RarityPink);
                    }
                }
                else
                {
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_completed_a_step_of_an_Elite_Clue_Scroll 4");
                    }
                    else
                    {
                        Main.NewText("Elite Clue step complete!", Colors.RarityPurple);
                    }
                }
                player.GetModPlayer<OSRSplayer>().cluestep = 0;
                player.GetModPlayer<OSRSplayer>().eliteClue = 1 + Main.rand.Next(OldSchoolRuneScape.eliteClueCount);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Main.NewText("Current step: " + (player.GetModPlayer<OSRSplayer>().eliteStage + 1) + " / " + player.GetModPlayer<OSRSplayer>().eliteSteps);
                player.itemAnimation = 30;
                player.itemTime = 30;
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            ClueUI.texture = "OldSchoolRuneScape/Items/ClueScroll/Elite/Elite" + player.GetModPlayer<OSRSplayer>().eliteClue;
            if (player.channel && player.altFunctionUse != 2)
            {
                player.itemAnimation = 2;
                player.itemTime = 2;
                ClueUI.visible = true;
            }
            else
            {
                ClueUI.visible = false;
            }
            if (player.channel && player.altFunctionUse != 2)
            {
                player.itemAnimation = 2;
                player.itemTime = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().eliteClue == 18)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 136;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 4;
            }
            if (player.GetModPlayer<OSRSplayer>().eliteClue == 19)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 20160000;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 4;
            }
            if (player.GetModPlayer<OSRSplayer>().eliteClue == 20)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 35;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 4;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clue Scroll (Elite)");
            Tooltip.SetDefault("<right> to check progress");
        }
    }
}
