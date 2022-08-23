using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.Armor //whole set 58def 20damage 20crit 
{
    [AutoloadEquip(EquipType.Head)]
    public class Dharokhelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dharok's Helm");
            Tooltip.SetDefault("12% increased melee critical strike chance");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 12;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("Dharokbody").Type && legs.type == Mod.Find<ModItem>("Dharoklegs").Type;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<OSRSplayer>().Dharokset = true;
            player.setBonus = "Melee damage is increased by 1% for every 1% of missing health";
            player.GetDamage(DamageClass.Melee) += (1f - ((float)player.statLife/player.statLifeMax2));
            if (player.statLife < 100)
            {
                
            }
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
            if (player.GetModPlayer<OSRSplayer>().Dharokset && player.GetModPlayer<OSRSplayer>().Amuletdamned)
            {
                tooltips.Add(new TooltipLine(Mod, "Damned", "[c/5cdb7d:Amulet of the damned: Dharok's greataxe gains 5% increased damage for every 1 health under 100 life]"));
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