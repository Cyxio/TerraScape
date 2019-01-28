using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Veracbody : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's Brassard");
            Tooltip.SetDefault("25% increased melee damage");
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
            drawArms = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.25f;
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
            item.width = 30;
            item.height = 27;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 14;
        }
    }
}