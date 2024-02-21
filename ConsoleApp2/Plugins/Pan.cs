

namespace ConsoleApp2.Plugins
{
    public class Pan : PluginBase
    {
        double left = 1, right = 1;

        /// <summary>
        /// Set the value of this panning.
        /// [-1,1] where 0 is center, 1 is right and -1 is left.
        /// </summary>
        public double Value
        {
            get => right - left;
            set
            {
                if (value >= 0)
                {
                    right = 1;
                    left = 1 - value;
                }
                else
                {
                    left = 1;
                    right = 1 + value;
                }
            }
        }
        public override void Process(ref double l, ref double r)
        {
            l *= left;
            r *= right;
        }
    }
}
