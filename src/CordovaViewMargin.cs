using System.Windows;
using Microsoft.Phone.Controls;

namespace WPCordovaClassLib.Cordova.Commands
{
    public class CordovaViewMargin : BaseCommand
    {
        public void setMargin(string options)
        {
            string[] args = JSON.JsonHelper.Deserialize<string[]>(options);
            var left = int.Parse(args[0]);
            var top = int.Parse(args[1]);
            var right = int.Parse(args[2]);
            var bottom = int.Parse(args[3]);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
                if (frame == null)
                {
                    DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR, "PhoneApplicationFrame not found"));
                    return;
                }

                PhoneApplicationPage page = frame.Content as PhoneApplicationPage;
                if (page == null)
                {
                    DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR, "PhoneApplicationPage not found"));
                    return;
                }

                CordovaView cordovaView = page.FindName("CordovaView") as CordovaView;
                if (cordovaView == null)
                {
                    DispatchCommandResult(new PluginResult(PluginResult.Status.ERROR, "CordovaView not founda"));
                    return;
                }

                cordovaView.Margin = new Thickness(left, top, right, bottom);

                DispatchCommandResult(new PluginResult(PluginResult.Status.NO_RESULT));
            });
        }
    }
}


