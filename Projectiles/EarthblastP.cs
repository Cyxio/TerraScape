using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class EarthblastP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthblast");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.timeLeft = 1200;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.light = 0.2f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, projectile.oldPos[k] + new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        Color c = new Color(25, 145, 22);
        Vector3 x = new Vector3(25, 145, 22);
        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("WindboltD"), 0, 0, 150, c);
            Lighting.AddLight(projectile.position, x * 0.005f);
            projectile.velocity.Y = projectile.oldVelocity.Y;
            projectile.rotation += 0.2f;
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            Vector2 v = new Vector2(-20, -20);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Blast"), projectile.position);
            int dust = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0, 100, c);
            int dust1 = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0, 100, c);
            int dust2 = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0, 100, c);
            Main.dust[dust1].frame = new Rectangle(0, 40, 40, 40);
            Main.dust[dust1].position = Main.dust[dust].position;
            Main.dust[dust1].scale = 1f;
            Main.dust[dust].scale = 1f;
            Main.dust[dust2].frame = new Rectangle(0, 80, 40, 40);
            Main.dust[dust2].position = Main.dust[dust].position;
            Main.dust[dust2].scale = 1f;
            for (int i = 0; i < 9; ++i)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f).RotatedByRandom(MathHelper.ToRadians(360));
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windbolt"), perturbedSpeed.X, perturbedSpeed.Y, 100, c, 2f);
            }
            Lighting.AddLight(projectile.position, x * 0.005f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(9) == 0)
            {
                target.AddBuff(BuffID.Slow, 200);
            }
        }

    }
}