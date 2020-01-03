﻿using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace OldSchoolRuneScape.Items
{
    public class ZGS : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zamorak Godsword");
            Tooltip.SetDefault("A terrifying, heavy sword. \n[c/5cdb7d:Special attack (25 second cooldown): Ice Cleave]\n[c/5cdb7d:Strike forward dealing 250% increased damage with doubled critical strike chance and freeze the target for 20 seconds]");
        }
        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
            item.crit = 4;
            item.width = 76;
            item.height = 86;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 10f;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;
            item.shootSpeed = 0;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                item.noMelee = true;
                Projectile.NewProjectile(player.Center, new Vector2(player.direction, 0), mod.ProjectileType("ZGSspec"), (int)(item.damage * 2.5f), item.knockBack, player.whoAmI, 0, 0);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/AGSspec"), player.position);
                item.noUseGraphic = true;
                player.AddBuff(mod.BuffType("SpecCD"), 60*25);
            }
            else if (player.altFunctionUse == 2)
            {
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = false;
                return false;
            }
            else
            {
                item.noMelee = false;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
        }
        
    }
}

