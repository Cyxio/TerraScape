using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 66def 15damage 8crit
{
    [AutoloadEquip(EquipType.Head)]
    public class Toraghelm : ModItem
    {
        public override bool DrawHead()
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's Helm");
            Tooltip.SetDefault("8% increased melee critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 8;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Toragbody") && legs.type == mod.ItemType("Toraglegs");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Toragset = true;
            player.setBonus = "Defense is increased by 1% for every 1% of missing health";
            player.statDefense += (int)(100*(1f - ((float)player.statLife / player.statLifeMax2)));
        }
        public override void ArmorSetShadows(Player player)
        {
            if (player.statLife < 100)
            {
                player.armorEffectDrawOutlines = true;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Toragset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Torag's hammers have a chance of staggering the enemy]"));
            }           
        }
        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 27;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 18;
        }
    }
}