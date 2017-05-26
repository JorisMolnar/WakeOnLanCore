using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using WakeOnLanCore.Data;
using WakeOnLanCore.Exceptions;
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

        // GET: Wol/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var device = _deviceRepository.GetDevice(id);
                return Ok(device);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex);
                return NotFound(ex.Message);
            }
        }

        // POST: Wol
        [HttpPost]
        public IActionResult Post([FromBody] Device device)
        {
            try
            {
                var createdDevice = _deviceRepository.AddDevice(device);
                return CreatedAtAction(nameof(Get), new {id = createdDevice.ID}, createdDevice);
            }
            catch (ObjectNotUniqueException ex)
            {
                Console.WriteLine(ex);
                return StatusCode(409, new {Message = $"A {nameof(Device)} with the supplied properties already exists."});
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new {ex.Message});
            }
        }

        // DELETE: Wol/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deviceRepository.DeleteDevice(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex);
                return NotFound(new {ex.Message});
            }
        }
    }
}
