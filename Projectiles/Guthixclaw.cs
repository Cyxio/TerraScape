using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Guthixclaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthixclaw");
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 78;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
        }
        int i = 0;
        public override void AI()
        {
            Dust.NewDust(Projectile.position, 48, 78, DustID.ShadowbeamStaff, 0f, 0f);
            Lighting.AddLight(Projectile.position, new Vector3(0, 0, 0));
            i++;
            if (i == 3)
            {
                Projectile.frame++;
                i = 0;
                if (Projectile.frame > Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.frame > 6)
            {
                Projectile.alpha += 25;
            }
        }
    }
}