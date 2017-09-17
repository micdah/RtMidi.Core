using RtMidi.Core.Unmanaged.Devices;
using Moq;
using Xunit;

namespace RtMidi.Core.Tests
{
    public class MidiDeviceTests
    {
        private readonly Mock<IRtMidiDevice> _rtMidiDevice;
        private readonly MidiDevice<IRtMidiDevice> _sut;

        public MidiDeviceTests()
        {
            _rtMidiDevice = new Mock<IRtMidiDevice>();
            _sut = new MidiDeviceMock<IRtMidiDevice>(_rtMidiDevice.Object);
        }

        [Fact]
        public void Should_Delegate_IsOpen()
        {
            // Setup
            _rtMidiDevice
                .Setup(x => x.IsOpen)
                .Returns(true)
                .Verifiable("Should call IsOpen on RtMidiDevice");

            // Test
            var isOpen = _sut.IsOpen;

            // Verify
            Assert.True(isOpen);
            _rtMidiDevice.Verify();
        }

        [Fact]
        public void Should_Delegate_Open() 
        {
            // Setup
            _rtMidiDevice
                .Setup(x => x.Open())
                .Returns(true)
                .Verifiable("Should call Open on RtMidiDevice");

            // Test
            var wasOpened = _sut.Open();

            // Verify
            Assert.True(wasOpened);
            _rtMidiDevice.Verify();
        }

        [Fact]
        public void Should_Delegate_Close() 
        {
            // Setup
            _rtMidiDevice
                .Setup(x => x.Close())
                .Verifiable("Should call Close on RtMidiDevice");

            // Test
            _sut.Close();

            // Verify
            _rtMidiDevice.Verify();
        }

        /// <summary>
        /// Implementation used for testing base class
        /// </summary>
        class MidiDeviceMock<T> : MidiDevice<T> where T : class, IRtMidiDevice
        {
            public MidiDeviceMock(T rtMidiDevice) : base(rtMidiDevice)
            {
            }
        }
    }
}
