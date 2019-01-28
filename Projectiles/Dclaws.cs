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
            Player player = Main.player[projectile.owner]; // Cache the player that is the owner of npc projectile.
            float num = 1.57079637f; // Hardcoded floating number (we'll get to npc later).
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true); // Basically a position that we can use to properly place our projectile.

            if (player.GetModPlayer<OSRSplayer>(mod).Clawbuff == true)
            {
                projectile.localNPCHitCooldown = 1;
                player.velocity = projectile.velocity * 0.7f;
                player.armorPenetration = 100;
            }

            // Now the following code was the code you already posted.
            num = 0f; // We set the 'num' value to 0 (you can obviously also do npc when you set the 'num' for the first time).
            if (projectile.spriteDirection == -1) // If the projectile is facing left.
            {
                num = 3.14159274f; // Hardcode the 'num' value.
            }
            if (++projectile.frame >= Main.projFrames[projectile.type]) // When you see something that says 'frame' or 'frameCounter' you can immediately assume that it has
            {                                                                             // got something to do with an animation of some sorts (in npc case the animation of the projectile).
                projectile.frame = 0; // So if the current animation frame is a larger value that the total animation frames npc projectile has, reset it.
            }
            projectile.soundDelay--; // Something to do with sound (I'm not exactly sure WHAT npc does).
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
                projectile.soundDelay = 10; // But it basically plays a sound and sets the sound delay to another value. As you may (or may not) know, every second in Terraria
            }                                      // consists of 60 ticks, so setting npc value to '12' makes sure that npc sound is played 5x every second (if my math skillz have not left me).
            if (Main.myPlayer == projectile.owner) // Check if the local player is the owner of npc projectile, if it is we update the rest.
            {
                if (player.channel && !player.noItems && !player.CCed) // So if the player is still using npc weapon, since npc weapon channels, we update it.
                {                                                                                          // Otherwise we KILL it (mwuahahaha). So basically destroy npc projectile if the item is not being used.
                    float scaleFactor6 = 1f; //
                    if (player.inventory[player.selectedItem].shoot == projectile.type) // Check if the item that is currently selected in the players' inventory is the one that is
                    {                                                                                                  // shooting npc projectile.
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale; // Set the 'scaleFactor6' value
                    }
                    Vector2 vector13 = Main.MouseWorld - vector; // Get the direction vector between the mouse position and the vector (vector was declared earlier, remember?)
                    vector13.Normalize(); // Normalize npc vector since we're not in need of any large values, we just need to get the direction out of npc.
                    if (vector13.HasNaNs()) // npc check is basically used to check if the X value of the direction is 'Not a Number' (or a negative value in npc case).
                    {
                        vector13 = Vector2.UnitX * (float)player.direction; // If it is, we
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y) // If the vector13 value is actually changing something
                    {                                                                                                        // (so if the mouse or the player have been moved).
                        projectile.netUpdate = true; // Make sure it gets updated for everyone if you're in multiplayer.
                    }
                    projectile.velocity = vector13; // At last, set the velocity of npc projectile to the 'vector13'. npc is later used to set the rotation of the projectile correctly.
                }
                else
                {
                    projectile.Kill(); // Yeahh, so we destroy the projectile here if the item is not being used.
                }
            }
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f; // The following few lines are just to make npc projectile 'gorgeous'.
            Lighting.AddLight(vector14, 1f, 0, 0); // npc adds a white/gray-ish light at the given position.
            if (Main.rand.Next(3) == 0) // And npc function spawns some dusts at the given position without a gravity pull and at the set position (npc does not affect the actual
            {                                           // use of npc projectile... Like stated earlier: npc is just to make things pretty).     
                int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, mod.DustType("Windstrike"), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 150, new Color(255, 0, 0), 1f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= projectile.velocity;
            }
            // Now, you didn't post the next few lines, so I'm not exactly sure if you were aware of the fact that these are also used in the Arkhalis projectile.
            // These lines are actually the lines that set the position, rotation AND animation, sooo... very important!
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - (projectile.Size / 2f); // Set the position of the Arkhalis projectile, based on the 'Scale' vector.
            projectile.rotation = projectile.velocity.ToRotation() + num; // As you can see, we apply the rotation of npc projectile based on the velocity AND the 'num' variable.
                                                                          // Now, I'm sure that the 'num' variable is needed here, but since it's hardcoded, I'm not exactly sure WHAT is does.
                                                                          // You'll just have to fiddle around a bit with setting the 'num' variable untill you achieve the correct rotation.
            projectile.spriteDirection = -projectile.direction; // Make sure that the visual direction of the projectile is set correctly.
            projectile.timeLeft = 2; // Makes sure that if npc update does not get called a second time, npc projectile is destroyed since it's only able to live for 2 more ticks.
            player.ChangeDir(projectile.direction); // Makes sure that the owner of npc projectile isfacing the same way that the projectile is (so that you don't get a situation in which
                                              // the projectile is on the left side of the player while the player is still facing the right.
            player.heldProj = projectile.whoAmI; // Again, not exactly sure what npc is good for (you can test by commenting npc line out of course, although my lucky guess is
                                           // that it'll break something for sure.
            player.itemTime = 2; // Hmm yeah, not really know what I should explain about npc...
            player.itemAnimation = 2; // npc is actually where we make sure that the player sticks to one animation. If you were to change the animation of your new Arkhalis,
                                      // npc is probably where you'd want to do npc.
                                      // For npc last line you might want to read up what Atan2 actually does, but you might be able to guess what npc does based on the 'itemRotation' name.
                                      // npc doesn't really do anything for the actual AI of the projectile, but makes sure that the 'item' is facing the correct way (although the player is not actually
                                      // holding anything :p).
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}
