using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Guthanspear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Warspear");
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shootSpeed = 3.7f;
            Item.knockBack = 3f;
            Item.width = 58;
            Item.height = 60;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Guthanspear>();
            Item.value = Item.sellPrice(0, 10);
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = true;
        }

        public override float UseTimeMultiplier(Player player)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                return 1.10f;
            }
            return base.UseTimeMultiplier(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage.Flat *= 1.2f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: 20% increased damage & 10% increased speed]"));
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // this is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
