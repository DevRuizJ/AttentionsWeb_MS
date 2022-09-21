using Application.Commom.Status;
using Application.Interfaces.Patient;
using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.New;
using Application.Pattern.Patient.RegisterNew;
using Application.Pattern.Patient.Verify;
using AutoMapper;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly PatientClient _client;
        private readonly IPatientRepository _repository;

        public PatientService(PatientClient client, IMapper mapper, IPatientRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task<(ServiceStatus, string, GetPatientResponse)> GetPatient(GetPatientRequest request)
        {
            var response = new GetPatientResponse();

            try
            {
                #region SOLUCION 1 api/paciente
                //response = await _client.GetPatient(request);
                //response.TipoDocumento = request.TipoDocumento;
                //response.NumeroDocumento = request.NumeroDocumento;
                //response.Mensaje = $"Paciente: {response.Numpac}";
                #endregion

                #region SOLUCION 2 -- CODIGO PARA UTILIZAR CON OTRO ENDPOINT /api/patient
                var result = await _client.GetPatient(request);

                //Ahora Verifico Paciente en tabla SGS_REGISTRO_PACIENTE
                var req = new VerifyPatientRequest();
                req.TipoDoc = "00" + result.TipoDocumento;
                req.NumeroDoc = result.NumeroDocumento;
                req.Nombres = result.Nombre;
                req.ApellidoPaterno = result.ApellidoPaterno;
                req.ApellidoMaterno = result.ApellidoMaterno;
                req.ApellidoCasada = result.ApellidoCasada;
                req.FechaNacimiento = result.FechaNacimiento;
                req.Sexo = result.Sexo;
                req.TelefonoMovil1 = result.TelefonoMovil1;
                req.Email = result.Email;

                var (status, message, verify) = await _repository.VerifyPatient(req);

                // El procedure en VerifyPatient busca,
                // y si el registro existe, trae los datos de RP_PACANO y RP_NUMPAC
                //Si no existe, con los datos que envíe, genera un nuevo registro en la tabla
                // y trae los datos RP_PACANO y RP_NUMPAC
                result.AnioPac = verify.PatientYear;
                result.Numpac = verify.PatientId;
                #endregion

                if (result.AnioPac != null)
                {
                    response.TipoDocumento = result.TipoDocumento;
                    response.NumeroDocumento = result.NumeroDocumento;
                    response.Nombre = result.Nombre;
                    response.ApellidoPaterno = result.ApellidoPaterno;
                    response.ApellidoMaterno = result.ApellidoMaterno;
                    response.ApellidoCasada = result.ApellidoCasada;
                    response.FechaNacimiento = result.FechaNacimiento;
                    response.Sexo = result.Sexo;
                    response.EstadoCivil = result.EstadoCivil;
                    response.TelefonoMovil1 = result.TelefonoMovil1;
                    response.TelefonoMovil2 = result.TelefonoMovil2;
                    response.Email = result.Email;
                    response.Edad = result.Edad;
                    response.Numpac = result.Numpac;
                    response.AnioPac = result.AnioPac;
                    response.Mensaje = $"Paciente: {response.Numpac}";
                }
            }
            catch (Exception ex)
            {
                //await _logService.RegistrarLog(new LogEntity { Mensaje = $"PacienteService > ConsultaPaciente > {ex.Message}>{JsonConvert.SerializeObject(request)}", Nivel = 0 });

                return (ServiceStatus.InternalError, $"ERROR: Function 'GetPaciente' ... {ex.Message}", null);
            }

            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, GetPatientResponse)> NewPatient(NewPatientRequest request)
        {
            var response = new GetPatientResponse();

            try
            {
                var (statusReg, messageReg, regNewPat) = await RegisterNewPatient(request);

                var resp = new GetPatientRequest();
                resp.TipoDocumento = request.TipoDocumento;
                resp.NumeroDocumento = request.NumeroDocumento;

                (var status, var message, response) = await GetPatient(resp);
                response.Mensaje = regNewPat.Mensaje;
            }
            catch (Exception ex)
            {
                return (ServiceStatus.InternalError, $"ERROR: NewPatient ... {ex.Message}", null);
            }
            
            return (ServiceStatus.Ok, null, response);
        }

        public async Task<(ServiceStatus, string, NewPatientResponse)> RegisterNewPatient(NewPatientRequest request)
        {
            var response = new NewPatientResponse();

            try
            {
                response.Mensaje = await _client.RegisterPatient(request);
            }
            catch (Exception ex)
            {
                //await _logService.RegistrarLog(new LogEntity { Mensaje = $"PacienteService > RegistrarPaciente > {ex.Message}>{JsonConvert.SerializeObject(request)}", Nivel = 0 });
            }

            return (ServiceStatus.Ok, null, response);
        }
    }
}
