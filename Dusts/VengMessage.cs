using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Dusts
{
    public class VengMessage : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = 0.75f;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(1f, 1f, 0, dust.alpha);
        }

        public override bool Update(Dust dust)
        {
            dust.frame = new Rectangle(0, 0, 230, 30);
            dust.alpha += 3;
            if (dust.alpha > 3)
            {
                dust.active = false;
            }
            return false;
        }
    }
}