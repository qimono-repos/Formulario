namespace Formulario;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Evento {count} time";
		else
			CounterBtn.Text = $"Eventos {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

