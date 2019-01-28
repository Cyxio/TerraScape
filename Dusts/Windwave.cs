using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Dusts
{
    public class Windwave : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.2f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.alpha = 50;
        }
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.alpha += 5;
            if (dust.alpha > 249)
            {
                dust.active = false;
            }
            return false;
        }
    }
}