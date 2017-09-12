using System;
using System.Runtime.InteropServices;
using RtMidi.Core.Unmanaged.API;
using Serilog;

namespace RtMidi.Core.Unmanaged
{
    public static class RtMidiApiManager
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(typeof(RtMidiApiManager));

        /// <summary>
        /// Get array of available RtMidi API's (if any)
        /// </summary>
        /// <returns>The available apis.</returns>
        public static RtMidiApi[] GetAvailableApis()
        {
            var ptr = IntPtr.Zero;

            try
            {
                // Get number of API's
                var nullPtr = IntPtr.Zero;
                int count = RtMidiC.GetCompiledApi(ref nullPtr);

                // Get array of available API's
                var enumSize = RtMidiC.Utility.SizeofRtMidiApi();
                ptr = Marshal.AllocHGlobal(count * enumSize);
                RtMidiC.GetCompiledApi(ref ptr);

                // Convert to managed enum typess
                var ret = new RtMidiApi[count];
                switch (enumSize)
                {
                    case 1:
                        var bytes = new byte[count];
                        Marshal.Copy(ptr, bytes, 0, bytes.Length);
                        for (var i = 0; i < bytes.Length; i++)
                            ret[i] = (RtMidiApi)bytes[i];
                        break;
                    case 2:
                        var shorts = new short[count];
                        Marshal.Copy(ptr, shorts, 0, shorts.Length);
                        for (var i = 0; i < shorts.Length; i++)
                            ret[i] = (RtMidiApi)shorts[i];
                        break;
                    case 4:
                        var ints = new int[count];
                        Marshal.Copy(ptr, ints, 0, ints.Length);
                        for (var i = 0; i < ints.Length; i++)
                            ret[i] = (RtMidiApi)ints[i];
                        break;
                    case 8:
                        var longs = new long[count];
                        Marshal.Copy(ptr, longs, 0, longs.Length);
                        for (var i = 0; i < longs.Length; i++)
                            ret[i] = (RtMidiApi)longs[i];
                        break;
                    default:
                        throw new NotSupportedException("sizeof RtMidiApi is unexpected: " + enumSize);
                }
                return ret;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unexpected exception occurred while listing available RtMidi API's");

                return new RtMidiApi[0];
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
        }
    }
}
