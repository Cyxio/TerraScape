using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Dusts
{
    public class KodaiExplode : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.alpha = 0;
            dust.frame = new Rectangle(0, 0, 80, 80);
        }

        public override bool Update(Dust dust)
        {
            dust.alpha += 15;
            if (dust.alpha < 30)
            {
                dust.frame = new Rectangle(0, 80 * 0, 80, 80);
            }
            else if (dust.alpha < 60)
            {
                dust.frame = new Rectangle(0, 80 * 1, 80, 80);
            }
            else if (dust.alpha < 90)
            {
                dust.frame = new Rectangle(0, 80 * 2, 80, 80);
            }
            else if (dust.alpha < 120)
            {
                dust.frame = new Rectangle(0, 80 * 3, 80, 80);
            }
            else
            {
                dust.active = false;
            }
            return false;
        }
    }
}