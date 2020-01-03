using Microsoft.Xna.Framework;
using Terraria;
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
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 200;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.timeLeft = 1200;
            projectile.extraUpdates = 6;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }

        Vector2[] points = new Vector2[31];
        int pointcounter = 0;
        int changetime = 0;

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int j = 0; j < 200; j++)
            {
                Vector2 center = projectile.oldPos[j] + new Vector2(7, 7);
                if (targetHitbox.Contains(center.ToPoint()))
                {
                    return true;
                }
            }
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void AI()
        {
            if (projectile.timeLeft % 4 == 0)
            {
                Lighting.AddLight(projectile.position, new Vector3(0, 1f, 1f));
                if (true)
                {
                    Dust dust;
                    Vector2 position = projectile.position;
                    for (int i = 0; i < 4; i++)
                    {
                        dust = Main.dust[Dust.NewDust(projectile.oldPos[Main.rand.Next(0, 200)], projectile.width, projectile.height, 92, 0f, 0f, 0, new Color(255, 255, 255), 0.5263158f)];
                    }
                    dust = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, 92, 0f, 0f, 0, new Color(255, 255, 255), 0.5263158f)];
                }

            }
            if (changetime > 0)
            {
                changetime--;
            }
            if (projectile.timeLeft > 1199)
            {
                Vector2 velocityV = projectile.velocity;
                velocityV.Normalize();
                velocityV *= 1500;
                Vector2 destination = projectile.position + velocityV;
                Vector2 path = destination - projectile.position;
                float length = path.Length();
                float step = length / 30f;
                Vector2 offset = projectile.velocity.RotatedBy(Math.PI/2);
                offset.Normalize();
                for (int i = 0; i < 30; i++)
                {
                    points[i] = projectile.position + ((path / 30f) * i) + (offset * Main.rand.Next(-50, 50));
                    points[30] = destination;
                }
            }
            if (changetime <= 0 && pointcounter < 30)
            {
                pointcounter++;
                Vector2 totarget = points[pointcounter] - projectile.position;
                float speed = projectile.velocity.Length();
                changetime = (int)(totarget.Length() / projectile.velocity.Length());
                totarget.Normalize();
                projectile.velocity = totarget;
                projectile.velocity *= speed;
            }
            if (projectile.timeLeft <= 200)
            {
                projectile.velocity *= 0.0001f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 200;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(7, 7);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, new Rectangle(0, 0, 14, 14), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        
    }
}