using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ibanblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iban blast");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.scale = 0.8f;
            Projectile.light = 0.2f;
        }
        Vector3 x = new Vector3(255, 75, 75);
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            float distance = 500f;
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
            Lighting.AddLight(Projectile.position + Projectile.velocity, x * 0.005f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemRuby, Projectile.velocity.X * -0.1f);
                Main.dust[dust].noGravity = true;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frame++;
                if(Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
                Projectile.frameCounter = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Vector2 velo = new Vector2(0, 10).RotatedBy(MathHelper.ToRadians(45 * i));
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemRuby, velo.X, velo.Y);
                int dust1 = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemRuby, velo.X * 0.8f, velo.Y * 0.8f);
                int dust2 = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemRuby, velo.X * 0.6f, velo.Y * 0.6f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust1].noGravity = true;
                Main.dust[dust2].noGravity = true;
            }
            Lighting.AddLight(Projectile.position, x * 0.005f);
        }
    }
}