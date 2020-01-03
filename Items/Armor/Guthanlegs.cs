using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Guthanlegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Chainskirt");
            Tooltip.SetDefault("10% increased melee damage & movement speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.1f;
            player.moveSpeed += 0.1f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Guthanset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Increased chance & healing cap]"));
            }
        }
        public override void SetDefaults()
        {
            item.width = 13;
            item.height = 30;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 13;
        }
    }
}