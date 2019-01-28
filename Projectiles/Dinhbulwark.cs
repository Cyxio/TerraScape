using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Projectiles
{
    public class Dinhbulwark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dinh's Bulwark");
        }
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 54;
            projectile.timeLeft = 64;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.ownerHitCheck = true;
            projectile.timeLeft = 30;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (projectile.Size / 2f) + projectile.velocity;
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.timeLeft > 10)
            {
                player.velocity = projectile.velocity * (projectile.timeLeft / 30f);
            }         
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 107);
            Lighting.AddLight(projectile.Center, new Vector3(0.75f, 0.85f, 0.5f));
            if (projectile.direction == -1)
            {
                projectile.rotation = projectile.velocity.ToRotation() - MathHelper.Pi;
            }
            projectile.spriteDirection = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.ChangeDir(projectile.direction);
            if (projectile.ai[0] == 1 && player.velocity.Y < 0.1f && projectile.timeLeft > 2)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(new Vector2(player.Center.X + i * 3, player.position.Y + player.height), 0, 0, 163, i / 10, -4);
                    Dust.NewDust(new Vector2(player.Center.X - i * 3, player.position.Y + player.height), 0, 0, 163, -i / 10, -4);
                }
                projectile.timeLeft = 2;
                Main.PlaySound(SoundID.Item70, projectile.position);
                player.AddBuff(mod.BuffType("SpecCD"), 720);
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (target.WithinRange(player.Center, 500f) && target.type != NPCID.TargetDummy)
                    {
                        Vector2 spd = target.position - player.Center;
                        spd.Normalize();
                        target.velocity = spd * 13f;
                        target.netUpdate = true;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.TargetDummy)
            {
                target.velocity = Main.player[projectile.owner].velocity;
                target.netUpdate = true;
            }    
            Main.player[projectile.owner].velocity *= -0.3f;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.velocity += Main.player[projectile.owner].velocity;
            Main.player[projectile.owner].velocity *= -0.3f;
        }
    }
}