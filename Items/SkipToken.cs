using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace OldSchoolRuneScape.Items
{
    public class SkipToken : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skip Token");
            Tooltip.SetDefault("Resets your current slayer task");
        }
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item37;
            item.consumable = true;
            item.maxStack = 999;
            item.width = 52;
            item.height = 52;
            item.rare = -11;
            item.value = 0;
            item.useStyle = 4;
            item.useAnimation = 25;
            item.useTime = 25;
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<OSRSplayer>().ResetSlayer();
            Main.NewText("Your current slayer task has been cleared; return to a slayer master for a new assignment", Colors.RarityAmber);
            return true;
        }
    }
}
