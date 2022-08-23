using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Karillegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Karil's Leatherskirt");
            Tooltip.SetDefault("8% increased ranged critical strike chance & movement speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 8;
            player.moveSpeed += 0.08f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Karilset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Karil's crossbow has a chance to spawn a homing bolt]"));
            }
        }
        public override void SetDefaults()
        {
            Item.width = 13;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }
    }
}