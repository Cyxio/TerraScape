using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class SaraswordSpec : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SaraswordSpec");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 200;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 6;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
        }

        Vector2[] points = new Vector2[31];
        int pointcounter = 0;
        int changetime = 0;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int j = 0; j < 200; j++)
            {
                Vector2 center = Projectile.oldPos[j] + new Vector2(7, 7);
                if (targetHitbox.Contains(center.ToPoint()))
                {
                    return true;
                }
            }
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void AI()
        {
            if (Projectile.timeLeft % 4 == 0)
            {
                Lighting.AddLight(Projectile.position, new Vector3(0, 1f, 1f));
                if (true)
                {
                    Dust dust;
                    Vector2 position = Projectile.position;
                    for (int i = 0; i < 4; i++)
                    {
                        dust = Main.dust[Dust.NewDust(Projectile.oldPos[Main.rand.Next(0, 200)], Projectile.width, Projectile.height, DustID.Frost, 0f, 0f, 0, new Color(255, 255, 255), 0.5263158f)];
                    }
                    dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Frost, 0f, 0f, 0, new Color(255, 255, 255), 0.5263158f)];
                }

            }
            if (changetime > 0)
            {
                changetime--;
            }
            if (Projectile.timeLeft > 1199)
            {
                Vector2 velocityV = Projectile.velocity;
                velocityV.Normalize();
                velocityV *= 1500;
                Vector2 destination = Projectile.position + velocityV;
                Vector2 path = destination - Projectile.position;
                float length = path.Length();
                float step = length / 30f;
                Vector2 offset = Projectile.velocity.RotatedBy(Math.PI/2);
                offset.Normalize();
                for (int i = 0; i < 30; i++)
                {
                    points[i] = Projectile.position + ((path / 30f) * i) + (offset * Main.rand.Next(-50, 50));
                    points[30] = destination;
                }
            }
            if (changetime <= 0 && pointcounter < 30)
            {
                pointcounter++;
                Vector2 totarget = points[pointcounter] - Projectile.position;
                float speed = Projectile.velocity.Length();
                changetime = (int)(totarget.Length() / Projectile.velocity.Length());
                totarget.Normalize();
                Projectile.velocity = totarget;
                Projectile.velocity *= speed;
            }
            if (Projectile.timeLeft <= 200)
            {
                Projectile.velocity *= 0.0001f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 200;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(7, 7);
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, 0, 14, 14), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
        
    }
}