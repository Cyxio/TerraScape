using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 1f;
            Projectile.light = 0.2f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, Projectile.oldPos[k] + new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f), Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
        Color c = new Color(25, 145, 22);
        Vector3 x = new Vector3(25, 145, 22);
        public override void AI()
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("WindboltD").Type, 0, 0, 150, c);
            Lighting.AddLight(Projectile.position, x * 0.005f);
            Projectile.velocity.Y = Projectile.oldVelocity.Y;
            Projectile.rotation += 0.2f;
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
        }
        public override void Kill(int timeLeft)
        {
            Vector2 v = new Vector2(-20, -20);
            SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthhit3"), Projectile.position);
            int dust = Dust.NewDust(Projectile.Center + v, 0, 0, Mod.Find<ModDust>("WindblastD").Type, 0, 0, 100, c);
            int dust1 = Dust.NewDust(Projectile.Center + v, 0, 0, Mod.Find<ModDust>("WindblastD").Type, 0, 0, 100, c);
            int dust2 = Dust.NewDust(Projectile.Center + v, 0, 0, Mod.Find<ModDust>("WindblastD").Type, 0, 0, 100, c);
            Main.dust[dust1].frame = new Rectangle(0, 40, 40, 40);
            Main.dust[dust1].position = Main.dust[dust].position;
            Main.dust[dust1].scale = 1f;
            Main.dust[dust].scale = 1f;
            Main.dust[dust2].frame = new Rectangle(0, 80, 40, 40);
            Main.dust[dust2].position = Main.dust[dust].position;
            Main.dust[dust2].scale = 1f;
            for (int i = 0; i < 9; ++i)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f).RotatedByRandom(MathHelper.ToRadians(360));
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("Windbolt").Type, perturbedSpeed.X, perturbedSpeed.Y, 100, c, 2f);
            }
            Lighting.AddLight(Projectile.position, x * 0.005f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(9))
            {
                target.AddBuff(BuffID.Slow, 200);
            }
        }

    }
}