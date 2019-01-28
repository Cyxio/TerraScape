using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items
{
    public class Dragonhuntercrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Hunter Crossbow");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 45;
            item.ranged = true;
            item.scale = 0.75f;
            item.width = 58;
            item.height = 60;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item99;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Dart;
            item.shoot = 10;
            item.shootSpeed = 50;
        }
    }
}