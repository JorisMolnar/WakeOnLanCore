using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using WakeOnLanCore.Models;

namespace WakeOnLanCore.Data
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly XmlSerializer _serializer;
        private readonly string _settingsFilePath;

        public DeviceRepository(string filePath)
        {
            _serializer = new XmlSerializer(typeof(List<Device>));
            _settingsFilePath = filePath;

            CreateSettingsDirectory();
        }

        private void CreateSettingsDirectory()
        {
            if (!File.Exists(_settingsFilePath))
            {
                string settingsDirectoryPath = _settingsFilePath.Remove(_settingsFilePath.LastIndexOf(Path.DirectorySeparatorChar));
                Directory.CreateDirectory(settingsDirectoryPath);
            }
        }

        public void AddDevice(Device device)
        {
            var devices = GetAllDevices();
            devices.Add(device);
            SaveAllDevices(devices);
        }

        public void DeleteDevice(int id)
        {
            var devices = GetAllDevices()
                .Where(d => d.ID != id)
                .ToList();

            SaveAllDevices(devices);
        }

        public List<Device> GetAllDevices()
        {
            if (!File.Exists(_settingsFilePath)) return new List<Device>();
            
            using (FileStream fs = new FileStream(_settingsFilePath, FileMode.Open))
            {
                var devices = _serializer?.Deserialize(fs) as List<Device>;
                return devices;
            }
        }

        private void SaveAllDevices(List<Device> devices)
        {
            CreateSettingsDirectory();

            using (FileStream fs = new FileStream(_settingsFilePath, FileMode.Create))
            {
                _serializer.Serialize(fs, devices);
            }
        }
    }
}
