using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using OldSchoolRuneScape.Common.DropRules;

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
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ItemRarityID.Blue;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<CrushingHand>()) && !Main.dayTime;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<CrushingHand>());
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
    }
    [AutoloadBossHead]
    public class CrushingHand : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crushing Hand");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.npcSlots = 10f;
            NPC.lavaImmune = true;
            NPC.damage = 25;
            NPC.defense = 0;
            NPC.lifeMax = 1500;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 2, 0, 0) / 2.5f;
            NPC.buffImmune[BuffID.Confused] = true;
            Music = OldSchoolRuneScape.slayerMusic;
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
            get { return NPC.ai[AI_State_Slot]; }
            set { NPC.ai[AI_State_Slot] = value; }
        }

        public float AI_Timer
        {
            get { return NPC.ai[AI_Timer_Slot]; }
            set { NPC.ai[AI_Timer_Slot] = value; }
        }

        public float AI_Attack
        {
            get { return NPC.ai[AI_AttackTimer_Slot]; }
            set { NPC.ai[AI_AttackTimer_Slot] = value; }
        }

        public float speed = 4f;
        public float accelerate = 0.075f;

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 2300 + 1000 * numPlayers;
            NPC.damage = (int)(NPC.damage * 0.7f);
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
            NPC.TargetClosest(true);
            Player target = Main.player[NPC.target];
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.alpha = 0;
            if (AI_State == Move)
            {
                if (target.dead || !target.active || !NPC.HasValidTarget || Main.dayTime)
                {
                    NPC.velocity *= 0;
                    AI_Timer = 85;
                    AI_State = Flee;
                    NPC.netUpdate = true;
                }
                if ((target.Top.Y > NPC.Bottom.Y) && NPC.collideY)
                {
                    NPC.position.Y++;
                }
                if (AI_Timer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (target.position.Y + target.height * 2 < NPC.position.Y)
                    {
                        AI_Timer = 360;                   
                        targetdirection = 1 * Math.Sign(target.position.X - NPC.position.X);
                        AI_State = Attack2;
                        NPC.netUpdate = true;
                    }
                    else if ((NPC.life < NPC.lifeMax / 2) && Main.rand.NextBool(3))
                    {
                        AI_Timer = 360;
                        targetdirection = 1 * Math.Sign(target.position.X - NPC.position.X);
                        AI_State = Attack2;
                        NPC.netUpdate = true;
                    }
                    else if (Main.rand.NextBool())
                    {
                        AI_Timer = 45;
                        AI_State = Attack;
                        NPC.netUpdate = true;
                    }
                    else
                    {
                        AI_Timer = Main.rand.Next(120, 181);
                        AI_State = Sprint;
                        targetdirection = 1 * Math.Sign(target.position.X - NPC.position.X);
                        NPC.netUpdate = true;
                    }
                }
                if (target.MountedCenter.X > NPC.Center.X)
                {
                    NPC.velocity.X += accelerate;
                    if (NPC.velocity.X > speed)
                    {
                        NPC.velocity.X = speed;
                    }
                }
                if (target.MountedCenter.X < NPC.Center.X)
                {
                    NPC.velocity.X -= accelerate;
                    if (NPC.velocity.X < -speed)
                    {
                        NPC.velocity.X = -speed;
                    }
                }
                if (NPC.velocity.X > speed || NPC.velocity.X < -speed || NPC.velocity.Y < -speed)
                {
                    NPC.velocity.X *= 0.9f;
                    NPC.netUpdate = true;
                }
                if (NPC.collideX)
                {
                    NPC.velocity.Y = -4f;
                    if (Vector2.Distance(target.MountedCenter, NPC.Center) < 200f)
                    {
                        AI_Timer = 60;
                        AI_State = Phase;
                        targetdirection = 1 * Math.Sign(target.position.X - NPC.position.X);
                        NPC.netUpdate = true;
                    }
                }
            }
            if (AI_State == Sprint)
            {
                if (target.MountedCenter.X > NPC.Center.X)
                {
                    NPC.velocity.X += accelerate*2;
                    if (NPC.velocity.X > speed*2f)
                    {
                        NPC.velocity.X = speed * 2f;
                        NPC.netUpdate = true;
                    }
                }
                if (target.MountedCenter.X < NPC.Center.X)
                {
                    NPC.velocity.X -= accelerate*2;
                    if (NPC.velocity.X < -speed * 2f)
                    {
                        NPC.velocity.X = -speed * 2f;
                        NPC.netUpdate = true;
                    }
                }
                if (NPC.velocity.X > speed * 2f || NPC.velocity.X < -speed * 2f)
                {
                    NPC.velocity.X *= 0.9f;
                    NPC.netUpdate = true;
                }
                if (NPC.collideX)
                {
                    NPC.velocity.Y = -4f;
                    if (Vector2.Distance(target.MountedCenter, NPC.Center) < 200f)
                    {
                        AI_Timer = 60;
                        AI_State = Phase;
                        targetdirection = 1 * Math.Sign(target.position.X - NPC.position.X);
                        NPC.netUpdate = true;
                    }
                }
                if (AI_Timer <= 0 || NPC.direction != targetdirection)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    NPC.netUpdate = true;
                }
            }
            if (AI_State == Attack)
            {
                if (AI_Timer > 25)
                {
                    NPC.velocity.X *= 0.7f;
                    NPC.netUpdate = true;
                }
                else if (AI_Timer > 24 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.velocity = new Vector2(Main.rand.NextFloat(5f, 13f) * NPC.direction, Main.rand.NextFloat(-12f, -4f));
                    NPC.netUpdate = true;
                }
                else
                {
                    AI_Timer = 2;
                    if (NPC.collideY || NPC.velocity.Length() < 0.1f)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            AI_Timer = Main.rand.Next(80, 121);
                            AI_State = Move;
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
            if (AI_State == Attack2)
            {
                Vector2 targetpos = target.MountedCenter + new Vector2(0, -280);
                NPC.noTileCollide = true;
                Vector2 veloc = Vector2.Subtract(targetpos, NPC.Center);
                veloc.Normalize();
                float sped = (Vector2.Distance(NPC.Center, targetpos) / 45f);
                NPC.velocity = veloc * Math.Max(sped, target.velocity.Length()*1.5f);
                if (AI_Timer <= 0 || NPC.direction != targetdirection || Vector2.Distance(NPC.Center, targetpos) < 50f)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    NPC.netUpdate = true;
                }
                NPC.netUpdate = true;
            }
            if (AI_State == Phase)
            {
                NPC.velocity = new Vector2(2.5f*NPC.direction, 0f);
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.alpha = 125;
                if (AI_Timer <= 0 || NPC.direction != targetdirection)
                {
                    AI_State = Move;
                    AI_Timer = 120;
                    NPC.netUpdate = true;
                }
            }
            if (AI_State == Flee)
            {
                NPC.alpha = (85 - (int)(AI_Timer)) * 3;
                if (AI_Timer < 10)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var leadingCondition = new LeadingConditionRule(new SlayerBossFirstKillCondition(1));
            int itemType = ModContent.ItemType<Items.SlayerToken>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 10,
                MaximumItemDropsCount = 10,
            };
            leadingCondition.OnSuccess(new DropOneByOne(itemType, parameters));
            npcLoot.Add(leadingCondition);
        }
        public override void OnKill()
        {
            if (OSRSworld.slayBossProgress < 1)
            {
                OSRSworld.slayBossProgress = 1;
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
            NPC.spriteDirection = NPC.direction;
            if (AI_State == Move || AI_State == Flee)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 20)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 30)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else if (AI_State == Attack)
            {
                if (AI_Timer > 25)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
            }
            else if (AI_State == Sprint)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter < 5)
                {
                    NPC.frame.Y = 0 * frameHeight;
                }
                else if (NPC.frameCounter < 10)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else if (NPC.frameCounter < 15)
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
                else
                {
                    NPC.frameCounter = 0;
                }
            }
            else if (AI_State == Attack2)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("CrushingHand1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("CrushingHand2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("CrushingHand3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("CrushingHand4").Type, 1f);
            }
        }
    }
}