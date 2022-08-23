using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Projectile.aiStyle = -1;
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ai[0] = 0;
        }

        public override void AI()
        {
            while (Projectile.velocity.X >= 16f || Projectile.velocity.X <= -16f || Projectile.velocity.Y >= 16f || Projectile.velocity.Y < -16f)
            {
                Projectile.velocity *= 0.97f;
            }
            Projectile.velocity.Y += 0.025f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.ai[0] == 1)
            {
                Lighting.AddLight(Projectile.Center + Projectile.velocity, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
                float distance = 400f;
                int speed = 2;
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

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.Purple;
            lightColor.A = 150;
            return true;
        }
    }
}
