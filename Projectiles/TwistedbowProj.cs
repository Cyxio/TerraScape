using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class TwistedbowProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twistedbowproj");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.inventory[player.selectedItem].type != ModContent.ItemType<Items.Twistedbow>())
            {
                projectile.Kill();
            }
            projectile.position = player.MountedCenter;
            if (projectile.timeLeft >= 420)
            {
                player.position -= player.velocity;
                for (int o = 0; o < (600 - projectile.timeLeft) / 5; o++)
                {
                    int dust = Dust.NewDust(player.Center, 0, 0, 267, 0, 0, 0, Color.ForestGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 0.5f;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].position = player.MountedCenter + new Vector2(0, 40).RotatedBy(MathHelper.ToRadians(5 * o));
                    dust = Dust.NewDust(player.Center, 0, 0, 267, 0, 0, 0, Color.ForestGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 0.5f;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].position = player.MountedCenter + new Vector2(0, 40).RotatedBy(MathHelper.ToRadians(-5 * o));
                }
            }
            else
            {
                if (player.velocity.Length() < 18f)
                {
                    player.velocity *= 1.015f;
                }
                for (int i = 0; i < 72; i++)
                {
                    int dust = Dust.NewDust(player.Center, 0, 0, 267, 0, 0, 0, Color.ForestGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 0.75f;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].position = player.MountedCenter + new Vector2(0, 40).RotatedBy(MathHelper.ToRadians(5 * i));
                }
                NPC target = null;
                for (int i = 0; i < 200; i++)
                {
                    NPC mem = Main.npc[i];
                    if (mem.WithinRange(projectile.position, 500f))
                    {
                        if (target == null)
                        {
                            target = mem;
                        }
                        if (projectile.Distance(mem.Center) < projectile.Distance(target.Center) && mem.active)
                        {
                            target = mem;
                        }
                    }
                }
                if (target != null && !target.friendly && target.life > 0 && projectile.timeLeft % 3 == 0)
                {
                    Vector2 spd = target.Center - projectile.position;
                    spd.Normalize();
                    Projectile.NewProjectile(projectile.position, spd * 20f, ProjectileID.CursedArrow, player.GetWeaponDamage(player.inventory[player.selectedItem]), 0f, player.whoAmI);
                }              
            }           
        }
    }
}
