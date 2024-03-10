
namespace ConsoleApp2.Plugins.Fx.Dynamic
{
    public class Envelope : PluginBase
    {
        public double Sustain = 1;
        uint attack, decay, release;
        double curAttack, attckStep, curDecay, decayStep, curRelease, releaseStep;
        bool isRelease;

        public bool IsRelease
        {
            get=>isRelease;
        }
        public uint Attack
        {
            get => attack;
            set
            {
                attack = value;
                attckStep = 1.0 / value;
            }
        }
        public uint Decay
        {
            get => decay;
            set
            {
                decay = value;
                decayStep = 1.0 / value;
            }
        }
        public uint Release
        {
            get => release;
            set
            {
                release = value;
                releaseStep = 1.0 / value;
            }
        }
        public void Start()
        {
            isRelease = false;
            curRelease = 0;
            curAttack = 0;
            curDecay = 1;
        }

        public void End()
        {
            isRelease = true;
            curAttack = 1;
            curDecay = 0;
            curRelease = 1;
        }
        public override void Process(ref double l, ref double r)
        {
            if (curAttack < 1)
            {
                curAttack += attckStep;
                if(curAttack > 1)
                {
                    curAttack = 1;
                }
                l *= curAttack;
                r *= curAttack;
            }
            else if(curDecay > 0 && curDecay > Sustain)
            {
                curDecay -= decayStep;
                if(curDecay < 0)
                {
                    curDecay = 0;
                }
                l *= curDecay;
                r *= curDecay;
            }
            else if(curRelease > 0)
            {
                curRelease -= releaseStep;
                if(curRelease < 0)
                {
                    curRelease = 0;
                }
                l *= curRelease;
                r *= curRelease;
            }
            else
            {
                isRelease = false;
                l *= Sustain;
                r *= Sustain;
            }
        }
    }
}
