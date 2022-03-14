using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dbs_oobex
{
    /// <summary>
    /// 关于时间
    /// </summary>
    public class DTime
    {
        /// <summary>
        /// 延时等待函数
        /// </summary>
        /// <param name="delaynum">等待时间（毫秒）</param>
        public static void Delay(int delaynum)
        {
            DateTime time = DateTime.Now;//获得当前时间
            while ((DateTime.Now - time).TotalMilliseconds <= delaynum) ;//等待
        }
    }
    /// <summary>
    /// 系统命令相关
    /// </summary>
    public class Syscmd
    {
        /// <summary>
        /// 执行cmd命令，返回cmd命令的输出
        /// </summary>
        /// <param name="command">cmd命令</param>
        /// <param name="seconds">等待命令执行的时间（单位：毫秒），
        /// 如果设定为0，则无限等待</param>
        /// <returns>返回cmd命令的输出</returns>
        public static string ExecuteCMD(string command, int seconds)
        {
            string output = ""; //输出字符串
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }
        /// <summary>
        /// 执行powershell指令
        /// </summary>
        /// <param name="commands">指令</param>
        /// <param name="mseconds">等待时间(毫秒)</param>
        /// <returns></returns>
        public static string ExecutePwsh(string commands, int mseconds)
        {
            string output = ""; //输出字符串
            if (commands != null && !commands.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "powershell.exe";//设定需要执行的命令
                startInfo.Arguments = "-Command " + commands;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        if (mseconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(mseconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
    }
    /// <summary>
    /// API信息窗口
    /// </summary>
    public class WinMessage
    {
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, string message, string title, int type);//MessageBox函数导入
        public const int MB_OK = 0;//只有一个确定按钮
        public const int MB_YESNO = 0x01;//带有两个按钮：是，否
        public const int MB_RAF = 0x02;//带有3个按钮：重试，跳过，取消
        public const int MB_YESNOCANCEL = 0x03;//带有3个按钮：是，否，取消
        public const int MB_RETRYCANCEL = 0x05;//带有2个按钮：重试，取消
        public const int MB_CRC = 0x06;//带有3个按钮：取消，重试，继续
        public const int ICON_ERROR = 0x10;//错误图标
        public const int ICON_QUESTION = 0x20;//询问图标
        public const int ICON_WARNING = 0x30;//警告(惊叹号)图标
        public const int ICON_INFORMATION = 0x40;//信息图标
        public const int SOUND_NORMAL = 0x50;//这是啥来着……
    }
    /// <summary>
    /// 音频处理相关
    /// </summary>
    public class Media
    {
        [DllImport("winmm.dll")]
        public static extern int PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
        public const uint SND_SYNC = 0x0000;//同步播放
        public const uint SND_ASYNC = 0x0001;//异步播放
        public const uint SND_NODEFAULT = 0x0002;
        public const uint SND_MEMORY = 0x0004;//内存地址
        public const uint SND_LOOP = 0x0008;//循环播放
        public const uint SND_NOSTOP = 0x0010;
        public const uint SND_NOWAIT = 0x00002000;
        public const uint SND_ALIAS = 0x00010000;
        public const uint SND_ALIAS_ID = 0x00110000;
        public const uint SND_FILENAME = 0x00020000;//从文件播放
        public const uint SND_RESOURCE = 0x00040004;//资源文件
    }
    /// <summary>
    /// 系统配置
    /// </summary>
    public class WSystemd
    {
        public class Systemctl
        {
            [DllImport("kernel32.dll", EntryPoint = "SetComputerNameEx")]
            public static extern int apiSetComputerNameEx(int type, string lpComputerName);//有bug,别用
            public const int ASC_NORMAL_TYPE = 5;
            /// <summary>
            /// 获取系统计算机名
            /// </summary>
            /// <returns></returns>
            public static string GetComputerName()
            {
                try
                {
                    return Environment.GetEnvironmentVariable("ComputerName");
                }
                catch
                {
                    return "";
                }
            }
            /// <summary>
            /// 修改计算机名(通过注册表)
            /// </summary>
            /// <param name="newname">新名称</param>
            public static void SetComputerName(string newname)
            {
                RegistryKey pregkey;
                pregkey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Control\\ComputerName\\ComputerName", true);
                pregkey.SetValue("ComputerName", newname);
                pregkey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Services\\Tcpip\\Parameters", true);
                pregkey.SetValue("NV Hostname", newname);
                pregkey.SetValue("Hostname", newname);
                pregkey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", true);
                pregkey.SetValue("ComputerName", newname);
                pregkey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
                pregkey.SetValue("NV Hostname", newname);
                pregkey.SetValue("Hostname", newname);
            }
        }
        public class Users
        {
            /// <summary>
            /// 修改Administrator的密码
            /// </summary>
            /// <param name="NewPass">新密码</param>
            public static void ResetAdminPass(string NewPass)
            {
                //Create New Process
                System.Diagnostics.Process QProc = new System.Diagnostics.Process();
                //Do Something To hide Command(cmd) Window
                QProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                QProc.StartInfo.CreateNoWindow = true;
                //Call Net.exe
                QProc.StartInfo.WorkingDirectory = "C://Windows//SYSTEM32";
                QProc.StartInfo.FileName = "net.exe";
                QProc.StartInfo.UseShellExecute = false;
                QProc.StartInfo.RedirectStandardError = true;
                QProc.StartInfo.RedirectStandardInput = true;
                QProc.StartInfo.RedirectStandardOutput = true;
                //Prepare Command for Exec
                QProc.StartInfo.Arguments = @" user Administrator " + NewPass;
                QProc.Start();
                //MyProc.WaitForExit();
                QProc.Close();
            }
            /// <summary>
            /// 修改指定用户密码
            /// </summary>
            /// <param name="UserName">用户名</param>
            /// <param name="NewPass">新密码</param>
            public static void ResetPass(string UserName, string NewPass)
            {
                //Create New Process
                System.Diagnostics.Process QProc = new System.Diagnostics.Process();
                //Do Something To hide Command(cmd) Window
                QProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                QProc.StartInfo.CreateNoWindow = true;
                //Call Net.exe
                QProc.StartInfo.WorkingDirectory = "C://Windows//SYSTEM32";
                QProc.StartInfo.FileName = "net.exe";
                QProc.StartInfo.UseShellExecute = false;
                QProc.StartInfo.RedirectStandardError = true;
                QProc.StartInfo.RedirectStandardInput = true;
                QProc.StartInfo.RedirectStandardOutput = true;
                //Prepare Command for Exec
                QProc.StartInfo.Arguments = @" user " + UserName + " " + NewPass;
                QProc.Start();
                //MyProc.WaitForExit();
                QProc.Close();
            }
        }
    }
    /// <summary>
    /// 下拉菜单
    /// </summary>
    public class drop_down_list
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int IDS { get; set; }
    }
    /// <summary>
    /// 字符串操作
    /// </summary>
    public class Estring
    {
        public static string Int16ToHEX(int num)
        {
            string fc, lc, st;
            int fn, ln;
            fn = num / 16;
            ln = num % 16;
            switch (fn)
            {
                case 0:
                    fc = "0";
                    break;
                case 1:
                    fc = "1";
                    break;
                case 2:
                    fc = "2";
                    break;
                case 3:
                    fc = "3";
                    break;
                case 4:
                    fc = "4";
                    break;
                case 5:
                    fc = "5";
                    break;
                case 6:
                    fc = "6";
                    break;
                case 7:
                    fc = "7";
                    break;
                case 8:
                    fc = "8";
                    break;
                case 9:
                    fc = "9";
                    break;
                case 10:
                    fc = "A";
                    break;
                case 11:
                    fc = "B";
                    break;
                case 12:
                    fc = "C";
                    break;
                case 13:
                    fc = "D";
                    break;
                case 14:
                    fc = "E";
                    break;
                case 15:
                    fc = "F";
                    break;
                default:
                    fc = "";
                    break;
            }
            switch (ln)
            {
                case 0:
                    lc = "0";
                    break;
                case 1:
                    lc = "1";
                    break;
                case 2:
                    lc = "2";
                    break;
                case 3:
                    lc = "3";
                    break;
                case 4:
                    lc = "4";
                    break;
                case 5:
                    lc = "5";
                    break;
                case 6:
                    lc = "6";
                    break;
                case 7:
                    lc = "7";
                    break;
                case 8:
                    lc = "8";
                    break;
                case 9:
                    lc = "9";
                    break;
                case 10:
                    lc = "A";
                    break;
                case 11:
                    lc = "B";
                    break;
                case 12:
                    lc = "C";
                    break;
                case 13:
                    lc = "D";
                    break;
                case 14:
                    lc = "E";
                    break;
                case 15:
                    lc = "F";
                    break;
                default:
                    lc = "";
                    break;
            }
            st = fc + lc;
            return st;
        }
        public static bool isnum(string _str)
        {
            Int64 result;
            try
            {
                result = Convert.ToInt64(_str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 这是啥来着
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T properValue, T newValue, string properName = null)
        {
            if (object.Equals(properValue, newValue))
                return false;
            properValue = newValue;
            OnPropertyChanged(properName);
            return true;
        }
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// Application拓展定义
    /// </summary>
    public partial class App : Application
    {
        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        public static void DoEvents()
        {
            DispatcherFrame nestedFrame = new DispatcherFrame();
            DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, exitFrameCallback, nestedFrame);
            Dispatcher.PushFrame(nestedFrame);
            if (exitOperation.Status !=
            DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }
        private static Object ExitFrame(Object state)
        {
            DispatcherFrame frame = state as
            DispatcherFrame;
            frame.Continue = false;
            return null;
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();
        int i, j, nowt;
        bool isfinish = false;
        bool istimedout = false;
        bool isanotherbgm = false;
        bool iserror = false;
        int month, day;
        DateTime time1, time2;
        public MainWindow()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;//支持报告进度更新
            bgWorker.WorkerSupportsCancellation = true;//支持异步取消
            bgWorker.DoWork += DoWork_Handler;//将DoWork_Handler绑定在RunWorkerAsync()
            bgWorker.ProgressChanged += ProgressChanged_Handler;//将ProgressChanged_Handler绑定在ReportProgress()
            bgWorker.RunWorkerCompleted += RunWorkerCompleted_Handler;//退出时发生
            List<drop_down_list> Drop_down_f_g = new List<drop_down_list>();//创建下拉菜单List
            Drop_down_f_g.Add(new drop_down_list { Name = "安徽", ID = 0, IDS = 1 });
            Drop_down_f_g.Add(new drop_down_list { Name = "北京", ID = 1, IDS = 2 });
            Drop_down_f_g.Add(new drop_down_list { Name = "重庆", ID = 2, IDS = 3 });
            Drop_down_f_g.Add(new drop_down_list { Name = "福建", ID = 3, IDS = 4 });
            Drop_down_f_g.Add(new drop_down_list { Name = "广东", ID = 4, IDS = 5 });
            Drop_down_f_g.Add(new drop_down_list { Name = "甘肃", ID = 5, IDS = 6 });
            Drop_down_f_g.Add(new drop_down_list { Name = "广西壮族自治区", ID = 6, IDS = 7 });
            Drop_down_f_g.Add(new drop_down_list { Name = "贵州", ID = 7, IDS = 8 });
            Drop_down_f_g.Add(new drop_down_list { Name = "河北", ID = 8, IDS = 9 });
            Drop_down_f_g.Add(new drop_down_list { Name = "湖北", ID = 9, IDS = 10 });
            Drop_down_f_g.Add(new drop_down_list { Name = "黑龙江", ID = 10, IDS = 11 });
            Drop_down_f_g.Add(new drop_down_list { Name = "河南", ID = 11, IDS = 12 });
            Drop_down_f_g.Add(new drop_down_list { Name = "湖南", ID = 12, IDS = 13 });
            Drop_down_f_g.Add(new drop_down_list { Name = "海南", ID = 13, IDS = 14 });
            Drop_down_f_g.Add(new drop_down_list { Name = "吉林", ID = 14, IDS = 15 });
            Drop_down_f_g.Add(new drop_down_list { Name = "江苏", ID = 15, IDS = 16 });
            Drop_down_f_g.Add(new drop_down_list { Name = "江西", ID = 16, IDS = 17 });
            Drop_down_f_g.Add(new drop_down_list { Name = "辽宁", ID = 17, IDS = 18 });
            Drop_down_f_g.Add(new drop_down_list { Name = "内蒙古自治区", ID = 18, IDS = 19 });
            Drop_down_f_g.Add(new drop_down_list { Name = "宁夏回族自治区", ID = 19, IDS = 20 });
            Drop_down_f_g.Add(new drop_down_list { Name = "青海", ID = 20, IDS = 21 });
            Drop_down_f_g.Add(new drop_down_list { Name = "山东", ID = 21, IDS = 22 });
            Drop_down_f_g.Add(new drop_down_list { Name = "上海", ID = 22, IDS = 23 });
            Drop_down_f_g.Add(new drop_down_list { Name = "四川", ID = 23, IDS = 24 });
            Drop_down_f_g.Add(new drop_down_list { Name = "山西", ID = 24, IDS = 25 });
            Drop_down_f_g.Add(new drop_down_list { Name = "陕西", ID = 25, IDS = 26 });
            Drop_down_f_g.Add(new drop_down_list { Name = "天津", ID = 26, IDS = 27 });
            Drop_down_f_g.Add(new drop_down_list { Name = "新疆维吾尔自治区", ID = 27, IDS = 28 });
            Drop_down_f_g.Add(new drop_down_list { Name = "西藏自治区", ID = 28, IDS = 29 });
            Drop_down_f_g.Add(new drop_down_list { Name = "云南", ID = 29, IDS = 30 });
            Drop_down_f_g.Add(new drop_down_list { Name = "浙江", ID = 30, IDS = 31 });
            Drop_down_f_g.Add(new drop_down_list { Name = "香港", ID = 31, IDS = 32 });
            Drop_down_f_g.Add(new drop_down_list { Name = "澳门", ID = 32, IDS = 33 });
            Drop_down_f_g.Add(new drop_down_list { Name = "台湾", ID = 33, IDS = 34 });
            Drop_down_f_g.Add(new drop_down_list { Name = "海外", ID = 34, IDS = 35 });
            /*//绑定数据
            this.CLocate.ItemsSource = Drop_down_f_g;
            this.CLocate.DisplayMemberPath = "Name";
            this.CLocate.SelectedValuePath = "IDS";
            this.CLocate.SelectedIndex = 0;*/
            //窗口加载时执行
            this.GStart.Visibility = Visibility.Visible;
            App.DoEvents();
        }
        /// <summary>
        /// 启动时自动执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //使窗口全屏
            this.WindowState = System.Windows.WindowState.Normal;//还原窗口（非最小化和最大化）
            this.WindowStyle = System.Windows.WindowStyle.None; //仅工作区可见，不显示标题栏和边框
            this.ResizeMode = System.Windows.ResizeMode.NoResize;//不显示最大化和最小化按钮
            this.Topmost = true;    //窗口在最前（仅在Debug时注释）
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            //UI初始化
            this.LWelcome.Visibility = Visibility.Collapsed;
            this.LTodo.Visibility = Visibility.Collapsed;
            this.LInit.Visibility = Visibility.Collapsed;
            this.LContinue.Visibility = Visibility.Collapsed;
            //内容初始化
            nowt = 0;
            //初始任务
            this.BNext.Visibility = Visibility.Collapsed;
            Syscmd.ExecutePwsh("mkdir C:\\IDS", 100);
            Syscmd.ExecuteCMD("del C:\\IDS\\SR.txt", 100);
            Syscmd.ExecuteCMD("del index.php", 100);
            Syscmd.ExecuteCMD("del C:\\IDS\\ID.txt", 100);
            Clipboard.Clear();
            //背景音乐
            Random random = new Random();
            if ((random.Next(0, 5000) == 1024) || (DateTime.Now.Day == 1))  
            {
                Media.PlaySound("Symphony.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_FILENAME | Media.SND_NOWAIT);
                isanotherbgm = true;
            }
            else
                Media.PlaySound("tour.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_FILENAME | Media.SND_NOWAIT);
            time1 = DateTime.Now;
            //彩蛋
            if (random.Next(0, 10000) == 4096)
            {
                this.GRed.Visibility = Visibility.Visible;
                Syscmd.ExecuteCMD("copy eastrd.dll eastrd.wav", 100);
                Media.PlaySound("eastrd.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_LOOP | Media.SND_FILENAME | Media.SND_NOWAIT);
            }
            //初始动画
            this.GStart.Visibility = Visibility.Visible;
            for (i = 255; i > 0; i--)
            {
                this.BStart.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                App.DoEvents();
                Thread.Sleep(10);
            }
            if (isanotherbgm)
            {
                for (i = 0; i < 255; i++) 
                {
                    this.BStart.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                    App.DoEvents();
                    Thread.Sleep(10);
                }
                this.BStart.Visibility = Visibility.Collapsed;
                this.IOIP.Visibility = Visibility.Collapsed;
                for (i = 255; i > 0; i--)
                {
                    this.GStart.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                    App.DoEvents();
                    Thread.Sleep(10);
                }
            }
            Thread.Sleep(1000);
            this.GStart.Visibility = Visibility.Collapsed;
            Thread.Sleep(500);
            this.LWelcome.Visibility = Visibility.Visible;
            for (i = 0; i < 255; i += 5)
            {
                this.LWelcome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                App.DoEvents();
                Thread.Sleep(5);
            }
            this.LTodo.Visibility = Visibility.Visible;
            for (i = 0; i < 255; i += 5)
            {
                this.LTodo.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                App.DoEvents();
                Thread.Sleep(5);
            }
            this.LInit.Visibility = Visibility.Visible;
            for (i = 0; i < 255; i += 5)
            {
                this.LInit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                App.DoEvents();
                Thread.Sleep(5);
            }
            this.LContinue.Visibility = Visibility.Visible;
            for (i = 0; i < 255; i += 5)
            {
                this.LContinue.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                App.DoEvents();
                Thread.Sleep(5);
            }
            this.BNext.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 按下按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (this.BNext.IsEnabled == true)
                {
                    BNext_Click(sender, e);
                }
                if (this.TMonth.Text == "" && this.TDay.Text == "")
                {
                    Syscmd.ExecuteCMD("copy eastal.dll eastal.wav", 0);
                    Media.PlaySound("eastal.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_FILENAME);
                }
            }
        }
        /// <summary>
        /// 勾选“不修改计算机名”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CNoComN_Click(object sender, RoutedEventArgs e)
        {
            if (this.CNoComN.IsChecked == true)
            {
                this.TChComN.IsEnabled = false;
                this.TChComN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEEEEE"));
            }
            else
            {
                this.TChComN.IsEnabled = true;
                this.TChComN.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            }
        }
        /// <summary>
        /// 勾选“不修改密码”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CNoPass_Click(object sender, RoutedEventArgs e)
        {
            if (this.CNoPass.IsChecked == true)
            {
                this.TChPass.IsEnabled = false;
                this.TChPass.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEEEEE"));
            }
            else
            {
                this.TChPass.IsEnabled= true;
                this.TChPass.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            }
        }
        /// <summary>
        /// 按下“下一步”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BNext_Click(object sender, RoutedEventArgs e)
        {
            this.BNext.IsEnabled = false;
            App.DoEvents();
            switch (nowt)
            {
                case -1:
                    Application.Current.Shutdown();
                    break;
                case 0:
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LWelcome.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LWelcome.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LTodo.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LTodo.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LInit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LInit.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LContinue.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LContinue.Visibility = Visibility.Collapsed;
                    this.BCover.Visibility = Visibility.Visible;
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.ILOIP.Visibility = Visibility.Collapsed;
                    this.ICollec.Visibility = Visibility.Visible;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.BCover.Visibility = Visibility.Collapsed;
                    this.LCollect.Visibility = Visibility.Visible;
                    this.ILoading.Visibility = Visibility.Visible;
                    for (i = 0; i < 255; i += 5)
                    {
                        this.LCollect.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    if (!bgWorker.IsBusy) bgWorker.RunWorkerAsync();
                    break;
                case 1:
                    Int64 mon = 0;
                    Int64 dy = 0;
                    if (this.TMonth.Text == "114514" && this.TDay.Text == "1919810")
                    {
                        Syscmd.ExecuteCMD("copy east11.dll east11.wav", 0);
                        Media.PlaySound("east11.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_FILENAME);
                    }
                    if (!(Estring.isnum(this.TMonth.Text) && Estring.isnum(this.TDay.Text))) 
                    {
                        this.LBirErr.Visibility = Visibility.Visible;
                        App.DoEvents();
                        iserror = true;
                    }
                    else
                    {
                        month = Convert.ToInt32(this.TMonth.Text);
                        day = Convert.ToInt32(this.TDay.Text);
                        if (month < 1 || month > 12)
                        {
                            this.LBirErr.Visibility = Visibility.Visible;
                            App.DoEvents();
                            iserror = true;
                        }
                        else
                        {
                            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                            {
                                if (day < 1 || day > 31)
                                {
                                    this.LBirErr.Visibility = Visibility.Visible;
                                    App.DoEvents();
                                    iserror = true;
                                }
                                else
                                {
                                    Syscmd.ExecuteCMD("echo " + this.TMonth.Text + "-" + this.TDay.Text + " > C:\\IDS\\SR.txt", 100);
                                }
                            }
                            else if (month == 2)
                            {
                                if (day < 1 || day > 29)
                                {
                                    this.LBirErr.Visibility = Visibility.Visible;
                                    App.DoEvents();
                                    iserror = true;
                                }
                                else
                                {
                                    Syscmd.ExecuteCMD("echo " + this.TMonth.Text + "-" + this.TDay.Text + " > C:\\IDS\\SR.txt", 100);
                                }
                            }
                            else
                            {
                                if (day < 1 || day > 30)
                                {
                                    this.LBirErr.Visibility = Visibility.Visible;
                                    App.DoEvents();
                                    iserror = true;
                                }
                                else
                                {
                                    Syscmd.ExecuteCMD("echo " + this.TMonth.Text + "-" + this.TDay.Text + " > C:\\IDS\\SR.txt", 100);
                                }
                            }
                        }
                    }
                    if (iserror == false)
                    {
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.LBirth.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LBirth.Visibility = Visibility.Collapsed;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.LMonth.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            this.LDay.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            this.TMonth.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            this.TDay.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LMonth.Visibility = Visibility.Collapsed;
                        this.TDay.Visibility = Visibility.Collapsed;
                        this.LDay.Visibility = Visibility.Collapsed;
                        this.TMonth.Visibility = Visibility.Collapsed;
                        if (this.LBirErr.Visibility == Visibility.Visible)
                        {
                            for (i = 255; i > 0; i -= 5)
                            {
                                this.LBirErr.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.LBirErr.Visibility = Visibility.Collapsed;
                        }
                        this.BCover.Visibility = Visibility.Visible;
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.IBirth.Visibility = Visibility.Collapsed;
                        this.INetwork.Visibility = Visibility.Visible;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.BCover.Visibility = Visibility.Collapsed;
                        this.LChknet.Visibility = Visibility.Visible;
                        for (i = 0; i < 255; i += 5)
                        {
                            this.LChknet.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.ILoading.Visibility = Visibility.Visible;
                        if (!bgWorker.IsBusy) bgWorker.RunWorkerAsync();
                    }
                    else iserror = false;
                    break;
                case 2:
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LUpd0.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LUpd0.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LUpd1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LUpd1.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LContinue.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LContinue.Visibility = Visibility.Collapsed;
                    this.LUploading.Visibility = Visibility.Visible;
                    this.ILoading.Visibility = Visibility.Visible;
                    for (i = 0; i < 255; i += 5)
                    {
                        this.LUploading.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    if (!bgWorker.IsBusy) bgWorker.RunWorkerAsync();
                    break;
                case 3:
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LID.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LID.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LImp.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LImp.Visibility = Visibility.Collapsed;
                    this.LCompName.Visibility = Visibility.Visible;
                    for (i = 0; i < 255; i += 5)
                    {
                        this.LCompName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LChComN.Visibility = Visibility.Visible;
                    this.LChPass.Visibility = Visibility.Visible;
                    this.TChComN.Visibility = Visibility.Visible;
                    this.TChPass.Visibility = Visibility.Visible;
                    this.CNoComN.Visibility = Visibility.Visible;
                    this.CNoPass.Visibility = Visibility.Visible;
                    for (i = 0; i < 255; i += 5)
                    {
                        this.LChComN.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        this.LChPass.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        this.TChComN.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        this.TChPass.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    nowt++;
                    break;
                case 4:
                    if (this.CNoComN.IsChecked == false || this.TChComN.Text == "") {/*Nothing To Do*/}
                    else
                        WSystemd.Systemctl.SetComputerName(this.TChComN.Text);
                    if (this.CNoPass.IsChecked == false || this.TChPass.Text == "") {/*Nothing To Do*/}
                    else
                        WSystemd.Users.ResetAdminPass(this.TChPass.Text);
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LCompName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LCompName.Visibility = Visibility.Collapsed;
                    for (i = 255; i > 0; i -= 5)
                    {
                        this.LChComN.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        this.LChPass.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        this.TChComN.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        this.TChPass.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(5);
                    }
                    this.LChComN.Visibility = Visibility.Collapsed;
                    this.LChPass.Visibility = Visibility.Collapsed;
                    this.TChComN.Visibility = Visibility.Collapsed;
                    this.TChPass.Visibility = Visibility.Collapsed;
                    this.CNoComN.Visibility = Visibility.Collapsed;
                    this.CNoPass.Visibility = Visibility.Collapsed;
                    time2 = DateTime.Now;
                    if (((time2.Second - time1.Second) + ((time2.Minute - time1.Minute) * 60) + ((time2.Hour - time1.Hour) * 3600)) >= 506)  
                        istimedout = true;
                    this.GLast.Visibility = Visibility.Visible;
                    this.BLast.Visibility = Visibility.Visible;
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.BLast.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                        App.DoEvents();
                        Thread.Sleep(15);
                    }
                    Mouse.OverrideCursor = Cursors.None;
                    this.LLast.Visibility = Visibility.Visible;
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    for (i = 255; i >= 0; i -= 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    this.LLast.Content = "我们正在进行一些设置";
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    for (i = 255; i >= 0; i -= 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    this.LLast.Content = "请不要关闭计算机";
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    for (j = 0; j < 5; j++)
                    {
                        /*this.ILast1.Visibility = Visibility.Collapsed;
                        this.ILast2.Visibility = Visibility.Collapsed;
                        this.ILast3.Visibility = Visibility.Collapsed;
                        this.ILast4.Visibility = Visibility.Collapsed;
                        this.ILast5.Visibility = Visibility.Collapsed;
                        switch (j)
                        {
                            case 0:
                                this.ILast1.Visibility = Visibility.Visible;
                                break;
                            case 1:
                                this.ILast2.Visibility = Visibility.Visible;
                                break;
                            case 2:
                                this.ILast3.Visibility = Visibility.Visible;
                                break;
                            case 3:
                                this.ILast4.Visibility = Visibility.Visible;
                                break;
                            case 4:
                                this.ILast5.Visibility = Visibility.Visible;
                                break;
                        }*/
                        this.ILast1.Visibility = Visibility.Visible;
                        switch (j)
                        {
                            case 0:
                                this.ILast1.Source = new BitmapImage(new Uri("C:/IDS/Image/p1.png"));
                                break;
                            case 1:
                                this.ILast1.Source = new BitmapImage(new Uri("C:/IDS/Image/p2.png"));
                                break;
                            case 2:
                                this.ILast1.Source = new BitmapImage(new Uri("C:/IDS/Image/p3.png"));
                                break;
                            case 3:
                                this.ILast1.Source = new BitmapImage(new Uri("C:/IDS/Image/p4.png"));
                                break;
                            case 4:
                                this.ILast1.Source = new BitmapImage(new Uri("C:/IDS/Image/p5.png"));
                                break;
                        }
                        for (i = 255; i >= 0xDF; i--)
                        {
                            this.BLast.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(100);
                        }
                        for (i = 0xDF; i <= 255; i++)
                        {
                            this.BLast.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(100);
                        }
                    }
                    for (i = 255; i >= 0; i -= 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(1000);
                    this.LLast.Content = "请尽情使用吧";
                    for (i = 0; i <= 255; i += 5)
                    {
                        this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                        App.DoEvents();
                        Thread.Sleep(10);
                    }
                    Syscmd.ExecuteCMD("del \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\dbs_*\" /f /s /q", 0);
                    Thread.Sleep(1000);
                    if (istimedout == true)
                    {
                        this.LLast.Content = "但是我有一个问题";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "是什么让你停留了这么长时间";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "我并不认为这些操作很难";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "当然，也有可能你是想听完背景音乐";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "但其实，它来自Windows XP";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "所以我认为花费这么长的时间并不值得";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        for (i = 255; i >= 0; i -= 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(1000);
                        this.LLast.Content = "——余音歆风";
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.LLast.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(10);
                        }
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        Thread.Sleep(4000);
                    }
                    Application.Current.Shutdown();
                    break;
            }
            this.BNext.IsEnabled = true;
        }
        /// <summary>
        /// 后台线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DoWork_Handler(object sender, DoWorkEventArgs args)
        {
            Dispatcher.BeginInvoke(new Action((delegate { this.BNext.IsEnabled = false; })));
            //TODO:在这里放置代码
            switch (nowt)
            {
                case 0:
                    Syscmd.ShellExecute(IntPtr.Zero, "open", "dbs_dolog.exe", "", "", Syscmd.SW_HIDE);
                    Thread.Sleep(3000);
                    isfinish = true;
                    break;
                case 1:
                    Syscmd.ExecutePwsh("wget -Uri \"https://www.drblack-system.com/index.php\" -OutFile \"index.php\"", 5000);
                    break;
                case 2:
                    Syscmd.ExecuteCMD("del C:\\IDS\\ID.txt", 0);
                    Syscmd.ShellExecute(IntPtr.Zero, "open", "dbs_uplogx.exe", "", "", Syscmd.SW_MINIMIZE);
                    while (!File.Exists("C:\\IDS\\ID.txt")) ;
                    break;
            }
            Dispatcher.BeginInvoke(new Action(delegate
            {
                //TODO:在这里放置更改UI的代码
                //INFO:只放置用于更改UI的代码
                this.BNext.IsEnabled = false;
                switch (nowt)
                {
                    case 0:
                        this.ILoading.Visibility = Visibility.Collapsed;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.LCollect.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LCollect.Visibility = Visibility.Collapsed;
                        this.BCover.Visibility = Visibility.Visible;
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.ICollec.Visibility = Visibility.Collapsed;
                        this.IBirth.Visibility = Visibility.Visible;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.BCover.Visibility = Visibility.Collapsed;
                        this.LBirth.Visibility = Visibility.Visible;
                        for (i = 0; i < 255; i += 5)
                        {
                            this.LBirth.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LMonth.Visibility = Visibility.Visible;
                        this.LDay.Visibility = Visibility.Visible;
                        this.TMonth.Visibility = Visibility.Visible;
                        this.TDay.Visibility = Visibility.Visible;
                        for (i = 0; i < 255; i += 5)
                        {
                            this.LMonth.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            this.LDay.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            this.TMonth.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            this.TDay.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "FFFFFF"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        nowt++;
                        break;
                    case 1:
                        this.ILoading.Visibility = Visibility.Collapsed;
                        if (File.Exists("index.php"))
                        {
                            this.IAccess.Visibility = Visibility.Visible;
                            for (i = 255; i > 0; i -= 5)
                            {
                                this.LChknet.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.LChknet.Visibility = Visibility.Collapsed;
                            this.BCover.Visibility = Visibility.Visible;
                            for (i = 0; i <= 255; i += 5)
                            {
                                this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.IAccess.Visibility = Visibility.Collapsed;
                            this.INetwork.Visibility = Visibility.Collapsed;
                            this.IUpload.Visibility = Visibility.Visible;
                            for (i = 255; i > 0; i -= 5)
                            {
                                this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.BCover.Visibility = Visibility.Collapsed;
                            this.LUpd0.Visibility = Visibility.Visible;
                            for (i = 0; i < 255; i += 5)
                            {
                                this.LUpd0.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.LUpd1.Visibility = Visibility.Visible;
                            for (i = 0; i < 255; i += 5)
                            {
                                this.LUpd1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.LContinue.Visibility = Visibility.Visible;
                            for (i = 0; i < 255; i += 5)
                            {
                                this.LContinue.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            nowt++;
                        }
                        else
                        {
                            this.IBan.Visibility = Visibility.Visible;
                            for (i = 255; i > 0; i -= 5)
                            {
                                this.LChknet.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            this.LChknet.Visibility = Visibility.Collapsed;
                            this.LNonet.Visibility = Visibility.Visible;
                            for (i = 0; i < 255; i += 5)
                            {
                                this.LNonet.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                                App.DoEvents();
                                Thread.Sleep(5);
                            }
                            nowt = -1;
                        }
                        break;
                    case 2:
                        this.ILoading.Visibility = Visibility.Collapsed;
                        this.IAccess.Visibility = Visibility.Visible;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.LUploading.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LUploading.Visibility = Visibility.Collapsed;
                        Syscmd.ExecuteCMD("del /f /s /q C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\dbs_*.lnk", 0);
                        this.LID.Content += Syscmd.ExecuteCMD("type C:\\IDS\\ID.txt", 0);
                        this.BCover.Visibility = Visibility.Visible;
                        for (i = 0; i <= 255; i += 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.IUpload.Visibility = Visibility.Collapsed;
                        this.IAccess.Visibility = Visibility.Collapsed;
                        this.IRename.Visibility = Visibility.Visible;
                        for (i = 255; i > 0; i -= 5)
                        {
                            this.BCover.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "EEEEF0"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.BCover.Visibility = Visibility.Collapsed;
                        this.LID.Visibility = Visibility.Visible;
                        for (i = 0; i < 255; i += 5)
                        {
                            this.LID.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        this.LImp.Visibility = Visibility.Visible;
                        for (i = 0; i < 255; i += 5)
                        {
                            this.LImp.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + Estring.Int16ToHEX(i) + "000000"));
                            App.DoEvents();
                            Thread.Sleep(5);
                        }
                        nowt++;
                        break;
                }
                this.BNext.IsEnabled = true;
            }));
        }
        /// <summary>
        /// 执行完成或正常退出后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RunWorkerCompleted_Handler(object sender, RunWorkerCompletedEventArgs args)
        {
            if (args.Cancelled)
            {
                //如果取消
                //TODO:在这里放置代码
            }
            else
            {
                //如果正常结束
                //TODO:在这里放置代码
            }
        }
        /// <summary>
        /// 进度更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ProgressChanged_Handler(object sender, ProgressChangedEventArgs args)
        {
            //TODO:在这里放置代码
        }
    }
}
