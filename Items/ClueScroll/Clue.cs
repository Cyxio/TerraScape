using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.UI;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace OldSchoolRuneScape.Items.ClueScroll
{
    public class Clue : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(mod, "Warning", "[c/FF5A5A:Attempting to sell, trash or store a clue in a chest will destroy it permanently]"));
        }
        public override string Texture
        {
            get
            {
                return "OldSchoolRuneScape/Items/ClueScroll/Clue";
            }
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 3;
            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;
            item.noUseGraphic = true;
            item.channel = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !ClueRewardUI.visible;
        }
    }
}
