using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public interface ITextSerializer
    {
        string Serialize(object objectToSerialize, bool includePrivateProperties = true);
        object Deserialize(string payload, bool includePrivateProperties = true);
    }
}
