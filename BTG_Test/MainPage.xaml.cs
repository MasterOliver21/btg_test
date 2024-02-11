using BTG_Test.ViewModels;

namespace BTG_Test;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(GraphicViewModel graphicViewModel)
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

