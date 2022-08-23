using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                Projectile.alpha = 1;
            }
            Projectile.rotation += 0.2f;
            if (Projectile.ai[0] == 0)
            {
                int dust = Dust.NewDust(Projectile.position + new Vector2(25).RotatedBy(Projectile.rotation), Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Red);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(Projectile.position + new Vector2(25).RotatedBy(Projectile.rotation), Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Green);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[0] == 2)
            {
                int dust = Dust.NewDust(Projectile.position + new Vector2(25).RotatedBy(Projectile.rotation), Projectile.width, Projectile.height, DustID.PortalBolt, 0, 0, 0, Color.Blue);
                Main.dust[dust].scale = 1.3f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                bool highMeleeDamage = p.GetDamage(DamageClass.Melee).Flat > p.GetDamage(DamageClass.Ranged).Flat &&
                                        p.GetDamage(DamageClass.Melee).Flat > p.GetDamage(DamageClass.Magic).Flat;
                bool highRangeDamage = p.GetDamage(DamageClass.Ranged).Flat > p.GetDamage(DamageClass.Melee).Flat &&
                                        p.GetDamage(DamageClass.Ranged).Flat > p.GetDamage(DamageClass.Magic).Flat;
                bool highMagicDamage = p.GetDamage(DamageClass.Magic).Flat > p.GetDamage(DamageClass.Ranged).Flat &&
                                        p.GetDamage(DamageClass.Magic).Flat > p.GetDamage(DamageClass.Melee).Flat;
                if (p.Distance(Projectile.position) < 50f)
                {
                    if (Projectile.ai[0] == 0 && highMeleeDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        Projectile.Kill();
                    }
                    else if (Projectile.ai[0] == 1 && highRangeDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        Projectile.Kill();
                    }
                    else if (Projectile.ai[0] == 2 && highMagicDamage)
                    {
                        p.HealEffect(20);
                        p.statLife += 20;
                        Projectile.Kill();
                    }
                    else
                    {
                        Projectile.position = p.Center;
                        if (Projectile.timeLeft > 4)
                        {
                            Projectile.timeLeft = 4;
                        }                      
                    }
                }
            }
        }
    }
}
