using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows;

namespace EaseMoveDemo
{
    public class EaseMoveAnimation : DoubleAnimationBase
    {

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From", typeof(double?), typeof(EaseMoveAnimation), new PropertyMetadata(null));

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To", typeof(double?), typeof(EaseMoveAnimation), new PropertyMetadata(null));

        public double? From
        {
            get
            {
                return (double?)this.GetValue(EaseMoveAnimation.FromProperty);
            }
            set
            {
                this.SetValue(EaseMoveAnimation.FromProperty, value);
            }
        }

        public double? To
        {
            get
            {
                return (double?)this.GetValue(EaseMoveAnimation.ToProperty);
            }
            set
            {
                this.SetValue(EaseMoveAnimation.ToProperty, value);
            }
        }

        //圆形缓动
        protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, AnimationClock animationClock)
        {
            double from = (this.From == null ? defaultDestinationValue : (double)this.From);
            double to = (this.To == null ? defaultOriginValue : (double)this.To);
            double delta = to - from;
            double value = animationClock.CurrentProgress.Value;

            double t = value * this.Duration.TimeSpan.Ticks;
            double d = this.Duration.TimeSpan.Ticks;

            //加速
            //return delta * (1-Math.Sqrt(1-(t/=d)*t)) + from;

            //减速
            //return delta * Math.Sqrt(1 - (t = t / d - 1) * t) + from;

            //先加速,后减速
            if ((t /= (d / 2)) < 1)
            {
                return delta / 2 * (1 - Math.Sqrt(1 - t * t)) + from;
            }
            return delta / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + from;

        }


        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new EaseMoveAnimation();
        }
    }
}
