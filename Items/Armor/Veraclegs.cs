using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class Veraclegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verac's Plateskirt");
            Tooltip.SetDefault("8% increased melee critical strike chance & movement speed");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 8;
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
            Item.width = 13;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 20;
        }
    }
}