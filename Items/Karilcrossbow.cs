using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Karilcrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's Crossbow");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;
            item.scale = 0.75f;
            item.width = 29;
            item.height = 24;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item99;
            item.autoReuse = true;
            item.useTurn = false;
            item.noMelee = true;
            item.useAmmo = AmmoID.Dart;
            item.shoot = 10;
            item.shootSpeed = 40;
        }

        public override float UseTimeMultiplier(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                return 1.15f;
            }
            return base.UseTimeMultiplier(player);
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage = (int)(damage * 1.15f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: 15% increased damage & speed]"));
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned && player.GetModPlayer<OSRSplayer>().Karilset)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType<Projectiles.Boltrack>(), damage, knockBack, player.whoAmI, Main.rand.Next(3), 0);
                return false;
            }
            return true;
        }
    }
}
