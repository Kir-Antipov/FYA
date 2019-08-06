using System;
using FYA.PInvoke;
using System.Linq;
using FYA.PInvoke.Structs;
using System.Collections.Generic;

namespace FYA.Models
{
    public class VideoCard
    {
        #region Var
        public static Guid VideoAdaptersClass { get; } = new Guid("4D36E968-E325-11CE-BFC1-08002BE10318");

        public string ID { get; }
        public string Name { get; }
        public string[] Devices { get; }
        public string[] RegistryKeys { get; }
        public DisplayDeviceStateFlags StateFlags { get; }
        #endregion

        #region Init
        public VideoCard(string ID, string Name, DisplayDeviceStateFlags StateFlags, string[] Devices, string[] RegistryKeys)
        {
            this.ID = ID ?? throw new ArgumentNullException(nameof(ID));
            this.Name = Name ?? string.Empty;
            this.StateFlags = StateFlags;
            this.Devices = Devices ?? new string[0];
            this.RegistryKeys = RegistryKeys ?? new string[0];
        }
        #endregion

        #region Functions
        public static VideoCard FromDisplayDevices(params DISPLAY_DEVICE[] Devices)
        {
            if ((Devices?.Length ?? 0) == 0)
                throw new ArgumentException(nameof(Devices));
            string id = Devices[0].DeviceID;
            if (Devices.Skip(1).Any(x => x.DeviceID != id))
                throw new ArgumentException(nameof(Devices));
            string[] cardDevices = Devices.Select(x => x.DeviceName).ToArray();
            string[] cardKeys = Devices.Select(x => x.DeviceKey).ToArray();
            return new VideoCard(Devices[0].DeviceID, Devices[0].DeviceString, Devices[0].StateFlags, cardDevices, cardKeys);
        }
        public static VideoCard FromDisplayDevices(IEnumerable<DISPLAY_DEVICE> Devices) => FromDisplayDevices(Devices.ToArray());

        public void Enable() => SetupAPI.ChangeDeviceState(VideoAdaptersClass, ID, true);
        public void Disable() => SetupAPI.ChangeDeviceState(VideoAdaptersClass, ID, false);

        public static IEnumerable<VideoCard> EnumerateVideoCards() => User32.EnumerateVideoCards();

        public override string ToString() => Name;
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => obj is VideoCard o && o.ID == ID;
        #endregion
    }
}
