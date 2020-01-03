using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class ZGSfreeze : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Frozen");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.RSGlobalNPC>().ZGSfreeze = true;
        }
    }
}
