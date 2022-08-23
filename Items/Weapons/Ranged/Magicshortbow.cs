using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Ranged
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
            Item.damage = 44;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 8;
            Item.width = 18;
            Item.height = 66;
            Item.scale = 0.75f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item5;
            Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = Mod.Find<ModProjectile>("MSBspec").Type;
            Item.shootSpeed = 12;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && player.GetModPlayer<OSRSplayer>().SpecCD == false)
            {
                SoundEngine.PlaySound(new SoundStyle("OldSchoolRuneScape/Sounds/Item/MSB"), player.position);
                Projectile.NewProjectile(source, position + velocity, velocity, Mod.Find<ModProjectile>("MSBspec").Type, damage, knockback, Item.playerIndexTheItemIsReservedFor, 0, 0);
                Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(2)), Mod.Find<ModProjectile>("MSBspec").Type, damage, knockback, Item.playerIndexTheItemIsReservedFor, 0, 0);
                player.AddBuff(Mod.Find<ModBuff>("SpecCD").Type, 300);
                return false;
            }
            return true;
        }
    }
}