using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ballistaproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ballista javelin");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        NPC targett = null;
        List<NPC> targets = new List<NPC>();
        public override void AI()
        {            
            if (Projectile.ai[0] == 0 || Projectile.ai[0] == 1 || Projectile.ai[0] == 3)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
                Projectile.velocity.Y += 0.12f;
            }            
            if (Projectile.ai[0] == 2 && targett != null)
            {
                if (!targett.active)
                {
                    Projectile.ai[0] = 0;
                }
                Projectile.velocity = Vector2.Zero;
                Projectile.position += targett.velocity;
                if (Projectile.timeLeft < 4 && Projectile.friendly == false)
                {
                    SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
                    Projectile.friendly = true;
                    for (int o = 0; o < 36; o++)
                    {
                        Vector2 vel = new Vector2(3).RotatedBy(MathHelper.ToRadians(10 * o));
                        int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Smoke, vel.X, vel.Y, 100);
                        Main.dust[dust].scale = 1.5f;
                        dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Torch, vel.X, vel.Y);
                        Main.dust[dust].scale = 1.5f;
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
            if (Projectile.ai[0] == 1 || Projectile.ai[0] == 3)
            {
                int dust = Dust.NewDust(Projectile.position, 0, 0, DustID.RedTorch);
                Main.dust[dust].scale = 2.5f;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.ai[0] == 3)
            {
                if (Projectile.timeLeft > 300)
                {
                    Projectile.timeLeft = 300;
                }
                Projectile.localNPCHitCooldown = 90;
                Projectile.penetrate = -1;
                foreach (NPC npc in targets)
                {
                    npc.position = Projectile.position - new Vector2(npc.width / 2, npc.height / 2);
                    npc.netUpdate = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[0] == 1)
            {
                Projectile.velocity *= 0;
                Projectile.penetrate = 10;
                Projectile.friendly = false;
                Projectile.timeLeft = 70;
                Projectile.ai[0] = 2;
                targett = target;
            }
            if (Projectile.ai[0] == 3)
            {
                targets.Add(target);
                Projectile.velocity *= 0.98f;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            targets.Clear();
        }
    }
}
