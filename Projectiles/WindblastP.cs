using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class WindblastP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Windblast");
            Main.projFrames[projectile.type] = 4;
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
        Vector3 x = new Vector3(150, 150, 150);
        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("WindboltD"), 0, 0, 150);
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
            int dust = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0);
            int dust1 = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0);
            int dust2 = Dust.NewDust(projectile.Center + v, 0, 0, mod.DustType("WindblastD"), 0, 0);
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
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windbolt"), perturbedSpeed.X, perturbedSpeed.Y, 100, default(Color), 2f);
            }
            Lighting.AddLight(projectile.position, x * 0.005f);
        }

    }
}