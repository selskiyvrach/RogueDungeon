using RogueDungeon.Parameters;

namespace Common.Parameters
{
    public interface IParameters
    {
        float Get(Key key);
    }
}