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
            Main.projFrames[Projectile.type] = 11;
            Main.projPet[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Puppy);
            Projectile.width = 38;
            Projectile.height = 42;
            AIType = ProjectileID.Puppy;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.puppy = false;
            return true;
        }
        public override void AI()
        {
            DrawOffsetX = -19;
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }
            if (player.dead)
            {
                player.GetModPlayer<OSRSplayer>().BloodHound = false;
            }
            if (player.GetModPlayer<OSRSplayer>().BloodHound)
            {
                Projectile.timeLeft = 2;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
