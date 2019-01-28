using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
	public class SpecCD : ModBuff
	{
        public override void SetDefaults()
		{
            DisplayName.SetDefault("Fatique");
            Description.SetDefault("Can't use special attacks");
            Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OSRSplayer>(mod).SpecCD = true;
        }
    }
}
