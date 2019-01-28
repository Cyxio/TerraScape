using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace OldSchoolRuneScape.Dusts
{
    public class Guthanset : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 1f;
            dust.alpha = 0;
            dust.position -= new Vector2(12, 72);
        }

        public override bool Update(Dust dust)
        {
            Lighting.AddLight(dust.position + new Vector2(12, 36), new Vector3(116 * 0.01f, 35 * 0.01f, 78 * 0.01f));
            dust.alpha += 6;
            if (dust.alpha < 30)
            {
                dust.frame = new Rectangle(0, 72 * 0, 24, 72);
            }
            else if (dust.alpha < 60)
            {
                dust.frame = new Rectangle(0, 72 * 1, 24, 72);
            }
            else if (dust.alpha < 90)
            {
                dust.frame = new Rectangle(0, 72 * 2, 24, 72);
            }
            else if (dust.alpha < 120)
            {
                dust.frame = new Rectangle(0, 72 * 3, 24, 72);
            }
            else if (dust.alpha < 150)
            {
                dust.frame = new Rectangle(0, 72 * 4, 24, 72);
            }
            else
            {
                dust.active = false;
            }
            return false;
        }
    }
}