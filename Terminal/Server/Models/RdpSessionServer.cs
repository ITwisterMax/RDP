using System;
using RDPCOMAPILib;

namespace Rdp.Terminal.Core.Server.Models
{
    /// <summary>
    ///     RDP server class
    /// </summary>
    public class RdpSessionServer : IDisposable
    {
        /// <summary>
        ///     RDP session
        /// </summary>
        private readonly RDPSession _rdpSession;
        
        /// <summary>
        ///     Default constructor
        /// </summary>
        public RdpSessionServer()
        {
            _rdpSession = new RDPSession { colordepth = 8 };
            _rdpSession.add_OnAttendeeConnected(OnAttendeeConnected);
        }
        
        /// <summary>
        ///     Filter available windows
        /// </summary>
        public bool ApplicationFilterEnabled
        {
            get
            {
                return _rdpSession.ApplicationFilter.Enabled;
            }
            set
            {
                _rdpSession.ApplicationFilter.Enabled = value;
            }
        }

        /// <summary>
        ///     Available windows list
        /// </summary>
        public RDPSRAPIApplicationList ApplicationList
        {
            get
            {
                return _rdpSession.ApplicationFilter.Applications;
            }
        }

        /// <summary>
        ///     Open RDP session
        /// </summary>
        public void Open()
        {
            _rdpSession.Open();
        }

        /// <summary>
        ///     Close RDP session
        /// </summary>
        public void Close()
        {
            _rdpSession.Close();
        }

        /// <summary>
        ///     Pause RDP session
        /// </summary>
        public void Pause()
        {
            _rdpSession.Pause();
        }

        /// <summary>
        ///     Resume RDP session
        /// </summary>
        public void Resume()
        {
            _rdpSession.Resume();
        }

        /// <summary>
        ///     Connect to client
        /// </summary>
        ///
        /// <param name="connectionString">Connection parameters</param>
        public void ConnectToClient(string connectionString)
        {
            _rdpSession.ConnectToClient(connectionString);
        }

        /// <summary>
        ///     Create connection parameters    
        /// </summary>
        ///
        /// <param name="groupName">Group name</param>
        /// <param name="password">Password</param>
        ///
        /// <returns>string</returns>
        public string CreateInvitation(string groupName, string password)
        {
            var invitation = _rdpSession.Invitations.CreateInvitation(null, groupName, password, 1);
            
            return invitation.ConnectionString;
        }

        /// <summary>
        ///     Dispose connection
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_rdpSession != null)
                {
                    foreach (IRDPSRAPIAttendee attendees in _rdpSession.Attendees)
                    {
                        attendees.TerminateConnection();
                    }

                    _rdpSession.Close();
                }
            }
            catch
            {
            }
        }
        
        /// <summary>
        ///     Attendee connected
        /// </summary>
        ///
        /// <param name="pAttendee">Parameter</param>
        private void OnAttendeeConnected(object pAttendee)
        {
            var attendee = (IRDPSRAPIAttendee)pAttendee;
            attendee.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;
        }
    }
}