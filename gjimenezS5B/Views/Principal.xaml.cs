using gjimenezS5B.Models;
using System.Xml.Linq;

namespace gjimenezS5B.Views;

public partial class Principal : ContentPage
{
    private int idPersona;
    private string nombrePersona;
    public Principal()
    {
        InitializeComponent();
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        App.personRepo.AddNewPerson(txtName.Text);
        lblStatus.Text = App.personRepo.status;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersona.ItemsSource = people;
    }

    private void chkSeleccion_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var persona = checkBox?.BindingContext as Persona;
        if (persona != null)
        {
            if (e.Value)
            {
                idPersona = persona.Id;
            }
            else
            {
                idPersona = 0;
                nombrePersona = "";
            }
        }
    }

    private void btnModificar_Clicked(object sender, EventArgs e)
    {
        App.personRepo.ModificarRegistro(idPersona, nombrePersona);
        lblMensaje.Text = App.personRepo.status;

        btnMostrar_Clicked(sender, e);
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        App.personRepo.EliminarRegistro(idPersona);
        lblMensaje.Text = App.personRepo.status;

        btnMostrar_Clicked(sender, e);
    }

    private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
    {
        var valorTexto = sender as Entry;
        if (valorTexto?.BindingContext is Persona persona)
        {
            nombrePersona = e.NewTextValue;
            persona.Name = nombrePersona;
        }
        else
        {
            nombrePersona = "";
        }
    }
}