using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class WindstrikeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windstrike");
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
            projectile.damage = 5;
            projectile.scale = 0.8f;
            projectile.light = 0.2f;
        }
        Vector3 x = new Vector3(150, 150, 150);
        public override void AI()
        {
            Lighting.AddLight(projectile.position, x * 0.005f);
            projectile.velocity.Y = projectile.oldVelocity.Y;
            if (Main.rand.Next(1) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windstrike"), projectile.velocity.X * -0.2f, 0);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Strike"), projectile.position);
            Dust.NewDust(projectile.Center, 1, 1, mod.DustType("WindstrikeD"), 0, 0);
            Lighting.AddLight(projectile.position, x * 0.005f);
        }

    }
}