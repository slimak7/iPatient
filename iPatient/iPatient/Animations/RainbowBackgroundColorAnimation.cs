using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Animations
{
    public class RainbowBackgroundColorAnimation : BaseAnimation
    {
        public async override Task Animate(VisualElement view)
        {
            while (true)
            {
                await view.BackgroundColorTo(Color.FromHex("264653"));
                await view.BackgroundColorTo(Color.FromHex("2A9D8F"));
                await view.BackgroundColorTo(Color.FromHex("E9C46A"));
                await view.BackgroundColorTo(Color.FromHex("F4A261"));
                await view.BackgroundColorTo(Color.FromHex("E76F51"));
            }
        }
    }
}
