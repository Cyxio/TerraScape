using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    public class BloodhoundItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodhound");
            Tooltip.SetDefault("Tracking down clues all over the world!");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DogWhistle);
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.shoot = ModContent.ProjectileType<Bloodhound>();
            Item.buffType = ModContent.BuffType<Buffs.Bloodhound>();
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}
