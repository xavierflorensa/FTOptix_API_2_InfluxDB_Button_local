#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using System.Net.Http;
using System.IO;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    
    public void Inject()
    {
       GetWeather();
       Log.Info("Injecting: ");
    }

     private void GetWeather()
    {
        //URL = Project.Current.GetVariable("Model/datosAPI/URL");
        URL1 = Project.Current.GetVariable("Model/datosAPI/URL1");
        data_raw = Project.Current.GetVariable("Model/datosAPI/data_raw");
        API_Token = Project.Current.GetVariable("Model/datosAPI/API_Token");
        IoT_data = Project.Current.GetVariable("Model/datosAPI/IoT_data");
        HttpClient httpClient = new();
        
        var result3 = httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token",API_Token.Value);
        var content = new System.Net.Http.StringContent(data_raw.Value+" IoT_data="+IoT_data.Value, System.Text.Encoding.UTF8);
        //var result = httpClient.GetAsync(URL.Value).Result;
        var result1 = httpClient.PostAsync(URL1.Value,content).Result;

        if (result1.IsSuccessStatusCode)
        {
            StreamReader reader = new StreamReader(result1.Content.ReadAsStream());
            string json = reader.ReadToEnd();
            Log.Info("Valor json:" + json);
        }

        else
        {
            Log.Info("Failed to retrieve data. Status code: " + result1.StatusCode);
        }
        
    }
    private IUAVariable URL1; //datos API 
    private IUAVariable API_Token; //datos API 

    private IUAVariable data_raw; //datos API 
    private IUAVariable IoT_data; //datos API }
}   
    
