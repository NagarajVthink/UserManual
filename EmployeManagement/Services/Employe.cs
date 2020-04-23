using EmployeManagement.Models;
using EmployeManagement.Models.Employees;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeManagemant.Services
{
    /// <summary>
    /// This class will handle Employe CURD operations.
    /// </summary>
    public class Employe
    {
        public static AppConfig _appConfig;
        public Employe(AppConfig appSetting)
        {
            _appConfig = appSetting;
        }

        /// <summary>
        /// Insert EmployeDetails, If the Employe email already exists throw error message to the Employe.
        /// </summary>
        /// <param name="request">Employe information</param>
        public ResponseModel InsertEmployeDetails(CreateEmploye request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.InsertEmployeDetails, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmployeUID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
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
                            return ReturnResponse.CreateResponse(null, "Employe Details Inserted, Employe ID:" + reader["EmployeID"].ToString().ToUpper(), true);
                        else
                            return ReturnResponse.CreateResponse(null, "Employe Email Already Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to Insert Employe Details. Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new EmployeException("Error in InsertEmployeDetails, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Update existing Employe details bu Employe UID.
        /// </summary>
        /// <param name="request">request object to update Employe information based on EmployeUID</param>
        public ResponseModel UpdateEmployeDetailByEmployeUID(UpdateEmploye request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.UpdateEmployeDetails, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmployeUID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(request.EmployeUID);
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
                            return ReturnResponse.CreateResponse(null, "EmployeUID Not Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to Update Employe Details. Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new EmployeException("Error in UpdateEmployeDetailByEmployeUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Search existing Employe details.
        /// </summary>
        /// <param name="request">Employe Request, Result will return based on page index</param>
        public ResponseModel SearchEmployeDetail(SearchRequest request)
        {
            try
            {
                var Employee = new List<SearchEmployeResponse>();
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.SearchEmploye, con))
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
                                Employee.Add(new SearchEmployeResponse
                                {
                                    EmployeUID = reader["EmployeUID"].ToString(),
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
                            if (Employee.Count() > 0)
                                return ReturnResponse.CreateResponse(Employee, "", true);
                            else
                                return ReturnResponse.CreateResponse(null, "No Data Found", false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new EmployeException("Error in Update Employe Detail By EmployeUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Search existing Employe details by EmployeUID.
        /// </summary>
        public ResponseModel SearchEmployeDetailByEmployeUID(string EmployeUID)
        {
            try
            {
                var Employee = new List<SearchEmployeResponse>();
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                using (var cmd = new SqlCommand(_appConfig.StoredProcedures.SearchEmployeByEmployeID, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeUID", SqlDbType.VarChar).Value = EmployeUID;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee.Add(new SearchEmployeResponse
                        {
                            EmployeUID = reader["EmployeUID"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            Postal = reader["Postal"].ToString(),
                            IsActive = bool.Parse(reader["Active"].ToString()),
                        });
                    }
                    if (Employee.Count() > 0)
                        return ReturnResponse.CreateResponse(Employee, "", true);
                    else
                        return ReturnResponse.CreateResponse(null, "No Data Found", false);
                }
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message);
                throw new EmployeException("Error in UpdaetEmployeDetailByEmployeUID, " + e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Update existing Employe details bu Employe UID.
        /// </summary>
        /// <param name="request">request object to update Employe information based on EmployeUID</param>
        public ResponseModel InActiveEmployeByUID(DeleteEmploye request)
        {
            try
            {
                using (var con = new SqlConnection(_appConfig.ConnectionStrings.ClientPortal))
                {
                    using (var cmd = new SqlCommand(_appConfig.StoredProcedures.DeleteEmploye, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@EmployeUID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(request.EmployeUID);
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = request.UpdatedBy;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        int updatedRows = cmd.ExecuteNonQuery();
                        if (updatedRows > 0)
                            return ReturnResponse.CreateResponse(null, "Employe Deleted.", true);
                        else
                            return ReturnResponse.CreateResponse(null, "EmployeUID Not Exist.", false);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to DeleteEmploye. Error in InActiveEmployeByUID: " + e.Message + " Request: " + JsonConvert.SerializeObject(request));
                throw new EmployeException("Error in InActiveEmployeByUID, " + e.Message, e.InnerException);
            }
        }
    }
}