using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
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
            Item.damage = 400;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.crit = 11;
            Item.width = 14;
            Item.height = 26;
            Item.scale = 0.8f;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Thrust;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Dinhbulwark>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                Projectile.NewProjectile(source, position, new Vector2(0, 15), type, damage, knockback, player.whoAmI, 1, 0);
                return false;
            }
            return true;
        }
    }
}
