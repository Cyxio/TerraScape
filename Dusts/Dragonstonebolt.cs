using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Dusts
{
    public class Dragonstonebolt : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 1f;
            dust.alpha = 0;
            dust.frame = new Rectangle(0, 0, 42, 54);
        }

        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position, 0.2f, 0.2f, 0.2f);
            dust.alpha += 1;
            if (dust.alpha < 5)
            {
                dust.frame = new Rectangle(0, 54 * 0, 42, 54);
            }
            else if (dust.alpha < 10)
            {
                dust.frame = new Rectangle(0, 54 * 1, 42, 54);
            }
            else if (dust.alpha < 15)
            {
                dust.frame = new Rectangle(0, 54 * 2, 42, 54);
            }
            else if (dust.alpha < 20)
            {
                dust.frame = new Rectangle(0, 54 * 3, 42, 54);
            }
            else if (dust.alpha < 25)
            {
                dust.frame = new Rectangle(0, 54 * 4, 42, 54);
            }
            else if (dust.alpha < 30)
            {
                dust.frame = new Rectangle(0, 54 * 5, 42, 54);
            }
            else if (dust.alpha < 35)
            {
                dust.frame = new Rectangle(0, 54 * 6, 42, 54);
            }
            else if (dust.alpha < 40)
            {
                dust.frame = new Rectangle(0, 54 * 7, 42, 54);
            }
            else if (dust.alpha < 45)
            {
                dust.frame = new Rectangle(0, 54 * 8, 42, 54);
            }
            else if (dust.alpha < 50)
            {
                dust.frame = new Rectangle(0, 54 * 9, 42, 54);
            }
            else if (dust.alpha < 55)
            {
                dust.frame = new Rectangle(0, 54 * 10, 42, 54);
            }
            else if (dust.alpha < 60)
            {
                dust.frame = new Rectangle(0, 54 * 11, 42, 54);
            }
            else if (dust.alpha < 65)
            {
                dust.frame = new Rectangle(0, 54 * 12, 42, 54);
            }
            else if (dust.alpha < 70)
            {
                dust.frame = new Rectangle(0, 54 * 13, 42, 54);
            }
            else if (dust.alpha < 75)
            {
                dust.frame = new Rectangle(0, 54 * 14, 42, 54);
            }
            else if (dust.alpha < 80)
            {
                dust.frame = new Rectangle(0, 54 * 15, 42, 54);
            }
            else
            {
                dust.active = false;
            }
            return false;
        }
    }
}