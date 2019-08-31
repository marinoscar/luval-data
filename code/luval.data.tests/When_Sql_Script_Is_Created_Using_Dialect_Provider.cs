using luval.data.tests.Mocks.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.data.tests
{
    [TestFixture]
    public class When_Sql_Script_Is_Created_Using_Dialect_Provider
    {
        [Test]
        public void It_Should_Properly_Generate_A_Read_Statement()
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
            var provider = new SqlServerDialectProvider(entity);
            var statement = provider.GetReadCommand().Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("SELECT [ComplexId], [ComplexName], [ComplexValue], [ComplexUpdatedOn] FROM [ComplextTable] WHERE [ComplexId] = {0};", entity.Id)
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Create_Statement()
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
            var provider = new SqlServerDialectProvider(entity);
            var statement = provider.GetCreateCommand().Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("INSERT ([ComplexName], [ComplexValue], [ComplexUpdatedOn]) INTO [ComplextTable]  VALUES({0}, {1}, {2});",
                entity.Name.ToSql(), entity.Value.ToSql(), entity.UpdatedOn.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Update_Statement()
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
            var provider = new SqlServerDialectProvider(entity);
            var statement = provider.GetUpdateCommand().Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("UPDATE [ComplextTable] SET [ComplexName] = {0}, [ComplexValue] = {1}, [ComplexUpdatedOn] = {2} WHERE [ComplexId] = {3};",
                entity.Name.ToSql(), entity.Value.ToSql(), entity.UpdatedOn.ToSql(), entity.Id.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }
    }
}
