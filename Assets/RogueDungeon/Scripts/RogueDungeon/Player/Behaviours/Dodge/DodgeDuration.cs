using System;
using Common.Parameters;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeDuration : Parameter, IDodgeDuration
    {
        public DodgeDuration(Func<float> value) : base(value)
        {
        }
    }
}