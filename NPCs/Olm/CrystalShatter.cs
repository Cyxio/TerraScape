using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Olm
{
    public class CrystalShatter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Crystal");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 100;
        }
        public override void AI()
        {
            if (projectile.alpha == 0)
            {
                Main.PlaySound(SoundID.Item8, projectile.position);
                projectile.alpha = 20;
            }
            projectile.rotation += 0.1f;
            Lighting.AddLight(projectile.Center, new Vector3(0, 0.8f, 0));
            projectile.velocity *= 0.97f;
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.ActivePlayersCount; i++)
            {
                Vector2 spd = Main.player[i].MountedCenter - projectile.Center;
                spd.Normalize();
                Projectile.NewProjectile(projectile.Center, spd * 11, mod.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
            }
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(11).RotateRandom(Math.PI), mod.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
            }
        }
    }
}
