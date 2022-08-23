using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Elite
{
    [AutoloadEquip(EquipType.Body)]
    public class Rangerstunic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ranger's Tunic");
            Tooltip.SetDefault("25% increased ranged damage");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == Mod.Find<ModItem>("Robinhat").Type)
            {
                for (int i = 3; i < 9; i++)
                {
                    if (Main.player[Item.playerIndexTheItemIsReservedFor].armor[i].type == Mod.Find<ModItem>("Rangerboots").Type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "100% ranged critical strike";
            player.GetCritChance(DamageClass.Ranged) = 100;
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Orange;
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.25f;
        }
    }
}