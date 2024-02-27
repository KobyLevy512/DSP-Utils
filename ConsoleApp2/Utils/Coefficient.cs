
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
