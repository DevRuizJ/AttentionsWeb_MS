using Application.Interfaces.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pattern.Attention.Create.New
{
    public class NewAttentionRequest : IMapFrom<NewAttentionCommand>
    {
        public string Numcia { get; set; }
        public string Anopac { get; set; }
        public string Numpac { get; set; }
        public string Servicios { get; set; }
        public string Usumod { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }

        ////TARIFA CUBIERTA POR NAVAL
        //private string tarifaIpress;
        //public string TarifaIpress
        //{
        //    //IAFAS
        //    get { return tarifaIpress == null ? "0" : tarifaIpress; }
        //    set { tarifaIpress = value; }
        //}
        //private string tarifaDisamar;
        //public string TarifaDisamar
        //{
        //    //DISAMAR
        //    get { return tarifaDisamar == null ? "0" : tarifaDisamar; }
        //    set { tarifaDisamar = value; }
        //}   //----------------------------------------------------------
        //public string Diagnosis { get; set; }
        //public string NavalCareUnit { get; set; }
        public string Observation { get; set; }
        public string Numsuc { get; set; }
        public string Discount { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewAttentionCommand, NewAttentionRequest>()
                .ForMember(x => x.Numcia, y => y.MapFrom(z => z.Compania))
                .ForMember(x => x.Anopac, y => y.MapFrom(z => z.AnioPaciente))
                .ForMember(x => x.Numpac, y => y.MapFrom(z => z.NumeroPaciente))
                .ForMember(x => x.Servicios, y => y.MapFrom(z=> z.Servicios))
                .ForMember(x => x.Cellphone, y => y.MapFrom(z => z.Telefono1))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.correo))
                .ForMember(x => x.Observation, y => y.MapFrom(z => z.Observation))
                .ForMember(x => x.Numsuc, y => y.MapFrom(z => z.BranchOffice))
                .ForMember(x => x.Usumod, y => y.MapFrom(z => z.RegistrationUser));
        }
    }
}
