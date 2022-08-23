﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class BlacklegsG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Platelegs (g)");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.defense = 2;
        }
    }
}