using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RegistrationAPI.Models;

namespace RegistrationAPI.Services
{
    public class RegistrationService
    {
        private static string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        
        public static int RegisterUser(RegistrationModel objRegistrationModel)
        {
            int noOfRowsAffected = 0;
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ConnStr))
                {
                    using (SqlCommand objSqlCommand = new SqlCommand())
                    {

                        objSqlCommand.CommandText = "Proc_Register";
                        objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        objSqlCommand.Connection = objSqlConnection;

                        objSqlConnection.Open();

                        objSqlCommand.Parameters.AddWithValue("@RegID", objRegistrationModel.RegID);
                        objSqlCommand.Parameters.AddWithValue("@FirstName", objRegistrationModel.FirstName);
                        objSqlCommand.Parameters.AddWithValue("@LastName", objRegistrationModel.LastName);
                        objSqlCommand.Parameters.AddWithValue("@Address", objRegistrationModel.Address);
                        objSqlCommand.Parameters.AddWithValue("@UserName", objRegistrationModel.UserName);
                        objSqlCommand.Parameters.AddWithValue("@Password", objRegistrationModel.Password);
                        objSqlCommand.Parameters.AddWithValue("@ProfilePhoto", objRegistrationModel.ProfilePhoto);

                        noOfRowsAffected = objSqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
                return noOfRowsAffected;
        }
    }
}