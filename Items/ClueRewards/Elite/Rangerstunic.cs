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
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawArms = true;
            drawHands = true;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == mod.ItemType("Robinhat"))
            {
                for (int i = 3; i < 9; i++)
                {
                    if (Main.player[item.owner].armor[i].type == mod.ItemType("Rangerboots"))
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
            player.rangedCrit = 100;
        }
        public override void SetDefaults()
        {
            item.rare = 3;
            item.width = 30;
            item.height = 18;
            item.value = Item.sellPrice(0, 5);
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.25f;
        }
    }
}