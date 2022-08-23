using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 60def 20damage 10crit 10movespeed
{
    [AutoloadEquip(EquipType.Head)]
    public class Guthanhelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guthan's Helm");
            Tooltip.SetDefault("10% increased melee critical strike chance");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Guthanbody").Type && legs.type == Mod.Find<ModItem>("Guthanlegs").Type;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Guthanset = true;
            player.setBonus = "Melee attacks have a chance to heal on hit";
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
            Item.width = 23;
            Item.height = 27;
            Item.value = Item.sellPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 20;
        }
    }
}