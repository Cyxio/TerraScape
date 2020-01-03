using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Projectiles
{
    public class Ibanblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iban blast");
            Main.projFrames[projectile.type] = 3;
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
            projectile.aiStyle = -1;
            projectile.scale = 0.8f;
            projectile.light = 0.2f;
        }
        Vector3 x = new Vector3(255, 75, 75);
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            float distance = 500f;
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
            Lighting.AddLight(projectile.position + projectile.velocity, x * 0.005f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 90, projectile.velocity.X * -0.1f);
                Main.dust[dust].noGravity = true;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frame++;
                if(projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
                projectile.frameCounter = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Vector2 velo = new Vector2(0, 10).RotatedBy(MathHelper.ToRadians(45 * i));
                int dust = Dust.NewDust(projectile.Center, 0, 0, 90, velo.X, velo.Y);
                int dust1 = Dust.NewDust(projectile.Center, 0, 0, 90, velo.X * 0.8f, velo.Y * 0.8f);
                int dust2 = Dust.NewDust(projectile.Center, 0, 0, 90, velo.X * 0.6f, velo.Y * 0.6f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust1].noGravity = true;
                Main.dust[dust2].noGravity = true;
            }
            Lighting.AddLight(projectile.position, x * 0.005f);
        }
    }
}