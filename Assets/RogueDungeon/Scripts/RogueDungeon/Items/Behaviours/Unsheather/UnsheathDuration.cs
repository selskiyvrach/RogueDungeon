using System;
using Common.Parameters;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheathDuration : Parameter, IUnsheathDuration
    {
        public UnsheathDuration(Func<float> value) : base(value)
        {
        }
    }
}