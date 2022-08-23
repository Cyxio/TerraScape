using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class WaveExplode : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WaveExplode");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 112;
            Projectile.height = 112;
            Projectile.damage = 100;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.light = 0.2f;
            Projectile.penetrate = -1;
            Projectile.alpha = 30;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 19;
        }

        public override void AI()
        {
            Projectile.velocity *= 0f;
            Projectile.alpha += 12;
            Projectile.frameCounter++;
            if (Projectile.frameCounter == 3)
            {
                Projectile.frame++;
                if(Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
                Projectile.frameCounter = 0;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            switch (Projectile.ai[0])
            {
                case 0:
                    lightColor = Color.White;
                    break;
                case 1:
                    lightColor = Color.ForestGreen;
                    break;
                case 2:
                    lightColor = Color.OrangeRed;
                    break;
                case 3:
                    lightColor = Color.MediumBlue;
                    break;
                default:
                    break;
            }
            return base.PreDraw(ref lightColor);
        }
    }
}
