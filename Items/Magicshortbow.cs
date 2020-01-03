using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items
{
    public class Magicshortbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Shortbow");
            Tooltip.SetDefault("[c/5cdb7d:Special attack (5 second cooldown): Fires two enchanted arrows]");
        }
        public override void SetDefaults()
        {
            item.damage = 44;
            item.ranged = true;
            item.crit = 8;
            item.width = 18;
            item.height = 66;
            item.scale = 0.75f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 1f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item5;
            item.useAmmo = AmmoID.Arrow;
            item.autoReuse = true;
            item.useTurn = false;
            item.shoot = mod.ProjectileType("MSBspec");
            item.shootSpeed = 12;
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
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MSB"), player.position);
                Projectile.NewProjectile(position + new Vector2(speedX, speedY), new Vector2(speedX, speedY), mod.ProjectileType("MSBspec"), damage, knockBack, item.owner, 0, 0);
                Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(2)), mod.ProjectileType("MSBspec"), damage, knockBack, item.owner, 0, 0);
                player.AddBuff(mod.BuffType("SpecCD"), 300);
                return false;
            }
            return true;
        }
    }
}