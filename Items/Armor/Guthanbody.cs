using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Guthanbody : ModItem
    {
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawArms = true;
            drawHands = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Platebody");
            Tooltip.SetDefault("10% increased melee damage");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.10f;
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
            item.width = 30;
            item.height = 27;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.defense = 27;
        }
    }
}