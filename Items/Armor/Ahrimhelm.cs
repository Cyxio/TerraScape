using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor 
{
    [AutoloadEquip(EquipType.Head)]
    public class Ahrimhelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ahrim's Hood");
            Tooltip.SetDefault("Increases maximum mana by 80 & 15% decreased mana usage");
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 80;
            player.manaCost -= 0.15f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("Ahrimbody") && legs.type == mod.ItemType("Ahrimlegs");
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Ahrimset = true;
            player.setBonus = "Magic attacks inflict shadowflame";
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            if (player.GetModPlayer<OSRSplayer>().Ahrimset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(mod, "Damned", "[c/5cdb7d:Amulet of the damned: Ahrim's staff fires additional homing projectiles]"));
            }           
        }
        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 27;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 10;
        }
    }
}