﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Body)]
    public class BronzebodyT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bronze Platebody (t)");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.defense = 2;
        }
    }
}