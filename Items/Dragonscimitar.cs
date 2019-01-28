using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Items
{
    public class Dragonscimitar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Scimitar");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.melee = true;
            item.scale = 0.8f;
            item.crit = 6;
            item.width = 58;
            item.height = 52;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
    }
}