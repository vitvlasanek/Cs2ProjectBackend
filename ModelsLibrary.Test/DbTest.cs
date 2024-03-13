using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitvlasanek.Cs2.Project.Backend.ORM.CrudOperations;
using Vitvlasanek.Cs2.Project.Backend.ORM.Sessions;

namespace Vitvlasanek.Cs2.Project.Backend.ModelsLibrary.Test
{
    [TestClass]
    public class DbTest
    {
        const string CONNECTION_STRING = @"Data Source=testdatabase.db";

        [TestMethod] 
        public void TestCreate() 
        {

            // Create connection string
            using var connection = new SQLiteConnection(CONNECTION_STRING);
            using BaseDbSession session = new BaseDbSession(connection);

            List<Class1> dtos = new() { new Class1 { Description = "Test1", Name = "Test1" }, new Class1 { Description = "Test2", Name = "Test2" } };
            var io = new InsertOperation<Class1>() { RowDataGateways = dtos };

            connection.Open();
            io.Execute(connection);
            connection.Close();
        }

        [TestMethod]
        public void TestSelect()
        {
            using var connection = new SQLiteConnection(CONNECTION_STRING);
            using BaseDbSession session = new BaseDbSession(connection);


            connection.Open();
            var so = new SelectOperation<Class1>();

            var res = so.Execute(connection);
            connection.Close();

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType<IEnumerable<Class1>>(res);

        }

        [TestMethod]
        public void TestUpdate()
        {
            using var connection = new SQLiteConnection(CONNECTION_STRING);
            using BaseDbSession session = new BaseDbSession(connection);


            connection.Open();
            var so = new SelectOperation<Class1>();

            var res = so.Execute(connection);
            Assert.IsNotNull(res);

            var instance = res.First();

            var random = new Random();



            var randName = random.Next().ToString();
            instance.Name = randName;

            var uo = new UpdateOperation<Class1>() { Instance = instance};

            uo.Execute(connection);

            var res2 = so.Execute(connection);

            Assert.AreEqual(randName, res2.First().Name);

            Assert.IsInstanceOfType<IEnumerable<Class1>>(res);

            connection.Close();
        }

        //[TestMethod]
        //public void TestUpdate() { }

        //[TestMethod]
        //public void TestDelete() 
        //{
        //    using var connection = new SQLiteConnection(CONNECTION_STRING);

        //    using BaseDbSession session = new BaseDbSession(connection);

        //    session.Delete(new Class1 { Description = "", Name = "Test" });


        //}


        //[TestMethod]
        //public void TestSelect() 
        //{
        //    using var connection = new SQLiteConnection(CONNECTION_STRING);

        //    using BaseDbSession session = new BaseDbSession(connection);

        //    Class1.SelectAsQuery().ToString();


        //}





    }
}
