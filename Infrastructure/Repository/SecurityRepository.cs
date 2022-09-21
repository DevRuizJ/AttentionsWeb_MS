using Application.Commom.Helpers;
using Application.Commom.Status;
using Application.Interfaces.Security;
using Application.Pattern.Auth;
using Application.Pattern.ValidateUser;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    internal class SecurityRepository : ISecurityRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;

        public SecurityRepository(IConfiguration configuration/*, ILogService logService*/)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
        }

        public async Task<(ServiceStatus, string, AuthResponse)> Authentication(AuthRequest request)
        {
            var response = new AuthResponse();

            try
            {
                var valReq = new ValidateUserRequest();
                valReq.user = request.User;
                valReq.password = request.Password;

                var (statusVal, messageVal, validate) = await ValidateUser(valReq);

                if (statusVal != ServiceStatus.Ok)
                    return (statusVal, messageVal, null);


                if (messageVal != null)
                    return (statusVal, messageVal, null);        

                response.User = validate.Usuario;
                response.Name = validate.Nombres;
                response.LastName = validate.ApellidoPaterno;
                response.MotherLastName = validate.ApellidoMaterno;
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: Function 'Authentication' ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, ValidateUserResponse)> ValidateUser(ValidateUserRequest request)
        {
            var response = new ValidateUserResponse();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_VALIDATE_CREDENTIALS", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("P_USER", OracleDbType.Varchar2).Value = request.user;
                    cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = Security.Encrypt(request.password.ToUpper());

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            response.Usuario = dr["USER"].ToString();
                            response.Nombres = dr["NAME"].ToString();
                            response.ApellidoPaterno = dr["LAST_NAME"].ToString();
                            response.ApellidoMaterno = dr["MOTHER_LASTNAME"].ToString();
                            response.ValUser = Convert.ToInt16(dr["USER_VAL"]);
                            response.ValPassword = Convert.ToInt16(dr["PASS_VAL"]);
                        }
                    }

                    cmd.Dispose();

                    if ((DataStatus)response.ValUser == DataStatus.Inoperative)
                        return (ServiceStatus.FailedValidation, "Credeciales de usuario Inactivas.", null);

                    if ((DataStatus)response.ValPassword == DataStatus.NotExists)
                        return (ServiceStatus.FailedValidation, "Contraseña incorrecta.", null);
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: Function 'ValidateUser' ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
