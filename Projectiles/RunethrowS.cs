using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class RunethrowS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RunethrowS");
        }
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 26;
            projectile.height = 30;
            projectile.penetrate = 5;
            projectile.timeLeft = 1200;
            projectile.tileCollide = true;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.localAI[0] = 0;
        }
        public override void AI()
        {
            projectile.damage = 40;
            projectile.rotation += 0.35f * projectile.direction;
            Lighting.AddLight(projectile.position, new Vector3(20 * 0.005f, 143 * 0.005f, 255 * 0.005f));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC targ = Main.npc[i];
                    if (!targ.friendly && targ.type != NPCID.TargetDummy && targ.Distance(projectile.Center) < 500f && targ != target)
                    {
                        if (projectile.direction == 1 && targ.position.X > projectile.position.X + 20 || projectile.direction == -1 && targ.position.X < projectile.position.X - 20)
                        {
                            Vector2 spd = targ.Center - projectile.Center;
                            spd.Normalize();
                            projectile.velocity = spd * projectile.velocity.Length();
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 9; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Runedust"), Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), 150, new Color(20, 143, 255));
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
        }
    }
}
