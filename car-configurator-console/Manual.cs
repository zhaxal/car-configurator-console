using System;
using IniParser;
using IniParser.Model;

namespace car_configurator_console
{

    public class Manual
    {
        public Car _car;
        public Suspensions _suspensions;
        public Tyres _tyres;
    }

    public class Car
    {
        public String path;
        //HEADER
        public String version;
        //BASIC
        public String totalmass;
        public String inertia;
        public SectionData controls;
        public SectionData fuel;
        public SectionData fueltank;

        public Car(String path)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(path);
            this.path = path;
            version = data["HEADER"]["VERSION"];
            totalmass = data["BASIC"]["TOTALMASS"];
            inertia = data["BASIC"]["INERTIA"];
            controls = data.Sections.GetSectionData("CONTROLS");
            fuel = data.Sections.GetSectionData("FUEL");
            fueltank = data.Sections.GetSectionData("FUELTANK");
        }

    }
    
    public class Suspensions
    {
        public String path;
        //HEADER
        public String version;
        //BASIC
        public String cg_location;

        public SectionData arb;
        public SectionData front;
        public SectionData rear;

        public Suspensions(String path)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(path);
            this.path = path;
            version = data["HEADER"]["VERSION"];
            cg_location = data["BASIC"]["CG_LOCATION"];
            arb = data.Sections.GetSectionData("ARB");
            front = data.Sections.GetSectionData("FRONT");
            rear = data.Sections.GetSectionData("REAR");
        }
    }
    
    public class Tyres
    {
        public String path;
        //HEADER
        public String version;

        public SectionData front;
        public SectionData rear;
        public SectionData thermal_front;
        public SectionData thermal_rear;

        public Tyres(String path)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(path);
            this.path = path;
            version = data["HEADER"]["VERSION"];
            front = data.Sections.GetSectionData("FRONT");
            rear = data.Sections.GetSectionData("REAR");
            thermal_front = data.Sections.GetSectionData("THERMAL_FRONT");
            thermal_rear = data.Sections.GetSectionData("THERMAL_REAR");

        }
    }
}