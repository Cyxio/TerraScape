using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dinhbulwark : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dinh's Bulwark");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (12 second cooldown): Slams the shield down, knocking enemies away]");
        }
        public override void SetDefaults()
        {
            item.damage = 400;
            item.melee = true;
            item.crit = 11;
            item.width = 14;
            item.height = 26;
            item.scale = 0.8f;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 3;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = false;
            item.useTurn = true;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType<Projectiles.Dinhbulwark>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                Projectile.NewProjectile(position, new Vector2(0, 15), type, damage, knockBack, player.whoAmI, 1, 0);
                return false;
            }
            return true;
        }
    }
}
