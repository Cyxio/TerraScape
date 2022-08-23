using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.NPCs.Barrows.Torag
{
    public class Ghostshield : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's Shield");
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 5;
            NPC.aiStyle = -1;
            NPC.defense = 9999;
            NPC.width = 36;
            NPC.height = 58;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.HitSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Shieldhit");
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
        }
        public override void AI()
        {            
            Lighting.AddLight(NPC.Center, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (NPC.alpha == 0)
            {
                NPC.life = NPC.lifeMax = 5 + (int)(5 * NPC.ai[1]);
                NPC.alpha = 1;
            }
            if (NPC.ai[0] == 0)
            {
                NPC.alpha++;
                if (NPC.alpha > 230)
                {
                    NPC.active = false;
                }
            }
            if (NPC.ai[0] == 1)
            {
                NPC.ai[2] += 3;
                NPC owner = Main.npc[(int)NPC.ai[1]];
                NPC.position = owner.Center + new Vector2(150, 0).RotatedBy(MathHelper.ToRadians(NPC.ai[2]));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink);
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink);
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Enchanted_Pink);
        }
    }
}
