
namespace ConsoleApp2.Plugins
{
    public class EnvelopeShaper : PluginBase
    {
        public double Attack = 1;
        public ushort AttackLength;
        public double Release;
        bool reset = true;
        double attackPos = 0.1;
        int releasePos = 0;
        double releaseDec = 0;
        double releaseVol = 1;
        List<double> releaseBuffer = new List<double>();

        public ushort ReleaseLength
        {
            get => (ushort)releaseBuffer.Count;
            set
            {
                releaseDec = 1.0 / value;
                releaseBuffer = new double[value].ToList();
            }
        }
        public override void Process(ref double l, ref double r)
        {
            if(reset)
            {
                l *= Attack * attackPos;
                r *= Attack * attackPos;
                if(attackPos > 1.0)
                {
                    attackPos -= 1.0 / AttackLength;
                }
                releaseBuffer.Add(l);
                releaseBuffer.Add(r);
                releaseBuffer.RemoveAt(0);
                releaseBuffer.RemoveAt(0);

                if(l == 0 && r == 0)
                {
                    reset = false;
                    releaseVol = 1;
                }
            }
            else
            {
                if(l != 0 || r != 0)
                {
                    reset = true;
                    attackPos = Attack;
                }
                if(releasePos < releaseBuffer.Count)
                    l += releaseBuffer[releasePos++] * releaseVol;
                if (releasePos < releaseBuffer.Count)
                    r += releaseBuffer[releasePos++] * releaseVol;
                releaseVol -= releaseDec;
                if (releaseVol < 0)
                    releaseVol = 0;

            }
        }
    }
}
