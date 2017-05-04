using log4net;

using ProjectManager.Common.Constrants;

namespace ProjectManager.Common.Providers
{
    public class FileLogger : ILogger
    {
        private static readonly ILog Log;

        static FileLogger()
        {
            Log = LogManager.GetLogger(typeof(FileLogger));
        }

        public void Info(object msg)
        {
            Log.Info(msg);
        }     
           
        public void Error(object msg)
        {
            Log.Error(msg);
        }   
             
        public void Fatal(object msg)
        {
            Log.Fatal(msg);
        }
    }
}
