using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items.ClueScroll.ClueRewards.Master
{
    [AutoloadEquip(EquipType.Legs)]
    public class Ankoulegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ankou Leggings");
            Tooltip.SetDefault("This will make your flesh transparent");
        }
        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            color *= 0.8f;
        }
        public override bool DrawLegs()
        {
            return false;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            Lighting.AddLight(player.MountedCenter, new Vector3(148, 38, 27) / 350f);
        }
        public override void SetDefaults()
        {
            item.rare = 1;
            item.width = 22;
            item.height = 18;
            item.value = Item.sellPrice(0, 1);
        }
    }
}