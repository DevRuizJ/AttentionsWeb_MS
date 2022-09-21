using Application.Pattern.Patient.Get;
using Application.Pattern.Patient.New;
using Application.Pattern.Patient.RegisterNew;
using Microsoft.Extensions.Configuration;
using SS_SuizaNaval_API.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service.PatientService
{
    public class PatientClient : BaseHttpClient
    {
        private readonly string _Usuario;

        public PatientClient(HttpClient httpClient, IConfiguration configuration) : base(httpClient)
        {
            _Usuario = configuration["SISTEMA:Usuario"];
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " +
            Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "ADMIN", "123456."))));

        }

        public async Task<GetPatientResponse> GetPatient(GetPatientRequest request)
        {
            //return await Post<GetPatientResponse>(Endpoints.Patient.Get, request);    //utiliza api/paciente
            return await Post<GetPatientResponse>(Endpoints.Patient.Get, request);  //utiliza api/patient
        }

        public async Task<string> RegisterPatient(NewPatientRequest request)
        {
            //request.UsuarioRegistro = _Usuario;
            return await Post<string>(Endpoints.Patient.New, request);//Registro tabla SGS_PACIENTES_VALIDADOS
        }    
    }
}
