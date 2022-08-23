using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class D2Hspec2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("D2Hspec2");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Dust.NewDust(Projectile.position, 48, 36, DustID.Pixie, 0, 0, 0, new Color(255, 0, 0), 0.5f);
            Lighting.AddLight(Projectile.position, new Vector3(1, 0, 0));
            if (Projectile.timeLeft < 50)
            {
                Projectile.alpha += 5;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 12)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
        }
    }
}