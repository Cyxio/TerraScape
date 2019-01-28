using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    public class Bloodhound : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodhound");
            Main.projFrames[projectile.type] = 11;
            Main.projPet[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Puppy);
            projectile.width = 38;
            projectile.height = 42;
            aiType = ProjectileID.Puppy;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.puppy = false;
            return true;
        }
        public override void AI()
        {
            drawOffsetX = -19;
            Player player = Main.player[projectile.owner];
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            if (player.dead)
            {
                player.GetModPlayer<OSRSplayer>().BloodHound = false;
            }
            if (player.GetModPlayer<OSRSplayer>().BloodHound)
            {
                projectile.timeLeft = 2;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
