using System;
using System.Runtime.InteropServices;
using RtMidi.Core.Unmanaged.API;
using Serilog;
using System.Linq;
using System.Collections.Generic;

namespace RtMidi.Core.Unmanaged
{
    internal static class RtMidiApiManager
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(typeof(RtMidiApiManager));

        /// <summary>
        /// Get array of available RtMidi API's (if any)
        /// </summary>
        /// <returns>The available apis.</returns>
        public static IEnumerable<RtMidiApi> GetAvailableApis()
        {
            var apisPtr = IntPtr.Zero;

            try
            {
                // Get number of API's
                var count = RtMidiC.GetCompiledApi(IntPtr.Zero, 0);
                if (count <= 0)
                    return new RtMidiApi[0];

                // Get array of available API's
                var enumSize = RtMidiC.Utility.SizeofRtMidiApi();
                apisPtr = Marshal.AllocHGlobal(count * enumSize);
                RtMidiC.GetCompiledApi(apisPtr, (uint)count);

                // Convert to managed enum types
                switch (enumSize)
                {
                    case 1:
                        var bytes = new byte[count];
                        Marshal.Copy(apisPtr, bytes, 0, bytes.Length);
                        return bytes.Select(b => (RtMidiApi)b);
                    case 2:
                        var shorts = new short[count];
                        Marshal.Copy(apisPtr, shorts, 0, shorts.Length);
                        return shorts.Select(s => (RtMidiApi)s);
                    case 4:
                        var ints = new int[count];
                        Marshal.Copy(apisPtr, ints, 0, ints.Length);
                        return ints.Select(i => (RtMidiApi)i);
                    case 8:
                        var longs = new long[count];
                        Marshal.Copy(apisPtr, longs, 0, longs.Length);
                        return longs.Select(l => (RtMidiApi)l);
                    default:
                        Log.Error("Unexpected size {Size} of RtMidiApi enum", enumSize);
                        throw new NotSupportedException($"Unexpected size of RtMidiApi enum {enumSize}");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected exception occurred while listing available RtMidi API's");

                return new RtMidiApi[0];
            }
            finally
            {
                if (apisPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(apisPtr);
                }
            }
        }
    }
}
