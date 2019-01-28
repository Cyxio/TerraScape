using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Guthanspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Warspear");
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shootSpeed = 3.7f;
            item.knockBack = 3f;
            item.width = 58;
            item.height = 60;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType<Projectiles.Guthanspear>();
            item.value = Item.sellPrice(0, 10);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = true;
        }

        public override float UseTimeMultiplier(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                return 1.10f;
            }
            return base.UseTimeMultiplier(player);
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage = (int)(damage * 1.2f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: 20% increased damage & 10% increased speed]"));
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // this is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
