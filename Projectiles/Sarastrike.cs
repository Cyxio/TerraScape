using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Sarastrike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sarastrike");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 14;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.timeLeft = 45;
            projectile.extraUpdates = 1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 0.5f;
            projectile.alpha = 100;
            projectile.light = 0.75f;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (projectile.timeLeft < 31)
            {
                projectile.tileCollide = true;
            }
            if (projectile.timeLeft == 45)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(30));
            }
            if (projectile.timeLeft == 40)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-60));
            }
            if (projectile.timeLeft == 30)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(60));
            }
            if (projectile.timeLeft == 25)
            {
                projectile.timeLeft = 14;
                projectile.velocity *= 0.000001f;
            }           
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.timeLeft > 14)
            {
                projectile.timeLeft = 14;
            }
            projectile.velocity *= 0.000001f;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(7, 7);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, new Rectangle(0, 0, 14, 14), color, projectile.rotation, drawOrigin, projectile.scale + (i * 0.0357f), SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}