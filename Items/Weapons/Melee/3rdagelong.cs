using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class _3rdagelong : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd Age Longsword\n-Cheat Weapon-");
        }
        public override void SetDefaults()
        {
            Item.damage = 450;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.crit = 21;
            Item.width = 58;
            Item.height = 56;
            Item.scale = 1f;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(1);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shootSpeed = 23f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thirdagemelee>();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight(new Vector2(hitbox.Center.X, hitbox.Center.Y), new Vector3(1f, 1f, 1f));
        }
    }
}