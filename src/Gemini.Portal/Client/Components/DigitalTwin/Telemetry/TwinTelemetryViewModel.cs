using Gemini.Portal.Client.Components.DigitalTwin.Schema;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Components.DigitalTwin.Telemetry;

public class TwinTelemetryViewModel : ViewModelBase
{
    public TwinTelemetryViewModel(TwinTelemetry? model = null)
    {
        IsNew = model == null;
        Model = model ?? new TwinTelemetry
        {
            Id = "dtmi:;1",
            Schema = new StringTwinSchema()
        };

        RegisterProperty(nameof(Id), (Dtmi)Model.Id, value => Model.Id = value);
        RegisterProperty(nameof(Comment), Model.Comment, value => Model.Comment = value);
        RegisterProperty(nameof(Name), Model.Name, value => Model.Name = value);
        RegisterProperty(nameof(Description), Model.Description, value => Model.Description = value);
        RegisterProperty(nameof(DisplayName), Model.DisplayName, value => Model.DisplayName = value);
        RegisterProperty(nameof(Unit), Model.Unit, value => Model.Unit = value);
        RegisterProperty(nameof(Schema), new TwinSchemaViewModel(Model.Schema), value => Model.Schema = value.Model);
    }

    public override bool IsEdited => base.IsEdited || IsNew || IsDeleted;

    public TwinTelemetry Model { get; private set; }

    public EditableProperty<Dtmi> Id => GetProperty<Dtmi>();

    public Iri Type => Model.Type;

    public EditableProperty<string> Name => GetProperty<string>();

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public EditableProperty<TwinUnit> Unit => GetProperty<TwinUnit>();

    public EditableProperty<TwinSchemaViewModel> Schema => GetProperty<TwinSchemaViewModel>();

    public bool IsNew { get; private set; }

    public bool IsDeleted { get; private set; }

    public void MarkForDeletion()
    {
        IsDeleted = true;
    }

    public override void Reset()
    {
        base.Reset();
        IsDeleted = false;
    }

    public void Stored()
    {
        IsNew = false;
    }
}