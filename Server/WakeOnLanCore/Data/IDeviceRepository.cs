using System.Collections.Generic;
using WakeOnLanCore.Models;

namespace WakeOnLanCore.Data
{
    public interface IDeviceRepository
    {
        IList<Device> GetAllDevices();
        Device AddDevice(Device device);
        void DeleteDevice(int id);
    }
}
