using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Interface;

public class TwinInterfacesViewModel : ViewModelBase
{
    public TwinInterfacesViewModel(TwinInterface? model = null)
    {
        IsNew = model == null;
        Model = model ?? new TwinInterface
        {
            Id = "dtmi:;1",
        };

        RegisterProperty(nameof(Id), Model.Id, value => Model.Id = value);
        RegisterProperty(nameof(Comment), Model.Comment, value => Model.Comment = value);
        RegisterProperty(nameof(Contents), Model.Contents, value => Model.Contents = value);
        RegisterProperty(nameof(Description), Model.Description, value => Model.Description = value);
        RegisterProperty(nameof(DisplayName), Model.DisplayName, value => Model.DisplayName = value);
        RegisterProperty(nameof(Extends), Model.Extends, value => Model.Extends = value);
        RegisterProperty(nameof(Schema), Model.Schema, value => Model.Schema = value);
    }

    public override bool IsEdited => base.IsEdited || IsNew || IsDeleted;

    public TwinInterface Model { get; private set; }

    public string Name => !string.IsNullOrWhiteSpace(DisplayName.Value)
        ? DisplayName.Value
        : Id.Value ?? "Interface";

    public EditableProperty<Dtmi> Id => GetProperty<Dtmi>();

    public Iri Type => Model.Type;

    public Iri Context => Model.Context;

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<IList<TwinModelBase>?> Contents => GetProperty<IList<TwinModelBase>?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public EditableProperty<IList<Dtmi>> Extends => GetProperty<IList<Dtmi>>();

    public EditableProperty<TwinSchema?> Schema => GetProperty<TwinSchema?>();

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
