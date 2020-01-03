﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Easy
{
    [AutoloadEquip(EquipType.Legs)]
    public class IronlegsT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Platelegs (t)");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 1, 50);
            item.defense = 1;
        }
    }
}