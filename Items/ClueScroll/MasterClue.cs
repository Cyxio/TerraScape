using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OldSchoolRuneScape.UI;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class Masterrecipe : ModRecipe
    {
        public Masterrecipe(Mod mod) : base(mod)
        {
        }
        public override bool RecipeAvailable()
        {
            return !Main.player[Main.myPlayer].HasItem(ModContent.ItemType<MasterClue>());
        }
    }
    public class MasterClue : Clue
    {
        public override void AddRecipes()
        {
            Masterrecipe r = new Masterrecipe(mod);
            r.AddIngredient(ModContent.ItemType<EasyClue>());
            r.AddIngredient(ModContent.ItemType<MediumClue>());
            r.AddIngredient(ModContent.ItemType<HardClue>());
            r.AddIngredient(ModContent.ItemType<EliteClue>());
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
        public override bool CanPickup(Player player)
        {
            return !player.HasItem(ModContent.ItemType<MasterClue>()) && player.HeldItem.type != ModContent.ItemType<MasterClue>() && player.trashItem.type != ModContent.ItemType<MasterClue>();
        }
        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().masterSteps == 0)
            {
                player.GetModPlayer<OSRSplayer>().masterSteps = Main.rand.Next(14, 21);
            }
            if (player.GetModPlayer<OSRSplayer>().masterClue == 0)
            {
                player.GetModPlayer<OSRSplayer>().masterClue = 1 + Main.rand.Next(OldSchoolRuneScape.masterClueCount);
                player.GetModPlayer<OSRSplayer>().masterStage = 0;
            }
            if (player.GetModPlayer<OSRSplayer>().cluestep == 5)
            {
                player.GetModPlayer<OSRSplayer>().masterStage++;
                if (player.GetModPlayer<OSRSplayer>().masterStage >= player.GetModPlayer<OSRSplayer>().masterSteps)
                {
                    item.TurnToAir();
                    player.QuickSpawnItem(ModContent.ItemType<CasketMaster>());
                    player.GetModPlayer<OSRSplayer>().ClueReset(ModContent.ItemType<MasterClue>(), player);
                    player.GetModPlayer<OSRSplayer>().CompleteClue(5, player);
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_has_completed_a_Master_Clue_Scroll! 10");
                    }
                    else
                    {
                        Main.NewText("Master Clue Scroll complete!", new Color(225, 6, 67));
                    }
                }
                else
                {
                    if (Main.netMode == 1)
                    {
                        player.GetModPlayer<OSRSplayer>().ClueMessage(player.name + "_completed_a_step_of_a_Master_Clue_Scroll 5");
                    }
                    else
                    {
                        Main.NewText("Master Clue step complete!", Colors.RarityAmber);
                    }
                }
                player.GetModPlayer<OSRSplayer>().cluestep = 0;
                player.GetModPlayer<OSRSplayer>().masterClue = 1 + Main.rand.Next(OldSchoolRuneScape.masterClueCount);
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
                Main.NewText("Current step: " + (player.GetModPlayer<OSRSplayer>().masterStage + 1) + " / " + player.GetModPlayer<OSRSplayer>().masterSteps);
                player.itemAnimation = 30;
                player.itemTime = 30;
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            ClueUI.texture = "OldSchoolRuneScape/Items/ClueScroll/Master/Master" + player.GetModPlayer<OSRSplayer>().masterClue;
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
            if (player.GetModPlayer<OSRSplayer>().masterClue == 3)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 134;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 5;
            }
            if (player.GetModPlayer<OSRSplayer>().masterClue == 12)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 20130;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 5;
            }
            if (player.GetModPlayer<OSRSplayer>().masterClue == 13)
            {
                player.GetModPlayer<OSRSplayer>().challengeAns = 188;
                player.GetModPlayer<OSRSplayer>().challengeDiff = 5;
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clue Scroll (Master)");
            Tooltip.SetDefault("<right> to check progress");
        }
    }
}
