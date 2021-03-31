using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string rec;

        private async void Form1_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=498817&appid=1ca249977d08a417991015f5d707dfd9");

            request.Method = "POST";

            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();

            string answer = string.Empty;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }

            response.Close();

            richTextBox1.Text = answer;

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);

            panel1.BackgroundImage = oW.weather[0].Icon;

            label1.Text = oW.weather[0].main;

            label2.Text = oW.weather[0].description;

            label3.Text = "Mean temperature (°С): " + oW.main.temp.ToString("0.##");

            label4.Text = "Humidity (%): " + oW.main.humidity.ToString();

            label5.Text = "Pressure (mm): " + ((int)oW.main.pressure).ToString();

            label6.Text = "Speed (m/s): " +oW.wind.speed.ToString();

            label7.Text = "Deg °: " +oW.wind.deg.ToString();


            if (oW.main.temp <= 10)
            {
                rec = "How about Tea?";
            }
            MessageBox.Show(rec);
         }
    }
}
