namespace RtMidi.Core.Devices.Nrpn
{
    public enum NrpnMode
    {
        /// <summary>
        /// Do not interpret NRPN messages
        /// </summary>
        Off,

        /// <summary>
        /// Interpret NRPN messages, only sending Control Change (CC) messages
        /// if they definitely cannot be a NRPN message (Default mode)
        /// </summary>
        /// <remarks>
        /// While receiving Control Change messages, we cannot know whether they
        /// form a NRPN message before we have received enough to determine if
        /// they are indeed a NRPN message or not.<para/>
        /// Until we know for sure, we will hold on to Control Change messages,
        /// and then either dispatch a NRPN message (if they formed one) or the
        /// queued CC messages, if they did not.
        /// </remarks>
        On,

        /// <summary>
        /// Interpret NRPN messages, but do not hold on to Control Change (CC)
        /// messages (meaning CC messages will be dispatched immediately even if
        /// it turns out they formed a NRPN message)
        /// </summary>
        OnSendControlChange
    }
}
