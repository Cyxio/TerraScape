using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    [AutoloadEquip(EquipType.Body)]
    public class Ankoubody : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ankou Top");
            Tooltip.SetDefault("This will make your flesh transparent");
            ArmorIDs.Body.Sets.HidesTopSkin[Item.bodySlot] = true;
        }
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            color *= 0.8f;
        }
        public override void UpdateVanity(Player player)
        {
            Lighting.AddLight(player.MountedCenter, new Vector3(148, 38, 27) / 350f);
        }
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Blue;
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(0, 1);
        }
    }
}