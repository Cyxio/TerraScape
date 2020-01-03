using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Guthan
{
    public class Guthanbag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 5;
            item.expert = true;
        }

        public override int BossBagNPC => mod.NPCType("Guthan");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            int helm = ModContent.ItemType<Items.Armor.Guthanhelm>();
            int body = ModContent.ItemType<Items.Armor.Guthanbody>();
            int legs = ModContent.ItemType<Items.Armor.Guthanlegs>();
            int wep = ModContent.ItemType<Items.Guthanspear>();
            int drop = 0;
            for (int i = 0; i < 200; i++)
            {
                bool allcheck = false;
                if (player.HasItem(helm) && player.HasItem(body) && player.HasItem(legs) && player.HasItem(wep))
                {
                    allcheck = true;
                }
                int ch = Main.rand.Next(4);
                if (ch == 0 && (allcheck || !player.HasItem(helm)))
                {
                    drop = helm;
                    break;
                }
                if (ch == 1 && (allcheck || !player.HasItem(body)))
                {
                    drop = body;
                    break;
                }
                if (ch == 2 && (allcheck || !player.HasItem(legs)))
                {
                    drop = legs;
                    break;
                }
                if (ch == 3 && (allcheck || !player.HasItem(wep)))
                {
                    drop = wep;
                    break;
                }
            }
            player.QuickSpawnItem(drop);
        }
    }
    [AutoloadBossHead]
    public class Guthan : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan the Infested");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 45;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.npcSlots = 15f;
            npc.lavaImmune = true;
            npc.damage = 80;
            npc.defense = 50;
            npc.lifeMax = 25000;
            npc.scale = 1.5f;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = 35000f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            music = OldSchoolRuneScape.barrowsMusic;
            bossBag = mod.ItemType("Guthanbag");
        }

        public override void OnHitPlayer(Player target, int damage, bool crit) // heal bby
        {
            npc.HealEffect(damage * 10);
            npc.life += damage * 10;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 35000 + 2500 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.4f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Guthan the Infested";
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            OSRSworld.downedGuthan = true;
            Item.NewItem(npc.Hitbox, ModContent.ItemType<Guthansummon>());
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int drop = 0;
                switch (Main.rand.Next(4))
                {
                    case 0:
                        drop = ModContent.ItemType<Items.Guthanspear>();
                        break;
                    case 1:
                        drop = ModContent.ItemType<Items.Armor.Guthanhelm>();
                        break;
                    case 2:
                        drop = ModContent.ItemType<Items.Armor.Guthanbody>();
                        break;
                    case 3:
                        drop = ModContent.ItemType<Items.Armor.Guthanlegs>();
                        break;
                    default:
                        break;
                }
                Item.NewItem(npc.Hitbox, drop);
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ranged)
            {
                damage = (int)(damage * 0.9f);
            }
            if (projectile.magic)
            {
                damage = (int)(damage * 1.1f);
            }
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int Fly = 0;
        const int Attack = 1;
        const int Flee = 2;

        public float AI_State
        {
            get { return npc.ai[AI_State_Slot]; }
            set { npc.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return npc.ai[AI_Timer_Slot]; }
            set { npc.ai[AI_Timer_Slot] = value; }
        }

        public int attacknum
        {
            get { return (int)npc.ai[2]; }
            set { npc.ai[2] = value; }
        }

        public float speed = 7f;
        public float accelerate = 0.2f;
        public int healthmod = 0;
        public Vector2 spd = Vector2.Zero;

        public override void AI()
        {
            if (Main.dayTime)
            {
                AI_State = Flee;
            }
            if (Main.rand.Next(2) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 52);
            }
            Lighting.AddLight(npc.Center, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (npc.life < npc.lifeMax * 0.75f)
            {
                healthmod = 1;
            }
            if (npc.life < npc.lifeMax * 0.5f)
            {
                healthmod = 2;
            }
            if (npc.life < npc.lifeMax * 0.2f)
            {
                healthmod = 3;
            }
            if (AI_State == Fly)
            {
                while (npc.velocity.X > speed || npc.velocity.Y > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity *= 0.97f;
                }
                AI_Timer++;
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                if (target.MountedCenter.Y > npc.Center.Y && npc.velocity.Y < speed)
                {
                    npc.velocity.Y += accelerate;
                }
                if (target.MountedCenter.Y < npc.Center.Y && npc.velocity.Y > -speed)
                {
                    npc.velocity.Y -= accelerate;
                }
                if (target.MountedCenter.X > npc.Center.X && npc.velocity.X < speed)
                {
                    npc.velocity.X += accelerate;
                }
                if (target.MountedCenter.X < npc.Center.X && npc.velocity.X > -speed)
                {
                    npc.velocity.X -= accelerate;
                }
                if (AI_Timer > (180 - 40 * healthmod) && Main.netMode != 1)
                {
                    AI_Timer = 0;
                    int meme = Main.rand.Next(1, 3 + healthmod);
                    if (meme == 1)
                    {
                        npc.velocity = new Vector2(12 * npc.direction, 8 * Main.rand.Next(-1, 2));
                        AI_State = Attack;
                        attacknum = meme;
                        npc.netUpdate = true;
                    }
                    else if (meme == 2)
                    {
                        AI_State = Attack;
                        attacknum = meme;
                        npc.netUpdate = true;
                    }
                    else if (meme == 3)
                    {
                        npc.velocity *= 0f;
                        AI_State = Attack;
                        attacknum = meme;
                        npc.netUpdate = true;
                    }
                    else if (meme == 4)
                    {
                        npc.velocity.X = npc.direction * 20;
                        AI_State = Attack;
                        attacknum = meme;
                        npc.netUpdate = true;
                    }
                    else if (meme == 5)
                    {
                        AI_State = Attack;
                        attacknum = meme;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        AI_State = Fly;
                    }
                }
                if (!npc.HasValidTarget)
                {
                    npc.velocity *= 0;
                    AI_State = Flee;
                }
            }
            if (AI_State == Attack)
            {
                AI_Timer++;
                if (attacknum == 1)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 58, npc.velocity.X * 0.01f, npc.velocity.Y * 0.01f);
                    if (AI_Timer == 1)
                    {
                        npc.TargetClosest(true);
                        Player target = Main.player[npc.target];
                        float speedX = target.MountedCenter.X - npc.Center.X;
                        float speedY = target.MountedCenter.Y - npc.Center.Y;
                        spd = new Vector2(speedX, speedY);
                        spd.Normalize();
                        spd *= 14;
                    }
                    if (AI_Timer % 8 == 0)
                    {
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, spd.RotatedBy(MathHelper.ToRadians(AI_Timer)), mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f);
                        }
                    }
                    if (AI_Timer > 40)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 2)
                {
                    npc.TargetClosest(true);
                    Player target = Main.player[npc.target];
                    if (AI_Timer == 3)
                    {
                        Projectile.NewProjectile(new Vector2(target.MountedCenter.X + 960, target.MountedCenter.Y), new Vector2(-20, 0), mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 2, 0);
                        Projectile.NewProjectile(new Vector2(target.MountedCenter.X - 960, target.MountedCenter.Y), new Vector2(20, 0), mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 2, 0);
                    }
                    if (AI_Timer > 5)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 3)
                {
                    if (AI_Timer == 2)
                    {
                        npc.TargetClosest(true);
                        Player target = Main.player[npc.target];
                        spd = new Vector2(target.MountedCenter.X + 960, target.MountedCenter.Y - 590);
                    }
                    if (AI_Timer % 6 == 0)
                    {
                        Projectile.NewProjectile(new Vector2(spd.X - AI_Timer * 10, spd.Y), new Vector2(0, 16), mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 1, 0);
                        Projectile.NewProjectile(new Vector2((spd.X - 1920) + AI_Timer * 10, spd.Y), new Vector2(0, 16), mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 1, 0);             
                    }
                    if (AI_Timer > 90)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 4)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 58, npc.velocity.X * 0.01f, npc.velocity.Y * 0.01f);
                    if (AI_Timer % 8 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 3, 0);
                    }
                    if (AI_Timer > 40)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
                if (attacknum == 5)
                {
                    if (AI_Timer == 2)
                    {
                        npc.TargetClosest(true);
                        Player target = Main.player[npc.target];
                        spd = target.MountedCenter;
                    }
                    if (AI_Timer > 2)
                    {
                        Vector2 rotat = new Vector2(0, 350);
                        Projectile.NewProjectile(spd + rotat.RotatedBy(MathHelper.ToRadians(AI_Timer * 10)), -rotat.RotatedBy(MathHelper.ToRadians(AI_Timer * 10)) * 0.0001f, mod.ProjectileType("Ghostspear"), (npc.damage / 4), 0f, 0, 4, 0);
                    }
                    if (AI_Timer > 38)
                    {
                        AI_Timer = 0;
                        AI_State = Fly;
                        npc.netUpdate = true;
                    }
                }
            }
            if (AI_State == Flee)
            {
                npc.velocity.Y += 0.05f;
            }
        }


        const int Fly1 = 0;
        const int Fly2 = 1;
        const int Fly3 = 2;
        const int Fly4 = 3;
        const int Fly5 = 4;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = Fly1 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = Fly2 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = Fly3 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = Fly4 * frameHeight;
            }
            else if (npc.frameCounter < 25)
            {
                npc.frame.Y = Fly5 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override bool PreNPCLoot()
        {
            for (int i = 0; i < 120; i++)
            {
                Vector2 rotata = new Vector2(0, 8).RotatedBy(MathHelper.ToRadians(3 * i));
                Dust.NewDust(npc.Center + rotata, 0, 0, 58, rotata.X, rotata.Y, 0, default(Color), 1.5f);
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Barrowsspirit>()))
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type == ModContent.NPCType<Barrowsspirit>())
                    {
                        Vector2 spd = Main.npc[i].Center - npc.Center;
                        spd.Normalize();
                        Projectile.NewProjectile(npc.Center, spd * 5f, ModContent.ProjectileType<Barrowsdamageproj>(), 100, 0, Main.player[npc.target].whoAmI);
                    }
                }
            }
            return true;
        }
    }
}
