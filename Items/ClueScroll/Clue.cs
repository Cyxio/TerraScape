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
            tooltips.Add(new TooltipLine(Mod, "Warning", "[c/FF5A5A:Attempting to sell, trash or store a clue in a chest will destroy it permanently]"));
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
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.channel = true;
        }
        public override bool CanUseItem(Player player)
        {
            return !ClueRewardUI.visible;
        }
    }
}
