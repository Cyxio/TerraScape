using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Thirdagemage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd age spell");
            Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 14;
            projectile.height = 14;
            projectile.scale = 1f;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(0.6f, 0.6f, 0.6f));
            projectile.rotation += MathHelper.ToRadians(projectile.velocity.Length());
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
            if (projectile.timeLeft >= 570)
            {
                projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(projectile.ai[0]));
            }            
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(8, 8);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                int o = (projectile.frame + i) % Main.projFrames[projectile.type];
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, new Rectangle(0, 16 * (o), 16, 16), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.Center - (projectile.velocity * 0.1f * i), 0, 0, 124, projectile.velocity.X * 0.3f, projectile.velocity.Y * 0.3f, 0, Color.White, 1f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}