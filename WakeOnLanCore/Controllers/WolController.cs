using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakeOnLanCore.Models;
using WakeOnLanCore.Data;
using Microsoft.AspNetCore.Hosting;

namespace WakeOnLanCore.Controllers
{
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
        public ActionResult Index()
        {
            var devices = _deviceRepository.GetAllDevices();
            return View(devices);
            
        }

        // GET: Wol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Wol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Device device)
        {
            try
            {
                _deviceRepository.AddDevice(device);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wol/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Wol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wol/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Wol/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}