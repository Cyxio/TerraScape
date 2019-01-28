using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Toraghammers : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's Hammers");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PaladinsHammer);
            item.shoot = mod.ProjectileType<Projectiles.Toraghammer>();
            item.damage = 65;
            item.width = 54;
            item.height = 40;
            item.shootSpeed = 15f;
            item.useAnimation = 8;
            item.useTime = 4;
            item.autoReuse = true;
            item.rare = 7;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage = (int)(damage * 1.2f);
                item.shootSpeed = 25f;
            }
            else
            {
                item.shootSpeed = 15f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Increased range & 20% increased damage]"));
            }
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
