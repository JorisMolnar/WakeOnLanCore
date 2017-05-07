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
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDeviceRepository _deviceRepository;

        public WolController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

            string settingsPath = Path.Combine(_hostingEnvironment.ContentRootPath, "AppData", "devices.xml");
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
        public void Post([FromBody] Device device)
        {
        }

        // PUT: Wol/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Device value)
        {
        }

        // DELETE: Wol/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _deviceRepository.DeleteDevice(id);
        }
    }
}
