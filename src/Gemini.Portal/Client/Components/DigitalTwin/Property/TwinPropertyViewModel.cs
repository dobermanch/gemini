using Gemini.Portal.Client.Components.DigitalTwin.Interface;
using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Property;

public class TwinPropertyViewModel : ViewModelBase
{
    private readonly TwinProperty _model;

    public TwinPropertyViewModel(TwinProperty? model = null)
    {
        _model = model ?? new TwinProperty
        {
            Id = "dtmi:;1",
        };

        RegisterProperty(nameof(Id), _model.Id, value => _model.Id = value);
        RegisterProperty(nameof(Name), _model.Name, value => _model.Name = value);
        RegisterProperty(nameof(Schema), _model.Schema, value => _model.Schema = value);
        RegisterProperty(nameof(Comment), _model.Comment, value => _model.Comment = value);
        RegisterProperty(nameof(Description), _model.Description, value => _model.Description = value);
        RegisterProperty(nameof(DisplayName), _model.DisplayName, value => _model.DisplayName = value);
        RegisterProperty(nameof(Unit), _model.Unit, value => _model.Unit = value);
        RegisterProperty(nameof(Writable), _model.Writable, value => _model.Writable = value);
    }

    public EditableProperty<string> Name => GetProperty<string>();

    public EditableProperty<Dtmi> Id => GetProperty<Dtmi>();

    public Iri Type => _model.Type;

    public EditableProperty<TwinSchema> Schema => GetProperty<TwinSchema>();

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public EditableProperty<TwinUnit> Unit => GetProperty<TwinUnit>();

    public EditableProperty<bool?> Writable => GetProperty<bool?>();
}