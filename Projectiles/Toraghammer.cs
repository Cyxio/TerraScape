using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Toraghammer : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's hammer");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
            aiType = ProjectileID.PaladinsHammerFriendly;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, projectile.GetAlpha(new Color(160, 160, 160, 0)), projectile.rotation, drawOrigin, (4f / (4f + k)), SpriteEffects.None, 0f);
            }
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(160, 160, 160, 0);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.player[projectile.owner].GetModPlayer<OSRSplayer>().Toragset && Main.player[projectile.owner].GetModPlayer<OSRSplayer>().Amuletdamned && Main.rand.Next(10) == 0)
            {
                target.velocity *= 0f;
                target.netUpdate = true;
            }
        }
    }
}
