using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Smokebattlestaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoke Battlestaff");
            Tooltip.SetDefault("Creates a cloud of lingering smoke");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 120;
            item.magic = true;
            item.mana = 12;
            item.width = 52;
            item.height = 58;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.scale = 0.9f;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 10);
            item.rare = 8;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = ModContent.ProjectileType<Projectiles.Smokebattlestaff>();
            item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.ownedProjectileCounts[type] > 2)
            {
                int toKill = -1;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.projectile[i].type == type && toKill == -1)
                    {
                        toKill = i;
                    }
                    if (toKill != -1 && Main.projectile[i].type == type)
                    {
                        if (Main.projectile[i].timeLeft < Main.projectile[toKill].timeLeft)
                        {
                            toKill = i;
                        }
                    }
                }
                Main.projectile[toKill].Kill();
            }
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
    }
}