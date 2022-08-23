using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class EarthboltP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthbolt");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = 1;
            Projectile.scale = 1f;
            Projectile.light = 0.2f;
        }
        Color c = new Color(25, 145, 22);
        Vector3 x = new Vector3(25, 145, 22);
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, x * 0.005f);
            Projectile.velocity.Y = Projectile.oldVelocity.Y;
            if (Main.rand.NextBool(1))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("Windbolt").Type, Projectile.velocity.X * -0.2f, 0, 0, c);
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/Earthhit2"), Projectile.position);
            Dust.NewDust(Projectile.Center, 1, 1, Mod.Find<ModDust>("WindstrikeD").Type, 0, 0, 0, c);
            for (int i = 0; i < 9; ++i)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(360));
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("WindboltD").Type, perturbedSpeed.X, perturbedSpeed.Y, 0, c);
            }
            Lighting.AddLight(Projectile.position, x * 0.005f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(9))
            {
                target.AddBuff(BuffID.Slow, 150);
            }
        }

    }
}