using System;
using System.IO;
using System.Linq;
using IniParser;
using IniParser.Model;

namespace car_configurator_console
{
    public class Converter
    {
        private String patientPath;
        private String convertFolderPath = "C:/Users/zhaxa/Desktop/configurator";
        private Data donor;
        private Manual _manual;

        public Converter()
        {
        }

        public void SetChanges(String pathDonor,String carName)
        {
            pathDonor = pathDonor + "/data";
            patientPath += "/data";
            File.WriteAllText(patientPath+"/aero.ini",donor._aero.Content);
            File.WriteAllText(patientPath+"/brakes.ini",donor._brakes.Content);
            File.WriteAllText(patientPath+"/drivetrain.ini",donor._drivetrain.Content);
            File.WriteAllText(patientPath+"/electronics.ini",donor._electronics.Content);
            File.WriteAllText(patientPath+"/engine.ini",donor._engine.Content);
            File.WriteAllText(patientPath+"/setup.ini",donor._setup.Content);
            
            CopyCurve(pathDonor,patientPath);

            SetCar(patientPath+"/car.ini");
            SetSuspension(patientPath+"/suspensions.ini");
            SetTyres(patientPath+"/tyres.ini");
            
            RemoveSpaces(patientPath+"/car.ini");
            RemoveSpaces(patientPath+"/suspensions.ini");
            RemoveSpaces(patientPath+"/tyres.ini");
            
            

        }

        public void RemoveSpaces(String path)
        {
            string text = File.ReadAllText(path);
            text = text.Replace(" ", "");
            File.WriteAllText(path,text);
        }



        public void Convert()
        {
            Console.WriteLine("Enter car name:");
            string carName = Console.ReadLine();
            Console.WriteLine("Enter patient path:");
            string path = Console.ReadLine();
            Console.WriteLine("Enter donor path:");
            string donorPath = Console.ReadLine();
            
            SetDonor(donorPath);

            this.patientPath = convertFolderPath + "/" + carName;
            DirectoryCopy(path, patientPath, true);
            
            SetChanges(donorPath,carName);

        }
        
        
        
        public void SetDonor(String pathDonor)
        {
            pathDonor = pathDonor + "/data";
            donor = new Data
            {
                _aero = new Aero(pathDonor + "/aero.ini"),
                _brakes = new Brakes(pathDonor + "/brakes.ini"),
                _drivetrain = new Drivetrain(pathDonor + "/drivetrain.ini"),
                _electronics = new Electronics(pathDonor + "/electronics.ini"),
                _engine = new Engine(pathDonor + "/engine.ini"),
                _setup = new Setup(pathDonor + "/setup.ini")
            };

            _manual = new Manual
            {
                _car = new Car(pathDonor + "/car.ini"),
                _suspensions = new Suspensions(pathDonor + "/suspensions.ini"),
                _tyres = new Tyres(pathDonor + "/tyres.ini")
            };
        }
        
        public void CopyCurve(String pathDonor, String pathPatient)
        {
            var extensions = new[] {".lut", ".rto"}; 

            var files = (from file in Directory.EnumerateFiles(pathDonor)
                where extensions.Contains(Path.GetExtension(file), StringComparer.InvariantCultureIgnoreCase) 
                select new 
                { 
                    Source = file, 
                    Destination = Path.Combine(pathPatient, Path.GetFileName(file))
                });

            foreach(var file in files)
            {
                if(File.Exists(file.Destination))
                {
                    File.Delete(file.Destination);
                }
                File.Copy(file.Source, file.Destination);
            }
        }
        
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
        
            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);        

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
        
        public void SetTyres(String path)
        {
            var parser = new FileIniDataParser();
            IniData tyres = parser.ReadFile(path);

            String radiusfront = tyres["FRONT"]["RADIUS"];
            String radiusrear = tyres["REAR"]["RADIUS"];

            tyres["HEADER"]["VERSION"] = _manual._tyres.version;
            
            tyres.Sections.SetSectionData("FRONT",_manual._tyres.front);
            tyres.Sections.SetSectionData("REAR",_manual._tyres.rear);
            tyres.Sections.SetSectionData("THERMAL_FRONT",_manual._tyres.thermal_front);
            tyres.Sections.SetSectionData("THERMAL_REAR",_manual._tyres.thermal_rear);

            tyres["FRONT"]["RADIUS"] = radiusfront;
            tyres["REAR"]["RADIUS"] = radiusrear;
            
            parser.WriteFile(path,tyres);
        }

        public void SetSuspension(String path)
        {
            var parser = new FileIniDataParser();
            IniData suspension = parser.ReadFile(path);

            suspension["HEADER"]["VERSION"] = _manual._suspensions.version;
            suspension["BASIC"]["CG_LOCATION"] = _manual._suspensions.cg_location;
            
            suspension.Sections.SetSectionData("ARB",_manual._suspensions.arb);
            suspension.Sections.SetSectionData("FRONT",_manual._suspensions.front);
            suspension.Sections.SetSectionData("REAR",_manual._suspensions.rear);
            
            parser.WriteFile(path,suspension);
        }

        public void SetCar(String path)
        {
            var parser = new FileIniDataParser();
            IniData car = parser.ReadFile(path);
            car["HEADER"]["VERSION"] = _manual._car.version;
            car["BASIC"]["TOTALMASS"] = _manual._car.totalmass;
            car["BASIC"]["INERTIA"] = _manual._car.inertia;
            
            car.Sections.SetSectionData("CONTROLS",_manual._car.controls);
            car.Sections.SetSectionData("FUEL",_manual._car.fuel);
            car.Sections.SetSectionData("FUELTANK",_manual._car.fueltank);
            
            parser.WriteFile(path,car);
        }


    }
}