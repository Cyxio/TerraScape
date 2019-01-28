using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class Clawbuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overload");
            Description.SetDefault("Your claws are supercharged!");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OSRSplayer>(mod).Clawbuff = true;
            int dust = Dust.NewDust(player.position, player.width, player.height, 130, player.velocity.X * -0.5f, player.velocity.Y * -0.5f);
            Main.dust[dust].noGravity = true;
        }
    }
}
