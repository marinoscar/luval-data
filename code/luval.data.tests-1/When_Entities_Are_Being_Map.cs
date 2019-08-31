using luval.data.tests.Mocks.Data;
using luval.data.tests.Mocks.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace luval.data.tests
{
    [TestFixture]
    public class When_Entities_Are_Being_Map
    {
        [Test]
        public void It_Should_Map_Elements_Without_Custom_Attributes()
        {
            var entity = new Simple() { Name = "Oscar Marin", UpdatedOn = DateTime.Today };
            var record = new DictionaryDataRecord(new Dictionary<string, object>() {
                { "Id", entity.Id },
                { "Name", entity.Name },
                { "Value", entity.Value },
                { "UpdatedOn", entity.UpdatedOn },
            });
            var db = new Database(() => new MConnection() { Records = new List<System.Data.IDataRecord>(new[] { record }) });
            var res = db.ExecuteToEntityList<Simple>("");
            Assert.AreEqual(entity, res.Single());
        }
    }
}
