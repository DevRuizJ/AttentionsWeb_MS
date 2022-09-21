using Application.Commom.Status;
using Application.Interfaces.Company;
using Application.Pattern.Company.Read.Get;
using Application.Pattern.Company.Read.List;
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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;

        public CompanyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
        }

        public async Task<(ServiceStatus, string, CompanyListResponse)> CompanyList(CompanyListRequest request)
        {
            var response = new CompanyListResponse();
            response.CompanyList = new List<CompanyResponse>();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_SEARCH_COMPANIAS", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var company = new CompanyResponse();
                            company.Descripcion = rdr["COMPANIA"].ToString();
                            company.Seekcia = rdr["CODIGO"].ToString();

                            response.CompanyList.Add(company);
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: CompanyList ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
