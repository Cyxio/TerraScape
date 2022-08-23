using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class WindwaveP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windwave");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1f;
            Projectile.light = 0.2f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        Vector3 x = new Vector3(150, 150, 150);
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2f);
            Lighting.AddLight(Projectile.position, x * 0.005f);
            if (Projectile.timeLeft % 5 == 0)
            {
                Vector2 position = Projectile.Center;
                Vector2 spd = Projectile.velocity;
                spd.Normalize();
                spd *= 7.5f;
                for (int i = 0; i < 18; i++)
                {
                    Dust dust;
                    float strength = 1f - Math.Abs((i - 9f) / 9f);
                    Vector2 spawn = position + (spd.RotatedBy((Math.PI / 18f) * i) * (1f + strength));
                    Vector2 speed = Projectile.Center - spawn;
                    speed.Normalize();
                    dust = Terraria.Dust.NewDustPerfect(spawn, 263, speed, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                    spawn = position + (spd.RotatedBy((Math.PI / 18f) * -i) * (1f + strength));
                    speed = Projectile.Center - spawn;
                    speed.Normalize();
                    dust = Terraria.Dust.NewDustPerfect(spawn, 263, speed, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                }
            }
            if (Projectile.timeLeft < 19)
            {
                if (Projectile.alpha != 255)
                {
                    Projectile.alpha = 255;
                    SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Windhit4"), Projectile.position);
                }
                Projectile.width = 112;
                Projectile.height = 112;
            }
        }
        public override void PostDraw(Color lightColor)
        {
            if (Projectile.timeLeft < 18)
            {
                int frame = 5 - (int)(Projectile.timeLeft / 3f);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("Projectiles/WaveExplode").Value, Projectile.position - Main.screenPosition, new Rectangle(0, 112 * frame, 112, 112), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 18)
            {
                Projectile.timeLeft = 18;
                Projectile.velocity *= 0f;
                Projectile.tileCollide = false;
                Projectile.position -= new Vector2(41, 41);
                Projectile.alpha = 254;
                Projectile.netUpdate = true;
            }          
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 18)
            {
                Projectile.timeLeft = 18;
                Projectile.velocity *= 0f;
                Projectile.tileCollide = false;
                Projectile.position -= new Vector2(41, 41);
                Projectile.alpha = 254;
                Projectile.netUpdate = true;            
            }
            return false;
        }
    }
}