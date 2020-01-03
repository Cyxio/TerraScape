using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Slayer
{
    public class CrawlingSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crawling Hand");
            Tooltip.SetDefault("I'm glad its just the hand I can see...");
        }
        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 2, 0, 0);
            item.consumable = true;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useTime = 45;
            item.useAnimation = 45;
            item.rare = 1;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CrushingHand>()) && !Main.dayTime;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<CrushingHand>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
    [AutoloadBossHead]
    public class CrushingHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crushing Hand");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.npcSlots = 10f;
            npc.lavaImmune = true;
            npc.damage = 25;
            npc.defense = 0;
            npc.lifeMax = 1500;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 2, 0, 0) / 2.5f;
            npc.buffImmune[BuffID.Confused] = true;
            music = OldSchoolRuneScape.slayerMusic;
        }

        const int AI_State_Slot = 0;
        const int AI_Timer_Slot = 1;
        const int AI_AttackTimer_Slot = 2;
        const int Move = 0;
        const int Attack = 1;
        const int Sprint = 2;
        const int Attack2 = 3;
        const int Phase = 4;
        const int Flee = 5;

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

        public float AI_Attack
        {
            get { return npc.ai[AI_AttackTimer_Slot]; }
            set { npc.ai[AI_AttackTimer_Slot] = value; }
        }

        public float speed = 4f;
        public float accelerate = 0.075f;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2300 + 1000 * numPlayers;
            npc.damage = (int)(npc.damage * 0.7f);
            speed = 5f;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Crushing Hand";
            potionType = ItemID.LesserHealingPotion;
        }

        int targetdirection = 1;

        public override void AI()
        {
            if (AI_Timer > 0)
            {
                AI_Timer--;
            }
            npc.TargetClosest(true);
            Player target = Main.player[npc.target];
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.alpha = 0;
            if (AI_State == Move)
            {
                if (target.dead || !target.active || !npc.HasValidTarget || Main.dayTime)
                {
                    npc.velocity *= 0;
                    AI_Timer = 85;
                    AI_State = Flee;
                    npc.netUpdate = true;
                }
                if ((target.Top.Y > npc.Bottom.Y) && npc.collideY)
                {
                    npc.position.Y++;
                }
                if (AI_Timer <= 0 && Main.netMode != 1)
                {
                    if (target.position.Y + target.height * 2 < npc.position.Y)
                    {
                        AI_Timer = 360;                   
                        targetdirection = 1 * Math.Sign(target.position.X - npc.position.X);
                        AI_State = Attack2;
                        npc.netUpdate = true;
                    }
                    else if ((npc.life < npc.lifeMax / 2) && Main.rand.Next(3) == 0)
                    {
                        AI_Timer = 360;
                        targetdirection = 1 * Math.Sign(target.position.X - npc.position.X);
                        AI_State = Attack2;
                        npc.netUpdate = true;
                    }
                    else if (Main.rand.NextBool())
                    {
                        AI_Timer = 45;
                        AI_State = Attack;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        AI_Timer = Main.rand.Next(120, 181);
                        AI_State = Sprint;
                        targetdirection = 1 * Math.Sign(target.position.X - npc.position.X);
                        npc.netUpdate = true;
                    }
                }
                if (target.MountedCenter.X > npc.Center.X)
                {
                    npc.velocity.X += accelerate;
                    if (npc.velocity.X > speed)
                    {
                        npc.velocity.X = speed;
                    }
                }
                if (target.MountedCenter.X < npc.Center.X)
                {
                    npc.velocity.X -= accelerate;
                    if (npc.velocity.X < -speed)
                    {
                        npc.velocity.X = -speed;
                    }
                }
                if (npc.velocity.X > speed || npc.velocity.X < -speed || npc.velocity.Y < -speed)
                {
                    npc.velocity.X *= 0.9f;
                    npc.netUpdate = true;
                }
                if (npc.collideX)
                {
                    npc.velocity.Y = -4f;
                    if (Vector2.Distance(target.MountedCenter, npc.Center) < 200f)
                    {
                        AI_Timer = 60;
                        AI_State = Phase;
                        targetdirection = 1 * Math.Sign(target.position.X - npc.position.X);
                        npc.netUpdate = true;
                    }
                }
            }
            if (AI_State == Sprint)
            {
                if (target.MountedCenter.X > npc.Center.X)
                {
                    npc.velocity.X += accelerate*2;
                    if (npc.velocity.X > speed*2f)
                    {
                        npc.velocity.X = speed * 2f;
                        npc.netUpdate = true;
                    }
                }
                if (target.MountedCenter.X < npc.Center.X)
                {
                    npc.velocity.X -= accelerate*2;
                    if (npc.velocity.X < -speed * 2f)
                    {
                        npc.velocity.X = -speed * 2f;
                        npc.netUpdate = true;
                    }
                }
                if (npc.velocity.X > speed * 2f || npc.velocity.X < -speed * 2f)
                {
                    npc.velocity.X *= 0.9f;
                    npc.netUpdate = true;
                }
                if (npc.collideX)
                {
                    npc.velocity.Y = -4f;
                    if (Vector2.Distance(target.MountedCenter, npc.Center) < 200f)
                    {
                        AI_Timer = 60;
                        AI_State = Phase;
                        targetdirection = 1 * Math.Sign(target.position.X - npc.position.X);
                        npc.netUpdate = true;
                    }
                }
                if (AI_Timer <= 0 || npc.direction != targetdirection)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    npc.netUpdate = true;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer > 25)
                {
                    npc.velocity.X *= 0.7f;
                    npc.netUpdate = true;
                }
                else if (AI_Timer > 24 && Main.netMode != 1)
                {
                    npc.velocity = new Vector2(Main.rand.NextFloat(5f, 13f) * npc.direction, Main.rand.NextFloat(-12f, -4f));
                    npc.netUpdate = true;
                }
                else
                {
                    AI_Timer = 2;
                    if (npc.collideY || npc.velocity.Length() < 0.1f)
                    {
                        if (Main.netMode != 1)
                        {
                            AI_Timer = Main.rand.Next(80, 121);
                            AI_State = Move;
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            if (AI_State == Attack2)
            {
                Vector2 targetpos = target.MountedCenter + new Vector2(0, -280);
                npc.noTileCollide = true;
                Vector2 veloc = Vector2.Subtract(targetpos, npc.Center);
                veloc.Normalize();
                float sped = (Vector2.Distance(npc.Center, targetpos) / 45f);
                npc.velocity = veloc * Math.Max(sped, target.velocity.Length()*1.5f);
                if (AI_Timer <= 0 || npc.direction != targetdirection || Vector2.Distance(npc.Center, targetpos) < 50f)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    npc.netUpdate = true;
                }
                npc.netUpdate = true;
            }
            if (AI_State == Phase)
            {
                npc.velocity = new Vector2(2.5f*npc.direction, 0f);
                npc.noGravity = true;
                npc.noTileCollide = true;
                npc.alpha = 125;
                if (AI_Timer <= 0 || npc.direction != targetdirection)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    npc.netUpdate = true;
                }
            }
            if (AI_State == Flee)
            {
                npc.alpha = (85 - (int)(AI_Timer)) * 3;
                if (AI_Timer < 10)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
        }


        public override void NPCLoot()
        {
            if (OSRSworld.slayBossProgress < 1)
            {
                OSRSworld.slayBossProgress = 1;
                Item.NewItem(npc.position, new Vector2(npc.width, npc.height), ModContent.ItemType<Items.SlayerToken>(), 10);
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SlayerMasterTurael>())
                {
                    Main.npc[i].life = 1;
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 180);
                    Main.npc[i].netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (AI_State == Move || AI_State == Flee)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 20)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (AI_State == Attack)
            {
                if (AI_Timer > 25)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 1 * frameHeight;
                }
            }
            else if (AI_State == Sprint)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 5)
                {
                    npc.frame.Y = 0 * frameHeight;
                }
                else if (npc.frameCounter < 10)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 15)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (AI_State == Attack2)
            {
                npc.frame.Y = 1 * frameHeight;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/CrushingHand1"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/CrushingHand2"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/CrushingHand3"), 1f);
                Gore.NewGore(npc.position, npc.velocity * Main.rand.NextFloat(0.9f, 1.1f), mod.GetGoreSlot("Gores/CrushingHand4"), 1f);
            }
        }
    }
}