namespace KarjalaBasket.Scoreboard.Views;

public partial class StepperView : ContentView
{
    public static readonly BindableProperty MinProperty = 
        BindableProperty.Create(nameof(Min), typeof(int), typeof(StepperView), 0);

    public int Min
    {
        get => (int)GetValue(MinProperty);
        set => SetValue(MinProperty, value);
    }
    
    public static readonly BindableProperty MaxProperty = 
        BindableProperty.Create(nameof(Max), typeof(int), typeof(StepperView), int.MaxValue);

    public int Max
    {
        get => (int)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }
    
    public static readonly BindableProperty ValueProperty = 
        BindableProperty.Create(nameof(Value), typeof(int), typeof(StepperView), 0);

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public static readonly BindableProperty IncrementProperty = 
        BindableProperty.Create(nameof(Increment), typeof(int), typeof(StepperView), 1);

    public int Increment
    {
        get => (int)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
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
    
    public static readonly BindableProperty FontSizeProperty = 
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(StepperView), 16d);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    
    public StepperView()
    {
        InitializeComponent();
    }
}