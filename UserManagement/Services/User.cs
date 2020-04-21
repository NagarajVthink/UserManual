using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UserManagement.Models;
using UserManagement.Models.Users;

namespace UserManagement.Services
{
    /// <summary>
    /// This class will handle user CURD operations.
    /// </summary>
    public class User
    {
        public static AppConfig _appConfig;
        public User(AppConfig appSetting)
        {
            _appConfig = appSetting;
        }

        /// <summary>
        /// Insert UserDetails, If the User email already exists throw error message to the user.
        /// </summary>
        /// <param name="request">User information</param>
        public ResponseModel InsertUserDetails(CreateUser request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.InsertUserDetails, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserUID", SqlDbType.UniqueIdentifier).Value =  Guid.NewGuid();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = request.Email;
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = request.Phone;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = request.FirstName;
                        cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = request.MiddleName;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = request.LastName;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = request.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = request.State;
                        cmd.Parameters.Add("@Postal", SqlDbType.VarChar).Value = request.Postal;
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = request.Country;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = request.CreatedBy;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        var reader = cmd.ExecuteReader();
                        var response = reader.Read();
                        if (reader["Message"].ToString().ToUpper() == "SUCCESS")
                            return ReturnResponse.CreateResponse(null, "User Details Inserted, User ID:" + reader["UserID"].ToString().ToUpper(), true);
                        else
                            return ReturnResponse.CreateResponse(null, "User Email Already Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to Insert User Details. Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new UserException("Error in InsertUserDetails, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Update existing user details bu user UID.
        /// </summary>
        /// <param name="request">request object to update user information based on UserUID</param>
        public ResponseModel UpdateUserDetailByUserUID(UpdateUser request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.UpdateUserDetails, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserUID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(request.UserUID);
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = request.Email;
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = request.Phone;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = request.FirstName;
                        cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = request.MiddleName;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = request.LastName;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = request.City;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = request.State;
                        cmd.Parameters.Add("@Postal", SqlDbType.VarChar).Value = request.Postal;
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = request.Country;
                        cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = request.UpdatedBy;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        var updatedRows = cmd.ExecuteNonQuery();
                        if (updatedRows > 0)
                                return ReturnResponse.CreateResponse(null, "Update Successfully.", true);
                            else
                                return ReturnResponse.CreateResponse(null, "UserUID Not Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to Update User Details. Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new UserException("Error in UpdateUserDetailByUserUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Search existing user details.
        /// </summary>
        /// <param name="request">User Request, Result will return based on page index</param>
        public ResponseModel SearchUserDetail(SearchRequest request)
        {
            try
            {
                var users = new List<SearchUsersResponse>();
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.SearchUser, con))
                    {
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = request.Email;
                            cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = request.Phone;
                            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = request.FirstName;
                            cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = request.MiddleName;
                            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = request.LastName;
                            cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = request.City;
                            cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = request.State;
                            cmd.Parameters.Add("@Postal", SqlDbType.VarChar).Value = request.Postal;
                            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = request.Country;
                            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = request.IsActive;
                            cmd.Parameters.Add("@StartIndex", SqlDbType.Int).Value = request.StartIndex;
                            cmd.Parameters.Add("@EndIndex", SqlDbType.Int).Value = request.EndIndex;
                            if (con.State != ConnectionState.Open)
                                con.Open();
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                users.Add(new SearchUsersResponse
                                {
                                    UserUID = reader["UserUID"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    Postal = reader["Postal"].ToString(),
                                    IsActive = bool.Parse(reader["IsActive"].ToString()),
                                });
                            }
                            if (users.Count() > 0)
                                return ReturnResponse.CreateResponse(users, "", true);
                            else
                                return ReturnResponse.CreateResponse(null, "No Data Found", false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new UserException("Error in UpdaetUserDetailByUserUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Search existing user details by UserUID.
        /// </summary>
        public ResponseModel SearchUserDetailByUserUID(string userUID)
        {
            try
            {
                var users = new List<SearchUsersResponse>();
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                using (var cmd = new SqlCommand(_appConfig.StoredProcedures.SearchUserByUserID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserUID", SqlDbType.VarChar).Value = userUID;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        users.Add(new SearchUsersResponse
                        {
                            UserUID = reader["UserUID"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Postal = reader["Postal"].ToString(),
                            IsActive =bool.Parse(reader["Active"].ToString()),
                        });
                    }
                    if (users.Count() > 0)
                        return ReturnResponse.CreateResponse(users, "", true);
                    else
                        return ReturnResponse.CreateResponse(null, "No Data Found", false);
                }
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message);
                throw new UserException("Error in UpdaetUserDetailByUserUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Update existing user details bu user UID.
        /// </summary>
        /// <param name="request">request object to update user information based on UserUID</param>
        public ResponseModel InActiveUserByUID(DeleteUser request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.DeleteUser, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UserUID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(request.UserUID);
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = request.UpdatedBy;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        int updatedRows = cmd.ExecuteNonQuery();
                        if (updatedRows > 0)
                            return ReturnResponse.CreateResponse(null, "User Deleted.", true);
                        else
                            return ReturnResponse.CreateResponse(null, "UserUID Not Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to DeleteUser. Error in InActiveUserByUID: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new UserException("Error in InActiveUserByUID, " + e.Message, e.InnerException);
            }
        }
    }
}