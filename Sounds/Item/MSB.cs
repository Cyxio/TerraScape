using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace OldSchoolRuneScape.Sounds.Item
{
    public class MSB : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = volume * 0.5f;
            soundInstance.Pan = pan;
            return soundInstance;
        }
    }
}
