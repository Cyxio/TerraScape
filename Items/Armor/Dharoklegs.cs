using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Dharoklegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dharok's Platelegs");
            Tooltip.SetDefault("8% increased melee damage & critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.08f;
            player.meleeCrit += 8;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Dharokset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Dharok's greataxe gains 5% increased damage for every 1 health under 100 life]"));
            }
        }
        public override void SetDefaults()
        {
            item.width = 13;
            item.height = 30;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 14;
        }
    }
}