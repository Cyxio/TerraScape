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
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].type != ModContent.ItemType<Items.Weapons.Ranged.Twistedbow>())
            {
                Projectile.Kill();
            }
            Projectile.position = player.MountedCenter;
            if (Projectile.timeLeft >= 420)
            {
                player.position -= player.velocity;
                for (int o = 0; o < (600 - Projectile.timeLeft) / 5; o++)
                {
                    int dust = Dust.NewDust(player.Center, 0, 0, DustID.RainbowMk2, 0, 0, 0, Color.ForestGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 0.5f;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].position = player.MountedCenter + new Vector2(0, 40).RotatedBy(MathHelper.ToRadians(5 * o));
                    dust = Dust.NewDust(player.Center, 0, 0, DustID.RainbowMk2, 0, 0, 0, Color.ForestGreen, 1f);
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
                    int dust = Dust.NewDust(player.Center, 0, 0, DustID.RainbowMk2, 0, 0, 0, Color.ForestGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 0.75f;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].position = player.MountedCenter + new Vector2(0, 40).RotatedBy(MathHelper.ToRadians(5 * i));
                }
                NPC target = null;
                for (int i = 0; i < 200; i++)
                {
                    NPC mem = Main.npc[i];
                    if (mem.WithinRange(Projectile.position, 500f))
                    {
                        if (target == null)
                        {
                            target = mem;
                        }
                        if (Projectile.Distance(mem.Center) < Projectile.Distance(target.Center) && mem.active)
                        {
                            target = mem;
                        }
                    }
                }
                if (target != null && !target.friendly && target.life > 0 && Projectile.timeLeft % 3 == 0)
                {
                    Vector2 spd = target.Center - Projectile.position;
                    spd.Normalize();
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, spd * 20f, ProjectileID.CursedArrow, player.GetWeaponDamage(player.inventory[player.selectedItem]), 0f, player.whoAmI);
                }              
            }           
        }
    }
}
