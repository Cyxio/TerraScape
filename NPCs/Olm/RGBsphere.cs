using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class RGBsphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Sphere");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item15, projectile.position);
                projectile.alpha = 1;
            }
            projectile.rotation += 0.2f;
            if (projectile.ai[0] == 0)
            {
                int dust = Dust.NewDust(projectile.position + new Vector2(25).RotatedBy(projectile.rotation), projectile.width, projectile.height, 263, 0, 0, 0, Color.Red);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(projectile.position + new Vector2(25).RotatedBy(projectile.rotation), projectile.width, projectile.height, 263, 0, 0, 0, Color.Green);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.ai[0] == 2)
            {
                int dust = Dust.NewDust(projectile.position + new Vector2(25).RotatedBy(projectile.rotation), projectile.width, projectile.height, 263, 0, 0, 0, Color.Blue);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                if (p.Distance(projectile.position) < 50f)
                {
                    if (projectile.ai[0] == 0 && p.meleeDamage > p.rangedDamage && p.meleeDamage > p.magicDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        projectile.Kill();
                    }
                    else if (projectile.ai[0] == 1 && p.rangedDamage > p.magicDamage && p.rangedDamage > p.meleeDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        projectile.Kill();
                    }
                    else if (projectile.ai[0] == 2 && p.magicDamage > p.rangedDamage && p.magicDamage > p.meleeDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        projectile.Kill();
                    }
                    else
                    {
                        projectile.position = p.Center;
                        if (projectile.timeLeft > 4)
                        {
                            projectile.timeLeft = 4;
                        }                      
                    }
                }
            }
        }
    }
}
