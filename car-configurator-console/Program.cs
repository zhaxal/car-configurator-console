using System;
using IniParser;
using IniParser.Model;

namespace car_configurator_console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Converter converter = new Converter();
            converter.Convert();
            
            //var parser = new FileIniDataParser();
            //IniData data =
            //    parser.ReadFile(@"C:\Users\zhaxa\Desktop\configurator\wdts_toyota_cresta_jzx100\data\car.ini");
            //var controls = data["CONTROLS"];
            //Console.WriteLine(controls);
            //parser.WriteFile(@"C:\Users\zhaxa\Desktop\configurator\kaef\data\car.ini",controls);
        }
    }
}