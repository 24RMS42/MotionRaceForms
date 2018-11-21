using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Threading.Tasks;
using UIKit;
using CoreAnimation;
using MotionRaceBrowser.iOS;

[assembly: ResolutionGroupName(nameof(MotionRaceBrowser))]
[assembly: ExportEffect(typeof(VerticalSlideEffect), nameof(VerticalSlideEffect))]

namespace MotionRaceBrowser.iOS
{
	public class VerticalSlideEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			Element.PropertyChanged += OnPropertyChanged;
		}

		protected override void OnDetached()
		{
			Element.PropertyChanged -= OnPropertyChanged;
		}

		private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            if (e.PropertyName == MotionRaceBrowser.VerticalSlideEffect.IsShownProperty.PropertyName)
			{
				var visualElement = Element as VisualElement;

				if (visualElement != null)
				{
                    if (MotionRaceBrowser.VerticalSlideEffect.GetIsShown(visualElement))
					{
						await AnimateIn();
					}
					else
					{
						await AnimateOut();
					}
				}
			}
		}

        private async Task AnimateIn()
        {
            var target = -Container.Frame.Height;
            await UIView.AnimateNotifyAsync(0.3,
            	() =>
            	{
                    CATransform3D transform = Container.Layer.Transform;
                    transform.m42 = target;
                    Container.Layer.Transform = transform;
                });
            var visualElement = Element as VisualElement;
            if (visualElement != null)
            {
                visualElement.TranslationY = target;
            }
        }

        private async Task AnimateOut()
        {
        	await UIView.AnimateNotifyAsync(0.3,
        		() =>
        		{
        			CATransform3D transform = Container.Layer.Transform;
        			transform.m42 = 0;
        			Container.Layer.Transform = transform;
        		});
        	var visualElement = Element as VisualElement;
        	if (visualElement != null)
        	{
        		visualElement.TranslationY = 0; 
            } 
        }
	}
}