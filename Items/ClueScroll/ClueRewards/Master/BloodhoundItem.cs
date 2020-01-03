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
            item.CloneDefaults(ItemID.DogWhistle);
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.shoot = ModContent.ProjectileType<Bloodhound>();
            item.buffType = ModContent.BuffType<Buffs.Bloodhound>();
        }
        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
