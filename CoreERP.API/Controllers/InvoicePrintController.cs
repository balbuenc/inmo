using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicePrintController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        public InvoicePrintController(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("DownloadInvoice/{id}")]
        public async Task<FileStreamResult> DownloadInvoice(int id)
        {
            string mimeType = "";
            int extension = 1;
            var path = $"C:\\Users\\cbalbuena\\source\\repos\\Lugaro\\ReportDesign\\Factura.rdlc";

            DataTable _dt;
            LocalReport localReport = new LocalReport(path);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44342/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/budgetDetail/BudgetPDF/{id}");

                ////Checking the response is successful or not which is sent using HttpClient  
                //if (Res.IsSuccessStatusCode)
                //{
                //Storing the response details recieved from web api   
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                _dt = (DataTable)JsonConvert.DeserializeObject(EmpResponse, (typeof(DataTable)));

                //}

            }

            //Seteo los parametros del reporte
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("p_id_presupuesto", _dt.Rows[0]["id_presupuesto"].ToString());
            parameter.Add("p_fecha", Convert.ToDateTime(_dt.Rows[0]["fecha"].ToString()).ToShortDateString());
            parameter.Add("p_cliente", _dt.Rows[0]["cliente"].ToString());
            parameter.Add("p_direccion", _dt.Rows[0]["direccion"].ToString());
            parameter.Add("p_telefono", _dt.Rows[0]["telefono"].ToString());
            parameter.Add("p_ruc", _dt.Rows[0]["ruc"].ToString());
            parameter.Add("p_moneda", _dt.Rows[0]["moneda"].ToString());
            parameter.Add("p_condicion", _dt.Rows[0]["condicion"].ToString());
            parameter.Add("p_nro_factura", _dt.Rows[0]["id_presupuesto"].ToString());
            parameter.Add("p_monto_total", _dt.Rows[0]["monto_total"].ToString());

            localReport.AddDataSource("VentasDS", _dt);

            try
            {
                var result = localReport.Execute(RenderType.Pdf, extension, null, mimeType);

                MemoryStream m = new MemoryStream(result.MainStream);
                var r = new FileStreamResult(m, "application/pdf");
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
