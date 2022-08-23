using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.timeLeft = 64;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 30;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (Projectile.Size / 2f) + Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.timeLeft > 10)
            {
                player.velocity = Projectile.velocity * (Projectile.timeLeft / 30f);
            }         
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TerraBlade);
            Lighting.AddLight(Projectile.Center, new Vector3(0.75f, 0.85f, 0.5f));
            if (Projectile.direction == -1)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.Pi;
            }
            Projectile.spriteDirection = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;
            player.ChangeDir(Projectile.direction);
            if (Projectile.ai[0] == 1 && player.velocity.Y < 0.1f && Projectile.timeLeft > 2)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(new Vector2(player.Center.X + i * 3, player.position.Y + player.height), 0, 0, DustID.PoisonStaff, i / 10, -4);
                    Dust.NewDust(new Vector2(player.Center.X - i * 3, player.position.Y + player.height), 0, 0, DustID.PoisonStaff, -i / 10, -4);
                }
                Projectile.timeLeft = 2;
                SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 720);
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
                target.velocity = Main.player[Projectile.owner].velocity;
                target.netUpdate = true;
            }    
            Main.player[Projectile.owner].velocity *= -0.3f;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.velocity += Main.player[Projectile.owner].velocity;
            Main.player[Projectile.owner].velocity *= -0.3f;
        }
    }
}