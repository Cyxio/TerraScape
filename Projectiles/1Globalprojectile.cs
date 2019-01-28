using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class _1Globalprojectile : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<OSRSplayer>().TomeFire && projectile.magic)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 27);
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 75);
                }
            }
        }
    }
}
