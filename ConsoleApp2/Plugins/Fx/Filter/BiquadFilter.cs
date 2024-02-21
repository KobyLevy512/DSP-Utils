namespace ConsoleApp2.Plugins.Fx.Filter
{
    /// <summary>
    /// Class for filtering an audio.
    /// </summary>
    public class BiquadFilter : PluginBase
    {
        const double antiDenormal = 1e-30;
        const double sqr2_2 = 0.70710678118654;
        private class FilterState
        {
            public double InputMem1 = 0;
            public double InputMem2 = 0;
            public double OutputMem1 = 0;
            public double OutputMem2 = 0;
        };

        double inputCoeff0 = 0;
        double inputCoeff1 = 0;
        double inputCoeff2 = 0;
        double outputCoeff1 = 0;
        double outputCoeff2 = 0;

        FilterState
            leftState = new FilterState(),
            rightState = new FilterState();

        public override void Process(ref double l, ref double r)
        {
            //Left
            double output = inputCoeff0 * l + inputCoeff1 * leftState.InputMem1 + inputCoeff2 * leftState.InputMem2
                + outputCoeff1 * leftState.OutputMem1 + outputCoeff2 * leftState.OutputMem2;

            output += antiDenormal;
            output -= antiDenormal;

            leftState.InputMem2 = leftState.InputMem1;
            leftState.InputMem1 = l;
            leftState.OutputMem2 = leftState.OutputMem1;
            leftState.OutputMem1 = output;

            l = output;

            //Right
            output = inputCoeff0 * r + inputCoeff1 * rightState.InputMem1 + inputCoeff2 * rightState.InputMem2
                + outputCoeff1 * rightState.OutputMem1 + outputCoeff2 * rightState.OutputMem2;

            output += antiDenormal;
            output -= antiDenormal;

            rightState.InputMem2 = rightState.InputMem1;
            rightState.InputMem1 = r;
            rightState.OutputMem2 = rightState.OutputMem1;
            rightState.OutputMem1 = output;

            r = output;
        }

        public void MakeLowPass(double theta)
        {
            double alpha = Math.Sin(theta) * sqr2_2;
            double cs0 = Math.Cos(theta);
            double cs1 = 1 - cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = cs1 * _a0;
            inputCoeff2 = inputCoeff0;
            outputCoeff1 = 2 * cs0 * _a0;
            outputCoeff2 = -(1 - alpha) * _a0;
        }
        public void MakeLowPass(double theta, double q)
        {
            double alpha = Math.Sin(theta) / (q * 2);
            double cs0 = Math.Cos(theta);
            double cs1 = 1 - cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = cs1 * _a0;
            inputCoeff2 = inputCoeff0;
            outputCoeff1 = 2 * cs0 * _a0;
            outputCoeff2 = -(1 - alpha) * _a0;
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

                inputCoeff0 *= b0 * _a0;
                inputCoeff1 *= cs1 * _a0;
                inputCoeff2 *= inputCoeff0;
                outputCoeff1 *= 2 * cs0 * _a0;
                outputCoeff2 *= -(1 - alpha) * _a0;
            }
        }
        public void MakeHighPass(double theta)
        {
            double alpha = Math.Sin(theta) * sqr2_2;
            double cs0 = Math.Cos(theta);
            double cs1 = 1 + cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = -cs1 * _a0;
            inputCoeff2 = inputCoeff0;
            outputCoeff1 = -2 * cs0 * _a0;
            outputCoeff2 = -(1 - alpha) * _a0;
        }
        public void MakeHighPass(double theta, double q)
        {
            double alpha = Math.Sin(theta) / (q * 2);
            double cs0 = Math.Cos(theta);
            double cs1 = 1 + cs0;
            double b0 = cs1 * 0.5;
            double _a0 = 1 / (1 + alpha);

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = -cs1 * _a0;
            inputCoeff2 = inputCoeff0;
            outputCoeff1 = -2 * cs0 * _a0;
            outputCoeff2 = -(1 - alpha) * _a0;
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

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = b2 * _a0;
            outputCoeff1 = -a1 * _a0;
            outputCoeff2 = -a2 * _a0;
        }
        public void MakeHighShelf(double theta, double doubleSquareRootGain)
        {
            double A = doubleSquareRootGain * doubleSquareRootGain;
            double alpha = Math.Sin(theta) * sqr2_2;
            double sqrtAx2xAlpha = 2 * alpha * doubleSquareRootGain;


            double cs0 = Math.Cos(theta);
            double b0 = A * (A + 1 + (A - 1) * cs0 + sqrtAx2xAlpha);
            double b1 = -2 * A * (A - 1 + (A + 1) * cs0);
            double b2 = A * (A + 1 + (A - 1) * cs0 - sqrtAx2xAlpha);
            double _a0 = 1 / (A + 1 - (A - 1) * cs0 + sqrtAx2xAlpha);
            double a1 = 2 * (A - 1 - (A + 1) * cs0);
            double a2 = A + 1 - (A - 1) * cs0 - sqrtAx2xAlpha;

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = b2 * _a0;
            outputCoeff1 = -a1 * _a0;
            outputCoeff2 = -a2 * _a0;
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

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = b2 * _a0;
            outputCoeff1 = -inputCoeff1;
            outputCoeff2 = -a2 * _a0;
        }

        public void MakeBandPass(double theta, double halfBwInOctava)
        {
            double alpha = Math.Sin(theta) * halfBwInOctava;
            double _a0 = 1.0 / (1.0 + alpha);
            double b0 = alpha * _a0;
            double a1 = -2 * Math.Cos(theta);
            double a2 = 1 - alpha;

            inputCoeff0 = b0;
            inputCoeff1 = 0;
            inputCoeff2 = -b0;
            outputCoeff1 = -a1 * _a0;
            outputCoeff2 = -a2 * _a0;
        }
        public void MakeNotch(double theta, double halfBwInOctava)
        {
            double alpha = Math.Sin(theta) * halfBwInOctava;
            double cs0 = Math.Cos(theta);
            double b1 = -2 * cs0;
            double _a0 = 1.0 / (1.0 + alpha);
            double a1 = -2 * cs0;
            double a2 = 1 - alpha;

            inputCoeff0 = _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = _a0;
            outputCoeff1 = -a1 * _a0;
            outputCoeff2 = -a2 * _a0;
        }
        public void MakeAllPass(double theta, double q)
        {
            double alpha = Math.Sin(theta) * 0.5 / q;
            double cs0 = Math.Sin(theta);
            double b0 = 1 - alpha;
            double b1 = -2 * cs0;
            double b2 = 1 + alpha;
            double _a0 = 1 / b2;

            inputCoeff0 = b0 * _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = b2 * _a0;
            outputCoeff1 = -inputCoeff1;
            outputCoeff2 = -inputCoeff0;
        }

        public void MakeFormantFilter(double theta, double bandwidth, double gain)
        {
            double alpha = Math.Sin(theta) * Math.Sinh(Math.Log(2) / 2 * bandwidth * theta / Math.Sin(theta));
            double A = gain;

            double b0 = 1 + alpha * A;
            double b1 = -2 * Math.Cos(theta);
            double b2 = 1 - alpha * A;
            double _a0 = 1.0 / (1.0 + alpha / A);
            double a1 = -2 * Math.Cos(theta);
            double a2 = 1 - alpha / A;

            // Set filter coefficients
            inputCoeff0 = b0 * _a0;
            inputCoeff1 = b1 * _a0;
            inputCoeff2 = b2 * _a0;
            outputCoeff1 = a1 * _a0;
            outputCoeff2 = a2 * _a0;
        }
    }
}
