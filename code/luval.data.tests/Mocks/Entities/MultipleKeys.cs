using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.data.tests.Mocks.Entities
{
    public class MultipleKeys
    {
        [PrimaryKey]
        public object IntKey { get; set; }
        [PrimaryKey]
        public string StrKey { get; set; }
        public string Data { get; set; }
        [NotMapped]
        public string DoNotMap { get; set; }
    }
}
