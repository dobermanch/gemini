﻿using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Interface;

public class TwinInterfaceViewModel : ViewModelBase
{
    private readonly TwinInterface _model;

    public TwinInterfaceViewModel(TwinInterface? model = null)
    {
        _model = model ?? new TwinInterface
        {
            Id = "dtmi:;1",
        };

        RegisterProperty(nameof(Id), _model.Id, value => _model.Id = value);
        RegisterProperty(nameof(Comment), _model.Comment, value => _model.Comment = value);
        RegisterProperty(nameof(Contents), _model.Contents, value => _model.Contents = value);
        RegisterProperty(nameof(Description), _model.Description, value => _model.Description = value);
        RegisterProperty(nameof(DisplayName), _model.DisplayName, value => _model.DisplayName = value);
        RegisterProperty(nameof(Extends), _model.Extends, value => _model.Extends = value);
        RegisterProperty(nameof(Schema), _model.Schema, value => _model.Schema = value);
    }

    public string Name => !string.IsNullOrWhiteSpace(DisplayName.Value)
        ? DisplayName.Value
        : Id.Value ?? "Interface";

    public EditableProperty<Dtmi> Id => GetProperty<Dtmi>();

    public Iri Type => _model.Type;

    public Iri Context => _model.Context;

    public EditableProperty<string?> Comment => GetProperty<string?>();

    public EditableProperty<IList<TwinModelBase>?> Contents => GetProperty<IList<TwinModelBase>?>();

    public EditableProperty<string?> Description => GetProperty<string?>();

    public EditableProperty<string?> DisplayName => GetProperty<string?>();

    public EditableProperty<IList<Dtmi>> Extends => GetProperty<IList<Dtmi>>();

    public EditableProperty<TwinSchema?> Schema => GetProperty<TwinSchema?>();
}