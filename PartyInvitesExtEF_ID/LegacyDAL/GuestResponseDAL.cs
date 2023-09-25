using Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LegacyDAL;
public class GuestResponseDAL
{
    private string _connectionString;

    public GuestResponseDAL(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<GuestResponse> GetGuestResponses()
    {
        using (SqlConnection oConn = new SqlConnection(_connectionString))
        {
            string strSql = "SELCT * FROM guestresponses ORDER BY name";
            SqlCommand oCmd = new SqlCommand(strSql, oConn);
            oConn.Open();
            // execute query
            SqlDataReader oReader = oCmd.ExecuteReader();
            List<GuestResponse> guestResponses = null;
            guestResponses = GetGuestResponseCollectionFromReader(oReader);
            oReader.Close();
            return guestResponses;
        }
    }

    protected GuestResponse GetGuestResponseFromReader(IDataRecord oReader)
    {
        GuestResponse guestResponse = new GuestResponse();
        guestResponse.Id = (int)oReader["Id"];
        guestResponse.Name = oReader.GetString(1);
        guestResponse.Email = (string)oReader["email"];
        guestResponse.Phone = (string)oReader["phone"];
        if (oReader["willattent"] != DBNull.Value)
            guestResponse.WillAttent = (bool)oReader["willattent"];
        
        return guestResponse;
    }

    protected List<GuestResponse> GetGuestResponseCollectionFromReader(IDataReader oReader)
    {
        List<GuestResponse> guestResponses = new List<GuestResponse>();
        while (oReader.Read())
            // read line by line and map to an object
            guestResponses.Add(GetGuestResponseFromReader(oReader));

        return guestResponses;
    }

}
