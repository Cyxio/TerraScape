using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dragonspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Spear");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (5 second cooldown): Shoves the enemy with the back end of the spear, knocking them away]");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shootSpeed = 3.0f;
            item.knockBack = 3f;
            item.width = 42;
            item.height = 56;
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType<Projectiles.Dragonspear>();
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0, 1);
                player.AddBuff(mod.BuffType("SpecCD"), 300);
                return false;
            }
            return true;
        }
    }
}
