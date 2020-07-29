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
            Main.projFrames[projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 40;
            projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f; 
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            if (player.GetModPlayer<OSRSplayer>().Clawbuff == true)
            {
                projectile.localNPCHitCooldown = 1;
                player.velocity = projectile.velocity * 0.7f;
                player.armorPenetration = 100;
            }

            num = 0f;
            if (projectile.spriteDirection == -1) 
            {
                num = 3.14159274f;
            }
            if (++projectile.frame >= Main.projFrames[projectile.type]) 
            {                                                                             
                projectile.frame = 0;
            }
            projectile.soundDelay--; 
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
                projectile.soundDelay = 10; 
            }                                    
            if (Main.myPlayer == projectile.owner) 
            {
                if (player.channel && !player.noItems && !player.CCed) 
                {                                                                                          
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type) 
                    {                                                                                                 
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale; 
                    }
                    Vector2 vector13 = Main.MouseWorld - vector; 
                    vector13.Normalize(); 
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction; 
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true; 
                    }
                    projectile.velocity = vector13; 
                }
                else
                {
                    projectile.Kill();
                }
            }
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
            Lighting.AddLight(vector14, 1f, 0, 0);
            if (Main.rand.Next(3) == 0) 
            {      
                int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, mod.DustType("Windstrike"), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, new Color(255, 0, 0), 1f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= projectile.velocity;
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (projectile.Size / 2f); 
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = -projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}
