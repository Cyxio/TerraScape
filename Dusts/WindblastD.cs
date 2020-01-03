using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace OldSchoolRuneScape.Dusts
{
    public class WindblastD : ModDust
    {

        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.alpha = 100;
            dust.frame = new Rectangle(0, 0, 40, 40);
        }

        public override bool Update(Dust dust)
        {
            dust.alpha += 10;
            dust.scale *= 1.02f;
            dust.position -= new Vector2(0.33f * dust.scale, 0.33f * dust.scale);
            if (dust.alpha > 249)
            {
                dust.active = false;
            }
            return false;
        }
    }
}