using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Tormentedsoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tormented Soul");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(356);
            aiType = 356;
        }

        public override void AI()
        {
            //projectile.tileCollide = false;
            Dust dust = Dust.NewDustPerfect(projectile.position, 87);
            dust.scale = 1.5f;
            dust.velocity *= 0f;
            dust.noGravity = true;
        }
    }
}