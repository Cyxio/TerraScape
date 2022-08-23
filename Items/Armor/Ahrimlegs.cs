﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Ahrimlegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's Robe Skirt");
            Tooltip.SetDefault("8% increased magic critical strike chance & movement speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 8;
            player.moveSpeed += 0.08f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Ahrimset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Ahrim's staff fires additional homing projectiles]"));
            }
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 12;
        }
    }
}