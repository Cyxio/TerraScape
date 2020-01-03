using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Magic
{
    public class Ahrimstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's Staff");           
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.magic = true;
            item.mana = 12;
            item.width = 52;
            item.height = 58;
            item.useTime = 8;
            item.useAnimation = 24;
            item.reuseDelay = 26;
            item.useStyle = 5;
            item.noMelee = true;
            item.scale = 0.75f;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 10);
            item.rare = 7;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Ahrimspell>();
            item.shootSpeed = 16;
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
                damage = (int)(damage * 1.2f);
                item.mana -= (int)(item.mana * 0.15f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: 20% increased damage & 15% increased speed & decreased mana cost]"));
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned && player.GetModPlayer<OSRSplayer>().Ahrimset && Main.rand.Next(2) == 0)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotateRandom(MathHelper.ToRadians(3.5f)), type, damage, knockBack, player.whoAmI, 1, 0);
            }
            Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotateRandom(MathHelper.ToRadians(3.5f)), type, damage, knockBack, player.whoAmI, 0, 0);
            return false;
        }
    }
}