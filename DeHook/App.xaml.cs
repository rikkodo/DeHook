using System.Windows;
using System.Diagnostics;
using System.Reflection;

namespace DeHook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class  App : System.Windows.Application
    {
        private System.Windows.Forms.ContextMenuStrip menu = new();
        System.Windows.Forms.NotifyIcon notifyIcon = new();
        private readonly MyHook myhook = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            // 右クリックに出すのコンテキストメニューを作成
            menu.Items.Add("Default", null, (obj, e) => { });
            menu.Items.Add("Mouse", null, (obj, e) => { });
            menu.Items.Add("Disable", null, (obj, e) => { });
            menu.Items.Add("Quit", null, (obj, e) => { Shutdown(); });

            // タスクトレイ上のアイコンを作成
            notifyIcon.Visible = true;
            notifyIcon.Icon = DeHook.Properties.Resources.OUT_0000;
            notifyIcon.Text = "DeHook";
            // notifyIcon.ContextMenuStrip = menu;


            // アイコンを押したときの処理
            notifyIcon.Click += (obj, e) =>
            {
                if (menu.Visible)
                {
                    menu.Hide();
                }
                else
                {
                    menu.Show(Control.MousePosition);
                }
            };

            // Start Hook
            myhook.StartHook();

            // プロセス通常起動
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {

            // End Hook
            myhook.EndHook();

            menu.Dispose();
            notifyIcon.Dispose();

            base.OnExit(e);
        }
    }

}
