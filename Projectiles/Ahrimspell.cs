using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ahrimspell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's spell");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.scale = 1f;
            projectile.penetrate = 3;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 1200;
            projectile.alpha = 0;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center + projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
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
            if (projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(projectile.Center, 0, 0, 58);
                Main.dust[dust].noGravity = true;
                float distance = 400f;
                int speed = 3;
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
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(projectile.Center, 0, 0, 58);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.ai[0] == 1)
            {
                return Color.BlueViolet;
            }
            return base.GetAlpha(lightColor);
        }
    }
}