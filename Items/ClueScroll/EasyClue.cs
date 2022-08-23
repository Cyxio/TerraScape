using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using System;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class EasyClue : Clue
    {
        public override bool CanPickup(Player player)
        {
            return !player.HasItem(ModContent.ItemType<EasyClue>()) && player.HeldItem.type != ModContent.ItemType<EasyClue>() && player.trashItem.type != ModContent.ItemType<EasyClue>();
        }

        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().easySteps == 0)
            {
                player.GetModPlayer<OSRSplayer>().easySteps = Main.rand.Next(3, 6);
            }
            if (player.GetModPlayer<OSRSplayer>().easyClue == 0)
            {
                player.GetModPlayer<OSRSplayer>().easyClue = 1 + Main.rand.Next(OldSchoolRuneScape.easyClueCount);
                player.GetModPlayer<OSRSplayer>().easyStage = 0;
            }
            if (player.GetModPlayer<OSRSplayer>().cluestep == 1)
            {
                player.GetModPlayer<OSRSplayer>().cluestep = 0;
                player.GetModPlayer<OSRSplayer>().easyStage++;
                if (player.GetModPlayer<OSRSplayer>().easyStage >= player.GetModPlayer<OSRSplayer>().easySteps)
                {
                    Item.TurnToAir();
                    player.QuickSpawnItem(player.GetSource_Loot(), ModContent.ItemType<CasketEasy>());
                    player.GetModPlayer<OSRSplayer>().ClueReset(ModContent.ItemType<EasyClue>(), player);
                    player.GetModPlayer<OSRSplayer>().CompleteClue(1, player);
                    if (player.GetModPlayer<OSRSplayer>().masterClue == 9)
                    {
                        player.GetModPlayer<OSRSplayer>().cluestep = 5;
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_has_completed_an_Easy_Clue_Scroll! 6");
                    }
                    else
                    {
                        Main.NewText("Easy Clue Scroll complete!", Colors.RarityLime);
                    }
                }
                else
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_completed_a_step_of_an_Easy_Clue_Scroll 1");
                    }
                    else
                    {
                        Main.NewText("Easy Clue step complete!", Colors.RarityGreen);
                    }
                }
                player.GetModPlayer<OSRSplayer>().easyClue = 1 + Main.rand.Next(OldSchoolRuneScape.easyClueCount);
            }
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Main.NewText("Current step: " + (player.GetModPlayer<OSRSplayer>().easyStage + 1) + " / " + player.GetModPlayer<OSRSplayer>().easySteps);
                player.itemAnimation = 30;
                player.itemTime = 30;
            }
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            ClueUI.texture = "OldSchoolRuneScape/Items/ClueScroll/Easy/Easy" + player.GetModPlayer<OSRSplayer>().easyClue;
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
            if (player.GetModPlayer<OSRSplayer>().easyClue == 25)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 59;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 1;
            }
            if (player.GetModPlayer<OSRSplayer>().easyClue == 26)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 40;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 1;
            }
            if (player.GetModPlayer<OSRSplayer>().easyClue == 27)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 7;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 1;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clue Scroll (Easy)");
            Tooltip.SetDefault("<right> to check progress");
        }
    }
}
