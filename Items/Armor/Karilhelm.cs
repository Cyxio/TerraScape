﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 60def 20damage 10crit 10movespeed
{
    [AutoloadEquip(EquipType.Head)]
    public class Karilhelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's Coif");
            Tooltip.SetDefault("20% chance not to consume ammo");
        }
        public override void UpdateEquip(Player player)
        {
            player.ammoCost80 = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Karilbody").Type && legs.type == Mod.Find<ModItem>("Karillegs").Type;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Karilset = true;
            player.setBonus = "Ranged attacks have a chance to duplicate";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Karilset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Karil's crossbow has a chance to spawn a homing bolt]"));
            }           
        }
        public override void SetDefaults()
        {
            Item.width = 23;
            Item.height = 27;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 12;
        }
    }
}