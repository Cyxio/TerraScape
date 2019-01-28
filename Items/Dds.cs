using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Dagger");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (3 second cooldown): Strikes twice with guaranteed critical strikes]");
        }
        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.crit = 11;
            item.width = 14;
            item.height = 26;
            item.scale = 0.8f;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 3;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType<Projectiles.DDS>();
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            hitbox.X += 20 * player.direction;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            item.noMelee = false;
            item.noUseGraphic = false;
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.noUseGraphic = true;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>(mod).SpecCD)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/DDS"), player.MountedCenter);
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.DDS>(), damage, 0f);
                player.AddBuff(mod.BuffType("SpecCD"), 180);
                return true;
            }
            return false;
        }
    }
}
