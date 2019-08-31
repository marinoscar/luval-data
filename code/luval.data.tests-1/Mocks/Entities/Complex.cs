﻿using System;
using System.Collections.Generic;
using System.Text;

namespace luval.data.tests.Mocks.Entities
{
    [TableName("ComplextTable")]
    public class Complex
    {
        [ColumnName("ComplexId")]
        public int Id { get; set; }
        [ColumnName("ComplexName")]
        public string Name { get; set; }
        [ColumnName("ComplexValue")]
        public double Value { get; set; }
        [ColumnName("ComplexUpdatedOn")]
        public DateTime UpdatedOn { get; set; }
    }
}