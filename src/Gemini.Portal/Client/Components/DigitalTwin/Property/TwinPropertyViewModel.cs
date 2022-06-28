using Gemini.Portal.Client.Components.DigitalTwin.Interface;
using Gemini.Portal.Client.Components.DigitalTwin.Schema;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Components.DigitalTwin.Property;

public class TwinPropertyViewModel : ViewModelBase
{
    public TwinPropertyViewModel(TwinProperty? model = null)
    {
        IsNew = model == null;
        Model = model ?? new TwinProperty
        {
            Id = "dtmi:;1",
            Schema = new StringTwinSchema()
        };

        RegisterProperty(nameof(Id), Model.Id, value => Model.Id = value);
        RegisterProperty(nameof(Name), Model.Name, value => Model.Name = value);
        RegisterProperty(nameof(Schema), new TwinSchemaViewModel(Model.Schema), value => Model.Schema = value.Model);
        RegisterProperty(nameof(Comment), Model.Comment, value => Model.Comment = value);
        RegisterProperty(nameof(Description), Model.Description, value => Model.Description = value);
        RegisterProperty(nameof(DisplayName), Model.DisplayName, value => Model.DisplayName = value);
        RegisterProperty(nameof(Unit), Model.Unit, value => Model.Unit = value);
        RegisterProperty(nameof(Writable), Model.Writable, value => Model.Writable = value);
    }

    public override bool IsEdited => base.IsEdited || IsNew || IsDeleted;

    public TwinProperty Model { get; private set; }

    public EditableProperty<string> Name => GetProperty<string>();

    public EditableProperty<Dtmi> Id => GetProperty<Dtmi>();

    public Iri Type => Model.Type;

    public EditableProperty<TwinSchemaViewModel> Schema => GetProperty<TwinSchemaViewModel>();

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public EditableProperty<TwinUnit> Unit => GetProperty<TwinUnit>();

    public EditableProperty<bool?> Writable => GetProperty<bool?>();

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