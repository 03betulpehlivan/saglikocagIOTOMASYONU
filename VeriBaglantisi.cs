// ======================= VeriBaglantisi.cs =======================
using System.Data.SqlClient;

namespace SaglikOcagiOtomasyon
{
    public static class VeriBaglantisi
    {
        public static string Yol =
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=SaglikOcagiDB;Integrated Security=True";

        public static SqlConnection BaglantiAl()
        {
            return new SqlConnection(Yol);
        }
    }
}
