using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 120;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.width = 52;
            Item.height = 58;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.scale = 0.9f;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<Projectiles.Smokebattlestaff>();
            Item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
    }
}