using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 60def 20damage 10crit 10movespeed
{
    [AutoloadEquip(EquipType.Head)]
    public class Guthanhelm : ModItem
    {
        public override bool DrawHead()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Helm");
            Tooltip.SetDefault("10% increased melee critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 10;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Guthanbody") && legs.type == mod.ItemType("Guthanlegs");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Guthanset = true;
            player.setBonus = "Melee attacks have a chance to heal on hit";
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
            item.width = 23;
            item.height = 27;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 20;
        }
    }
}