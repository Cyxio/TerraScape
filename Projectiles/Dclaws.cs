using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace OldSchoolRuneScape.Projectiles
{
    public class Dclaws : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dclaws");
            Main.projFrames[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 40;
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f; 
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            if (player.GetModPlayer<OSRSplayer>().Clawbuff == true)
            {
                Projectile.localNPCHitCooldown = 1;
                player.velocity = Projectile.velocity * 0.7f;
                player.GetArmorPenetration(DamageClass.Generic) = 100;
            }

            num = 0f;
            if (Projectile.spriteDirection == -1) 
            {
                num = 3.14159274f;
            }
            if (++Projectile.frame >= Main.projFrames[Projectile.type]) 
            {                                                                             
                Projectile.frame = 0;
            }
            Projectile.soundDelay--; 
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                Projectile.soundDelay = 10; 
            }                                    
            if (Main.myPlayer == Projectile.owner) 
            {
                if (player.channel && !player.noItems && !player.CCed) 
                {                                                                                          
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type) 
                    {                                                                                                 
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale; 
                    }
                    Vector2 vector13 = Main.MouseWorld - vector; 
                    vector13.Normalize(); 
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction; 
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true; 
                    }
                    Projectile.velocity = vector13; 
                }
                else
                {
                    Projectile.Kill();
                }
            }
            Vector2 vector14 = Projectile.Center + Projectile.velocity * 3f;
            Lighting.AddLight(vector14, 1f, 0, 0);
            if (Main.rand.NextBool(3)) 
            {      
                int num30 = Dust.NewDust(vector14 - Projectile.Size / 2f, Projectile.width, Projectile.height, Mod.Find<ModDust>("Windstrike").Type, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, new Color(255, 0, 0), 1f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= Projectile.velocity;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (Projectile.Size / 2f); 
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = -Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
        }
    }
}
