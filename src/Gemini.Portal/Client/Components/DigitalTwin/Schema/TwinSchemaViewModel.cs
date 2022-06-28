namespace Gemini.Portal.Client.Components.DigitalTwin.Schema;

public class TwinSchemaViewModel : ViewModelBase
{
    private readonly TwinSchema _model;

    public TwinSchemaViewModel(TwinSchema model)
    {
        _model = model;

        RegisterProperty(nameof(Comment), _model.Comment, value => _model.Comment = value);
        RegisterProperty(nameof(Description), _model.Description, value => _model.Description = value);
        RegisterProperty(nameof(DisplayName), _model.DisplayName, value => _model.DisplayName = value);
    }

    public virtual bool ReadOnly => true;

    public string Name => _model.Type;

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();
}
