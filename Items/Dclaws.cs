using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dclaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Claws");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (30 second cooldown): Supercharges your claws for 8 seconds, making you able to dash with them]");
        }
        public override void SetDefaults()
        {
            item.damage = 144;
            item.melee = true;
            item.crit = 21;
            item.width = 62;
            item.height = 52;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 10;
            item.knockBack = 2f;
            item.autoReuse = false;
            item.useTurn = true;
            item.shootSpeed = 20;
            item.shoot = mod.ProjectileType("Dclaws");
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                player.AddBuff(mod.BuffType("Clawbuff"), 480);
                player.AddBuff(mod.BuffType("SpecCD"), 1800);
                return false;
            }
            else if (player.altFunctionUse == 2)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
    }
}