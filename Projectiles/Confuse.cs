using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Confuse : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Confuse");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 360;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.light = 0.2f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(160, 160, 160, 0);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY) - projectile.velocity;
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, projectile.GetAlpha(lightColor), projectile.rotation, drawOrigin, (4f / (4f + k)), SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(6);
            projectile.velocity.Y += 0.1f;
            projectile.damage = 0;
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (projectile.Colliding(projectile.Hitbox, target.Hitbox) && target.active && !target.friendly)
                {
                    target.AddBuff(BuffID.Confused, 120);
                    projectile.active = false;
                }
            }
        }
    }
}