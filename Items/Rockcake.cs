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
            Item.width = 26;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item2;
        }
        public override bool CanUseItem(Player player)
        {
            return player.statLife > 1;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.Hurt(Terraria.DataStructures.PlayerDeathReason.LegacyDefault(), player.statLife - 1 + (int)(player.statDefense * 0.75), 0);
            player.statLife = 1;
            player.AddBuff(BuffID.Bleeding, 1800);
            return true;
        }
    }
}
