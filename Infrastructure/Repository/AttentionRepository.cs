using Application.Commom.Status;
using Application.Interfaces.Attention;
using Application.Interfaces.Pdf;
using Application.Pattern.Attention.Create.New;
using Application.Pattern.Attention.Create.Ticket;
using Application.Pattern.Attention.Create.Ticket.Data;
using Application.Pattern.Attention.Read.Detail;
using Application.Pattern.Attention.Read.Detail.List;
using Application.Pattern.Attention.Read.Get;
using Application.Pattern.Attention.Read.Header;
using Application.Pattern.Attention.Read.Header.List;
using Application.Pattern.Attention.Read.List;
using Application.Pattern.Service.Read.Get;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Pattern.Attention.Create.Ticket.Data.AttentionTicketDataResponse;

namespace Infrastructure.Repository
{
    public class AttentionRepository : IAttentionRepository
    {
        private readonly string _connectionString;
        private readonly string _scheme;
        private readonly string _package;
        private readonly IPdfRepository _pdfRepository;

        public AttentionRepository(IConfiguration configuration, IPdfRepository pdfRepository)
        {
            _connectionString = configuration.GetConnectionString("OracleDBConnection");
            _scheme = "YOUR_SCHEME";
            _package = "YOUR_PACKAGE";
            _pdfRepository = pdfRepository;
        }


