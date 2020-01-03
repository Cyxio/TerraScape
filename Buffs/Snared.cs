using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class Snared : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Snared");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.RSGlobalNPC>().snared = true;
        }
    }
}
