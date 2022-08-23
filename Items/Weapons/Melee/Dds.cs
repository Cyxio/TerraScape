using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items.Weapons.Melee
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
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.crit = 11;
            Item.width = 14;
            Item.height = 26;
            Item.scale = 0.8f;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<Projectiles.DDS>();
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
            Item.noMelee = false;
            Item.noUseGraphic = false;
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && !player.GetModPlayer<OSRSplayer>().SpecCD)
            {
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/DDS"), player.MountedCenter);
                Projectile.NewProjectile(source, position, position, ModContent.ProjectileType<Projectiles.DDS>(), damage, 0f);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 180);
                return true;
            }
            return false;
        }
    }
}
