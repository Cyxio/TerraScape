using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Dragonhuntercrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Hunter Crossbow");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.75f;
            Item.width = 58;
            Item.height = 60;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = 1;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 50;
        }
    }
}