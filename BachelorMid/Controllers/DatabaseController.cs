using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using BachelorMid.DAL;
using BachelorMid.Models;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BachelorMid.Controllers
{
    public class DatabaseController
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly DataAccessContext _context;

        public DatabaseController(ILogger<DatabaseController> logger, DataAccessContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void UpdateDatabase()
        {
            var result = new ValuesModel();
            foreach (var item in MicrocontrollerMap.IpMapId)
            {
                string url = item.Value;
                int id = item.Key;
                if (!String.IsNullOrEmpty(url))
                {
                    result.MicrocontrollerID = id;
                    result.Temperature = GetTemperature(url);
                    result.DoorOpen = GetDoorOpen(url);
                    result.Dust = GetDust(url);
                    result.DateTime = DateTime.Now;
                    result.Humidity = GetHumidity(url);
                    result.Power = GetPower(url);
                    _context.Add(result);
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        public float GetTemperature(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.8.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public float GetDust(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.10.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public float GetHumidity(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.11.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }

        public bool GetDoorOpen(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.4.1.318.1.1.1.2.2.1.0")) },
                           3000)[0].Data?.ToString();
            var temp = !String.IsNullOrEmpty(response) ? Int32.Parse(response) : 0;
            var result = true;
            if (temp == 0) result = false;
            return result;
        }

        public float GetPower(string url)
        {
            var response = Messenger.Get(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(url), 161),
                           new OctetString("public"),
                           new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.9.0")) },
                           3000)[0].Data?.ToString();
            var result = !String.IsNullOrEmpty(response) ? float.Parse(response, CultureInfo.InvariantCulture.NumberFormat) : 0;
            return result;
        }
    }
}
