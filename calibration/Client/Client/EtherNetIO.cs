using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Net.Sockets;//use this namespace for sockets
using System.Net;//for ip addressing
using System.IO;//for streaming io
using System.Threading;
//using ChatClient;//for running threads
   public enum RobotJogMode{步進,連續};


    public class EtherNetIO
    {

     

        public TcpClient Client;//variable needed to listen for connections
        private StreamReader MessageReader;//variable for reading messages
        private BinaryWriter MessageWriter;//variable for writing messages
        private NetworkStream DataStream;//variable for keeping server and client in a stream and synchronized
        private Thread ClientThread;//variable that is assigned to a thread listening for incoming connections and preventing the pc from blocking
        private bool IsConnected;
        private bool IsLogIn;


        public RichTextBox RichTextBoxMessage = null;
        string SpelLang;

        string Ip, Port;
        delegate void UpdateTextBox(string msg);
        delegate void UpdateTextBox2(RichTextBox rh,string msg);

       static public Thread ShowConnecting(string title)
        {
            Thread th=null;
            th = new Thread(new ThreadStart(
                                delegate()
            {
                      System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
                            frm.Text = title;
                            System.Windows.Forms.ProgressBar pg=new System.Windows.Forms.ProgressBar();
                            pg.Maximum = 100;
                            frm.ControlBox = false;
                         
                            
                            frm.Controls.Add(pg);
                            frm.ClientSize= pg.ClientSize;
                            Thread ths = new Thread(new ParameterizedThreadStart(
                                         delegate(System.Object ob)
                                         {
                                             System.Windows.Forms.ProgressBar ps = (System.Windows.Forms.ProgressBar)ob;

                                             while (ps != null)
                                             {
                                                 try
                                                 {
                                                     if (th.IsAlive == false)
                                                         break;

                                                     if (ps.InvokeRequired)
                                                     {
                                                      
                                                         
                                                         ps.Invoke(
                                                             new ThreadStart(
                                                                 delegate()
                                                                 {
                                                                     if (ps.Value < 100)
                                                                         ps.Value += 1;
                                                                     else
                                                                         ps.Value = 1;
                                                                     Thread.Sleep(50);

                                                                 }

                                                                 )
                                                             );


                                                     }

                                                     else
                                                     {

                                                         if (ps.Value < 100)
                                                             ps.Value += 1;
                                                         else
                                                             ps.Value = 1;
                                                         Thread.Sleep(50);
                                                        

                                                     }

                                                 }
                                                 catch (Exception ex)
                                                 {

                                                     //MessageBox.Show(ex.ToString());
                                                    // break;
                                                 }
                                             
                                             }
                                         

                                         }

                                         ));


                            ths.Start(pg);
                          

                            frm.ShowDialog();

                         
                           
                        

                  
                                
                                    
                                
                               
                            
                     
              
                            
            }));

            th.Start();
            return th;

        }


        public void Connect(string Ip_, string Port_, RichTextBox RichTextBoxMessage_)
        {
            try
            {
             

                    Ip = Ip_;
                    Port = Port_;

                    if (RichTextBoxMessage_ != null)
                        RichTextBoxMessage = RichTextBoxMessage_;

                    //try Connecting on the give ip address
                    try
                    {
                       // if (Client == null)
                            Client = new TcpClient();//assign new tcp client object

                        if (Client.Connected == false)
                        {
                           // ChangeTextBoxContent("正在連線......");
                           Thread th=ShowConnecting("正在連接EtherMoteIO");//顯示ProgressBar


                            try
                            {
                                Client.Connect(IPAddress.Parse(Ip), Convert.ToInt32(Port));//connect to given ip on port 80 allways
                            }
                            catch(Exception ex)
                            {
                              th.Abort();
                                MessageBox.Show(ex.Message);
                                return;
                            }
                          th.Abort();


                       

                            DataStream = Client.GetStream();

                            MessageReader = new StreamReader(DataStream);
                            MessageWriter = new BinaryWriter(DataStream);
                            IsConnected = true;

                            //MessageBox.Show("連線成功");
                            ChangeTextBoxContent("己連線");

                           // HandleConnection();
                           HandMessageRespone = new Thread(new ThreadStart(HandleConnection));
                        HandMessageRespone.Start();
                         LogIn("1234");

                  
                        }
                        else
                        {
                            MessageBox.Show("己有連線");
                        }

                       // HandMessageRespone = new Thread(new ThreadStart(HandleConnection));
                      //HandMessageRespone.Start();
                     // LogIn("1234");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("連線失敗");//signal the error in a message box
                  
                    }
                  // PerformConnection();

            }
            catch (Exception)
            {
                MessageBox.Show("Wrong Ip Address");//signal the error in a message box
            }
        }

        //delegate void UpdateTextBox2(RichTextBox rh, string msg);
        public static void ChangeTextBoxContent2(System.Windows.Forms.RichTextBox RichTextBoxMessage, string tx)
        {
            if (RichTextBoxMessage != null)
            {
                if (RichTextBoxMessage.InvokeRequired)//if the messages text box needs a delegate invoking
                {
                    RichTextBoxMessage.Invoke(new UpdateTextBox2(ChangeTextBoxContent2), new object[] { RichTextBoxMessage, tx });
                }
                else
                {
                    //if no invoking required then change
                    // RichTextBoxMessage.SelectAll();
                    RichTextBoxMessage.Text += tx + System.Environment.NewLine; //concatinate the original with the given message and a new line

                    RichTextBoxMessage.SelectionStart = RichTextBoxMessage.TextLength;
                    RichTextBoxMessage.ScrollToCaret();
                }


            }
        }

        public  void  ChangeTextBoxContent(string tx)
        {
            if (RichTextBoxMessage != null)
            {
                if (RichTextBoxMessage.InvokeRequired)//if the messages text box needs a delegate invoking
                {
                    RichTextBoxMessage.Invoke(new UpdateTextBox(ChangeTextBoxContent), new object[] { tx });
                }
                else
                {
                 //if no invoking required then change
                   // RichTextBoxMessage.SelectAll();
                    try
                    {
                        RichTextBoxMessage.Text += tx + System.Environment.NewLine; //concatinate the original with the given message and a new line
                        if (initial == true)//必須必單初始化後來能對RichTextBox操作
                        {
                            RichTextBoxMessage.SelectionStart = RichTextBoxMessage.TextLength;
                            RichTextBoxMessage.ScrollToCaret();
                        }
                    }
                    catch
                    { 
                    }

                }
                
              
             

                

            
         

            }
        }

        string responestring;
        public bool handling;
        int readtime=0;
        bool ShowReadText = true;
        public void HandleConnection()
        {
            string message;
            //loop until infinity
           

            do
            {
                //try reading from the data stream if anything went wrong with the connection break
                try
                {

                 
                        message = MessageReader.ReadLine();//read message
                      
                        if(ShowReadText==true)
                        ChangeTextBoxContent(message);//call the function that manipulates text box from a thread and change the contents.
                      if(sending==true)
                        responestring = message;
                        sending = false;
                    
                     
                }
                catch (Exception)
                {
                   // MessageBox.Show("connection Lost");

                    // ChangeTextBoxContent("connection Lost");
                    break;//get out of the while loop
                }

            } while (true);

           // MessageWriter.Close();
          //  MessageReader.Close();
          //  DataStream.Close();
         //   Client.Close();

        }

        Thread HandMessageRespone;

        public void PerformConnection()
        {
           
        }

        public void CloseHandleConnect()
        {
            try
            {
                MessageWriter.Close();
                MessageReader.Close();
            }
            catch
            { 
            
            }
         
        
        }
       public string GetResponse()
        {
            string message = "";
            try
            {

               MessageReader = new StreamReader(DataStream);
             message = MessageReader.ReadLine();//read message
             
                return message;
              
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
             return message;
            }

          

           // ChangeTextBoxContent(message);//call the function that manipulates text box from a thread and change the contents.
        }


       public string GetResponseString()
       {
           string message = "";
           try
           {
               while (sending == true)
               {
                   Thread.Sleep(1);
               }
              
  

               return responestring;

           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.ToString());
               return message;
           }



           // ChangeTextBoxContent(message);//call the function that manipulates text box from a thread and change the contents.
       }

       public string GetResponseStringWaitReadTimeClear()
       {

           string message = "";
           try
           {
               while (sending == true)
               {
                   Thread.Sleep(1);
               }

               while (readtime>0)
               {
                   Thread.Sleep(1);
               }

               return responestring;

           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.ToString());
               return message;
           }



           // ChangeTextBoxContent(message);//call the function that manipulates text box from a thread and change the contents.
       }



     string sendandrespond(string command)
       {
          
           send(command);

           string result = GetResponse();
           return result;
        
       }

        public void LogIn(string password)
        {

            string sendCommand = "$login," + password;
                send(sendCommand);

               string result=GetResponseString();
              string code = result.Split(',')[1];
               if (code == "0") 
               ChangeTextBoxContent("登入密碼正確");
               else
               ChangeTextBoxContent("登入密碼錯誤");

              

        }

        public void LogOut()
        {
            string sendCommand = "$logout";
            send(sendCommand);

            string result = GetResponse();
            MessageBox.Show(result);
        }

        public void PowerOn()
        {
          /*
            string sendCommand = "$SetMotorsOn";//傳送啟動馬達
            send(sendCommand);

           string result =GetResponseString();
            MessageBox.Show(result);
           */
            CommentTH("$SetMotorsOn",null);
        }
       public bool initial= false;

        List<int> ToolList=new List<int>();




        public void  RobotInitial( System.Windows.Forms.ComboBox ToolListCom,System.Windows.Forms.Form frm,System.Windows.Forms.TextBox x, System.Windows.Forms.TextBox y, System.Windows.Forms.TextBox z, System.Windows.Forms.TextBox u, System.Windows.Forms.TextBox rl,

            System.Windows.Forms.TextBox tx, System.Windows.Forms.TextBox ty, System.Windows.Forms.TextBox tz, System.Windows.Forms.TextBox tu)
        {
        //確定己連線
            initial = false;
       frm.Enabled = false;

       if (Client.Connected == false)
       {
           frm.Close();
       }

            Thread InitialThread = new Thread(new ThreadStart(
             delegate()
             {
                 while (Client == null)
                 {
                     Thread.Sleep(1);
                 }

                 while (Client.Connected == false)
                 {
                     Thread.Sleep(1);
                 }






                 //目前的工具座標

                 send("$execute,\"" + "print Tool " + "\"");

                 string currentool = GetResponseString();
                 currentool = currentool.Split(',')[1];
                 currentool = currentool.Substring(1, currentool.Length - 1);
                 int a = Convert.ToInt32(currentool);

                 Thread.Sleep(5);

                 //列出可用工具座標
                 ToolList.Clear();
                 ShowReadText = false;
                 for (int i = 0; i <= 15; i++)
                 {
                     send("$execute,\"" + "Tool " + i.ToString() + "\"");
                     string result = GetResponseString();
                     if (result.Contains("#execute,0"))
                     {

                         ToolList.Add(i);

                     }

                     //  MessageBox.Show(result);
                 }


                 if (ToolListCom.InvokeRequired)
                 {
                     ToolListCom.Invoke(new updateui(
                         delegate()
                         {
                             ToolListCom.DataSource = this.ToolList;
                             ToolListCom.SelectedItem = a;
                         }

                     ));
                 }
                 else
                 {
                     ToolListCom.DataSource = this.ToolList;
                     ToolListCom.SelectedItem = a;
                 }
                 Thread.Sleep(5);
                 string str;
                 send("$Reset");
                 str = GetResponseString();


      
                 






                 try
                 {

                     send("$execute,\"" + "Tool " + a.ToString() + "\"");
                     string result = GetResponseString();
                     Thread.Sleep(10);
                     GetToolNoThread(a.ToString(), tx, ty, tz, tu);
                     Thread.Sleep(10);
                     
                     this.TechNoThread(x, y, z, u, rl);
                     Thread.Sleep(10);
                         
            
                     ChangeTextBoxContent("初始化完成");
                   

                   
                    

                     initial = true;
                 }
                 catch (Exception ex)
                 {
                    // MessageBox.Show(ex.ToString());
                 }


                 int ass = 0;
             redo:

                 Thread.Sleep(10);
                 send("$SetMotorsOn");
                 str = GetResponseString();

                 /*
                 if (str.Split(',')[1] != "0")
                 {
                     ass++;
                     
                     // if(ass>5)
                    // MessageBox.Show("遠端IO未啟動");
                     goto redo;
                 }
                 */




                 ShowReadText = true;
                 if (frm.InvokeRequired)
                 {
                     frm.Invoke(new updateui(
                         delegate()
                         {
                             frm.Enabled = true;
                            // frm.Focus();
                            // frm.Activate();
                         }
                         ));
                 }
                 else
                 {
                     frm.Enabled = true;
                     //frm.Focus();
                     //frm.Activate();
                 }
                 // str = GetResponseString();
                 //  MessageBox.Show(str);

             }

            ));


            InitialThread.Start();
        //

        
        }

        public void SendEtherModeComment(string EtherMoteComment)
        {
            Thread Commentth = new Thread(new ParameterizedThreadStart(
                   delegate(System.Object ob)
                   {
                       if (Client == null)
                       {
                           MessageBox.Show("未連線成功");
                           return;
                       }
                       string comment=(string)ob;
                       send(comment);
                       string str = GetResponseString();

                   
                      
                       /*
                       if (str.Split(',')[1] != "0")
                       {
                           MessageBox.Show("遠端IO未啟動");
                           return;
                       }
                       */
                      // Client.Close();
                      // Client.Client.Close();
                      // CloseHandleConnect();
                    

                   }

                  ));

            Commentth.Start(EtherMoteComment);
     
        }


         public void SendRobtMotionComment(string EtherMoteComment,string pointName,RobotAssembleBase.RobotPoint p1)
         {
            string codinate = "XY(" + p1.x + "," + p1.y + "," + p1.z+ "," + p1.u + ")";
            string comment=EtherMoteComment.Replace(pointName, codinate);

            SendRobtMotionComment(comment);
         }
       

        public void SendRobtMotionComment(string EtherMoteComment)
        {
            Thread Commentth=null;
            Commentth = new Thread(new ParameterizedThreadStart(
                   delegate(System.Object ob)
                   {
                       if (Client == null)
                       {
                           MessageBox.Show("未連線成功");
                           return;
                       }

                       valueget vb=new valueget(
                           delegate(string val)
                           {
                           
                                  /*
                                   send("$GetStatus");
                                   string str = GetResponseString();
                                   string code = str.Split(',')[1];
                                   string p8= code[9].ToString();
                             */
                               if (val.Contains("execute")&&val != "#execute,0")
                                   MessageBox.Show("指令錯誤");

                               Commentth.Abort();
                           

                           }
                       );

                        
                       ExecuteRotComment((string)ob, vb);
                       RobotAssembleBase.PauseDialog passd = new RobotAssembleBase.PauseDialog(this,(string)ob);
        
                          passd.ShowDialog();

                

                       /*
                       if (str.Split(',')[1] != "0")
                       {
                           MessageBox.Show("遠端IO未啟動");
                           return;
                       }
                       */
                       // Client.Close();
                       // Client.Client.Close();
                       // CloseHandleConnect();


                   }

                  ));

            Commentth.Start(EtherMoteComment);

        }



        public   Thread ThreadCom;
        public void Empty()
        {
        
        }

        public void CommentAbortandGetCodinate(valueget ValueGet)
        { 
            Thread AbortThread = new Thread(new ThreadStart(
             delegate()
             {
            
         
                 send("$Abort");//馬達停止

                 string result = GetResponseString();
                 string comments = "$execute,\"" + "print here" + "\"";
                 //列印目前座標值
                 redo:
                 send(comments);
                 result = GetResponseString();
                 if (result.Contains("X:") == false)
                 {
                     goto redo;
                   
                 }

                 ValueGet.Invoke(result);
             }

            ));


            AbortThread.Start();
        
        }

        public void CommentTH(string comment, valueget ValueGet)
        {
           
            if(ThreadCom==null)
            {
              ThreadCom=new Thread(new ThreadStart(Empty));
            }
            
            if(ThreadCom.IsAlive==false)
            {
               ThreadCom = new Thread(new ParameterizedThreadStart(

                    delegate(object ob)
                    {
                        string str =(string) ob;
                        string sendCommand = str;//傳送啟動馬達
                        send(sendCommand);

                        string result = GetResponseString();
                  


                        if (ValueGet != null)
                        {
                            ValueGet.Invoke(result);
                        }
              

                    }

                    ));
               ThreadCom.Start(comment);
            }

      
        }


        public void Pause()
        {
            string sendCommand = "$Pause";//傳送啟動馬達
            send(sendCommand);

           // string result = GetResponse();
          //  MessageBox.Show(result);
        }

        public void Continue()
        {
            string sendCommand = "$Continue";//傳送啟動馬達
            send(sendCommand);
           // string result = GetResponse();
            //MessageBox.Show(result);
        }

        public void EvalueMoveRange(string dir,double CurrentX,double CurrentY,string handDirection,double maxR,double minR,out double value)
        {

            double cx;
            double cy;
            double R;
            value = 1;
        
            switch (dir)
            {
                case "X+":
                    {

                        for (int i = 0; i < 600; i++)
                        {
                            cx = i + CurrentX;
                            cy = CurrentY;
                            R = System.Math.Pow(cx * cx + cy * cy, 0.5);
                            //value = cx;
                            if (R > maxR ||R<minR)
                            {
                                break;
                            }


                            value = (double)i;
                        }





                            break;
                    }

                case "X-":
                    {
                        for (int i = 0; i < 600; i++)
                        {
                            cx =  CurrentX-i;
                            cy = CurrentY;
                            R = System.Math.Pow(cx * cx + cy * cy, 0.5);
                            //value = cx;
                            if (R > maxR || R < minR)
                            {
                                break;
                            }
                            value = (double)i;

                        }
                        break;
                    }

                case "Y+":
                    {
                        for (int i = 0; i < 600; i++)
                        {
                            cx = CurrentX ;
                            cy = CurrentY+i;
                            R = System.Math.Pow(cx * cx + cy * cy, 0.5);
                            //value = cx;
                            if (R > maxR || R < minR)
                            {
                                break;
                            }

                           



                            value = (double)i;





                        }
                        break;
                    }


                case "Y-":
                    {
                        for (int i = 0; i < 600; i++)
                        {
                            cx = CurrentX;
                            cy = CurrentY -i;
                            R = System.Math.Pow(cx * cx + cy * cy, 0.5);
                            //value = cx;
                            if (R > maxR || R < minR)
                            {
                                break;
                            }

                            if (handDirection == "R")
                            {
                                if (cx > 0 && cy < 40)
                                    break;

                            }

                            if (handDirection == "L")
                            {
                                if (cx < 0 && cy < 40)
                                    break;

                            }

                            value = (double)i;

                        }
                        break;
                    }


                default:
                    break;
            }



            //value = 1.0;
        }


