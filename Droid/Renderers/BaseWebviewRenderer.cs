using System;
using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Webkit;
using Java.IO;
using MotionRaceBrowser.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#if MotionsRace
#else
using TwitchBrowser.Droid;
#endif

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(BaseWebviewRenderer))]
namespace MotionRaceBrowser.Droid.Renderers
{
    public class BaseWebviewRenderer : WebViewRenderer
    {
        Activity mContext;
        private static int FILECHOOSER_RESULTCODE = 1;

        public BaseWebviewRenderer(Context context) : base(context)
        {
            this.mContext = context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = false;

                Control.Settings.LoadWithOverviewMode = true;
                Control.Settings.UseWideViewPort = true;
                Control.Settings.JavaScriptEnabled = true;
                //Control.SetWebChromeClient(new MyWebClient(mContext));//TODO not working
            }

            var chromeClient = new FileChooserWebChromeClient((uploadMsg, acceptType, capture) => {
                MainActivity.UploadMessage = uploadMsg;

                // Create MyAppFolder at SD card for saving our images
                File imageStorageDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                        Android.OS.Environment.DirectoryPictures), "MyAppFolder");

                if (!imageStorageDir.Exists())
                {
                    imageStorageDir.Mkdirs();
                }

                // Create camera captured image file path and name, add ticks to make it unique 
                var file = new File(imageStorageDir + File.Separator + "IMG_"
                    + DateTime.Now.Ticks + ".jpg");

                MainActivity.mCapturedImageURI = Android.Net.Uri.FromFile(file);

                // Create camera capture image intent and add it to the chooser
                var captureIntent = new Intent(MediaStore.ActionImageCapture);
                captureIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

                var i = new Intent(Intent.ActionGetContent);
                i.AddCategory(Intent.CategoryOpenable);
                i.SetType("image/*");

                var chooserIntent = Intent.CreateChooser(i, "Choose image");
                chooserIntent.PutExtra(Intent.ExtraInitialIntents, new Intent[] { captureIntent });

                mContext.StartActivityForResult(chooserIntent, FILECHOOSER_RESULTCODE);
            });

            Control.SetWebChromeClient(chromeClient);
        }

        public class FileChooserWebChromeClient : WebChromeClient
        {
            private Action<IValueCallback, Java.Lang.String, Java.Lang.String> _callback;

            public FileChooserWebChromeClient(Action<IValueCallback, Java.Lang.String, Java.Lang.String> callback)
            {
                _callback = callback;
            }

            // For Android < 5.0
            [Java.Interop.Export]
            public void openFileChooser(IValueCallback uploadMsg, Java.Lang.String acceptType, Java.Lang.String capture)
            {
                _callback(uploadMsg, acceptType, capture);
            }

            // For Android > 5.0
            public override Boolean OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback uploadMsg, WebChromeClient.FileChooserParams fileChooserParams)
            {
                try
                {
                    _callback(uploadMsg, null, null);
                }
                catch (Exception)
                {

                }
                return true;
            }
        }

        public class MyWebClient : WebChromeClient
        {
            Activity mContext;
            public MyWebClient(Activity context)
            {
                this.mContext = context;
            }

            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(PermissionRequest request)
            {
                mContext.RunOnUiThread(() => {
                    request.Grant(request.GetResources());
                });
            }
        }
    }
}
