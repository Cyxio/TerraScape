﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class WaterstrikeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waterstrike");
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.timeLeft = 1200;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            projectile.damage = 5;
            projectile.scale = 0.8f;
            projectile.light = 0.2f;
        }
        Color newColor = new Color(78, 71, 239);
        Vector3 x = new Vector3(78, 71, 239);
        public override void AI()
        {
            Lighting.AddLight(projectile.position, x * 0.005f);
            projectile.velocity.Y = projectile.oldVelocity.Y;
            if (Main.rand.Next(1) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Windstrike"), projectile.velocity.X * -0.2f, 0, 0, newColor);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Strike"), projectile.position);
            Dust.NewDust(projectile.Center, 1, 1, mod.DustType("WindstrikeD"), 0, 0, 0, newColor);
            Lighting.AddLight(projectile.position, x * 0.005f);
        }

    }
}