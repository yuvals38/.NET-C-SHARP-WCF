using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Scodix.PressToStudio;
using Scodix.Kernel;
using Scodix.LoggingSystem;
using System.Collections.Concurrent;
using System.Timers;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace ScodixServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientNotifier : ISubscription, IFileTransfer, IRipData
    {

        public int ClientId;
        Type CLIENT_ID = typeof(int);
        Type CLIENT_MESSAGE = typeof(string);
        Timer m_timer;
        Timer m_keepAliveTimer;
        IPublishing m_registeredUser;


        private static ConcurrentDictionary<int, IPublishing> _callbackList = new ConcurrentDictionary<int, IPublishing>();
        private static ConcurrentDictionary<IContextChannel, int> _callbackListClientIdByChannel = new ConcurrentDictionary<IContextChannel, int>();
        private static ConcurrentDictionary<int, IContextChannel> _callbackListChannelByClientId = new ConcurrentDictionary<int, IContextChannel>();
        private static ConcurrentDictionary<int, DateTime> _clientKeepAliveMessageByClientId = new ConcurrentDictionary<int, DateTime>();       
        private RipData m_RipData = null;


        public ClientNotifier()
        {
            m_keepAliveTimer = new Timer(5000);
            m_keepAliveTimer.Elapsed += CheckKeepAlive;
            m_keepAliveTimer.Start();
        }

        public void Subscribe(int clientId)
        {
            m_registeredUser = OperationContext.Current.GetCallbackChannel<IPublishing>();

            IContextChannel channel = OperationContext.Current.Channel;
            ICommunicationObject obj = (ICommunicationObject)m_registeredUser;

            obj.Faulted += new EventHandler(Channel_Faulted);
            obj.Closed += new EventHandler(Channel_Faulted);

            var result = false;
            if (!_callbackList.Contains(new KeyValuePair<int, IPublishing>(clientId, m_registeredUser)))
            {
                result = _callbackList.TryAdd(clientId, m_registeredUser);
                if (result)
                {
                    LogServer.LogStatus(STT.General.INFO_GENERAL, $"ClientNotifier client ID= {clientId} had been subscribe ");
                }
                else
                {
                    LogServer.LogStatus(STT.General.ERROR_GENERAL, $"ClientNotifier client ID= {clientId} subscribtion Failure ");
                }

                result = _callbackListClientIdByChannel.TryAdd(channel, clientId);
                result = _callbackListChannelByClientId.TryAdd(clientId, channel);

                DateTime today = DateTime.Now;
                result = _clientKeepAliveMessageByClientId.TryAdd(clientId, today);
            }

            m_timer = new Timer(1000);
            m_timer.Elapsed += Channel_Opened;
            m_timer.Start();

        }

        private void Channel_Opened(object sender, EventArgs e)
        {
            LogServer.LogStatus(STT.General.INFO_GENERAL, $"ClientNotifier channel open successfuly ");
            m_registeredUser.ServerNotify("connect");
            m_timer.Stop();

            Console.WriteLine("connected.");
        }


        public void SendDataToClient(int clientId, string message)
        {
            try
            {
                IPublishing registeredUser = null;
                
                _callbackList.TryGetValue(clientId, out registeredUser);
                if (registeredUser != null)
                {
                    LogServer.LogStatus(STT.General.INFO_GENERAL, $"ClientNotifier message to client ID= {clientId} had been sent successfuly ");
                    registeredUser.ServerNotify(message);
                }
                else
                {
                    LogServer.LogStatus(STT.General.ERROR_GENERAL, $"ClientNotifier message to client ID= {clientId} message sent Failure ");
                }
               
            }
            catch (Exception e)
            {
                LogServer.LogStatus(STT.General.ERROR_GENERAL, $"ClientNotifier message to client ID= {clientId} message sent Failure ");
                Console.WriteLine(e.Message);
            }
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            int clientId = 0;

            foreach (var retriveChannelPair in _callbackListClientIdByChannel)
            {
                KeyValuePair<IContextChannel, int> myValue = retriveChannelPair;
                try
                {
                    IContextChannel testChannel = myValue.Key;
                    if (testChannel.State != CommunicationState.Opened)
                    {
                        clientId = myValue.Value;
                        UnSubscribe(clientId);
                        LogServer.LogStatus(STT.General.INFO_GENERAL, $"ClientNotifier unregister client ID= {clientId} successfuly ");
                    }
                }
                catch (Exception ex)
                {
                    clientId = myValue.Value;
                    LogServer.LogStatus(STT.General.ERROR_GENERAL, $"ClientNotifier Unable to unregister client ID= {clientId}  ");
                }
            }
        }

        public void UnSubscribe(int clientId)
        {
            IPublishing registeredUser = null;
            IContextChannel channel = null;
            DateTime value;

            bool result = false;

            if (!_callbackList.Contains(new KeyValuePair<int, IPublishing>(clientId, registeredUser)))
            {
                result = _callbackList.TryRemove(clientId, out registeredUser);
                if (result == true)
                {
                    result = _callbackListChannelByClientId.TryRemove(clientId, out channel);
                    if (result == true)
                    {
                        result = _callbackListClientIdByChannel.TryRemove(channel, out clientId);
                        result = _clientKeepAliveMessageByClientId.TryRemove(clientId, out value);
                        LogServer.LogStatus(STT.General.INFO_GENERAL, "client id:" + clientId + " was removed.");
                    }
                }
            }
            else
            {
                LogServer.LogStatus(STT.General.INFO_GENERAL, "unable to remove client id:" + clientId);
            }
        }

        public virtual void SendData(int clientId, string data)
        {
        }


        public void KeepAlive(int clientId)
        {
            DateTime today = DateTime.Now;
            DateTime value;

            _clientKeepAliveMessageByClientId.TryRemove(clientId, out value);
            _clientKeepAliveMessageByClientId.TryAdd(clientId, today);
        }


        private void CheckKeepAlive(object sender, ElapsedEventArgs e)
        {
            int MAX_ALLOW_KEEPALIVE_DIFF = 5000;
            DateTime today = DateTime.Now;

            foreach (var clientEntry in _clientKeepAliveMessageByClientId)
            {
                if ((today - clientEntry.Value).TotalSeconds > MAX_ALLOW_KEEPALIVE_DIFF)
                {
                    UnSubscribe(clientEntry.Key);
                }
            }
        }

        #region Not Implemented methods of ISubscription
        public void RegisterClient(ServiceType type, IManager manager)
        {
            throw new NotImplementedException();
        }

        public void Invoke(ServiceType type, string data)
        {
            throw new NotImplementedException();
        }

        public object Get(ServiceType type, string data)
        {
            throw new NotImplementedException();
        }

        public object GetPropertyData(ServiceType type, string data)
        {
            throw new NotImplementedException();
        }

        public string GetDeviceType(ServiceType type)
        {
            throw new NotImplementedException();
        }

        public ResponseFileDetails RetrieveRipFileFromServer(RipData ripData)
        {
            m_RipData = ripData;
            //get the package name given package id from the original package list.
            string fileName = "ServiceFramework.log";

            //given package id and filename need to get the source download file path.
            //get the file path of the package to download.
            string fileToBeRipped = null;
            ripData.Parameters.TryGetValue("RipThumbnail", out fileToBeRipped);

            
            string strIndex = null;
            bool ret = ripData.Parameters.TryGetValue("RipThumbnailIndex", out strIndex);

            int index = 0;
            if (ret == true)
            {
                index = Int32.Parse(strIndex);
            }

            //Async Task ripper           
            Task<bool> ripTask =  CommClientNotifier.Instance.RipTask(fileToBeRipped, index);
            if (ripTask.Result == true)
            {
                // task completed within timeout         
                // open stream
                Image rippedImage = CommClientNotifier.Instance.GetRippedImage();               
                ResponseFileDetails result = new ResponseFileDetails();
                result.FileName = fileName;
                Stream imageStream = ToStream(rippedImage);
                result.Length = imageStream.Length;
                result.FileByteStream = imageStream;
                result.ByteStart = 0;
                return result;              
            }
            else
            {
                return null; //Task didnt complete within timeout. 
            }                      
        }

        public static Stream ToStream(Image image)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            return stream;
        }


        ResponseFileDetails IFileTransfer.UploadFileDataName(FileData fileData)
        {
            //get the package name given package id from the original package list.
            string fileName = "ServiceFramework.log";

            //given package id and filename need to get the source download file path.
            //get the file path of the package to download.
            string filePath = Path.GetFullPath(fileData.FileNameFullPath);
            // get some info about the input file                
            FileInfo fileInfo = new FileInfo(filePath);

            // open stream
            FileStream stream = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            stream.Seek(0, SeekOrigin.Begin);
            ResponseFileDetails result = new ResponseFileDetails();
            result.FileName = fileName;
            result.Length = stream.Length;
            result.FileByteStream = stream;
            result.ByteStart = 0;
            return result;
        }

        public void KeepAlive()
        {
            //throw new NotImplementedException();
        }



        #endregion
    }
}
