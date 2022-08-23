using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            Item.damage = 70;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.width = 52;
            Item.height = 58;
            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.reuseDelay = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.scale = 0.75f;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ahrimspell>();
            Item.shootSpeed = 16;
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
                damage *= 1.2f;
                Item.mana -= (int)(Item.mana * 0.15f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: 20% increased damage & 15% increased speed & decreased mana cost]"));
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned && player.GetModPlayer<OSRSplayer>().Ahrimset && Main.rand.NextBool(2))
            {
                Projectile.NewProjectile(source, position, velocity.RotateRandom(MathHelper.ToRadians(3.5f)), type, damage, knockback, player.whoAmI, 1, 0);
            }
            Projectile.NewProjectile(source, position, velocity.RotateRandom(MathHelper.ToRadians(3.5f)), type, damage, knockback, player.whoAmI, 0, 0);
            return false;
        }
    }
}