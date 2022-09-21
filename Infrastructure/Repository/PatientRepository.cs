using Application.Commom.Status;
using Application.Interfaces.Patient;
using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.Verify;
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
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;

        public PatientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
        }

        public async Task<(ServiceStatus, string, VerifyPatientResponse)> VerifyPatient(VerifyPatientRequest request)
        {
            var response = new VerifyPatientResponse();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_VERIFY_PATIENT", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // InParameter
                    cmd.Parameters.Add("xxNUMTDOC", OracleDbType.Varchar2).Value = request.TipoDoc;
                    cmd.Parameters.Add("xxNUMDOC", OracleDbType.Varchar2).Value = request.NumeroDoc;
                    cmd.Parameters.Add("xxNOMPAC", OracleDbType.Varchar2).Value = request.Nombres;
                    cmd.Parameters.Add("xxPATPAC", OracleDbType.Varchar2).Value = request.ApellidoPaterno;
                    cmd.Parameters.Add("xxMATPAC", OracleDbType.Varchar2).Value = request.ApellidoMaterno;
                    cmd.Parameters.Add("xxAPECAS", OracleDbType.Varchar2).Value = request.ApellidoCasada;
                    cmd.Parameters.Add("xxFNACPAC", OracleDbType.Varchar2).Value = request.FechaNacimiento;
                    cmd.Parameters.Add("xxSEXPAC", OracleDbType.Varchar2).Value = request.Sexo;
                    cmd.Parameters.Add("xxTELMOVIL", OracleDbType.Varchar2).Value = request.TelefonoMovil1;
                    cmd.Parameters.Add("xxEMAPAC", OracleDbType.Varchar2).Value = request.Email;
                    cmd.Parameters.Add("xxUSUMOD", OracleDbType.Varchar2).Value = request.UsuarioModificacion;

                    //OutParameter
                    cmd.Parameters.Add("xxRP_NUMPAC", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("xxRP_PACANO", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("RESULTADO", OracleDbType.Int16).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MENSAJE", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    response.PatientId = cmd.Parameters["xxRP_NUMPAC"].Value.ToString();
                    response.PatientYear = cmd.Parameters["xxRP_PACANO"].Value.ToString();
                    response.Resultado = int.Parse(cmd.Parameters["Resultado"].Value.ToString());
                    response.Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: VerifyPatient ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
