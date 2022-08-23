using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Runethrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runethrow");
        }
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 26;
            Projectile.height = 30;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
        }
        public override void AI()
        {
            Projectile.position += Projectile.velocity;
            Projectile.rotation += 0.07f * Projectile.direction + Projectile.velocity.X * 0.15f;
            Projectile.velocity.X *= 0.975f;
            Projectile.velocity.Y *= 0.975f;
            if (Projectile.timeLeft < 1185)
            {
                Projectile.velocity.Y += 0.2f;
                Projectile.velocity.Y /= 0.975f;
            }
            if (Projectile.velocity.Y > 6)
            {
                Projectile.velocity.Y = 6;
            }

        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            if (Main.rand.NextBool(2))
            {
                Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, Mod.Find<ModItem>("Runethrownaxe").Type, 1);
            }
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("Runedust").Type, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
            }
        }
    }
}
