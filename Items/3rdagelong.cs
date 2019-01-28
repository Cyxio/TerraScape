using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class _3rdagelong : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("3rd Age Longsword\n-Cheat Weapon-");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (14 second cooldown): Throws a flurry of returning daggers]");
        }
        public override void SetDefaults()
        {
            item.damage = 450;
            item.melee = true;
            item.crit = 21;
            item.width = 58;
            item.height = 56;
            item.scale = 1f;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.knockBack = 4f;
            item.value = Item.sellPrice(1);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 23f;
            item.shoot = mod.ProjectileType<Projectiles.Thirdagemelee>();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Lighting.AddLight(new Vector2(hitbox.Center.X, hitbox.Center.Y), new Vector3(1f, 1f, 1f));
        }
    }
}