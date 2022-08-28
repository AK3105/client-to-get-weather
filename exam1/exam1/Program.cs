
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
namespace HttpClientExtensionMethods
{    //Город
     //Текущая температура, Минимальная температура,
     //Максимальная температура, Скорость ветра, Направление ветра, Облачность,
     //Состояние погоды (Солнечно, Пасмурно, Дождь, Снег)
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public int speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Root
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }


    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Input City");
            string city = Console.ReadLine();


            //using HttpClient client = new()
            //{
            //    BaseAddress = new Uri("https://api.openweathermap.org")
            //};
            var url = "https://api.openweathermap.org/data/2.5/weather?q="+city+"&APPID=215049ee62e20f3d1596e6b2e5c9e141";
            //using (var httpClient = new HttpClient())
            //{
            //    var json = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=Barnaul&APPID=215049ee62e20f3d1596e6b2e5c9e141");

            //}
            using (var webClient = new WebClient())
            {
                try
                {
                    string jsonString = webClient.DownloadString(url);
                    //Console.WriteLine(jsonString);
               

                JsonNode node = JsonNode.Parse(jsonString);
               
                    float temp = (float)node["main"]["temp"] - 273 ;
                    float maxtemp = (float)node["main"]["temp_max"] - 273;
                    float mintemp = (float)node["main"]["temp_min"] - 273;
                    float windsp = (float)node["wind"]["speed"];
                    float winddir = (float)node["wind"]["deg"];
                    float cloud = (float)node["clouds"]["all"];
                    float pres = (float)node["main"]["pressure"];
                    string main = (string)node["weather"][0]["main"];
                    //string dir = "";
                    Console.WriteLine($"Forecast in {city}: " +
                    $"\n TemperatureNow: {temp} °C " +
                    $"\n max temp: {maxtemp} °C" +
                    $"\n min temp: {mintemp} °C" +
                    $"\n wind speed: {windsp} m/s+" +
                    $"\n Wind Direction: {winddir} deg."+ //+ //$" Wind Direction: {dir} "+
                    $"\n Cloudness: {cloud} %" +
                    $"\n Pressure: {pres} " +
                    $"\n Main Weather: {main}");
                }
            

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }




            }

        }
    }
}
            