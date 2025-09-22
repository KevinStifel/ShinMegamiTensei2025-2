using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;
public class TeamValidatorView : AbstractView
{
    public TeamValidatorView(View view) : base(view) { }

    public void ShowInvalidTeams() =>
        View.WriteLine("Archivo de equipos inválido");
}