using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs
{
    public class Impling : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impling");
            Main.npcFrameCount[NPC.type] = 33;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0.05f + (0.1f * (Math.Abs(spawnInfo.SpawnTileX - Main.spawnTileX) * 2f / Main.maxTilesX));
        }
        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 30;
            NPC.aiStyle = 65;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 0.25f;
            NPC.noGravity = true;
            NPC.catchItem = (short)ModContent.ItemType<Items.Onyx>();
            NPC.alpha = 1;
        }
        public override void AI()
        {
            if (NPC.alpha == 1 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[2] = Main.rand.Next(1, 8);
                if (Main.hardMode && Main.rand.NextBool(10))
                {
                    NPC.ai[2] = 8;
                }
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.rand.NextBool(20))
                {
                    NPC.ai[2] = 10;
                }
                if (NPC.downedPlantBoss && Main.rand.NextBool(30))
                {
                    NPC.ai[2] = 9;
                }
                if (NPC.downedPlantBoss && Main.rand.NextBool(120))
                {
                    NPC.ai[2] = 10;
                }
                NPC.alpha = 0;
                NPC.netUpdate = true;         
            }
            string mmmmm = ("Implingjar" + ((int)NPC.ai[2] - 1));
            NPC.catchItem = (short)Mod.Find<ModItem>(mmmmm).Type;
            if (NPC.ai[2] == 5 && Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Flare_Blue);
            }
            if (NPC.ai[2] == 8 && Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TreasureSparkle);
            }
            if (NPC.ai[2] == 9 && Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Butterfly);
            }
            if (NPC.ai[2] == 10 && Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.HeatRay);
            }
            if (NPC.ai[2] == 11 && Main.rand.NextBool(5))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CrystalPulse);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int num117 = 7;
            NPC.rotation = NPC.velocity.X * 0.3f;
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter = NPC.frameCounter + 1.0 + (double)((Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) / 2f);
            if (NPC.frameCounter < (double)num117)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < (double)(num117 * 2))
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter < (double)(num117 * 3))
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                NPC.frame.Y = frameHeight;
                if (NPC.frameCounter >= (double)(num117 * 4 - 1))
                {
                    NPC.frameCounter = 0.0;
                }
            }
            NPC.frame.Y = NPC.frame.Y + (int)((float)(frameHeight * 3) * (NPC.ai[2] - 1f));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SilverCoin);
            }
        }
    }
}
