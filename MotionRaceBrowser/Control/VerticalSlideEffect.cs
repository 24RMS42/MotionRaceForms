using Xamarin.Forms;

namespace MotionRaceBrowser
{
    public class VerticalSlideEffect : RoutingEffect 
    { 
        public const string ID = nameof(MotionRaceBrowser) + "." + nameof(VerticalSlideEffect);  
        public VerticalSlideEffect()  
            : base(ID) 
        {
    
        }

        public static readonly BindableProperty IsShownProperty = BindableProperty.CreateAttached(ID + ".IsShown", typeof(bool), typeof(VerticalSlideEffect), default(bool)); 

        public static bool GetIsShown(BindableObject obj) 
        { 
            return (bool)obj.GetValue(IsShownProperty); 
        }

        public static void SetIsShown(BindableObject obj, bool value) 
        { 
            obj.SetValue(IsShownProperty, value); 
        }
    }
}
