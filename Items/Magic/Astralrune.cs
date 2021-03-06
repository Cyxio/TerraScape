﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Astralrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Rune");
            Tooltip.SetDefault("'Used for Lunar Spells'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 28;
            item.value = 10;
            item.rare = 5;
        }
    }
}
