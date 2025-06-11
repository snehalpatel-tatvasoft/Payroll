using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PalladiumPayroll.Helper.Log4net
{
    public class Log4net: ILog4net
    {
        string configFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "PalladiumPayroll.Helper", "Log4net", "log4net.config");
        private static readonly log4net.ILog _log = GetLogger(typeof(Log4net));
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public Log4net(IHttpContextAccessor IhttpContextAccessor)
        {
            _httpContextAccessor = IhttpContextAccessor;
        }
        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
        public void Warn(object message)
        {
            SetLog4NetConfiguration("Warn");
            _log.Warn(message);
        }

        public void Debug(object message)
        {
            SetLog4NetConfiguration("Debug");
            _log.Debug(message);
        }

        public void Error(object message)
        {
            SetLog4NetConfiguration("Error");
            _log.Error(message);
        }

        public void Info(object message)
        {
            SetLog4NetConfiguration("Info");
            _log.Info(message);
        }
        private void SetLog4NetConfiguration(string logType)
        {
            string logFolderPath = "C:\\logs\\";

            string LogFile = logFolderPath + @"PalladiumPayroll\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(configFilePath));

            XmlElement? documentElement = log4netConfig.DocumentElement;
            XmlNode root = documentElement;
            XmlNode subNode2 = root.SelectNodes("appender")[1];

            XmlNode nodeForModifyfile = subNode2.SelectSingleNode("file");

            nodeForModifyfile.Attributes[0].Value = LogFile;

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("track-id", out StringValues track_id) && track_id.Any())
            {
                log4net.ThreadContext.Properties["track-id"] = track_id;
            }
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("track-transid", out StringValues trans_id) && trans_id.Any())
            {
                log4net.ThreadContext.Properties["track-transid"] = trans_id;
            }

            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

        }
    }
}
