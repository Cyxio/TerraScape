using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Items
{
    public class Runiteore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runite Ore");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.rare = 3;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.value = 5000;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("Runiteore");
        }
    }
}