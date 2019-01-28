using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.width = 18;
            projectile.height = 18;
            projectile.timeLeft = 1200;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.light = 0.2f;
        }
        Color c = new Color(25, 145, 22);
        Vector3 x = new Vector3(25, 145, 22);
        public override void AI()
        {
            Lighting.AddLight(projectile.position, x * 0.005f);
            projectile.velocity.Y = projectile.oldVelocity.Y;
            if (Main.rand.Next(1) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windbolt"), projectile.velocity.X * -0.2f, 0, 0, c);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Bolt"), projectile.position);
            Dust.NewDust(projectile.Center, 1, 1, mod.DustType("WindstrikeD"), 0, 0, 0, c);
            for (int i = 0; i < 9; ++i)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(360));
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("WindboltD"), perturbedSpeed.X, perturbedSpeed.Y, 0, c);
            }
            Lighting.AddLight(projectile.position, x * 0.005f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(9) == 0)
            {
                target.AddBuff(BuffID.Slow, 150);
            }
        }

    }
}