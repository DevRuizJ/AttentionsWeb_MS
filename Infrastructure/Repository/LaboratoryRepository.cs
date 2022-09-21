using Application.Commom.Status;
using Application.Interfaces.Laboratory;
using Application.Interfaces.Pdf;
using Application.Pattern.Laboratory.Sample.Read.Get;
using Application.Pattern.Laboratory.Sample.Read.List;
using Application.Pattern.Laboratory.Samples.Barcode;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LaboratoryRepository : ILaboratoryRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;
        private readonly IPdfRepository _pdfRepository;

        public LaboratoryRepository(IConfiguration _configuration, IPdfRepository pdfRepository)
        {
            _connectionString = _configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
            _pdfRepository = pdfRepository;
        }

        public async Task<(ServiceStatus, string, SamplesBarcodeResponse)> SamplesBarcode(SamplesBarcodeRequest request)
        {
            var result = new SamplesBarcodeResponse();

            try
            {
                var req = new SampleListRequest();
                req.OSNumber = request.OSNumber;
                req.Period = request.Period;
                req.Year = request.Year;
                req.BranchOffice = request.BranchOffice;

                var (status, message, response) = await SampleList(req);

                switch (status)
                {
                    case ServiceStatus.FailedValidation:
                    case ServiceStatus.InternalError:
                        return (ServiceStatus.InternalError, "Servicio de gestión de reportes no se encuentra disponible en este momento", null);
                    default:
                        break;
                }

                var (status2, msg, resp) = await _pdfRepository.SampleBarcodeList(response);

                if (message != null)
                {
                    return (status2, msg, null);
                }

                result.pdf = resp;
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: SamplesBarcode ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, result);
        }

        public async Task<(ServiceStatus, string, SampleListResponse)> SampleList(SampleListRequest request)
        {
            var response = new SampleListResponse();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_SAMPLE_LIST_BARCODE", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("P_NUMOSCAB", OracleDbType.Varchar2).Value = request.OSNumber;
                    cmd.Parameters.Add("P_PEROSCAB", OracleDbType.Varchar2).Value = request.Period;
                    cmd.Parameters.Add("P_ANOOSCAB", OracleDbType.Varchar2).Value = request.Year;
                    cmd.Parameters.Add("P_NUMSUC", OracleDbType.Varchar2).Value = request.BranchOffice;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        response.Samples = new List<SampleResponse>();

                        while (rdr.Read())
                        {
                            response.Patient = rdr["PACIENTE"].ToString();
                            response.Sex = rdr["SEXO"].ToString();
                            response.Age = rdr["EDAD"].ToString();

                            response.Samples.Add(new SampleResponse()
                            {
                                Codigo = rdr["NUMERO_ORDEN"].ToString() + '-' + rdr["CODIGO_MUESTRA"].ToString(),
                                Descripcion = rdr["DESCRIPCION"].ToString(),
                                Tubo = rdr["TAPA"].ToString()
                            });
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: SampleList ... {ex.Message}", response);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
