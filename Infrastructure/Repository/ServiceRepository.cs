using Application.Commom.Status;
using Application.Interfaces.Service;
using Application.Pattern.Service.Group.List;
using Application.Pattern.Service.ListXGroup;
using Application.Pattern.Service.Read.Filter;
using Application.Pattern.Service.Read.Get;
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
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;

        public ServiceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
        }
        public async Task<(ServiceStatus, string, IEnumerable<ServiceGroupListResponse>)> GetServiceGroupList(ServiceGroupListRequest request)
        {
            List<ServiceGroupListResponse> listGrupos = null;
            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_LAB_EXAM_TYPE_LIST", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxnocia", OracleDbType.Varchar2).Value = request.IdCompania;
                    cmd.Parameters.Add("xxvalor", OracleDbType.Char).Value = request.Tipo;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        listGrupos = new List<ServiceGroupListResponse>();

                        while (rdr.Read())
                        {
                            listGrupos.Add(new ServiceGroupListResponse
                            {
                                Codigo = rdr["ID"].ToString(),
                                Descripcion = rdr["DESCRIPCION"].ToString().Trim(),
                            });
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                //await _logService.RegistrarLog(new LogEntity { Mensaje = "ServiciosRepository > GetListGruposServicio > " + ex.Message, Nivel = 0 });
                return (ServiceStatus.InternalError, $"ERROR: Function 'GetServiceGroupList' ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, listGrupos);
        }

        public async Task<(ServiceStatus, string, IEnumerable<GetServiceListXGroupResponse>)> GetServiceListXGroup(GetServiceListXGroupRequest request)
        {
            List<GetServiceListXGroupResponse> listGrupos = null;

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_EXAM_LIST_GLOBAL", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxnocia", OracleDbType.Varchar2).Value = request.IdCompania;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        listGrupos = new List<GetServiceListXGroupResponse>();

                        while (rdr.Read())
                        {
                            listGrupos.Add(new GetServiceListXGroupResponse
                            {
                                Seekcia = rdr["CODIGO"].ToString(),
                                Numser = rdr["S_NUMSER"].ToString(),
                                Descripcion = rdr["DESCRIPCION"].ToString().Trim(),
                                Precio = Convert.ToDecimal(rdr["PRECIO_SOLES"].ToString())
                            }); ;
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                listGrupos = null;
                //await _logService.RegistrarLog(new LogEntity { Mensaje = "ServiciosRepository > GetListServicioXGrupo > " + ex.Message, Nivel = 0 });
                return (ServiceStatus.InternalError, $"ERROR: Function 'GetServiceListXGroup' ... {ex.Message} ", listGrupos);
            }

            return (ServiceStatus.Ok, null, listGrupos);
        }

        public async Task<(ServiceStatus, string, IEnumerable<ServiceFilterResponse>)> ServiceFilter(ServiceFilterRequest request)
        {
            var response = new List<ServiceFilterResponse>();         

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_LIST_EXAMENES_X_CRIT_LAB", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxnocia", OracleDbType.Varchar2).Value = request.IdCompania;
                    cmd.Parameters.Add("xxvdesc", OracleDbType.Varchar2).Value = request.Filtro;

                    // Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        response = new List<ServiceFilterResponse>();

                        while (rdr.Read())
                        {
                            response.Add(new ServiceFilterResponse
                            {
                                Seekcia = rdr["CODIGO"].ToString(),
                                Numser = rdr["S_NUMSER"].ToString(),
                                Descripcion = rdr["DESCRIPCION"].ToString().Trim(),
                                Precio = Convert.ToDecimal(rdr["PRECIO_SOLES"].ToString())
                            }); ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: ServiceFilter ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
