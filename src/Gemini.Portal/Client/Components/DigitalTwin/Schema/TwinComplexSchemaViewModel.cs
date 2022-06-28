namespace Gemini.Portal.Client.Components.DigitalTwin.Schema;

public class TwinComplexSchemaViewModel : TwinSchemaViewModel
{
    private readonly TwinComplexSchema _model = null!;

    public TwinComplexSchemaViewModel(TwinComplexSchema model)
        : base(model)
    {
        _model = model;

        RegisterProperty(nameof(Id), _model.Id, value => _model.Id = value);
    }

    public override bool ReadOnly => false;

    public EditableProperty<Dtmi?> Id => GetProperty<Dtmi?>();
}