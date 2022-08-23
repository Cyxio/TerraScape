using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 14;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 45;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 0.5f;
            Projectile.alpha = 100;
            Projectile.light = 0.75f;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.timeLeft < 31)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.timeLeft == 45)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(30));
            }
            if (Projectile.timeLeft == 40)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(-60));
            }
            if (Projectile.timeLeft == 30)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(60));
            }
            if (Projectile.timeLeft == 25)
            {
                Projectile.timeLeft = 14;
                Projectile.velocity *= 0.000001f;
            }           
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.timeLeft > 14)
            {
                Projectile.timeLeft = 14;
            }
            Projectile.velocity *= 0.000001f;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(7, 7);
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, new Rectangle(0, 0, 14, 14), color, Projectile.rotation, drawOrigin, Projectile.scale + (i * 0.0357f), SpriteEffects.None, 0);
            }
            return true;
        }
    }
}