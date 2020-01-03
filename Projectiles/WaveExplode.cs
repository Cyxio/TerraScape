using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class WaveExplode : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WaveExplode");
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 112;
            projectile.height = 112;
            projectile.damage = 100;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.light = 0.2f;
            projectile.penetrate = -1;
            projectile.alpha = 30;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 19;
        }

        public override void AI()
        {
            projectile.velocity *= 0f;
            projectile.alpha += 12;
            projectile.frameCounter++;
            if (projectile.frameCounter == 3)
            {
                projectile.frame++;
                if(projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.Kill();
                }
                projectile.frameCounter = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] == 1)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/WaveExplode1");
            }
            if (projectile.ai[0] == 2)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/WaveExplode2");
            }
            if (projectile.ai[0] == 3)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/WaveExplode3");
            }
            if (projectile.ai[0] == 4)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/WaveExplode");
            }
            return true;
        }
    }
}
