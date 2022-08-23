using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Chaoselemental
{
    public class Chaostele : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic teleport");
            Main.projFrames[Projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            Projectile.hostile = true;
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Projectile.damage = 0;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(255 * 0.005f, 0, 0));
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TeleportationPotion);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
            Player target = Main.player[0];
            for (int k = 0; k < Main.CurrentFrameFlags.ActivePlayersCount; k++)
            {
                if (Projectile.Distance(Main.player[k].position) < Projectile.Distance(target.position))
                {
                    target = Main.player[k];
                }
            }
            if (Projectile.ai[0] == 0)
            {
                float speedX = target.MountedCenter.X - Projectile.Center.X;
                float speedY = target.MountedCenter.Y - Projectile.Center.Y;
                Vector2 spd = new Vector2(speedX, speedY);
                spd.Normalize();
                Projectile.velocity = spd * 15f;
                if (Projectile.Distance(target.MountedCenter) < 100)
                {
                    Projectile.ai[0] = 1;
                }
            }
            if (Projectile.Colliding(Projectile.Hitbox, target.Hitbox))
            {
                target.Teleport(new Vector2(target.position.X + Main.rand.Next(-700, 700), target.position.Y + Main.rand.Next(-500, 100)), 1, 0);
                Projectile.active = false;
            }
        }
    }
}