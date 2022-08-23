using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Veracflail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's Flail");
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.damage = 80;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Lime;
            Item.width = 48;
            Item.height = 56;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.reuseDelay = 15;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Veracflail>();
            Item.shootSpeed = 20f;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage.Flat *= 1.2f;
                Item.shootSpeed = 30f;
            }
            else
            {
                Item.shootSpeed = 20f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Increased range & 20% increased damage]"));
            }
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
