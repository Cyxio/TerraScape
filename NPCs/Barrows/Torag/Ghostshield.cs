using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            npc.lifeMax = 5;
            npc.aiStyle = -1;
            npc.defense = 9999;
            npc.width = 36;
            npc.height = 58;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Shieldhit");
            npc.ai[0] = 0;
            npc.ai[1] = 0;
            npc.ai[2] = 0;
            npc.ai[3] = 0;
        }
        public override void AI()
        {            
            Lighting.AddLight(npc.Center, new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            if (npc.alpha == 0)
            {
                npc.life = npc.lifeMax = 5 + (int)(5 * npc.ai[1]);
                npc.alpha = 1;
            }
            if (npc.ai[0] == 0)
            {
                npc.alpha++;
                if (npc.alpha > 230)
                {
                    npc.active = false;
                }
            }
            if (npc.ai[0] == 1)
            {
                npc.ai[2] += 3;
                NPC owner = Main.npc[(int)npc.ai[1]];
                npc.position = owner.Center + new Vector2(150, 0).RotatedBy(MathHelper.ToRadians(npc.ai[2]));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(npc.position, npc.width, npc.height, 58);
            Dust.NewDust(npc.position, npc.width, npc.height, 58);
            Dust.NewDust(npc.position, npc.width, npc.height, 58);
        }
    }
}
