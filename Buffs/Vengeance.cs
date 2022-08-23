using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Buffs
{
    public class Vengeance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vengeance");
            Description.SetDefault("Getting hit will reflect 1000% damage back to the attacker");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<OSRSplayer>().Vengeance = true;
        }
    }
}
