using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class Spade : ModItem
    {
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddRecipeGroup(RecipeGroupID.Wood, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spade");
            Tooltip.SetDefault("A requirement for clue hunting!\n<right> to check clue statistics");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.rare = ItemRarityID.Orange;
        }
        public override bool CanUseItem(Player player)
        {
            return player.velocity.Y == 0;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                ClueStats(player);
                Item.noUseGraphic = true;
            }
            else
            {
                Item.noUseGraphic = false;
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Spade"), player.position);
                int block = Main.tile[(int)player.position.X / 16, (int)player.Bottom.Y / 16].TileType;
                player.GetModPlayer<OSRSplayer>().ClueDig(block);
            }
            return true;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            Vector2 fixVector = new Vector2(0, -8);
            if (player.direction == 1)
            {
                fixVector.X = -20;
            }
            if (player.direction == -1)
            {
                fixVector.X = -11;
            }
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                player.itemRotation += MathHelper.ToRadians(player.direction * 60f);
            }
            player.itemRotation -= MathHelper.ToRadians(player.direction * 2.5f);
            player.itemLocation = player.MountedCenter + fixVector;
        }
        private void ClueStats(Player player)
        {
            int easy = player.GetModPlayer<OSRSplayer>().completedEasy;
            int medium = player.GetModPlayer<OSRSplayer>().completedMedium;
            int hard = player.GetModPlayer<OSRSplayer>().completedHard;
            int elite = player.GetModPlayer<OSRSplayer>().completedElite;
            int master = player.GetModPlayer<OSRSplayer>().completedMaster;
            int total = easy + medium + hard + elite + master;
            Color color = Colors.RarityNormal;
            if (total >= 100)
            {
                color = Colors.RarityGreen;
            }
            if (total >= 200)
            {
                color = Colors.RarityBlue;
            }
            if (total >= 300)
            {
                color = Colors.RarityOrange;
            }
            if (total >= 400)
            {
                color = Colors.RarityRed;
            }
            if (total >= 500)
            {
                color = Colors.RarityPink;
            }
            if (total >= 600)
            {
                color = Colors.RarityPurple;
            }
            if (total >= 700)
            {
                color = Colors.RarityLime;
            }
            if (total >= 800)
            {
                color = Colors.RarityYellow;
            }
            if (total >= 900)
            {
                color = Colors.RarityCyan;
            }
            if (total >= 1000)
            {
                color = new Color(225, 6, 67);
            }
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(string.Format("You have completed a total of {0} clues!", total), color);
                Main.NewText(string.Format("Easy: {0} Medium: {1} Hard: {2} Elite: {3} Master: {4}", easy, medium, hard, elite, master), color);
            }
        }
    }
}
