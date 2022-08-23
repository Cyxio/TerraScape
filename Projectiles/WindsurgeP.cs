using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class WindsurgeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Surge");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 6;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        Vector3 x = new Vector3(150, 150, 150);
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter < 6)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.frameCounter < 12)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.frameCounter < 18)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.frameCounter < 24)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frameCounter = 0;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            Lighting.AddLight(Projectile.position, x * 0.005f);
            if (Projectile.timeLeft % 4 == 0)
            {
                Vector2 position = Projectile.Center;
                Vector2 spd = Projectile.velocity;
                spd.Normalize();
                spd *= 11f;
                float a = 36f;
                for (int i = 0; i < a; i++)
                {
                    Dust dust;
                    float strength = 1f - Math.Abs((i - a/2f) / (a/2f));
                    Vector2 spawn = position + (spd.RotatedBy((Math.PI / a) * i) * (1f + strength));
                    Vector2 speed = Projectile.Center - spawn;
                    speed.Normalize();
                    speed *= strength * 0.75f;
                    dust = Terraria.Dust.NewDustPerfect(spawn, 263, speed, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                    spawn = position + (spd.RotatedBy((Math.PI / a) * -i) * (1f + strength));
                    speed = Projectile.Center - spawn;
                    speed.Normalize();
                    speed *= strength * 0.75f;
                    dust = Terraria.Dust.NewDustPerfect(spawn, 263, speed, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                }
            }
            if (Projectile.timeLeft < 31)
            {
                if (Projectile.alpha != 255)
                {
                    Projectile.alpha = 255;
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windhit5"), Projectile.position);
                }
                Projectile.velocity *= 0f;
                Projectile.width = 112;
                Projectile.height = 112;
                Projectile.penetrate = -1;
                for (int i = 0; i < 7; i++)
                {
                    Dust dust;
                    dust = Terraria.Dust.NewDustPerfect(Projectile.Center, 263, new Vector2(0, Main.rand.NextFloat(5f, 7f)).RotatedBy(Main.rand.NextFloat(0, MathHelper.TwoPi)), 0, new Color(255, 255, 255), 2.5f);
                    dust.noGravity = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 30 && Projectile.penetrate == 2)
            {
                Projectile.timeLeft = 30;
                Projectile.tileCollide = false;
                Projectile.position -= new Vector2(34, 34);
                Projectile.alpha = 254;
                Projectile.netUpdate = true;
            }
            else if (Projectile.timeLeft > 30)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Windnado>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 30)
            {
                Projectile.timeLeft = 30;
                Projectile.tileCollide = false;
                Projectile.position -= new Vector2(34, 34);
                Projectile.alpha = 254;
                Projectile.netUpdate = true;            
            }
            return false;
        }
    }
}