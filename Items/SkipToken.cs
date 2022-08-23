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
            Item.UseSound = SoundID.Item37;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.width = 52;
            Item.height = 52;
            Item.rare = -11;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 25;
            Item.useTime = 25;
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<OSRSplayer>().ResetSlayer();
            Main.NewText("Your current slayer task has been cleared; return to a slayer master for a new assignment", Colors.RarityAmber);
            return true;
        }
    }
}
