using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace OldSchoolRuneScape.Dusts
{
    public class WindstrikeD : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;
            dust.frame.Height = 10;
            dust.frame.Width = 10;
        }
        public override bool Update(Dust dust)
        {
            dust.scale *= 1.11f;
            dust.position -= new Vector2(0.1f * dust.scale, 0.1f * dust.scale);
            dust.alpha += 12;
            float light = 0.2f;
            Lighting.AddLight(dust.position, light, light, light);
            if (dust.scale > 4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}