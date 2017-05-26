using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using WakeOnLanCore.Exceptions;
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

        public Device AddDevice(Device device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            if (device.ID == 0)
            {
                device.ID = GetNextID();
            }

            var devices = GetAllDevices();
            if (devices.Any(d => d.ID == device.ID ||
                                 d.Name == device.Name ||
                                 d.MacAddress == device.MacAddress))
            {
                throw new ObjectNotUniqueException();
            }

            devices.Add(device);
            SaveAllDevices(devices);
            return device;
        }

        public void DeleteDevice(int id)
        {
            var devices = GetAllDevices();

            var newDevices = devices
                .Where(d => d.ID != id)
                .ToList();

            if (newDevices.Count == devices.Count)
            {
                throw new KeyNotFoundException($"No {nameof(Device)} with the ID {id} could be found.");
            }

            SaveAllDevices(newDevices);
        }

        private void CreateSettingsDirectory()
        {
            if (!File.Exists(_settingsFilePath))
            {
                string settingsDirectoryPath = _settingsFilePath.Remove(_settingsFilePath.LastIndexOf(Path.DirectorySeparatorChar));
                Directory.CreateDirectory(settingsDirectoryPath);
            }
        }

        private int GetNextID()
        {
            var allDevices = GetAllDevices();
            if (allDevices.Count == 0)
                return 1;
            return allDevices.Max(d => d.ID) + 1;
        }

        public IList<Device> GetAllDevices()
        {
            if (!File.Exists(_settingsFilePath)) return new List<Device>();

            using (var fs = new FileStream(_settingsFilePath, FileMode.Open))
            {
                var devices = _serializer.Deserialize(fs) as List<Device>;
                return devices;
            }
        }

        public Device GetDevice(int id)
        {
            try
            {
                return GetAllDevices().Single(d => d.ID == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException($"No {nameof(Device)} with the ID {id} could be found.", ex);
            }
        }

        private void SaveAllDevices(IList<Device> devices)
        {
            CreateSettingsDirectory();
            FixDevices(devices);

            var devicesToSave = devices as List<Device> ?? devices.ToList();
            using (var fs = new FileStream(_settingsFilePath, FileMode.Create))
            {
                _serializer.Serialize(fs, devicesToSave);
            }
        }

        private void FixDevices(IList<Device> devices)
        {
            // Check non unique IDs
            if (devices.Count != devices.Select(d => d.ID).Distinct().Count())
                throw new ArgumentException("Unable to save devices with non unique ID's.", nameof(devices));

            if (devices.Any(d => d.ID <= 0))
                throw new ArgumentException("All Device ID's must be higher than 0", nameof(devices));

            foreach (var device in devices)
            {
                device.MacAddress = FormatMacAddress(device.MacAddress);
                device.Name = device.Name.Trim();
            }
        }

        private string FormatMacAddress(string macAddress)
        {
            if (macAddress == null) throw new ArgumentNullException(nameof(macAddress));
            macAddress = macAddress.Trim();
            if (macAddress.Length != 17) throw new ArgumentException($"MacAddress \"{macAddress}\" has the wrong length.", nameof(macAddress));

            macAddress = macAddress
                .Replace('-', ':')
                .ToUpperInvariant();

            bool isValid = Regex.IsMatch(macAddress, "^(?:[0-9A-F]{2}:){5}[0-9A-F]{2}$");
            if (!isValid) throw new ArgumentException($"MacAddress \"{macAddress}\" has no valid format.", nameof(macAddress));

            return macAddress;
        }
    }
}
