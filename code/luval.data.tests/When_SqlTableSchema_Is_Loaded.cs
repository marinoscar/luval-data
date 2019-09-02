using Luval.Data.tests.Mocks.Data;
using Luval.Data.tests.Mocks.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Data.tests
{
    [TestFixture]
    public class When_SqlTableSchema_Is_Loaded
    {
        [Test]
        public void It_Should_Properly_Load_The_Schema()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today,
                DoNotMap = "Do Not Map",
                DoNotMap2 = "Do Not Map2"
            };
            var schema = SqlTableSchema.Load(typeof(Complex));
            Assert.AreEqual("ComplextTable", schema.TableName);
            Assert.AreEqual(4, schema.Columns.Count);
            Assert.AreEqual("ComplexId", schema.Columns[0].Name);
            Assert.AreEqual("ComplexName", schema.Columns[1].Name);
            Assert.AreEqual("ComplexValue", schema.Columns[2].Name);
            Assert.AreEqual("ComplexUpdatedOn", schema.Columns[3].Name);
        }
    }
}
