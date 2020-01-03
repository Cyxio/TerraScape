using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 52def 25damage 24crit 8movespeed
{
    [AutoloadEquip(EquipType.Head)]
    public class Verachelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's Helm");
            Tooltip.SetDefault("18% increased melee critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Veracbody") && legs.type == mod.ItemType("Veraclegs");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Veracset = true;
            player.setBonus = "Increases armor penetration by 20";
            player.armorPenetration += 20;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Veracset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Verac's flail's critical strikes detonate, dealing increased damage based on defense]"));
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