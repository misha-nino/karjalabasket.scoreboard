using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Internals;

namespace KarjalaBasket.Scoreboard.Views;

public partial class TimeEntryView : ContentView
{
    public static readonly BindableProperty ValueProperty = 
        BindableProperty.Create(nameof(Value), typeof(int?), typeof(StepperView), 0);

    public int? Value
    {
        get => (int?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public static readonly BindableProperty FontSizeProperty = 
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(StepperView), 16d);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    public static readonly BindableProperty LabelProperty = 
        BindableProperty.Create(nameof(Label), typeof(string), typeof(StepperView));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    
    public static readonly BindableProperty ToolTipProperty = 
        BindableProperty.Create(nameof(ToolTip), typeof(string), typeof(StepperView));

    public string ToolTip
    {
        get => (string)GetValue(ToolTipProperty);
        set => SetValue(ToolTipProperty, value);
    }
    public TimeEntryView()
    {
        InitializeComponent();
    }
}