using System.Windows;
using System.Diagnostics;

namespace DeHook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class  App : System.Windows.Application
    {
        private System.Windows.Forms.ContextMenuStrip menu = new();
        System.Windows.Forms.NotifyIcon notifyIcon = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            // 右クリックに出すのコンテキストメニューを作成
            menu.Items.Add("アプリを終了します", null, (obj, e) => { Shutdown(); });
            menu.Items.Add("電卓を起動します", null, (obj, e) => { Process.Start(@"C:\Windows\System32\calc.exe"); });

            // タスクトレイ上のアイコンを作成
            notifyIcon.Visible = true;
            notifyIcon.Icon = DeHook.Properties.Resources.MyIcon;
            notifyIcon.Text = "マウスポインタを当てたときに表示する文言";
            notifyIcon.ContextMenuStrip = menu;

            // アイコンを押したときの処理
            notifyIcon.Click += (obj, e) =>
            {
                Debug.WriteLine("クリックされました");
            };

            // プロセス通常起動
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            menu.Dispose();
            notifyIcon.Dispose();

            base.OnExit(e);
        }
    }

}
