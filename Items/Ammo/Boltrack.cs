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
            item.damage = 20;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.value = 10;
            item.rare = 7;
            item.shoot = mod.ProjectileType("Boltrack");
            item.shootSpeed = 16f;
            item.ammo = AmmoID.Dart;
        }
    }
}
