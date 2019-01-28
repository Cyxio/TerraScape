using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Dusts
{
    public class Diamondbolt : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 0.8f;
            dust.alpha = 0;
            dust.frame = new Rectangle(0, 0, 66, 54);
        }

        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, 0.2f, 0.2f, 0.2f);
            dust.alpha += 6;
            if (dust.alpha < 30)
            {
                dust.frame = new Rectangle(0, 54 * 0, 66, 54);
            }
            else if (dust.alpha < 60)
            {
                dust.frame = new Rectangle(0, 54 * 1, 66, 54);
            }
            else if (dust.alpha < 90)
            {
                dust.frame = new Rectangle(0, 54 * 2, 66, 54);
            }
            else if (dust.alpha < 120)
            {
                dust.frame = new Rectangle(0, 54 * 3, 66, 54);
            }
            else if (dust.alpha < 150)
            {
                dust.frame = new Rectangle(0, 54 * 4, 66, 54);
            }
            else
            {
                dust.active = false;
            }
            return false;
        }
    }
}