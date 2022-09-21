using Application.Commom.Status;
using Application.Pattern.Laboratory.Samples.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Laboratory
{
    public interface ILaboratoryRepository
    {
        Task<(ServiceStatus, string, SamplesBarcodeResponse)> SamplesBarcode(SamplesBarcodeRequest request);
    }
}
