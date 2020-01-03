using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Veracflail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's Flail");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 80;
            item.knockBack = 2f;
            item.rare = 7;
            item.width = 48;
            item.height = 56;
            item.useAnimation = 25;
            item.useTime = 25;
            item.reuseDelay = 15;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<Projectiles.Veracflail>();
            item.shootSpeed = 20f;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage = (int)(damage * 1.2f);
                item.shootSpeed = 30f;
            }
            else
            {
                item.shootSpeed = 20f;
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
