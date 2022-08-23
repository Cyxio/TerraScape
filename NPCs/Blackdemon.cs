using System;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;

namespace OldSchoolRuneScape.NPCs
{
    public class Blackdemon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black demon");
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.scale = 1.2f;
            NPC.friendly = false;
            NPC.lifeMax = 300;
            NPC.defense = 20;
            NPC.damage = 60;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
            NPC.width = 44;
            NPC.height = 64;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Demon");
            NPC.value = 1000f;
            NPC.aiStyle = 14;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime && Main.hardMode)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.05f;
            }
            else
            {
                return 0;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Firerune>(), 5, 30, 120));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Lawrune>(), 20, 8, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Chaosrune>(), 20, 8, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Magic.Deathrune>(), 20, 3, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armor.Runeplate>(), 100));
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
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.Black);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 1f, 1f, 0, Color.Black);
                }
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Blackdemon").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Blackdemon1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * Main.rand.NextFloat(0.9f, 1.1f), Mod.Find<ModGore>("Blackdemon1").Type, NPC.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.08f;
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
