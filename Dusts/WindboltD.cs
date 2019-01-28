using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Dusts
{
    public class WindboltD : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.frame.Height = 9;
            dust.frame.Width = 9;
            dust.firstFrame = true;
        }
        public override bool Update(Dust dust)
        {
            dust.alpha += 6;
            dust.scale *= 0.99f;
            float light = 0.2f;
            dust.position += dust.velocity * 0.1f;
            dust.rotation += 0.2f;
            Lighting.AddLight(dust.position, light, light, light);
            if (dust.alpha > 250)
            {
                dust.active = false;
            }
            return false;
        }
    }
}