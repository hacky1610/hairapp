using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Controller
{
    public static class Session
    {
        static List<Interfaces.ISession> sessions = new List<Interfaces.ISession>();

        static public void Register(Interfaces.ISession session)
        {
            sessions.Add(session);
        }

        static public void DeRegister(Interfaces.ISession session)
        {
            sessions.Remove(session);
        }

        static public void Save()
        {
            foreach (var session in sessions)
                session.Save();
        }

        static public void Restore()
        {
            foreach (var session in sessions)
                session.Restore();
        }

    }
}
