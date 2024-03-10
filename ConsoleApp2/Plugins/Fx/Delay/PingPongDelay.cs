

using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class PingPongDelay : PluginBase
    {
        MonoDelay left, right, rightOffset;

        public PingPongDelay()
        {
            left = new MonoDelay();
            right = new MonoDelay();
            rightOffset = new MonoDelay();
        }

        /// <summary>
        /// Set the feedback of this ping pong.
        /// </summary>
        public double Feedback
        {
            get => left.Feedback;
            set
            {
                left.Feedback = value;
                right.Feedback = value;
            }
        }
        
        /// <summary>
        /// Set the mix of this ping pong.
        /// </summary>
        public double Mix
        {
            get => left.Mix;
            set
            {
                left.Mix = value;
                right.Mix = value;
            }
        }

        /// <summary>
        /// Set the delay quantize.
        /// </summary>
        public Quantize Quantize
        {
            set
            {
                left.Quantize = value;
                right.Quantize = value;
                rightOffset.Quantize = value;
            }
        }
        public override void Process(ref double l, ref double r)
        {
            left.Process(ref l);
            double right = r;
            rightOffset.Process(ref right);
            this.right.Process(ref right);
        }
    }
}
