using Terraria;
using Terraria.ID;
using OldSchoolRuneScape.UI;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class HardClue : Clue
    {
        public override bool CanPickup(Player player)
        {
            return !player.HasItem(ModContent.ItemType<HardClue>()) && player.HeldItem.type != ModContent.ItemType<HardClue>() && player.trashItem.type != ModContent.ItemType<HardClue>();
        }
        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().hardSteps == 0)
            {
                player.GetModPlayer<OSRSplayer>().hardSteps = Main.rand.Next(8, 13);
            }
            if (player.GetModPlayer<OSRSplayer>().hardClue == 0)
            {
                player.GetModPlayer<OSRSplayer>().hardClue = 1 + Main.rand.Next(OldSchoolRuneScape.hardClueCount);
                player.GetModPlayer<OSRSplayer>().hardStage = 0;
            }
            if (player.GetModPlayer<OSRSplayer>().cluestep == 3)
            {
                player.GetModPlayer<OSRSplayer>().hardStage++;
                if (player.GetModPlayer<OSRSplayer>().hardStage >= player.GetModPlayer<OSRSplayer>().hardSteps)
                {
                    Item.TurnToAir();
                    player.QuickSpawnItem(player.GetSource_Loot(), ModContent.ItemType<CasketHard>());
                    player.GetModPlayer<OSRSplayer>().ClueReset(ModContent.ItemType<HardClue>(), player);
                    player.GetModPlayer<OSRSplayer>().CompleteClue(3, player);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_has_completed_a_Hard_Clue_Scroll! 8");
                    }
                    else
                    {
                        Main.NewText("Hard Clue Scroll complete!", Colors.RarityRed);
                    }
                }
                else
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_completed_a_step_of_a_Hard_Clue_Scroll 3");
                    }
                    else
                    {
                        Main.NewText("Hard Clue step complete!", Colors.RarityOrange);
                    }
                }
                player.GetModPlayer<OSRSplayer>().cluestep = 0;
                player.GetModPlayer<OSRSplayer>().hardClue = 1 + Main.rand.Next(OldSchoolRuneScape.hardClueCount);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                Main.NewText("Current step: " + (player.GetModPlayer<OSRSplayer>().hardStage + 1) + " / " + player.GetModPlayer<OSRSplayer>().hardSteps);
                player.itemAnimation = 30;
                player.itemTime = 30;
            }
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            ClueUI.texture = "OldSchoolRuneScape/Items/ClueScroll/Hard/Hard" + player.GetModPlayer<OSRSplayer>().hardClue;
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
            if (player.GetModPlayer<OSRSplayer>().hardClue == 23)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 21;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 3;
            }
            if (player.GetModPlayer<OSRSplayer>().hardClue == 24)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 23;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 3;
            }
            if (player.GetModPlayer<OSRSplayer>().hardClue == 25)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 810;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 3;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clue Scroll (Hard)");
            Tooltip.SetDefault("<right> to check progress");
        }
    }
}
