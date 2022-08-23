using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
                Projectile.alpha = 85;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)(Math.PI / 2);
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade);
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0f;
        }
    }
}
