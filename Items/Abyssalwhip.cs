using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Abyssalwhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Whip");
        }
        public override void SetDefaults()
        {
            item.melee = true;
            item.damage = 55;
            item.knockBack = 2f;
            item.rare = 7;
            item.width = 52;
            item.height = 58;
            item.useAnimation = 25;
            item.useTime = 25;
            item.reuseDelay = 10;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 8);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Abyssalwhip");
            item.shoot = mod.ProjectileType<Projectiles.Abyssalwhip>();
            item.shootSpeed = 20f;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
