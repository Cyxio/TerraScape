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
    public class CrystalShatter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olm's Crystal");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100;
        }
        public override void AI()
        {
            if (Projectile.alpha == 0)
            {
                SoundEngine.PlaySound(SoundID.Item8, Projectile.position);
                Projectile.alpha = 20;
            }
            Projectile.rotation += 0.1f;
            Lighting.AddLight(Projectile.Center, new Vector3(0, 0.8f, 0));
            Projectile.velocity *= 0.97f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.CurrentFrameFlags.ActivePlayersCount; i++)
            {
                Vector2 spd = Main.player[i].MountedCenter - Projectile.Center;
                spd.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, spd * 11, ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
            }
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(11).RotateRandom(Math.PI), ModContent.ProjectileType<BasicCrystal>(), 200 / 4, 0f);
            }
        }
    }
}
