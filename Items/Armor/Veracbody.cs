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
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.25f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Veracset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Verac's flail's critical strikes detonate, dealing increased damage based on defense]"));
            }
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 27;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 14;
        }
    }
}