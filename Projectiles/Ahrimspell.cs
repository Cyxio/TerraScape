using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ahrimspell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's spell");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.scale = 1f;
            Projectile.penetrate = 3;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 1200;
            Projectile.alpha = 0;
            Projectile.ai[0] = 0;
            Projectile.ai[1] = 0;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
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
            if (Projectile.ai[0] == 1)
            {
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink);
                Main.dust[dust].noGravity = true;
                float distance = 400f;
                int speed = 3;
                if (Projectile.timeLeft % speed == 0)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.Distance(Projectile.Center) < distance && target.active)
                        {
                            if (!target.friendly && target.lifeMax > 5 && target.type != NPCID.TargetDummy)
                            {
                                Vector2 toTarget = new Vector2(target.position.X - Projectile.position.X, target.position.Y - Projectile.position.Y);
                                toTarget.Normalize();
                                toTarget *= Projectile.velocity.Length();
                                float maxSpeed = Projectile.velocity.Length();
                                Projectile.velocity = new Vector2((Projectile.velocity.X * 2 + toTarget.X) / 3, (Projectile.velocity.Y * 2 + toTarget.Y) / 3);
                                while (Projectile.velocity.Length() < maxSpeed)
                                {
                                    Projectile.velocity *= 1.01f;
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
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Pink);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.ai[0] == 1)
            {
                return Color.BlueViolet;
            }
            return base.GetAlpha(lightColor);
        }
    }
}