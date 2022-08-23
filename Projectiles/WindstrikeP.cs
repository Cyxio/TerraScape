using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class WindstrikeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Strike");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
        }
        private void DrawDustEllipse(float rotation) 
        {
            var step = 2f;
            var a = 20f;
            var b = 10f;
            var scaleModifier = 0.2f;
            for (float x = -a; x <= a; x += step)
            {
                float y = (float)(b * Math.Sqrt(a * a - x * x)) / a;
                float scale = (30 - Math.Abs(x)) / 30f * scaleModifier;
                float speed = 0.02f;
                Vector2 topVec = new Vector2(x, y).RotatedBy(rotation);
                Vector2 botVec = new Vector2(x, -y).RotatedBy(rotation);
                Dust.NewDustPerfect(Projectile.Center + topVec, ModContent.DustType<Dusts.Windstrike>(), Velocity: topVec * speed, Scale: 1f + scale);
                Dust.NewDustPerfect(Projectile.Center + botVec, ModContent.DustType<Dusts.Windstrike>(), Velocity: botVec * speed, Scale: 1f - scale);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, Color.White.ToVector3() * 0.003f);
            Projectile.velocity.Y = Projectile.oldVelocity.Y;
            Dust.NewDustPerfect(Projectile.position + new Vector2(Main.rand.Next(Projectile.width), Main.rand.Next(Projectile.height)), 
                ModContent.DustType<Dusts.Windstrike>(), Vector2.Zero, 0, Color.White);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.velocity.Y != 0) 
            {
                damage += 4;
                DrawDustEllipse(Projectile.rotation);
            }
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/WindStrikeHit"), Projectile.position);
            Dust.NewDust(Projectile.Center - new Vector2(8f), 1, 1, Mod.Find<ModDust>("WindstrikeD").Type, 0f, 0f, 0, Color.White);
        }

    }
}