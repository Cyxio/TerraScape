using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace OldSchoolRuneScape.NPCs
{
    public class Gargoyle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gargoyle");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.scale = 1.2f;
            NPC.friendly = false;
            NPC.lifeMax = 100;
            NPC.defense = 10;
            NPC.damage = 40;
            NPC.knockBackResist = 0.3f;
            NPC.noGravity = true;
            NPC.width = 44;
            NPC.height = 44;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 250f;
            NPC.aiStyle = 14;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Cavern.Chance * (Main.hardMode ? 0.05f : 0.15f);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Weapons.Melee.Granitemaul>(), 33));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.LightGray);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.LightGray);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Gargoyle1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Gargoyle").Type, 1f);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.1f;
            int frameSpeed = 7;
            NPC.frameCounter++;
            if (NPC.frameCounter < frameSpeed)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter < frameSpeed * 2)
            {
                NPC.frame.Y = frameHeight;
            }
            else if (NPC.frameCounter < frameSpeed * 3)
            {
                NPC.frame.Y = frameHeight * 2;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
    }
}
