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
            if (Main.player[projectile.owner].GetModPlayer<OSRSplayer>().TomeFire && projectile.CountsAsClass(DamageClass.Magic) && projectile.friendly)
            {
                if (Main.rand.NextBool(3))
                {
                    int t = Main.rand.Next(4);
                    switch (t)
                    {
                        case 0: Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Torch); break;
                        case 1: Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame); break;
                        case 2: Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.IceTorch); break;
                        case 3: Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.CursedTorch); break;
                        default:
                            break;
                    }    
                }
            }
        }
    }
}
