using Scodix.PressToStudio;
using System;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Timers;

namespace ScodixServiceClient
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FileTransferClient : IPublishing 
    {
        private int m_clientId;
       

        private ChannelFactory<IFileTransfer> m_channelFactory;
        IFileTransfer _proxySubStreaming;
        public event EventHandler<string> ServerDisconnectEvent;
        private Timer m_keepAliveTimer;
        public FileTransferClient()
        {
           
        }


        public void Init(int clientId, string port, string address)
        {
            m_clientId = clientId;            
            string endpointSubStreaming = "net.tcp://" + address + ":" + port + "/ServiceManagerSubscriberStreaming";
            MakeProxySubStreaming(endpointSubStreaming, this);            
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            //Server disconnected - should reconnect
            ServerDisconnectEvent?.Invoke(this, "ServerDisconnect");
        }



        private void MakeProxySubStreaming(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding();
            netTcpbinding.MaxReceivedMessageSize = int.MaxValue;
            netTcpbinding.MaxBufferSize = int.MaxValue;
            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);

            netTcpbinding.TransferMode = TransferMode.Streamed;           
            netTcpbinding.MaxBufferSize = 2147483647;
            netTcpbinding.MaxReceivedMessageSize = 2147483647;
            netTcpbinding.Security.Mode = SecurityMode.None;

            netTcpbinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            netTcpbinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            netTcpbinding.ReaderQuotas.MaxArrayLength = 2147483647;

            InstanceContext context = new InstanceContext(callbackinstance);            
            m_channelFactory = new ChannelFactory<IFileTransfer>( netTcpbinding, endpointAddress);
            
            _proxySubStreaming = m_channelFactory.CreateChannel();        
         
            keepAlive();
        }


        public void keepAlive()
        {
            //KeepAlive should be invoke at least once each 2 seconds.
            m_keepAliveTimer = new Timer(5000);
            m_keepAliveTimer.Elapsed += KeepAliveTimer_Elapsed;
            m_keepAliveTimer.Start();
        }

        private void KeepAliveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                CommunicationState state = m_channelFactory.State;
                _proxySubStreaming.KeepAlive();
            }
            catch (Exception ex)
            {
                m_keepAliveTimer.Stop();
                m_keepAliveTimer.Close();
                m_keepAliveTimer.Elapsed -= KeepAliveTimer_Elapsed;
                Channel_Faulted(this, null);
            }
        }

        public void RetrieveFile(string filePath, string dirToSaveTo, bool IsBitMap)
        {
            Stream inputStream = null;
            FileStream writeStream = null;
            MemoryStream writeBitMapStream = null;
            const int chunkSize = 1048576;
            int bytesRead = 0;
            long downloadedLength = 0;
            byte[] buffer = new byte[chunkSize];
      

            FileData fileData = new FileData();
            fileData.FileNameFullPath = filePath;           
            fileData.DirectoryName = Path.GetDirectoryName(filePath);
            
            fileData.FileName = Path.GetFileName(filePath);

            string saveToFile = dirToSaveTo + fileData.FileName;

            ResponseFileDetails retVal = _proxySubStreaming.UploadFileDataName(fileData);

            long length = retVal.Length;
            inputStream = retVal.FileByteStream;
            
            if (IsBitMap)
            {
                writeBitMapStream = new MemoryStream();
                while (length != downloadedLength)
                {

                    bytesRead = inputStream.Read(buffer, 0, chunkSize);                    
                    writeBitMapStream.Write(buffer, 0, bytesRead);

                    downloadedLength += bytesRead;
                }
                using (Image image = Image.FromStream(writeBitMapStream))
                {
                    // Upon success image contains the bitmap
                    //  and can be saved to a file:
                    image.Save(saveToFile);
                }
                writeBitMapStream.Close();
            }
            else
            {
                writeStream = new FileStream(saveToFile, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                while (length != downloadedLength)
                {

                    bytesRead = inputStream.Read(buffer, 0, chunkSize);
                    writeStream.Write(buffer, 0, bytesRead);
                    downloadedLength += bytesRead;
                }
                writeStream.Close();
            }
            

        }

        public void ServerNotify(string state)
        {
            throw new System.NotImplementedException();
        }

        public void ReflectionNotify(ServiceType type, string data)
        {
            throw new System.NotImplementedException();
        }
    }

}