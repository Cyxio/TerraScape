using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Dusts
{
    public class Runedust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.6f;
            dust.noGravity = false;
            dust.noLight = false;
            dust.scale *= Main.rand.NextFloat(0.9f, 1.2f);
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            if (dust.velocity.Y < 1f)
            {
                dust.velocity.Y += 0.08f;
            }
            else
            {
                dust.velocity.Y *= 1.05f;
            }           
            dust.scale *= 0.98f;
            if (dust.scale < 0.4f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}