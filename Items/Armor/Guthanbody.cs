using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class Guthanbody : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Platebody");
            Tooltip.SetDefault("10% increased melee damage");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Guthanset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Increased chance & healing cap]"));
            }
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 27;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 27;
        }
    }
}