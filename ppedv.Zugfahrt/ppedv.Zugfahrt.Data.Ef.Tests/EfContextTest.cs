using Xunit;

namespace ppedv.Zugfahrt.Data.Ef.Tests
{
    public class EfContextTest
    {
        [Fact]
        public void Can_create_DB()
        {
            var con = new EfContext();

            if (con.Database.Exists())
                con.Database.Delete();

            con.Database.Create();

            Assert.True(con.Database.Exists());
        }

    }
}
