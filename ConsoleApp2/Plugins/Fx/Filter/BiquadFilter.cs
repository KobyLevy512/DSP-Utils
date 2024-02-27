using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Filter
{
    /// <summary>
    /// Class for filtering an audio.
    /// </summary>
    public class BiquadFilter : PluginBase
    {
        const double sqr2_2 = 0.70710678118654;
        Coefficient cofR = new Coefficient(5), cofL = new Coefficient(5);

        public override void Process(ref double l, ref double r)
        {
            cofR.Process(ref r);
            cofL.Process(ref l);
        }
        public override void Process(ref double mono)
        {
            cofL.Process(ref mono);
        }
        public void MakeLowPass(double theta)
        {
            double alpha = Math.Sin(theta) * sqr2_2;
            double cs0 = Math.Cos(theta);
            double cs1 = 1 - cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            cofL[0, 0] = (b0 * _a0, 2 * cs0 * _a0);
            cofL[1, 1] = (cs1 * _a0, 0);
            cofL[2, 1] = (cofL[0, 0].input, -(1 - alpha) * _a0);

            cofR.Copy(cofL);
        }
        public void MakeLowPass(double theta, double q)
        {
            double alpha = Math.Sin(theta) / (q * 2);
            double cs0 = Math.Cos(theta);
            double cs1 = 1 - cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            cofL[0, 0] = (b0 * _a0, 2 * cs0 * _a0);
            cofL[1, 1] = (cs1 * _a0, 0);
            cofL[2, 1] = (cofL[0, 0].input, -(1 - alpha) * _a0);
            cofR.Copy(cofL);
        }
        public void MakeLowPass(double theta, double q, byte dbOctv)
        {
            MakeLowPass(theta, q);

            int len = dbOctv / 12;
            for (int i = 0; i < len; i++)
            {
                double alpha = Math.Sin(theta) * sqr2_2;
                double cs0 = Math.Cos(theta);
                double cs1 = 1 - cs0;
                double b0 = cs1 * 0.5;
                double _a0 = 1 / (1 + alpha);

                cofL[0, 0] = (cofL[0, 0].input * b0 * _a0, cofL[0, 0].output * 2 * cs0 * _a0);
                cofL[1, 1] = (cofL[1, 0].input * cs1 * _a0, cofL[1, 1].output);
                cofL[2, 1] = (cofL[0, 0].input, cofL[1, 1].output * -(1 - alpha) * _a0);
            }
            cofR.Copy(cofL);
        }
        public void MakeHighPass(double theta)
        {
           double theta64 = theta;
           double alpha = Math.Sin(theta64) * sqr2_2;
           double cs0 = Math.Cos(theta64);
           double b0 = (1 + cs0) * 0.5;
           double b1 = -(1 + cs0);
           double _a0 = 1 / (1 + alpha);
           double a1 = -2 * cs0;
           double a2 = 1 - alpha;

            cofL[0, 0] = (b0 * _a0, -a1 * _a0);
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (cofL[0, 0].input, -a2 * _a0);
            cofR.Copy(cofL);
        }
        public void MakeHighPass(double theta, double q)
        {
            double theta64 = theta;
            double alpha = Math.Sin(theta64) / (q * 2);
            double cs0 = Math.Cos(theta64);
            double b0 = (1 + cs0) * 0.5;
            double b1 = -(1 + cs0);
            double _a0 = 1 / (1 + alpha);
            double a1 = -2 * cs0;
            double a2 = 1 - alpha;

            cofL[0, 0] = (b0 * _a0, -a1 * _a0);
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (cofL[0, 0].input, -a2 * _a0);
            cofR.Copy(cofL);

        }

        public void MakeLowShelf(double theta, double doubleSquareRootGain)
        {
            double A = doubleSquareRootGain * doubleSquareRootGain;
            double theta64 = theta;
            double alpha = Math.Sin(theta64) * sqr2_2;
            double sqrtAx2xAlpha = 2 * alpha * doubleSquareRootGain;

            double cs0 = Math.Cos(theta64);
            double b0 = A * (A + 1 - (A - 1) * cs0 + sqrtAx2xAlpha);
            double b1 = 2 * A * (A - 1 - (A + 1) * cs0);
            double b2 = A * (A + 1 - (A - 1) * cs0 - sqrtAx2xAlpha);
            double _a0 = 1 / (A + 1 + (A - 1) * cs0 + sqrtAx2xAlpha);
            double a1 = -2 * (A - 1 + (A + 1) * cs0);
            double a2 = A + 1 + (A - 1) * cs0 - sqrtAx2xAlpha;

            cofL[0, 0] = (b0 * _a0, -a1 * _a0);
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (b2 * _a0, -a2 * _a0);
            cofR.Copy(cofL);
        }
        public void MakeHighShelf(double theta, double doubleSquareRootGain)
        {
            double A = doubleSquareRootGain * doubleSquareRootGain;
            double theta64 = theta;
            double alpha = Math.Sin(theta64) * sqr2_2;
            double sqrtAx2xAlpha = 2 * alpha * doubleSquareRootGain;

            
            double cs0 = Math.Cos(theta64);
            double b0 = A * ((A + 1) + (A - 1) * cs0 + sqrtAx2xAlpha);
            double b1 = -2 * A * ((A - 1) + (A + 1) * cs0);
            double b2 = A * ((A + 1) + (A - 1) * cs0 - sqrtAx2xAlpha);
            double _a0 = 1 / ((A + 1) - (A - 1) * cs0 + sqrtAx2xAlpha);
            double a1 = 2 * ((A - 1) - (A + 1) * cs0);
            double a2 = (A + 1) - (A - 1) * cs0 - sqrtAx2xAlpha;

            cofL[0, 0] = (b0 * _a0, -a1 * _a0);
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (b2 * _a0, -a2 * _a0);
            cofR.Copy(cofL);
        }

        public void MakePeak(double theta, double halfBwInOctava, double sqrtGain)
        {
            double A = sqrtGain;
            double alpha = Math.Sin(theta) * halfBwInOctava;
            double alphaA = alpha * A;
            double alpha_A = alpha / A;
            double b0 = 1 + alphaA;
            double b1 = -2 * Math.Cos(theta);
            double b2 = 1 - alphaA;
            double _a0 = 1.0 / (1.0 + alpha_A);
            double a2 = 1 - alpha_A;

            cofL[0, 0] = (b0 * _a0, -(b0 * _a0));
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (b2 * _a0, -a2 * _a0);
            cofR.Copy(cofL);
        }

        public void MakeBandPass(double theta, double halfBwInOctava)
        {
            double alpha = Math.Sin(theta) * halfBwInOctava;
            double _a0 = 1.0 / (1.0 + alpha);
            double b0 = alpha * _a0;
            double a1 = -2 * Math.Cos(theta);
            double a2 = 1 - alpha;

            cofL[0,0] = (b0, -a1 * _a0);
            cofL[1,1] = (0, 0);
            cofL[2,1] = (-b0, -a2 * _a0);
            cofR.Copy(cofL);
        }
        public void MakeBandPass(double theta, double bandwidth, double resonance)
        {
            double alpha = Math.Sin(theta) * bandwidth;
            double beta = Math.Sqrt(resonance);
            double _a0 = 1.0 / (1.0 + alpha / beta);

            double b0 = alpha * _a0;
            double a1 = -2 * Math.Cos(theta);
            double a2 = 1 - alpha / beta;

            cofL[0, 0] = (b0, -a1 * _a0);
            cofL[1, 1] = (0, 0);
            cofL[2, 1] = (-b0, -a2 * _a0);
            cofR.Copy(cofL);
             
        }
        public void MakeNotch(double theta, double halfBwInOctava)
        {
            double alpha = Math.Sin(theta) * halfBwInOctava;
            double cs0 = Math.Cos(theta);
            double b1 = -2 * cs0;
            double _a0 = 1.0 / (1.0 + alpha);
            double a1 = -2 * cs0;
            double a2 = 1 - alpha;

            cofL[0, 0] = (_a0, -a1 * _a0);
            cofL[1, 1] = (b1 * _a0, 0);
            cofL[2, 1] = (_a0, -a2 * _a0);
            cofR.Copy(cofL);

        }
        public void MakeAllPass(double theta, double q)
        {
            double alpha = Math.Sin(theta) * 0.5 / q;
            double cs0 = Math.Sin(theta);
            double b0 = 1 - alpha;
            double b1 = -2 * cs0;
            double b2 = 1 + alpha;
            double _a0 = 1 / b2;

            cofL[0, 0] = (b0 * _a0, 0);
            cofL[1, 1] = (b1 * _a0, -cofL[0, 1].input);
            cofL[2, 0] = (b2 * _a0, -cofL[1, 1].input);
            cofR.Copy(cofL);
        }
    }
}
