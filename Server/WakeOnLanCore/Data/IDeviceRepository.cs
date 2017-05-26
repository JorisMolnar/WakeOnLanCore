using System.Collections.Generic;
using WakeOnLanCore.Models;

namespace WakeOnLanCore.Data
{
    public interface IDeviceRepository
    {
        IList<Device> GetAllDevices();
        Device GetDevice(int id);
        Device AddDevice(Device device);
        void DeleteDevice(int id);
    }
}
