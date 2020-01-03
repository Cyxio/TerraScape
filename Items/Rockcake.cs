using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Rockcake : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dwarven Rock Cake");
            Tooltip.SetDefault("'Only for dwarf consumption'\nLowers your hitpoints to 1 and disables regeneration for 1 minute");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.useStyle = 2;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item2;
        }
        public override bool CanUseItem(Player player)
        {
            return player.statLife > 1;
        }
        public override bool UseItem(Player player)
        {
            player.Hurt(Terraria.DataStructures.PlayerDeathReason.LegacyDefault(), player.statLife - 1 + (int)(player.statDefense * 0.75), 0);
            player.statLife = 1;
            player.AddBuff(BuffID.Bleeding, 1800);
            return true;
        }
    }
}
