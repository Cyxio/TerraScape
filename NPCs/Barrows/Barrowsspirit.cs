using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows
{
    public class Barrowschest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barrows Chest");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 8;
            bossBagNPC = mod.NPCType("Barrowsspirit");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            for (int i = 0; i < 10; i++)
            {
                player.TryGettingDevArmor();
            }         
            Item.NewItem(player.Center, mod.ItemType<Tiles.BarrowsMusicBoxItem>(), 1, false, 0);
            player.QuickSpawnItem(mod.ItemType<Items.Accessories.Amuletdamned>());
        }
    }
    public class Barrowsspirit : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Barrows");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 666;
            npc.height = 70;
            npc.width = 50;
            npc.friendly = false;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 100;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.value = Item.buyPrice(0, 15);
            npc.HitSound = SoundID.NPCHit21;
            npc.DeathSound = SoundID.NPCDeath58;
            music = MusicID.Boss3; //mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DangerousWay");
            bossBag = mod.ItemType<Barrowschest>();
        }
        public override void AI()
        {
            npc.TargetClosest();
            if (!npc.HasValidTarget)
            {
                npc.active = false;
            }
            int dustCounter = 0;
            npc.ai[1]++;
            npc.dontTakeDamage = false;
            if (NPC.AnyNPCs(mod.NPCType<Ahrim.Ahrim>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(mod.NPCType<Dharok.Dharok>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(mod.NPCType<Torag.Torag>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(mod.NPCType<Guthan.Guthan>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(mod.NPCType<Verac.Verac>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            if (NPC.AnyNPCs(mod.NPCType<Karil.Karil>()))
            {
                npc.dontTakeDamage = true;
                dustCounter++;
            }
            for (int i = 0; i < dustCounter; i++)
            {
                Vector2 rotate = new Vector2(0, -130).RotatedBy(MathHelper.ToRadians(60 * i));
                Dust dust = Dust.NewDustPerfect(npc.Center + rotate.RotatedBy(MathHelper.ToRadians(npc.ai[1])), 58);
                dust.noGravity = true;
                dust.velocity *= 0f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -npc.direction;
            int frameSpeed = 5;
            npc.frameCounter++;
            if (npc.frameCounter < frameSpeed)
            {
                npc.frame.Y = 0;
            }
            else if (npc.frameCounter < frameSpeed * 2)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.frameCounter < frameSpeed * 3)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.frameCounter < frameSpeed * 4)
            {
                npc.frame.Y = frameHeight * 3;
            }
            else if (npc.frameCounter < frameSpeed * 5)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.frameCounter < frameSpeed * 6)
            {
                npc.frame.Y = frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }
        public override void NPCLoot()
        {
            npc.DropBossBags();
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Spirit of Barrows";
            potionType = ItemID.GreaterHealingPotion;
        }
    }
}
