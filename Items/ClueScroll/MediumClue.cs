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
    public class MediumClue : Clue
    {
        public override bool CanPickup(Player player)
        {
            return !player.HasItem(ModContent.ItemType<MediumClue>()) && player.HeldItem.type != ModContent.ItemType<MediumClue>() && player.trashItem.type != ModContent.ItemType<MediumClue>();
        }
        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().mediumSteps == 0)
            {
                player.GetModPlayer<OSRSplayer>().mediumSteps = Main.rand.Next(5, 11);
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 0)
            {
                player.GetModPlayer<OSRSplayer>().mediumClue = 1 + Main.rand.Next(OldSchoolRuneScape.mediumClueCount);
                player.GetModPlayer<OSRSplayer>().mediumStage = 0;
            }
            if (player.GetModPlayer<OSRSplayer>().cluestep == 2)
            {
                player.GetModPlayer<OSRSplayer>().mediumStage++;
                if (player.GetModPlayer<OSRSplayer>().mediumStage >= player.GetModPlayer<OSRSplayer>().mediumSteps)
                {
                    item.TurnToAir();
                    player.QuickSpawnItem(ModContent.ItemType<CasketMedium>());
                    player.GetModPlayer<OSRSplayer>().ClueReset(ModContent.ItemType<MediumClue>(), player);
                    player.GetModPlayer<OSRSplayer>().CompleteClue(2, player);
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_has_completed_a_Medium_Clue_Scroll! 7");
                    }
                    else
                    {
                        Main.NewText("Medium Clue Scroll complete!", Colors.RarityCyan);
                    }
                }
                else
                {
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_completed_a_step_of_a_Medium_Clue_Scroll 2");
                    }
                    else
                    {
                        Main.NewText("Medium Clue step complete!", Colors.RarityBlue);
                    }
                }
                player.GetModPlayer<OSRSplayer>().cluestep = 0;
                player.GetModPlayer<OSRSplayer>().mediumClue = 1 + Main.rand.Next(OldSchoolRuneScape.mediumClueCount);
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
                Main.NewText("Current step: " + (player.GetModPlayer<OSRSplayer>().mediumStage + 1) + " / " + player.GetModPlayer<OSRSplayer>().mediumSteps);
                player.itemAnimation = 30;
                player.itemTime = 30;
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            ClueUI.texture = "OldSchoolRuneScape/Items/ClueScroll/Medium/Medium" + player.GetModPlayer<OSRSplayer>().mediumClue;
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
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 31)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 15;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 32)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 13;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 2;
            }
            if (player.GetModPlayer<OSRSplayer>().mediumClue == 33)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 8;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 2;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clue Scroll (Medium)");
            Tooltip.SetDefault("<right> to check progress");
        }
    }
}
