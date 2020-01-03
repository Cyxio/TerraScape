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
            if (Main.player[projectile.owner].GetModPlayer<OSRSplayer>().TomeFire && projectile.magic && projectile.friendly)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int t = Main.rand.Next(4);
                    switch (t)
                    {
                        case 0: Dust.NewDust(projectile.position, projectile.width, projectile.height, 6); break;
                        case 1: Dust.NewDust(projectile.position, projectile.width, projectile.height, 27); break;
                        case 2: Dust.NewDust(projectile.position, projectile.width, projectile.height, 135); break;
                        case 3: Dust.NewDust(projectile.position, projectile.width, projectile.height, 75); break;
                        default:
                            break;
                    }    
                }
            }
        }
    }
}
