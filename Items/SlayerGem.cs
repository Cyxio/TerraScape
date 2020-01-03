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
            item.ToolTip = Terraria.UI.ItemTooltip.None;
            item.maxStack = 1;
            item.width = 20;
            item.height = 22;
            item.rare = -11;
            item.value = Item.buyPrice(0, 0, 1, 0);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = new TooltipLine(mod, "slayText", OSRSplayer.slayerText);
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
            tt.overrideColor = color;
            tooltips.Add(tt);
        }
    }
}

