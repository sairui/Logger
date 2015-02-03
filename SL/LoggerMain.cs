using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using System.IO;

namespace SL
{
    public class LoggerMain : ILogger
    {
        StreamWriter logWriter;
        IEventSource eventSource;

        public void Initialize(IEventSource eventSource)
        {
           this.eventSource = eventSource;
           logWriter = new StreamWriter(@"c:\temp\logger.txt");
           eventSource.ProjectStarted += eventSource_ProjectStarted;
           eventSource.ProjectFinished += eventSource_ProjectFinished;
           
        }

        void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            logWriter.WriteLine(e.ProjectFile + " has completed.");
        }

        void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {            
            logWriter.WriteLine(e.ProjectFile + " project has started ");
           
            //Console.WriteLine("Project: {0} has started",e.ProjectFile);
        }

        public string Parameters
        {
            get;
            set;
        }

        public void Shutdown()
        {
            this.eventSource.ProjectStarted -= eventSource_ProjectStarted;
            logWriter.Close();
        }

        public LoggerVerbosity Verbosity
        {
            get;
            set;
        }
    }
}
