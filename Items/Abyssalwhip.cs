using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
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
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 55;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Lime;
            Item.width = 52;
            Item.height = 58;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.reuseDelay = 10;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 8);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = new SoundStyle("OldSchoolRuneScape/Sounds/Item/Abyssalwhip");
            Item.shoot = ModContent.ProjectileType<Projectiles.Abyssalwhip>();
            Item.shootSpeed = 20f;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
