using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Projectiles
{
    public class Boltrack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's bolt");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 7;
            projectile.height = 7;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ai[0] = 0;
        }

        public override void AI()
        {
            while (projectile.velocity.X >= 16f || projectile.velocity.X <= -16f || projectile.velocity.Y >= 16f || projectile.velocity.Y < -16f)
            {
                projectile.velocity *= 0.97f;
            }
            projectile.velocity.Y += 0.025f;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (projectile.ai[0] == 1)
            {
                Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
                float distance = 400f;
                int speed = 2;
                if (projectile.timeLeft % speed == 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.Distance(projectile.Center) < distance && target.active)
                        {
                            if (!target.friendly && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
                            {
                                Vector2 toTarget = new Vector2(target.position.X - projectile.position.X, target.position.Y - projectile.position.Y);
                                toTarget.Normalize();
                                toTarget *= projectile.velocity.Length();
                                float maxSpeed = projectile.velocity.Length();
                                projectile.velocity = new Vector2((projectile.velocity.X * 2 + toTarget.X) / 3, (projectile.velocity.Y * 2 + toTarget.Y) / 3);
                                while (projectile.velocity.Length() < maxSpeed)
                                {
                                    projectile.velocity *= 1.01f;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] == 1)
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/BoltrackH");
            }
            else
            {
                Main.projectileTexture[projectile.type] = mod.GetTexture("Projectiles/Boltrack");
            }
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }
    }
}
