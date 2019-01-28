using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class Charge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Charge");
            Description.SetDefault("God spells deal 100% increased damage");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OSRSplayer>(mod).GodCharge = true;
        }
    }
}
