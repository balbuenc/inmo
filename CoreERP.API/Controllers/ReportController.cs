using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        public ReportController(IWebHostEnvironment webHostEnviroment)
        {
            _webHostEnviroment = webHostEnviroment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("GetReport/{id}")]
        public  async Task<IActionResult> GetReport(int id)
        {
            
            
            string mimeType = "";
            int extension = 1;
            var path = $"C:\\Users\\cbalbuena\\source\\repos\\Lugaro\\ReportDesign\\Presupuesto.rdlc";

            DataTable _dt;

            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("param", "RDLC Report");
            
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
                    _dt= (DataTable)JsonConvert.DeserializeObject(EmpResponse,(typeof(DataTable)));

                //}
                
            }

            localReport.AddDataSource("PresupuestosDataSet",_dt);

            var result = localReport.Execute(RenderType.Pdf, extension, null, mimeType);
 
            return File(result.MainStream, contentType: "application/pdf"); 

        }

        [HttpGet]
        [Route("DownloadReport/{id}")]
        public async Task<FileStreamResult> DownloadReport(int id)
        {
            string mimeType = "";
            int extension = 1;
            var path = $"C:\\Users\\cbalbuena\\source\\repos\\Lugaro\\ReportDesign\\Presupuesto.rdlc";

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
            parameter.Add("p_fecha", Convert.ToDateTime( _dt.Rows[0]["fecha"].ToString()).ToShortDateString());
            parameter.Add("p_cliente", _dt.Rows[0]["cliente"].ToString());
            parameter.Add("p_direccion", _dt.Rows[0]["direccion"].ToString());
            parameter.Add("p_forma_pago", _dt.Rows[0]["forma_pago"].ToString());
            parameter.Add("p_plazo_entrega", _dt.Rows[0]["plazo_entrega"].ToString());
            parameter.Add("p_telefono", _dt.Rows[0]["telefono"].ToString());


            localReport.AddDataSource("PresupuestosDataSet", _dt);

            

            var result = localReport.Execute(RenderType.Pdf, extension, parameter, mimeType);
          

            MemoryStream m = new MemoryStream(result.MainStream);
            var r = new FileStreamResult(m, "application/pdf");
            return r;

        }
    }
}
