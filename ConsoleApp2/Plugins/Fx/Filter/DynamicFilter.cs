
namespace ConsoleApp2.Plugins.Fx.Filter
{
    public class DynamicFilter : PluginBase
    {
        public double ThreshHold, Ratio;
        double freq = 6.283185307179586, bandWidth = 0;
        BiquadFilter band = new BiquadFilter();
        BiquadFilter peak = new BiquadFilter();

        public DynamicFilter()
        {
            band.MakeBandPass(6.283185307179586, 0);
            peak.MakePeak(6.283185307179586, 0, 0);
        }

        public double BandWidth
        {
            get => bandWidth;
            set
            {
                bandWidth = value;
                band.MakeBandPass(freq, bandWidth);
            }
        }

        public double Frequency
        {
            get => freq;
            set
            {
                freq = value;
                band.MakeBandPass(freq, bandWidth);
            }
        }
        public override void Process(ref double l, ref double r)
        {
            double left = l;
            double right = r;

            band.Process(ref left, ref right);

            if(Math.Abs(left) > ThreshHold)
            {
                double dec = (left - ThreshHold) * Ratio;
                peak.MakePeak(freq, bandWidth, -dec);
                peak.Process(ref l);
            }

            if (Math.Abs(right) > ThreshHold)
            {
                double dec = (right - ThreshHold) * Ratio;
                peak.MakePeak(freq, bandWidth, -dec);
                peak.Process(ref r);
            }
        }
        /// <summary>
        /// Return the volume by specific frequncy range.
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public double CheckVolume(double sample)
        {
            band.Process(ref sample, ref sample);
            return sample;
        }

    }
}
