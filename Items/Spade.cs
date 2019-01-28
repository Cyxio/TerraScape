using Terraria;
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
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.IronBar, 2);
            r.AddRecipeGroup("Wood", 10);
            r.AddTile(TileID.Anvils);
            r.SetResult(this);
            r.anyIronBar = true;
            r.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spade");
            Tooltip.SetDefault("A requirement for clue hunting!\n<right> to check clue statistics");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.useStyle = 5;
            item.useTime = 25;
            item.useAnimation = 25;
            item.rare = 3;
        }
        public override bool CanUseItem(Player player)
        {
            return player.velocity.Y == 0;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                ClueStats(player);
                item.noUseGraphic = true;
            }
            else
            {
                item.noUseGraphic = false;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Spade"), player.position);
                int block = Main.tile[(int)player.position.X / 16, (int)player.Bottom.Y / 16].type;
                player.GetModPlayer<OSRSplayer>().ClueDig(block);
            }
            return true;
        }
        public override void UseStyle(Player player)
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
            if (Main.netMode == 0)
            {
                Main.NewText(string.Format("You have completed a total of {0} clues!", total), color);
                Main.NewText(string.Format("Easy: {0} Medium: {1} Hard: {2} Elite: {3} Master: {4}", easy, medium, hard, elite, master), color);
            }
            else
            {
                Main.NewText(string.Format("{1} has completed a total of {0} clues!", total, player.name), color);
                Main.NewText(string.Format("Easy: {0} Medium: {1} Hard: {2} Elite: {3} Master: {4}", easy, medium, hard, elite, master), color);
            }
        }
    }
}
