using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OldSchoolRuneScape.Dusts
{
    public class WindwaveD : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 30;
            dust.scale = 1.5f;
        }
        internal int i = 0;
        internal int o = 0;
        public override bool Update(Dust dust)
        {
            dust.alpha += 12;
            i++;
            dust.frame = new Rectangle(0, 64 * o, 64, 64);
            if (i == 3)
            {
                o++;
                i = 0;
            }
            if(o == 6)
            {
                i = 0;
                o = 0;
                dust.active = false;
            }
            return false;
        }
    }
}