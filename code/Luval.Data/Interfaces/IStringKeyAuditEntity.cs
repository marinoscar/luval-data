using Luval.Data.Attributes;
using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies a <see cref="IAuditableEntity{TKey}"/> with a <see cref="string"/> type for the Id property
    /// </summary>
    public interface IStringKeyAuditEntity : IAuditableEntity<string>
    {

    }
}
