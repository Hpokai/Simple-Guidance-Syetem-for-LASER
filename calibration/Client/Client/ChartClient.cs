using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;//use this namespace for sockets
using System.Net;//for ip addressing
using System.IO;//for streaming io
using System.Threading;

namespace RobotAssembleBase
{
    public partial class ChartClient : Form
    {

        public TcpClient Client;//variable needed to listen for connections
        private StreamReader MessageReader;//variable for reading messages
        private BinaryWriter MessageWriter;//variable for writing messages
        private NetworkStream DataStream;//variable for keeping server and client in a stream and synchronized
        private Thread HandMessageRespone;

        public ChartClient()
        {
            InitializeComponent();
        }

        delegate void UpdateTextBox(RichTextBox rh,string msg);
        public void ChangeTextBoxContent(System.Windows.Forms.RichTextBox RichTextBoxMessage, string tx)
        {
            if (RichTextBoxMessage != null)
            {
                if (RichTextBoxMessage.InvokeRequired)//if the messages text box needs a delegate invoking
                {
                    RichTextBoxMessage.Invoke(new UpdateTextBox(ChangeTextBoxContent), new object[] { RichTextBoxMessage, tx });
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

        static public Thread ShowConnecting(string title)
        {
            Thread th = null;
            th = new Thread(new ThreadStart(
                                delegate()
                                {
                                    System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
                                    frm.Text = title;
                                    System.Windows.Forms.ProgressBar pg = new System.Windows.Forms.ProgressBar();
                                    pg.Maximum = 100;
                                    frm.ControlBox = false;


                                    frm.Controls.Add(pg);
                                    frm.ClientSize = pg.ClientSize;
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

                                                             if (th.IsAlive == false)
                                                                 break;
                                                             if (th == null)
                                                                 break;

                                                         }
                                                         catch (Exception ex)
                                                         {

                                                             MessageBox.Show(ex.ToString());
                                                              break;
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
                    ChangeTextBoxContent(this.richTextBox1,message);//call the function that manipulates text box from a thread and change the contents.
                


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


        private void button1_Click(object sender, EventArgs e)
        {
                Client = new TcpClient();//assign new tcp client object

                        if (Client.Connected == false)
                        {
                           // ChangeTextBoxContent("正在連線......");
                           Thread th=ShowConnecting("正在連接EtherMoteIO");//顯示ProgressBar

                           Client.SendBufferSize = 1000;
                            try
                            {
                                Client.Connect(IPAddress.Parse(this.textBox1.Text), Convert.ToInt32(this.textBox2.Text));//connect to given ip on port 80 allways
                            }
                            catch(Exception ex)
                            {
                              th.Abort();
                              th = null;
                                MessageBox.Show(ex.Message);
                                return;
                            }
                          th.Abort();

                         

                       

                            DataStream = Client.GetStream();

                            MessageReader = new StreamReader(DataStream);
                            MessageWriter = new BinaryWriter(DataStream);
                            this.label1.Text="已連線";
                            HandMessageRespone = new Thread(new ThreadStart(HandleConnection));
                            HandMessageRespone.Start();
                            //MessageBox.Show("連線成功");
                        }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string comm = this.textBox3.Text;
                comm = comm+"\r\n";
                byte[] msg = Encoding.ASCII.GetBytes(comm);
                MessageWriter.Write(msg);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChartClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if(Client!=null)
                Client.Close();
                if(MessageReader!=null)
                MessageWriter.Close();
                if(MessageWriter!=null)
                MessageReader.Close();
            }
            catch
            {

            }
        }
    }
}
