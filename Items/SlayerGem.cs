using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class SlayerGem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Gem");
            Tooltip.SetDefault("Displays information based on your current slayer task.");
        }
        public override void SetDefaults()
        {
            Item.ToolTip = Terraria.UI.ItemTooltip.None;
            Item.maxStack = 1;
            Item.width = 20;
            Item.height = 22;
            Item.rare = -11;
            Item.value = Item.buyPrice(0, 0, 1, 0);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = new TooltipLine(Mod, "slayText", OSRSplayer.slayerText);
            Color color = Colors.RarityBlue;
            switch (Main.player[Main.myPlayer].GetModPlayer<OSRSplayer>().slayerDifficulty)
            {
                case 0: color = Colors.RarityNormal; ; break;
                case 1: color = Colors.RarityBlue; ; break;
                case 2: color = Colors.RarityGreen; ; break;
                case 3: color = Colors.RarityOrange; ; break;
                case 4: color = Colors.RarityRed; ; break;
                case 5: color = Colors.RarityPink; ; break;
                case 6: color = Colors.RarityPurple; ; break;
                case 7: color = Colors.RarityLime; ; break;
                case 8: color = Colors.RarityYellow; ; break;
                case 9: color = Colors.RarityCyan; ; break;
                case 10: color = new Color(225, 6, 67); ; break;
                default:
                    break;
            }
            tt.OverrideColor = color;
            tooltips.Add(tt);
        }
    }
}

