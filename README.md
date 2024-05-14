FlipView for MAUI

A ContentView with a customizable flip animation.

[![NuGet version (FlipView-MAUI)](https://img.shields.io/nuget/v/FlipView-MAUI.svg)](https://www.nuget.org/packages/FlipView-MAUI/)


![Sample](https://raw.githubusercontent.com/Jon2G/MAUI-FlipView/main/sample.gif)

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