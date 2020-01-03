using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Dharokaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dharok's Greataxe");
        }
        public override void SetDefaults()
        {
            item.damage = 120;
            item.melee = true;
            item.crit = 21;
            item.width = 58;
            item.height = 56;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 3f;
            item.value = Item.sellPrice(0, 10);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage = (int)(damage * 1.4f);
                if (player.GetModPlayer<OSRSplayer>().Dharokset && player.statLife < 100)
                {
                    damage += (int)(damage * (0.05f * (100f - player.statLife)));
                }
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: 40% increased damage]"));
            }
        }
    }
}