using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class BasicCrystal : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Crystal Shard");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 180;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item27, projectile.position);
                projectile.alpha = 85;
            }
            projectile.rotation = projectile.velocity.ToRotation() + (float)(Math.PI / 2);
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 107);
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0f;
        }
    }
}
