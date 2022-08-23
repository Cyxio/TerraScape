using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 66def 15damage 8crit
{
    [AutoloadEquip(EquipType.Head)]
    public class Toraghelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Torag's Helm");
            Tooltip.SetDefault("8% increased melee critical strike chance");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 8;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Toragbody").Type && legs.type == Mod.Find<ModItem>("Toraglegs").Type;
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
            Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
            if (player.GetModPlayer<OSRSplayer>().Toragset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Torag's hammers have a chance of staggering the enemy]"));
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