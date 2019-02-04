using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Minesweeper3D
{
    class SoundSystem
    {
        public string[] paths { get; private set; }
        public bool soundEnabled { get; set; }
        private SoundPlayer soundEngine;
        public SoundSystem(string[] paths)
        {
            this.soundEnabled = true;
            this.paths = paths;
            soundEngine = new SoundPlayer();
        }

        public string this[int index]
        {
            get { return paths[index]; }
            set { paths[index] = value; }
        }

        public void PlayAndResetSound(int soundID)
        {
            soundEngine.Stop();
            if (soundEnabled)
            {
                soundEngine.SoundLocation = this[soundID];
                soundEngine.Play();
            }
        }
    }
}
