using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WakeOnLanCore.Data;
using WakeOnLanCore.Models;

namespace WakeOnLanCore.Controllers
{
    [Produces("application/json")]
    [Route("Wol")]
    public class WolController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;

        public WolController(IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null) throw new ArgumentNullException(nameof(hostingEnvironment));

            string settingsPath = Path.Combine(hostingEnvironment.ContentRootPath, "AppData", "devices.xml");
            _deviceRepository = new DeviceRepository(settingsPath);
        }

        // GET: Wol
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            var devices = _deviceRepository.GetAllDevices();
            return devices;
        }

        // POST: Wol
        [HttpPost]
        public Device Post([FromBody] Device device)
        {
            var createdDevice = _deviceRepository.AddDevice(device);
            return createdDevice;
        }

        // DELETE: Wol/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _deviceRepository.DeleteDevice(id);
        }
    }
}
