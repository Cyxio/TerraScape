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
            player.GetCritChance(DamageClass.Generic) += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Veracbody").Type && legs.type == Mod.Find<ModItem>("Veraclegs").Type;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Veracset = true;
            player.setBonus = "Increases armor penetration by 20";
            player.GetArmorPenetration(DamageClass.Generic) += 20;
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
            Item.width = 23;
            Item.height = 27;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }
    }
}