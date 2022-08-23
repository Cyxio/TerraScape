using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
{
    public class Karilcrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's Crossbow");
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.scale = 0.75f;
            Item.width = 29;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item99;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.noMelee = true;
            Item.useAmmo = 1;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 40;
        }

        public override float UseTimeMultiplier(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                return 1.15f;
            }
            return base.UseTimeMultiplier(player);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage.Flat *= 1.15f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: 15% increased damage & speed]"));
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned && player.GetModPlayer<OSRSplayer>().Karilset)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Boltrack>(), damage, knockback, player.whoAmI, Main.rand.Next(3), 0);
                return false;
            }
            return true;
        }
    }
}
