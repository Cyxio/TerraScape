using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace OldSchoolRuneScape.Items.Weapons.Melee
{
    public class Toraghammers : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's Hammers");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.PaladinsHammer);
            Item.shoot = ModContent.ProjectileType<Projectiles.Toraghammer>();
            Item.damage = 65;
            Item.width = 54;
            Item.height = 40;
            Item.shootSpeed = 15f;
            Item.useAnimation = 8;
            Item.useTime = 4;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Lime;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                damage.Flat *= 1.2f;
                Item.shootSpeed = 25f;
            }
            else
            {
                Item.shootSpeed = 15f;
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
