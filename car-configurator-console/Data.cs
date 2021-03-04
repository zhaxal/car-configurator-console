using System;
using System.IO;

namespace car_configurator_console
{
    public class Data
    {
        public Aero _aero;
        public Brakes _brakes;
        public Drivetrain _drivetrain;
        public Electronics _electronics;
        public Engine _engine;
        public Setup _setup;
        
        
    }
    
    public class Aero
    {
        private String path;
        private String content;

        public Aero(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
    public class Brakes
    {
        private String path;
        private String content;
        
        public Brakes(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
    public class Drivetrain
    {
        private String path;
        private String content;
        
        public Drivetrain(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
    public class Electronics
    {
        private String path;
        private String content;
        public Electronics(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
    public class Engine
    {
        private String path;
        private String content;
        public Engine(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
    public class Setup
    {
        private String path;
        private String content;
        public Setup(String path)
        {
            this.path = path;
            this.content = File.ReadAllText(path);
        }

        public string Path => path;
        public string Content => content;
    }
    
}