using luval.data.tests.Mocks.Data;
using luval.data.tests.Mocks.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace luval.data.tests
{
    [TestFixture]
    public class When_Entities_Are_Being_Map
    {
        [Test]
        public void It_Should_Map_Elements_Without_Custom_Attributes()
        {
            var entity = new Simple() {
                Id = new Random().Next(), Name = "Sample Text",
                Value = new Random().NextDouble(), UpdatedOn  = DateTime.Today };
            var record = new DictionaryDataRecord(new Dictionary<string, object>() {
                { "Id", entity.Id },
                { "Name", entity.Name },
                { "Value", entity.Value },
                { "UpdatedOn", entity.UpdatedOn },
            });
            var db = new Database(() => new MConnection() { Records = new List<System.Data.IDataRecord>(new[] { record }) });
            var res = db.ExecuteToEntityList<Simple>("");
            Assert.AreEqual(entity.Id, res.Single().Id);
            Assert.AreEqual(entity.Name, res.Single().Name);
            Assert.AreEqual(entity.Value, res.Single().Value);
            Assert.AreEqual(entity.UpdatedOn, res.Single().UpdatedOn);
        }

        [Test]
        public void It_Should_Map_Elements_With_Custom_Attributes()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today
            };
            var record = new DictionaryDataRecord(new Dictionary<string, object>() {
                { "ComplexId", entity.Id },
                { "ComplexName", entity.Name },
                { "ComplexValue", entity.Value },
                { "ComplexUpdatedOn", entity.UpdatedOn },
            });
            var db = new Database(() => new MConnection() { Records = new List<IDataRecord>(new[] { record }) });
            var res = db.ExecuteToEntityList<Complex>("");
            Assert.AreEqual(entity.Id, res.Single().Id);
            Assert.AreEqual(entity.Name, res.Single().Name);
            Assert.AreEqual(entity.Value, res.Single().Value);
            Assert.AreEqual(entity.UpdatedOn, res.Single().UpdatedOn);
        }

        [Test]
        public void It_Should_Ignore_Fields_Not_Present_In_The_Entity()
        {
            var entity = new Simple()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today
            };
            var record = new DictionaryDataRecord(new Dictionary<string, object>() {
                { "Id", entity.Id },
                { "Name", entity.Name },
                { "Value", entity.Value },
                { "UpdatedOn", entity.UpdatedOn },
                { "ExtraField", Guid.NewGuid() },
            });
            var db = new Database(() => new MConnection() { Records = new List<IDataRecord>(new[] { record }) });
            var res = db.ExecuteToEntityList<Simple>("");
            Assert.AreEqual(entity.Id, res.Single().Id);
            Assert.AreEqual(entity.Name, res.Single().Name);
            Assert.AreEqual(entity.Value, res.Single().Value);
            Assert.AreEqual(entity.UpdatedOn, res.Single().UpdatedOn);
        }

        [Test]
        public void It_Should_Not_Fields_That_Are_Not_Named_Correctly()
        {
            var entity = new Simple()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today
            };
            var record = new DictionaryDataRecord(new Dictionary<string, object>() {
                { "WrongId", entity.Id },
                { "Name", entity.Name },
                { "Value", entity.Value },
                { "UpdatedOn", entity.UpdatedOn },
            });
            var db = new Database(() => new MConnection() { Records = new List<IDataRecord>(new[] { record }) });
            var res = db.ExecuteToEntityList<Simple>("");
            Assert.AreEqual(0, res.Single().Id, "Need to match the default value of the Id field");
            Assert.AreEqual(entity.Name, res.Single().Name);
            Assert.AreEqual(entity.Value, res.Single().Value);
            Assert.AreEqual(entity.UpdatedOn, res.Single().UpdatedOn);
        }
    }
}
