using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.aiStyle = -1;
            Projectile.width = 26;
            Projectile.height = 30;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.friendly = true;
            Projectile.localAI[0] = 0;
        }
        public override void AI()
        {
            Projectile.damage = 40;
            Projectile.rotation += 0.35f * Projectile.direction;
            Lighting.AddLight(Projectile.position, new Vector3(20 * 0.005f, 143 * 0.005f, 255 * 0.005f));
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC targ = Main.npc[i];
                    if (!targ.friendly && targ.type != NPCID.TargetDummy && targ.Distance(Projectile.Center) < 500f && targ != target)
                    {
                        if (Projectile.direction == 1 && targ.position.X > Projectile.position.X + 20 || Projectile.direction == -1 && targ.position.X < Projectile.position.X - 20)
                        {
                            Vector2 spd = targ.Center - Projectile.Center;
                            spd.Normalize();
                            Projectile.velocity = spd * Projectile.velocity.Length();
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 9; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("Runedust").Type, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), 150, new Color(20, 143, 255));
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