        public async Task<(ServiceStatus, string, NewAttentionResponse)> NewAttention(NewAttentionRequest request)
        {
            var response = new NewAttentionResponse();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_CREATE_OS_WEB", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // InParameter
                    cmd.Parameters.Add("xxNUMPAC", OracleDbType.Varchar2).Value = request.Numpac;
                    cmd.Parameters.Add("xxANOPAC", OracleDbType.Varchar2).Value = request.Anopac;
                    cmd.Parameters.Add("xxNUMCIA", OracleDbType.Varchar2).Value = request.Numcia;
                    cmd.Parameters.Add("xxUSUMOD", OracleDbType.Varchar2).Value = request.Usumod;
                    cmd.Parameters.Add("xxSERVICIOS", OracleDbType.Varchar2).Value = request.Servicios;
                    cmd.Parameters.Add("xxNUMSUC", OracleDbType.Varchar2).Value = request.Numsuc;
                    cmd.Parameters.Add("xxCELLPHONE", OracleDbType.Varchar2).Value = request.Cellphone;
                    cmd.Parameters.Add("xxEMAIL", OracleDbType.Varchar2).Value = request.Email;
                    cmd.Parameters.Add("xxOBSERVATION", OracleDbType.Varchar2).Value = request.Observation;
                    cmd.Parameters.Add("xxDISCOUNT", OracleDbType.Varchar2).Value = request.Discount;

                    //OutParameter
                    cmd.Parameters.Add("RESULTADO", OracleDbType.Int16).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("MENSAJE", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    response.Result = int.Parse(cmd.Parameters["RESULTADO"].Value.ToString());
                    response.Message = cmd.Parameters["MENSAJE"].Value.ToString();

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: NewAttention ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, AttentionListResponse)> AttentionList(AttentionListRequest request)
        {
            var response = new AttentionListResponse();
            response.attentionList = new List<AttentionResponse>();

            try
            {
                var headerListReq = new AttentionHeaderListRequest();
                headerListReq.Numcia = request.Numcia;
                headerListReq.Numsuc = request.Numsuc;

                var (statusH, messageH, respH) = await AttentionHeaderList(headerListReq);

                if(respH == null)
                    return (ServiceStatus.Ok, $"ERROR: {messageH}", null);

                foreach (var attentionHeader in respH.HeaderList)
                {
                    var attention = new AttentionResponse();
                    attention.Header = attentionHeader;

                    var detailReq = new AttentionDetailRequest();
                    detailReq.Numoscab = attention.Header.Numoscab;
                    detailReq.Peroscab = attention.Header.Peroscab;
                    detailReq.Anooscab = attention.Header.Anooscab;
                    detailReq.Numsuc = attention.Header.Numsuc;
                    detailReq.Numemp = attention.Header.Numemp;

                    var (statusD, messageD, respD) = await AttentionDetail(detailReq);

                    if (respD == null)
                        return (ServiceStatus.Ok, $"ERROR: {messageD}", null);

                    attention.Detail = respD;

                    response.attentionList.Add(attention);
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: AttentionList ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, AttentionHeaderListResponse)> AttentionHeaderList(AttentionHeaderListRequest request)
        {
            var response = new AttentionHeaderListResponse();
            response.HeaderList = new List<AttentionHeaderResponse>();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_LIST_OS_SIN_ESCANEO", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("Numcia", OracleDbType.Varchar2).Value = request.Numcia;
                    cmd.Parameters.Add("Numsuc", OracleDbType.Varchar2).Value = request.Numsuc;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if (rdr["ESTADO"].ToString() == "C")
                            {
                                response.HeaderList.Add(new AttentionHeaderResponse
                                {
                                    Numoscab = rdr["NUMOSC"].ToString(),
                                    Peroscab = rdr["PEROSC"].ToString(),
                                    Anooscab = rdr["ANOOSC"].ToString(),
                                    Numsuc = rdr["NUMSUC"].ToString(),
                                    Numemp = rdr["NUMEMP"].ToString(),
                                    Ticket = rdr["TICKET"].ToString(),
                                    Aniopaciente = rdr["ANOPAC"].ToString(),
                                    Idpaciente = rdr["NUMPAC"].ToString(),
                                    Paciente = rdr["PACIENTE"].ToString(),
                                    TipoDoc = rdr["TIPODOC"].ToString(),
                                    PacienteDoc = rdr["NRODOC"].ToString(),
                                    FechaAtencion = Convert.ToDateTime(rdr["FECHA"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                                    HoraAtencion = Convert.ToDateTime(rdr["FECHA"]).ToShortTimeString(),
                                    TotalSol = rdr["IMPTOTSOL"].ToString() != "" ? Convert.ToDecimal(rdr["IMPTOTSOL"].ToString()) : 0,
                                    TotalDol = rdr["IMPTOTDOL"].ToString() != "" ? Convert.ToDecimal(rdr["IMPTOTDOL"].ToString()) : 0,
                                });
                            }
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: AttentionHeaderList ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, AttentionDetailResponse)> AttentionDetail(AttentionDetailRequest request)
        {
            var response = new AttentionDetailResponse();
            response.ServiceList = new List<ServiceResponse>();

            try
            {
                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_GET_LIST_OSD", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxNUMOSCAB", OracleDbType.Varchar2).Value = request.Numoscab;
                    cmd.Parameters.Add("xxPEROSCAB", OracleDbType.Varchar2).Value = request.Peroscab;
                    cmd.Parameters.Add("xxANOOSCAB", OracleDbType.Varchar2).Value = request.Anooscab;
                    cmd.Parameters.Add("xxNUMSUC", OracleDbType.Varchar2).Value = request.Numsuc;
                    cmd.Parameters.Add("xxNUMEMP", OracleDbType.Varchar2).Value = request.Numemp;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            response.ServiceList.Add(new ServiceResponse
                            {
                                Numser = rdr["NUMSER"].ToString(),
                                Seekcia = rdr["NUMFOX"].ToString(),
                                Descripcion = rdr["DESSER"].ToString(),
                                TotalDol = rdr["TOTALDOL"].ToString() != "" ? Convert.ToDecimal(rdr["TOTALDOL"].ToString()) : 0,
                                TotalSol = rdr["TOTALSOL"].ToString() != "" ? Convert.ToDecimal(rdr["TOTALSOL"].ToString()) : 0
                            });
                        }
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                return (ServiceStatus.Ok, $"ERROR: AttentionDetailList ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, AttentionTicketResponse)> AttentionTicket(AttentionTicketRequest request)
        {
            var result = new AttentionTicketResponse();

            try
            {
                var req = new AttentionTicketDataRequest();
                req.Numoscab = request.Numoscab;
                req.Peroscab = request.Peroscab;
                req.Anioscab = request.Anioscab;
                req.Numsuc = request.Numsuc;

                var (status, message1, response) = await AttentionTicketData(req);

                switch (status)
                {
                    case ServiceStatus.FailedValidation:
                    case ServiceStatus.InternalError:
                        return (ServiceStatus.InternalError, "Servicio de gestión de reportes no se encuentra disponible en este momento", null);
                    default:
                        break;
                }


                var (status2, message, result2) = await _pdfRepository.AttentionTicket(response);

                if (message != null)
                {
                    return (status2, message, null);
                }

                result.pdf = result2;
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: AttentionTicket ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, result);
        }

        public async Task<(ServiceStatus, string, AttentionTicketDataResponse)> AttentionTicketData(AttentionTicketDataRequest ticket)
        {
            var response = new AttentionTicketDataResponse();

            try
            {
                var eAtencion = new AttentionNavalHead();

                var attentionsNavalBody = new List<AttentionNavalBody>();

                using (OracleConnection con = new OracleConnection(_connectionString))
                {
                    con.Open();

                    OracleCommand cmd = new OracleCommand(_scheme + _package + "USP_HOJA_RUTA_NUMTICK", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxNumosc", OracleDbType.Varchar2).Value = ticket.Numoscab;
                    cmd.Parameters.Add("xxPerosc", OracleDbType.Varchar2).Value = ticket.Peroscab;
                    cmd.Parameters.Add("xxAnoosc", OracleDbType.Varchar2).Value = ticket.Anioscab;
                    cmd.Parameters.Add("xxNumsuc", OracleDbType.Varchar2).Value = ticket.Numsuc;

                    //Cursor
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    OracleDataReader rdr = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            eAtencion.Cip = rdr["RP_CID_IAFAS"].ToString();
                            eAtencion.Compania = rdr["NCOCIA"].ToString().Length > 43 ? rdr["NCOCIA"].ToString().Substring(0, 44) : rdr["NCOCIA"].ToString();
                            eAtencion.Contrasena = rdr["CLAWEB"].ToString();
                            eAtencion.Document = $"{ rdr["TIPO_DOC_PAC"] } - { rdr["NUM_DOC_PAC"] }";
                            eAtencion.Dx = rdr["OSC_NUMPQC"].ToString();
                            eAtencion.Edad = rdr["EDAD"].ToString();
                            eAtencion.Empresa = "SUIZA LAB S.A.C";  //  -----------------------
                            eAtencion.Fecha = Convert.ToDateTime(rdr["FECHA_ESPERA"]);
                            eAtencion.Fecnac = rdr["FECHA_NAC"].ToString().Length > 9 ? rdr["FECHA_NAC"].ToString().Substring(0, 10) : rdr["FECHA_NAC"].ToString();
                            eAtencion.OrdLab = rdr["YEARORDEN"].ToString();
                            eAtencion.Paciente = rdr["NCOPAC"].ToString();
                            eAtencion.Paren = rdr["RP_PARENTESCO_IAFAS"].ToString();
                            eAtencion.Sexo = rdr["SEXO"].ToString();
                            eAtencion.Sucursal = rdr["NOMBRE_SUCURSAL"].ToString();
                            eAtencion.Telefono = rdr["TELEFONO"].ToString().Length > 10 ? rdr["TELEFONO"].ToString().Substring(0, 11).Trim(new char[] { ' ', '.' }) : rdr["TELEFONO"].ToString().Trim(new char[] { ' ', '.' });
                            eAtencion.Piso = rdr["GRUPO1"].ToString();
                            eAtencion.Observacion = rdr["OBSERVACION"].ToString();
                            eAtencion.Ticket = $"{ ticket.Numoscab }{ ticket.Peroscab }{ ticket.Anioscab }{ ticket.Numsuc }";
                            eAtencion.Usuario = rdr["USU_ENVIO_ESPERA"].ToString();
                            eAtencion.UsuarioWeb = rdr["USUWEB"].ToString();
                            //eAtencion.Barcode = GenerateBarcode.Barcode39ConvertToBase64(eAtencion.Ticket);

                            var entidad = new AttentionNavalBody();

                            entidad.Cantidad = rdr["PISO"].ToString();
                            entidad.Description = rdr["DESSER"].ToString();
                            entidad.Check = "false"; //ESTADO ----------------------

                            attentionsNavalBody.Add(entidad);
                        }
                    }

                    cmd.Dispose();
                }

                response.Head = eAtencion;
                response.Body = attentionsNavalBody;

            }
            catch (Exception ex)
            {
                //await _logService.RegistrarLog(new LogEntity { Mensaje = $"TicketRepository > GetNavalData > {ex.Message} > {JsonConvert.SerializeObject("Ticket:" + $"{ ticket.Numoscab }{ ticket.Peroscab }{ ticket.Anioscab }{ ticket.Numsuc }")}", Nivel = 0 });
                return (ServiceStatus.InternalError, $"ERROR: AttentionTicketData ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
