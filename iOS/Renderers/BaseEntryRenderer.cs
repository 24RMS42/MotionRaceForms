using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Linq;
using MotionRaceBrowser.iOS.Renderers;
using MotionRaceBrowser.Service;

[assembly: ExportRenderer(typeof(Entry), typeof(BaseEntryRenderer))]
namespace MotionRaceBrowser.iOS.Renderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Entry item = this.Element;
            if (Control != null && item.ReturnType == ReturnType.Search)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
            }

            IViewContainer<Xamarin.Forms.View> parent = item.Parent as IViewContainer<Xamarin.Forms.View>;
            UIToolbar toolBar = new UIToolbar(new RectangleF(0.0f, 0.0f, (float)Frame.Size.Width, 44.0f)) { Translucent = true };
            List<UIBarButtonItem> toolBarItens = new List<UIBarButtonItem>();

            toolBar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem (UIBarButtonSystemItem.Done, delegate {
                    this.Control.ResignFirstResponder();
                })
            };

            // Navigation logic, Prev/Next/Done button in login page
            if (parent != null && item.ReturnType != ReturnType.Search)
            {
                List<Entry> entries = parent.Children
                        .OfType<Entry>()
                        .ToList();

                if (entries.Count > 1)
                {
                    Func<int> getFocusedIndex = () => entries
                        .Select((v, i) => new
                        {
                            item = v,
                            index = i
                        })
                        .Where(w => w.item.IsFocused)
                        .Select(s => (int)s.index)
                        .Single();

                    toolBarItens.AddRange(new UIBarButtonItem[]
                        {
                            new UIBarButtonItem("Prev", UIBarButtonItemStyle.Plain, delegate {
                                int focused = getFocusedIndex() - 1;
                                focused = focused < 0 ? entries.Count-1 : focused;
                                entries.ElementAt(focused).Focus();
                            }),
                            new UIBarButtonItem("Next", UIBarButtonItemStyle.Plain, delegate {
                                int focused = getFocusedIndex() + 1;
                                focused = focused+1 > entries.Count ? 0 : focused;
                                entries.ElementAt(focused).Focus();
                            })
                        }
                    );

                    toolBarItens.AddRange(new UIBarButtonItem[]
                        {
                            new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                            new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                            {
                                this.Control.ResignFirstResponder();
                                MessagingCenter.Send(new MessageServiceClass(), "login", "");
                            })
                        }
                    );
                }
            }
            //Done/Cancel button in Home page
            else
            {
                toolBarItens.AddRange(new UIBarButtonItem[]
                    {
                        new UIBarButtonItem("Cancel", UIBarButtonItemStyle.Plain, delegate {
                            this.Control.ResignFirstResponder();
                        }),
                        new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                        new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                        {
                            this.Control.ResignFirstResponder();
                            MessagingCenter.Send(new MessageServiceClass(), "searchDone", "");
                        })
                    }
                );
            }

            //toolBarItens.AddRange(new UIBarButtonItem[]
            //    {
            //        new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
            //        new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            //        {
            //            this.Control.ResignFirstResponder();
            //            MessagingCenter.Send(new MessageServiceClass(), "login", "");
            //        })
            //    }
            //);

            toolBar.Items = toolBarItens.ToArray();
            this.Control.InputAccessoryView = toolBar;
        }
    }
}
