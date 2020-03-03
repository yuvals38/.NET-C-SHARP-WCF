using ScodixServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Scodix.Kernel;
using Scodix.LoggingSystem;
using System.Threading;

namespace ScodixServiceHost
{
    public class CommClientNotifier : ClientNotifier
    {

        public delegate void StringDataToServer(int clientId, string str);
        public event StringDataToServer StringDataToServerEvent;

       

        public delegate Image rippedCallBack(string fullPathRippedFile, int index);
        public rippedCallBack SetRippedCallBack;
        private Image m_FinalRippedImage = null;
        public static CommClientNotifier Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CommClientNotifier();                    
                }
                return m_Instance;
            }

        }
        private static CommClientNotifier m_Instance = null;

        private CommClientNotifier():base()
        {           
        }
      
        public override void SendData(int clientId, string data)
        {
            LogServer.LogStatus(STT.General.INFO_GENERAL, $"CommClientNotifier client ID= {clientId}send data to server side. ");
            CommClientNotifier.Instance.StringDataToServerEvent?.Invoke(clientId, data);          
        }
       
        public Task<bool> RipTask(string fileToBeRipped, int index = 0)
        {
            return Task.Run(() =>
            {
                if (SetRippedCallBack != null)
                    m_FinalRippedImage = SetRippedCallBack(fileToBeRipped, index);
                else
                    m_FinalRippedImage = null;
                return (m_FinalRippedImage != null) ? true : false;
            });
          
        }

        public Image GetRippedImage()
        {
            return m_FinalRippedImage;
        }

    }
}
