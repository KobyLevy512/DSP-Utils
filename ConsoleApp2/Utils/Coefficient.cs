
using ConsoleApp2.Plugins;

namespace ConsoleApp2.Utils
{
    public class Coefficient : PluginBase
    {
        const double antiDenormal = 1e-30;
        double[] coefInput, coefOutput;
        double[] memInputs, memOutputs;
        public Coefficient(int size) 
        {
            if (size < 2) throw new ArgumentException("Size must be bigger than 1.");
            float half = size / 2.0f;
            coefInput = new double[(int)Math.Ceiling(half)];
            coefOutput = new double[(int)half];
            half--;
            memInputs = new double[(int)Math.Ceiling(half)];
            memOutputs = new double[(int)Math.Ceiling(half)];
        }
        public Coefficient(double[] arr)
        {
            int size = arr.Length;
            if (size < 2) throw new ArgumentException("Size must be bigger than 1.");
            float half = size / 2.0f;
            coefInput = new double[(int)Math.Ceiling(half)];
            coefOutput = new double[(int)half];
            half--;
            memInputs = new double[(int)Math.Ceiling(half)];
            memOutputs = new double[(int)Math.Ceiling(half)];

            int index = 0;
            for(int i= 0; i< coefInput.Length; i++)
            {
                coefInput[i] = arr[index++];
                if(i < coefOutput.Length)
                    coefOutput[i] = arr[index++];
            }
        }

        public (double input, double output) this[int x, int y]
        {
            get => (coefInput[x], coefOutput[y]);
            set
            {
                coefInput[x] = value.input;
                coefOutput[y] = value.output;
            }
        }

        public void Copy(Coefficient src)
        {
            for(int i = 0; i<coefInput.Length; i++)
            {
                coefInput[i] = src.coefInput[i];
            }
            for (int i = 0; i < coefOutput.Length; i++)
            {
                coefOutput[i] = src.coefOutput[i];
            }
        }
        /// <summary>
        /// Linear interpolate between 2 coefficient.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Coefficient Lerp(Coefficient l, Coefficient r, double value)
        {
            Coefficient ret = new Coefficient(l.coefInput.Length + l.coefOutput.Length);
            for (int i = 0; i < ret.coefInput.Length; i++)
            {
                ret[i, 0] = (EMath.Lerp(l.coefInput[0], r.coefInput[0], value), 0);
            }
            for (int i = 0; i < ret.coefOutput.Length; i++)
            {
                ret[0, i] = (ret[0, 0].input, EMath.Lerp(l.coefOutput[0], r.coefOutput[0], value);
            }
            return ret;
        }
        public override void Process(ref double mono)
        {
            double sum = 0;
            for(int i = 0; i < coefOutput.Length; i++)
            {
                sum += coefOutput[i] * memOutputs[i];
            }
            for (int i = 1; i < coefInput.Length; i++)
            {
                sum += coefInput[i] * memInputs[i - 1];
            }
            sum += coefInput[0] * mono;
            sum += antiDenormal;
            sum -= antiDenormal;
            for (int i = memOutputs.Length - 1; i > 0 ; i--)
            {
                memOutputs[i] = memOutputs[i - 1];
            }
            for (int i = memInputs.Length - 1; i > 0; i--)
            {
                memInputs[i] = memInputs[i - 1];
            }
            memOutputs[0] = sum;
            memInputs[0] = mono;
            mono = sum;

        }

        public override void Process(ref double l, ref double r)
        {
            Process(ref l);
            Process(ref r);
        }
    }
}
