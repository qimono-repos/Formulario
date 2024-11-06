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
			CounterBtn.Text = $"Evento : primera vez";
		else
			CounterBtn.Text = $"Eventos : {count} veces";

		SemanticScreenReader.Announce(CounterBtn.Text);

	}

	private void OnCounterCompleted(object s, EventArgs e)
	{
		CounterBtn.Text = $"Completado";
	}
}

