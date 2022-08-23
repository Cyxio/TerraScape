using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Ammo
{
    public class Boltrack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bolt Rack");
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 26;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = 10;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = Mod.Find<ModProjectile>("Boltrack").Type;
            Item.shootSpeed = 16f;
            Item.ammo = 1;
        }
    }
}
