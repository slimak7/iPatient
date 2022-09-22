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
                await view.BackgroundColorTo(Colors.Red);
                await view.BackgroundColorTo(Colors.Orange);
                await view.BackgroundColorTo(Colors.Yellow);
                await view.BackgroundColorTo(Colors.Green);
                await view.BackgroundColorTo(Colors.Blue);
                await view.BackgroundColorTo(Colors.Indigo);
                await view.BackgroundColorTo(Colors.Violet);
            }
        }
    }
}
