﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.Head)]
    public class Lavadragonmask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Dragon Mask");
        }
        public override void SetDefaults()
        {
            item.vanity = true;
            item.rare = 2;
            item.width = 24;
            item.height = 28;
        }
        public override bool DrawHead()
        {
            return false;
        }
    }
}