namespace Gemini.Portal.Client.Components.DigitalTwin.Schema;

public class TwinSchemaViewModel : ViewModelBase
{
    public TwinSchemaViewModel(TwinSchema model)
    {

        Model = model;

        RegisterProperty(nameof(Comment), Model.Comment, value => Model.Comment = value);
        RegisterProperty(nameof(Description), Model.Description, value => Model.Description = value);
        RegisterProperty(nameof(DisplayName), Model.DisplayName, value => Model.DisplayName = value);
    }

    public TwinSchema Model { get; private set; }

    public virtual bool ReadOnly => true;

    public string Name => Model.Type;

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public bool IsNew { get; private set; }
}