public void  ToolToOrg(double x ,double  y , double deg,double z, double tx,double  ty,double tu,double tz,out double orgx,out double orgy,out double orgthea,out double orgz )
{	


double rad, dx, dy;
double ru = deg - tu;

ru = ru/180.0*Math.PI;
dx = tx * Math.Cos(ru) - ty * Math.Sin(ru);
dy = tx * Math.Sin(ru) + ty * Math.Cos(ru);
orgx = x - dx;
orgy = y - dy;
orgthea=deg-tu;
orgz = z - tz;

}

        public void  ExecuteRotComment(string comment,valueget vb)
        {
            try
            {
              string sendCommand = "$execute,\"" + comment + "\"";
              CommentTH(sendCommand,vb);
                   
                   
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        }

        public void Reset()
        {
            
            CommentTH("$Reset", null);
        }

        public void PowerOff()
        {
            CommentTH("$SetMotorsOff",null);

            /*
            string sendCommand = "$SetMotorsOff";//傳送關閉馬達
            send(sendCommand);
            string result = GetResponse();
            MessageBox.Show(result);
             */

        }

        public void Home()
        {
            string sendCommand = "$Home";//傳送手臂回到Home點
            send(sendCommand);
            string result = GetResponse();
            MessageBox.Show(result);
        }

        public void GetHerePosition()
        {

            string sendCommand = "$execute,\"print here\"";
            send(sendCommand);
            string result = GetResponse();
             result = GetResponse();
            MessageBox.Show(result);

        }

        public void GetStatus()
        {
            string sendCommand = "$GetStatus";
            Thread th = new Thread(new ThreadStart(delegate()
                {
                    while (true)
                    {
                        sendandrespond(sendCommand);
                        
                    }


                }

                ));

            /*
            send(sendCommand);
            string result = GetResponse();
            MessageBox.Show(result);
             */
             
        }

        public void Abort(bool responemessage )
        {
            
            string sendCommand = "$Abort";
            send(sendCommand);
           // string result = GetResponse();
           // if (responemessage == true)
           // MessageBox.Show(result);
            
            

          //  CommentTH("$Abort");
        }

   bool sending;

        public void send(string sendCommand)
        {
            sending = true;
            //check if the their is a connection first
            try
            {
                if (Client.Connected)
                {
                    // textBox2.Text = sendCommand;
                    //textBox1.Text = textBox1.Text + sendCommand + "\r\n";
                    sendCommand += System.Environment.NewLine;
                    //sendCommand = sendCommand + System.Environment.NewLine;
                    //MessageWriter.Write(sendCommand);//send message via stream

                    ASCIIEncoding code = new ASCIIEncoding();
                    Byte[] bbb = code.GetBytes(sendCommand);
                    DataStream.Write(bbb, 0, bbb.Length);

                }
            }
            catch (Exception)
            {
                IsConnected = false;　
                MessageBox.Show("no client is connected");//signal the error in a messagebox
            }

        }

        public void PowerHigh()
        {
            string sendCommand = "$execute,\"Power High\"";
            send(sendCommand);

            string result = GetResponse();
            MessageBox.Show(result);
        }

        public void PowerLow()
        {
            string sendCommand = "$execute,\"Power Low\"";
            send(sendCommand);

            string result = GetResponse();
            MessageBox.Show(result);
        }

        public void Start(int FuntionNo)
        {
            string sendCommand = "$Start," + FuntionNo.ToString();
            send(sendCommand);

          // string result = GetResponseString();
          //  MessageBox.Show(result);

        }
        public void Stop()
        {
            string sendCommand = "$Stop";
            send(sendCommand);
           
           // string result = GetResponse();
            //if (responemessage==true)
            //MessageBox.Show(result);
        }

       public  void SFree( )
        {
            string comment = "SFree";
            string sendCommand = "$execute,\"" + comment + "\"";
            CommentTH(sendCommand,null);
      // ExecuteRotComment("SFree",true);
           

            
            
        }

       public void SLock()
       {
          // ExecuteRotComment("SLock",true);
           string comment = "SLock";
           string sendCommand = "$execute,\"" + comment + "\"";
           CommentTH(sendCommand,null);
       }

       public void Free()
       {
           // ExecuteRotComment("SLock",true);
           string comment = "$Reset";
   
           CommentTH(comment, null);
       }

      public  delegate void valueget(string str);

       public valueget OnValueGet;
            public   delegate void updateui();


            public void SplitCoidinate(string a,out string x, out string y,out string z,out string u,out string rl)
            {
                string substring;
                //int x=a.IndexOf("X");
                x= a.Substring(a.IndexOf("X:")+2, a.IndexOf("Y:") - a.IndexOf("X:")-2);
                x=x.Trim();
                y = a.Substring(a.IndexOf("Y:") + 2, a.IndexOf("Z:") - a.IndexOf("Y:") - 2);
                y=y.Trim();

                z= a.Substring(a.IndexOf("Z:") + 2, a.IndexOf("U:") - a.IndexOf("Z:") - 2);
                z=z.Trim();
                u= a.Substring(a.IndexOf("U:") + 2, a.IndexOf("V:") - a.IndexOf("U:") - 2);
                u=u.Trim();

                rl= a.Substring(a.IndexOf("/") , a.LastIndexOf("/") - a.IndexOf("/") );
                rl=rl.Trim('/');
                rl = rl.Trim();

             //  z = a.Substring(a.IndexOf("Z:") + 2, a.IndexOf("U:") - a.IndexOf("Z:") - 2);
            //  z.Trim();
              // rl = "";

            }

            public void Teach(System.Windows.Forms.TextBox x, System.Windows.Forms.TextBox y, System.Windows.Forms.TextBox z, System.Windows.Forms.TextBox u, System.Windows.Forms.TextBox rl)
       {
         EtherNetIO.valueget vb = new EtherNetIO.valueget(
                delegate(string str)
                {

                redo:
                    //  MessageBox.Show(result);
                 
                    string comments = "$execute,\"" + "print here" + "\"";
                   send(comments);
                    string result =GetResponseString();


                    string codinate = "";
                    try
                    {
                        codinate = result.Split(',')[1];
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1);
                        goto redo;
                    }
                    string xr, yr, zr, ur, rlr;
                    try
                    {
                       
                        SplitCoidinate(codinate, out xr, out  yr, out  zr, out  ur, out rlr);
                    }
                    catch
                    {
                        goto redo;
                    }

                 //   MessageBox.Show(str);
                    if (x.InvokeRequired)
                    {
                        x.Invoke(new updateui(delegate(){x.Text = xr;}));
                    }
                    else
                        x.Text = xr;


                    if (y.InvokeRequired)
                    {
                        y.Invoke(new updateui(delegate() { y.Text = yr; }));
                    }
                    else
                        y.Text = yr;
                    if (z.InvokeRequired)
                    {
                       z.Invoke(new updateui(delegate() { z.Text = zr; }));
                    }
                    else
                        z.Text = zr;

                  
                    if (u.InvokeRequired)
                    {
                       u.Invoke(new updateui(delegate() { u.Text = ur; }));
                    }
                    else
                        u.Text = ur;


                    if (rl.InvokeRequired)
                    {
                        rl.Invoke(new updateui(delegate() { rl.Text = rlr; }));
                    }
                    else
                        rl.Text = rlr;

                }
            );
         string comment = "$execute,\"" + "print here" + "\"";
         CommentTH(comment, vb);
        // GetHere(vb);
       }


            void TechNoThread(System.Windows.Forms.TextBox x, System.Windows.Forms.TextBox y, System.Windows.Forms.TextBox z, System.Windows.Forms.TextBox u, System.Windows.Forms.TextBox rl)
            { 
            
              redo:
                    //  MessageBox.Show(result);
                System.Threading.Thread.Sleep(10);
                    string comments = "$execute,\"" + "print here" + "\"";
                   send(comments);
                    string result =GetResponseString();

                   
                    string codinate = "";
                    try
                    {
                        codinate = result.Split(',')[1];
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1);
                        goto redo;
                    }
                    string xr, yr, zr, ur, rlr;
                    try
                    {
                       
                        SplitCoidinate(codinate, out xr, out  yr, out  zr, out  ur, out rlr);
                    }
                    catch
                    {
                        goto redo;
                    }

                 //   MessageBox.Show(str);
                    if (x.InvokeRequired)
                    {
                        x.Invoke(new updateui(delegate(){x.Text = xr;}));
                    }
                    else
                        x.Text = xr;


                    if (y.InvokeRequired)
                    {
                        y.Invoke(new updateui(delegate() { y.Text = yr; }));
                    }
                    else
                        y.Text = yr;
                    if (z.InvokeRequired)
                    {
                       z.Invoke(new updateui(delegate() { z.Text = zr; }));
                    }
                    else
                        z.Text = zr;

                  
                    if (u.InvokeRequired)
                    {
                       u.Invoke(new updateui(delegate() { u.Text = ur; }));
                    }
                    else
                        u.Text = ur;


                    if (rl.InvokeRequired)
                    {
                        rl.Invoke(new updateui(delegate() { rl.Text = rlr; }));
                    }
                    else
                        rl.Text = rlr;

              
    
            
            
            
            }



            public void GetTool(string Toolnum,System.Windows.Forms.TextBox x, System.Windows.Forms.TextBox y, System.Windows.Forms.TextBox z, System.Windows.Forms.TextBox u)
            {
                  Thread ThreadCom = new Thread(new ThreadStart(
                       delegate()
                       {

                       redo:
                           //  MessageBox.Show(result);

                           string comments = "$execute,\"" + "tlSet " + Toolnum + "\"";
                           send(comments);
                           string result = GetResponseString();


                           if (result.Contains("Tool")==false)
                           {
                               goto redo;

                           }

                          
                           try
                           {
                               result = result.Substring(result.LastIndexOf(":")+1, result.Length-1 - result.LastIndexOf(":"));
                           }
                           catch
                           {
                               System.Threading.Thread.Sleep(1);
                               goto redo;
                           }


                           string codinate = "";

                           string xr, yr, zr, ur, rlr;

                           try
                           {
                               xr = result.Split(',')[0];
                               yr = result.Split(',')[1];
                               zr = result.Split(',')[2];
                               ur = result.Split(',')[3];
                               xr=xr.Trim();
                               yr = yr.Trim();
                               zr = zr.Trim();
                               ur = ur.Trim();
                           //    SplitCoidinate(codinate, out xr, out  yr, out  zr, out  ur, out rlr);
                           }
                           catch
                           {
                               goto redo;
                           }

                           //   MessageBox.Show(str);
                           if (x.InvokeRequired)
                           {
                               x.Invoke(new updateui(delegate() { x.Text = xr; }));
                           }
                           else
                               x.Text = xr;


                           if (y.InvokeRequired)
                           {
                               y.Invoke(new updateui(delegate() { y.Text = yr; }));
                           }
                           else
                               y.Text = yr;
                           if (z.InvokeRequired)
                           {
                               z.Invoke(new updateui(delegate() { z.Text = zr; }));
                           }
                           else
                               z.Text = zr;


                           if (u.InvokeRequired)
                           {
                               u.Invoke(new updateui(delegate() { u.Text = ur; }));
                           }
                           else
                               u.Text = ur;


                         

                       }
                   ));

                  if (Toolnum != "0")
                  {
                      ThreadCom.Start();
                  }
                  else

                  {
                      if (x.InvokeRequired)
                      {
                          x.Invoke(new updateui(delegate() { x.Text = "0.0"; }));
                      }
                      else
                          x.Text = "0.0";


                      if (y.InvokeRequired)
                      {
                          y.Invoke(new updateui(delegate() { y.Text = "0.0"; }));
                      }
                      else
                          y.Text = "0.0";
                      if (z.InvokeRequired)
                      {
                          z.Invoke(new updateui(delegate() { z.Text = "0.0"; }));
                      }
                      else
                          z.Text = "0.0";


                      if (u.InvokeRequired)
                      {
                          u.Invoke(new updateui(delegate() { u.Text = "0.0"; }));
                      }
                      else
                          u.Text = "0.0";
                  
                  }
                // GetHere(vb);
            }



            public void GetToolNoThread(string Toolnum, System.Windows.Forms.TextBox x, System.Windows.Forms.TextBox y, System.Windows.Forms.TextBox z, System.Windows.Forms.TextBox u)
            {
           
                   if (Toolnum != "0")
                {
                redo:
                    Thread.Sleep(10);
                         //  MessageBox.Show(result);

                         string comments = "$execute,\"" + "tlSet " + Toolnum + "\"";
                         send(comments);
                         string result = GetResponseString();


                         if (result.Contains("Tool") == false)
                         {
                             goto redo;

                         }


                         try
                         {
                             result = result.Substring(result.LastIndexOf(":") + 1, result.Length - 1 - result.LastIndexOf(":"));
                         }
                         catch
                         {
                             System.Threading.Thread.Sleep(1);
                             goto redo;
                         }


                         string codinate = "";

                         string xr, yr, zr, ur, rlr;

                         try
                         {
                             xr = result.Split(',')[0];
                             yr = result.Split(',')[1];
                             zr = result.Split(',')[2];
                             ur = result.Split(',')[3];
                             xr = xr.Trim();
                             yr = yr.Trim();
                             zr = zr.Trim();
                             ur = ur.Trim();
                             //    SplitCoidinate(codinate, out xr, out  yr, out  zr, out  ur, out rlr);
                         }
                         catch
                         {
                             goto redo;
                         }

                         //   MessageBox.Show(str);
                         if (x.InvokeRequired)
                         {
                             x.Invoke(new updateui(delegate() { x.Text = xr; }));
                         }
                         else
                             x.Text = xr;


                         if (y.InvokeRequired)
                         {
                             y.Invoke(new updateui(delegate() { y.Text = yr; }));
                         }
                         else
                             y.Text = yr;
                         if (z.InvokeRequired)
                         {
                             z.Invoke(new updateui(delegate() { z.Text = zr; }));
                         }
                         else
                             z.Text = zr;


                         if (u.InvokeRequired)
                         {
                             u.Invoke(new updateui(delegate() { u.Text = ur; }));
                         }
                         else
                             u.Text = ur;




               
             

             
                  
                }
                else
                {
                    if (x.InvokeRequired)
                    {
                        x.Invoke(new updateui(delegate() { x.Text = "0.0"; }));
                    }
                    else
                        x.Text = "0.0";


                    if (y.InvokeRequired)
                    {
                        y.Invoke(new updateui(delegate() { y.Text = "0.0"; }));
                    }
                    else
                        y.Text = "0.0";
                    if (z.InvokeRequired)
                    {
                        z.Invoke(new updateui(delegate() { z.Text = "0.0"; }));
                    }
                    else
                        z.Text = "0.0";


                    if (u.InvokeRequired)
                    {
                        u.Invoke(new updateui(delegate() { u.Text = "0.0"; }));
                    }
                    else
                        u.Text = "0.0";

                }
                // GetHere(vb);
            }



       public void GetHere(valueget OnValueGet)
       {
           string comment = "$execute,\"" + "print here" + "\"";
           if (ThreadCom == null)
           {
               ThreadCom = new Thread(new ThreadStart(Empty));
           }

           if (ThreadCom.IsAlive == false)
           {
               ThreadCom = new Thread(new ParameterizedThreadStart(

                    delegate(object ob)
                    {
                        string str = (string)ob;
                        string sendCommand = str;//傳送啟動馬達
                        send(sendCommand);

                     
                        string result = GetResponseString();
                        if(OnValueGet!=null)
                       OnValueGet.Invoke(result);
                        //  MessageBox.Show(result);
                   


                    }

                    ));
               ThreadCom.Start(comment);
           }
       
       
       }

    }//EtherNetIO
