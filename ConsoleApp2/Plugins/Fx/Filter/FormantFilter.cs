
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Filter
{
    public class FormantFilter : PluginBase
    {
        public enum Vowel
        {
            A,
            E,
            I,
            O,
            U
        }
        static Coefficient[] vowels =
        [
            //A
            new Coefficient([8.11044e-06, 
            8.943665402,        -36.83889529,   92.01697887,    -154.337906,    181.6233289,
            -151.8651235,   89.09614114,        -35.10298511,   8.388101016,    -0.923313471 ]),

            //E
            new Coefficient([4.36215e-06,
            8.90438318, -36.55179099,   91.05750846,    -152.422234,    179.1170248,  
            -149.6496211,87.78352223,   -34.60687431,   8.282228154,    -0.914150747]),

            //I
            new Coefficient([3.33819e-06,
            8.893102966,        -36.49532826,   90.96543286,    -152.4545478,   179.4835618,
            -150.315433,        88.43409371,    -34.98612086,   8.407803364,    -0.932568035]),

            //O
            new Coefficient([1.13572e-06,
            8.994734087,        -37.2084849,    93.22900521,    -156.6929844,   184.596544,   
            -154.3755513,       90.49663749,    -35.58964535,   8.478996281,    -0.929252233]),

            //U
            new Coefficient([4.09431e-07,
            8.997322763,        -37.20218544,   93.11385476,    -156.2530937,   183.7080141, 
            -153.2631681,       89.59539726,    -35.12454591,   8.338655623,    -0.910251753]),
        ];
        static double[,] coeff = new double[5,11] {
            { 8.11044e-06,
            8.943665402,        -36.83889529,   92.01697887,    -154.337906,    181.6233289,
            -151.8651235,   89.09614114,        -35.10298511,   8.388101016,    -0.923313471  ///A
            },
            {4.36215e-06,
            8.90438318, -36.55179099,   91.05750846,    -152.422234,    179.1170248,  ///E
            -149.6496211,87.78352223,   -34.60687431,   8.282228154,    -0.914150747
            },
            { 3.33819e-06,
            8.893102966,        -36.49532826,   90.96543286,    -152.4545478,   179.4835618,
            -150.315433,        88.43409371,    -34.98612086,   8.407803364,    -0.932568035  ///I
            },
            {1.13572e-06,
            8.994734087,        -37.2084849,    93.22900521,    -156.6929844,   184.596544,   ///O
            -154.3755513,       90.49663749,    -35.58964535,   8.478996281,    -0.929252233
            },
            {4.09431e-07,
            8.997322763,        -37.20218544,   93.11385476,    -156.2530937,   183.7080141,  ///U
            -153.2631681,       89.59539726,    -35.12454591,   8.338655623,    -0.910251753
            }
        };
        int vowel;
        double[,] memory = new double[2,10];
        double[,] coff = new double[5, 11];
        public FormantFilter(Vowel vowel)
        {
            this.vowel = (int)vowel;
            for(int y = 0; y<5; y++)
            {
                for(int x = 0; x<11;x++)
                {
                    coff[y,x] = coeff[y,x];
                }
            }
        }

        public void SetCutoff(double cutoff)
        {
            cutoff = cutoff / 5000.0;
            int vowelLow = (int)cutoff;
            cutoff -= vowelLow;
            if(vowelLow == 4)
            {
                for (int i = 0; i < 11; i++)
                {
                    coff[vowel, i] = coeff[vowelLow, i];
                }
                return;
            }
            for (int i = 0; i < 11; i++)
            {
                coff[vowel, i] = EMath.Lerp(coeff[vowelLow, i], coeff[vowelLow + 1, i], cutoff);
            }
        }
        public override void Process(ref double l, ref double r)
        {
            double sumL = 0;
            double sumR = 0;
            for(int i = 0; i < 10; i++)
            {
                sumL += coff[vowel, i + 1] * memory[0, i];
                sumR += coff[vowel, i + 1] * memory[1, i];
            }
            l = coff[vowel, 0] * l + sumL;
            r = coff[vowel, 0] * r + sumR;
            for(int i = 9; i > 0; i--)
            {
                memory[0,i] = memory[0,i - 1];
                memory[1,i] = memory[1,i - 1];
            }
            memory[0,0] = l;
            memory[1,0] = r;
        }
    }
}
