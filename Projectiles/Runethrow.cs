using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.aiStyle = -1;
            projectile.width = 26;
            projectile.height = 30;
            projectile.penetrate = 3;
            projectile.timeLeft = 1200;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.thrown = true;
        }
        public override void AI()
        {
            projectile.position += projectile.velocity;
            projectile.rotation += 0.07f * projectile.direction + projectile.velocity.X * 0.15f;
            projectile.velocity.X *= 0.975f;
            projectile.velocity.Y *= 0.975f;
            if (projectile.timeLeft < 1185)
            {
                projectile.velocity.Y += 0.2f;
                projectile.velocity.Y /= 0.975f;
            }
            if (projectile.velocity.Y > 6)
            {
                projectile.velocity.Y = 6;
            }

        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem(projectile.Center, mod.ItemType("Runethrownaxe"), 1);
            }
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Runedust"), Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
            }
        }
    }
}
