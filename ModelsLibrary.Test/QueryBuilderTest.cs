namespace ModelsLibrary.Test
{
    [TestClass]
    public class QueryBuilderTest
    {
        [TestMethod]
        public void TestSelect()
        {
            var x = new Class1 { Id = 1, Description = "des", Name = "name" };

            var selectString = x.GenerateSelectQuery();

            Assert.IsNotNull(selectString);
            Console.WriteLine(selectString);
        }
    }
}