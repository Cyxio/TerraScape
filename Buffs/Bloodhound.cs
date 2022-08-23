using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class Bloodhound : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodhound");
            Description.SetDefault("Tracking down clues all over the world!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<OSRSplayer>().BloodHound = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[Mod.Find<ModProjectile>("Bloodhound").Type] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_ReleaseEntity() ,player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, Mod.Find<ModProjectile>("Bloodhound").Type, 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
