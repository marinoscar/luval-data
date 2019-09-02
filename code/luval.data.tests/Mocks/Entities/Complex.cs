using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.tests.Mocks.Entities
{
    [TableName("ComplextTable")]
    public class Complex
    {
        [ColumnName("ComplexId"), IdentityColumn, PrimaryKey]
        public int Id { get; set; }
        [ColumnName("ComplexName")]
        public string Name { get; set; }
        [ColumnName("ComplexValue")]
        public double Value { get; set; }
        [ColumnName("ComplexUpdatedOn")]
        public DateTime UpdatedOn { get; set; }
        [NotMapped]
        public string DoNotMap { get; set; }
        [NotMapped, ColumnName("ComplexDoNotMapped2")]
        public string DoNotMap2 { get; set; }
    }
}
