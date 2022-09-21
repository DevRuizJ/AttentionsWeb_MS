using Application.Commom.Status;
using Application.Interfaces.BranchOffice;
using Application.Pattern.BranchOffice.Read.Get;
using Application.Pattern.BranchOffice.Read.List;
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
    public class BranchOfficeRepository : IBranchOfficeRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;

        public BranchOfficeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
        }

        public async Task<(ServiceStatus, string, BranchOfficeListResponse)> BranchOfficeList(BranchOfficeListRequest request)
        {
            var response = new BranchOfficeListResponse();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_SEARCH_BRANCHOFFICE", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        response.BranchOfficeList = new List<BranchOfficeResponse>();

                        while (rdr.Read())
                        {
                            var branchOffice = new BranchOfficeResponse();
                            branchOffice.NumSuc = rdr["NUMSUC"].ToString();
                            branchOffice.Nombre = rdr["NOMCOR"].ToString();

                            response.BranchOfficeList.Add(branchOffice);
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: BranchOfficeList ... {ex.Message}", response);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
