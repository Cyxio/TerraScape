﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Hard
{
    [AutoloadEquip(EquipType.Head)]
    public class RunehelmG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rune Full Helm (g)");
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RunebodyG") && legs.type == mod.ItemType("RunelegsG");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased melee damage";
            player.meleeDamage += 0.2f;
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 28;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.defense = 7;
        }
    }
}