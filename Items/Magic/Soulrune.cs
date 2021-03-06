﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Soulrune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Rune");
            Tooltip.SetDefault("'Used for high level curse spells'");
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
