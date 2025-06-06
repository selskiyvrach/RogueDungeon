using System;

namespace Libs.Movesets
{
    public interface IMoveIdToTypeConverter
    {
        Type GetMoveType(string id);
    }
}