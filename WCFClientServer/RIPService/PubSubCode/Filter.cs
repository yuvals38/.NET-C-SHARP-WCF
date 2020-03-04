using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using RIPCommon;

namespace RIPService
{

    public class FilterEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }

        public string ClientId { get; set; }
    }

    public class ResponseEventArgs : EventArgs
    {
        public string S{ get; set; }
    }


    public class Filter  
    {
        private static Filter instance = null;
        private static readonly object padlock = new object();
        public event EventHandler ClientConnected;
        public event EventHandler ClientDisConnected;

        public event EventHandler ResponseSent;

        Filter()
        {
        }

        public static Filter Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Filter();
                    }
                    return instance;
                }
            }
        }

        private Dictionary<string, List<IPublishing>> _ClientsList = new Dictionary<string, List<IPublishing>>();

        public event EventHandler<FilterEventArgs> Filtered;
        public Dictionary<string, List<IPublishing>> ClientsList
        {
            get {
                lock (typeof(Filter))
                {
                    return _ClientsList;
                }
            }

        }

        protected void OnFiltered(FilterEventArgs e)
        {
            EventHandler<FilterEventArgs> handler = Filtered;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public List<IPublishing> GetClients(String request)
        {
            lock (typeof(Filter))
            {
                if (ClientsList.ContainsKey(request))
                {
                    return ClientsList[request];
                }
                else
                    return null;
            }
        }

        public void AddClient(String request, IPublishing ClientCallbackReference)
        {
            lock (typeof(Filter))
            {
                if (ClientsList.ContainsKey(request))
                {
                    if (!ClientsList[request].Contains(ClientCallbackReference))
                    {
                        ClientsList[request].Add(ClientCallbackReference);                        
                    }
                }
                else
                {
                    List<IPublishing> newClientsList = new List<IPublishing>();
                    newClientsList.Add(ClientCallbackReference);
                    ClientsList.Add(request, newClientsList);

                    FilterEventArgs args = new FilterEventArgs();
                    Random r = new Random(100);
                    args.Threshold = r.Next();
                    args.TimeReached = DateTime.Now;
                    OnFiltered(args);


                }
                FilterEventArgs a = new FilterEventArgs()
                {
                    ClientId = "222"
                };
                if (ClientConnected != null)
                    ClientConnected(this, a);
            }

        }

        public void RemoveClient(String request, IPublishing ClientCallbackReference)
        {
            lock (typeof(Filter))
            {
                if (ClientsList.ContainsKey(request))
                {
                    if (ClientsList[request].Contains(ClientCallbackReference))
                    {
                        ClientsList[request].Remove(ClientCallbackReference);
                    }
                }
            }
        }

        public void RequestReceived(string s)
        {
            ResponseEventArgs a = new ResponseEventArgs()
            {
                S = s
            };

            if (ResponseSent != null)
                ResponseSent(this, a);
        }

    }
}
