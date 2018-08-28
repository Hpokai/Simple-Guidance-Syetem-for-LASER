using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("..\\..\\..\\Client\\Client\\bin\\Debug\\Client.exe");
        }
        delegate void UpdateTextBox2(RichTextBox rh, string msg);
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
        private void tcpipControl11_ReadLines(string result)
        {
            string temp_str = string.Empty;
            temp_str = result.Substring(0, result.IndexOf("\r\n"));//去除換行字元
            temp_str = temp_str.Trim();
            string[] ar = temp_str.Split(',');//取得分割陣列


            foreach (var array_str in ar)
            {
                switch (array_str)
                {
                    #region CCD1
                    case "START":
                        {
                            
                            ////////////////////////TB_CCD1.Inputs["Input"].Value = (double)num_OffsetU.Value;
                            
                            
                            TB_CCD1.Run();

                            //回報成功和失敗
                            int OK = 0;
                            if (TB_CCD1.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
                                OK = 1;
                            //this.tcpipControl11.WriteServer(OK.ToString() + "O");//回應處理狀況

                            //CCD定位的座標(組裝物、含計算角度的兩點位置)                            
                            double SX ;
                            double SY ;
                            double Degree;
                            double EY = 0;// =SamplePisitionAngle.EndY = (double)TB_CCD1.Outputs["EndY"].Value;
                            double X = 0;// =SamplePisitionAngle.X = (double)TB_CCD1.Outputs["X"].Value;
                            double Y = 0;// =SamplePisitionAngle.Y = (double)TB_CCD1.Outputs["Y"].Value;

                            //顯示數值
                            SX = (double)TB_CCD1.Outputs["Output"].Value + (double)num_OffsetX.Value;
                            SY = 0-(double)TB_CCD1.Outputs["Output1"].Value + (double)num_OffsetY.Value;
                            Degree = 360-((double)TB_CCD1.Outputs["Output2"].Value * (180 / Math.PI)) + (double)num_OffsetZ.Value;    




                            //顯示Dispaly

                            //this.cogRecordDisplay1.Record = TB_CCD1.CreateLastRunRecord().SubRecords["CogCalibNPointToNPointTool1.OutputImage"];
                            //this.cogRecordDisplay1.Record = TB_CCD1.CreateLastRunRecord().SubRecords["CogCalibCheckerboardTool1.OutputImage"];
                            this.cogRecordDisplay1.Record = TB_CCD1.CreateLastRunRecord().SubRecords["CogFixtureTool1.OutputImage"];
                            this.cogRecordDisplay1.StaticGraphics.Clear();
                            Cognex.VisionPro.CogGraphicLabel g = new Cognex.VisionPro.CogGraphicLabel();
                            if (OK == 0)
                            {
                                g.X = X - 20; 
                                g.Y = Y + 50;
                                g.Font = new Font("標楷體", 80);
                                g.Text = "NG";
                                g.Color = Cognex.VisionPro.CogColorConstants.Red;
                                ChangeTextBoxContent2(this.tx_ServerMessage, "STATUS = NG");
                                ChangeTextBoxContent2(this.tx_ServerMessage, "SX=0");
                                ChangeTextBoxContent2(this.tx_ServerMessage, "SY=0");
                                ChangeTextBoxContent2(this.tx_ServerMessage, "Degree=0");
                                ChangeTextBoxContent2(this.tx_ServerMessage, "NG" + "\r\n");
                                this.tcpipControl11.WriteServer("NG" + "\r\n");//回應處理狀況
                               // this.tcpipControl11.WriteServer("X" + "NG" + "\r\n");//回應處理狀況
                               // this.tcpipControl11.WriteServer("Y"+"NG" + "\r\n");//回應處理狀況
                               // this.tcpipControl11.WriteServer("A"+"NG" + "\r\n");//回應處理狀況
                            }
                            else
                            {
                                g.X = X - 20;
                                g.Y = Y + 50;
                                g.Font = new Font("標楷體", 80);
                                g.Text = "OK";
                                g.Color = Cognex.VisionPro.CogColorConstants.Green;
                                ChangeTextBoxContent2(this.tx_ServerMessage, "STATUS = OK");
                                ChangeTextBoxContent2(this.tx_ServerMessage, "SX=" + SX.ToString("#.###"));
                                ChangeTextBoxContent2(this.tx_ServerMessage, "SY=" + SY.ToString("#.###"));
                                ChangeTextBoxContent2(this.tx_ServerMessage, "Degree=" + Degree.ToString("#.###"));

                                this.tcpipControl11.WriteServer("X"+ SX.ToString("#.###") + "\r\n");//回應處理狀況
                                this.tcpipControl11.WriteServer("Y"+ SY.ToString("#.###") + "\r\n");//回應處理狀況
                                this.tcpipControl11.WriteServer("A"+ Degree.ToString("#.###") + "\r\n");//回應處理狀況          
                            
                            
                            
                            
                            
                            }
                            this.cogRecordDisplay1.StaticGraphics.Add(g, "0");
                            if (OK != 0)
                            {
                                g.Text = "角度=" +Degree.ToString("0.000");
                                g.Font = new Font("標楷體", 20);
                                g.Color = Cognex.VisionPro.CogColorConstants.Blue;
                                g.X = X;
                                g.Y = Y;
                                this.cogRecordDisplay1.StaticGraphics.Add(g, "1");
                            }
                            this.cogRecordDisplay1.AutoFit = true;
                            this.cogRecordDisplay1.Fit();



                            //儲存執行結果圖像
                            //string imagepath=SaveLastRunPicture(TB_CCD1, "CogCalibNPointToNPointTool1.OutputImage", "a");
                            //string imagepath = SaveLastRunPicture(TB_CCD1, "CogCalibCheckerboardTool1.OutputImage", "a");
                            //儲存記錄
                            //WriteCSVFileLine(new List<string>() { "CCD編號", "圖檔路徑", "X", "Y", "StarX", "StarY", "EndX", "EndY" }, "CCDRecord.csv");
                            //WriteCSVFileLine(new List<string>() { "1", imagepath, X.ToString(), Y.ToString(), SX.ToString(), SY.ToString(), EX.ToString(), EY.ToString() }, "CCDRecord.csv");
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }

        }
        Cognex.VisionPro.ToolBlock.CogToolBlock TB_CCD1;
        string CurrentDoc;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.tcpipControl11.Connect();
            CurrentDoc = System.Environment.CurrentDirectory;
            TB_CCD1 = (Cognex.VisionPro.ToolBlock.CogToolBlock)Cognex.VisionPro.CogSerializer.LoadObjectFromFile(CurrentDoc + "\\A0006.vpp");

            int counter = 0;
            string line = string.Empty;
            string line_ = string.Empty;
            double result = 0.0;
            if (File.Exists(CurrentDoc + "\\A0002.txt"))
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(CurrentDoc + "\\A0002.txt");


                NumericUpDown[] nUpDown = new NumericUpDown[] { num_OffsetX, num_OffsetY, num_OffsetZ, num_OffsetU, num_OffsetL };
                while ((line = file.ReadLine()) != null)    // 每次讀一行，自己跳
                {
                    System.Console.WriteLine(line);

                    if (-1 == line.IndexOf("\r\n"))
                    {
                        line_ = line;
                    }
                    else
                    {
                        line_ = line.Substring(0, line.IndexOf("\r\n"));
                    }


                    if (true == double.TryParse(line_, out  result))
                    {
                        Console.WriteLine("correct");
                        nUpDown[counter].Value = (decimal)result;
                    }
                    else
                    {
                        Console.WriteLine("fail");
                        nUpDown[counter].Value = 0;
                    }

                    counter++;
                }

                file.Close();
                ChangeTextBoxContent2(this.tx_ServerMessage, "read counter=" + counter);
                ChangeTextBoxContent2(this.tx_ServerMessage, "read SX=" + num_OffsetX.Value);
                ChangeTextBoxContent2(this.tx_ServerMessage, "read SY=" + num_OffsetY.Value);
                ChangeTextBoxContent2(this.tx_ServerMessage, "read SZ=" + num_OffsetZ.Value);
                ChangeTextBoxContent2(this.tx_ServerMessage, "read SU=" + num_OffsetU.Value);
                ChangeTextBoxContent2(this.tx_ServerMessage, "read SL=" + num_OffsetL.Value);
                // Suspend the screen.
                System.Console.ReadLine();
            }
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToolBlock frm = new ToolBlock(TB_CCD1, CurrentDoc + "\\A0006.vpp");
            frm.Show();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.num_OffsetX.Value = 0;
            this.num_OffsetY.Value = 0;
            this.num_OffsetZ.Value = 0;
            this.num_OffsetU.Value = 0;
            this.num_OffsetL.Value = 0;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(CurrentDoc + "\\A0002.txt");
            sw.Write(num_OffsetX.Value + "\r\n");
            sw.Write(num_OffsetY.Value + "\r\n");
            sw.Write(num_OffsetZ.Value + "\r\n");
            sw.Write(num_OffsetU.Value + "\r\n");
            sw.Write(num_OffsetL.Value + "\r\n");
            sw.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

           
                    //switch (counter)
                    //{
                    //    case 0:

                    //        if (-1 == line.IndexOf("\r\n"))
                    //        {
                    //            line_ = line;
                    //        }
                    //        else
                    //        {
                    //            line_ = line.Substring(0, line.IndexOf("\r\n"));
                    //        }

               
                    //        if (true == double.TryParse(line_, out  result))
                    //        {
                    //            Console.WriteLine("correct");
                    //            num_OffsetX.Value = (decimal)result;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("fail");
                    //            num_OffsetX.Value = 0;
                    //        }


                    //        break;

                    //    case 1:
                    //        if (-1 == line.IndexOf("\r\n"))
                    //        {
                    //            line_ = line;
                    //        }
                    //        else
                    //        {
                    //            line_ = line.Substring(0, line.IndexOf("\r\n"));
                    //        }

                           
                    //        if (true == double.TryParse(line_, out  result))
                    //        {
                    //            Console.WriteLine("correct");
                    //            num_OffsetY.Value = (decimal)result;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("fail");
                    //            num_OffsetY.Value = 0;
                    //        }

                    //        break;
                    //    case 2:
                    //        if (-1 == line.IndexOf("\r\n"))
                    //        {
                    //            line_ = line;
                    //        }
                    //        else
                    //        {
                    //            line_ = line.Substring(0, line.IndexOf("\r\n"));
                    //        }

             
                    //        if (true == double.TryParse(line_, out  result))
                    //        {
                    //            Console.WriteLine("correct");
                    //            num_OffsetZ.Value = (decimal)result;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("fail");
                    //            num_OffsetZ.Value = 0;
                    //        }

                    //        break;
                    //    case 3:
                    //        if (-1 == line.IndexOf("\r\n"))
                    //        {
                    //            line_ = line;
                    //        }
                    //        else
                    //        {
                    //            line_ = line.Substring(0, line.IndexOf("\r\n"));
                    //        }

          
                    //        if (true == double.TryParse(line_, out  result))
                    //        {
                    //            Console.WriteLine("correct");
                    //            num_OffsetU.Value = (decimal)result;
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("fail");
                    //            num_OffsetU.Value = 0;
                    //        }

                    //        break;

                    //    default:

                    //        break;
                    //}

        }

        private void num_OffsetU_ValueChanged(object sender, EventArgs e)
        {
            TB_CCD1.Inputs["Input"].Value = (double)num_OffsetU.Value;
        }

        private void num_OffsetL_ValueChanged(object sender, EventArgs e)
        {
            TB_CCD1.Inputs["Input1"].Value = (double)num_OffsetL.Value;
        }
    }
}
