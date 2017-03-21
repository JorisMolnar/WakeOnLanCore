using System.Collections.Generic;
using WakeOnLanCore.Models;

namespace WakeOnLanCore.Data
{
    interface IDeviceRepository
    {
        List<Device> GetAllDevices();
        void AddDevice(Device device);
        void DeleteDevice(int id);
    }
}
