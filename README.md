FlipView for MAUI

A ContentView with a customizable flip animation.

![Sample](sample.gif)

Usage:
```
<flipView:FlipContentView IsFlipped="False">
                    <Image
                    Source="dotnet_bot.png"
                    HeightRequest="100"
                    Aspect="AspectFit"
                    SemanticProperties.Description="dot net bot in a race car number eight" />
                    <flipView:FlipContentView.BackView>
                        <Image
                        Source="dotnet_bot2.png"
                        HeightRequest="100"
                        Aspect="AspectFit" RotationY="180"
                        SemanticProperties.Description="dot net bot in a race car number eight" />
                    </flipView:FlipContentView.BackView>
                </flipView:FlipContentView>
```

Insipired by Expander Control and https://github.com/devcrux/Flip-Animation-in-Xamarin.Forms