namespace Gemini.Portal.Client.Components;

public class EditableProperty<T> : IEditable
{
    private T _value;
    private readonly Action<T> _updateSource;

    public event EventHandler<EditableEventArgs> Changed;

    public EditableProperty(T value)
    {
        Value = value;
        OriginalValue = value;
    }

    public EditableProperty(T value, Action<T> updateSource)
    {
        Value = OriginalValue = value;
        _updateSource = updateSource;
    }

    public T Value
    {
        get => _value;
        set 
        {
            _value = value;
            if (IsEdited)
            {
                Changed?.Invoke(this, new EditableEventArgs(this));
            }
        }
    }

    public T OriginalValue { get; private set; }

    public bool IsEdited => (Value is not null && !Value.Equals(OriginalValue));

    public void Reset()
    {
        bool isEdited = IsEdited;
        Value = OriginalValue;

        if (isEdited)
        {
            Changed?.Invoke(this, new EditableEventArgs(this));
        }
    }

    public void Commit() 
    {
        OriginalValue = Value;
        _updateSource?.Invoke(Value);
    }

    public static implicit operator T(EditableProperty<T> property) => property.Value;

    public static implicit operator EditableProperty<T>(T value) => new EditableProperty<T>(value);

    public static EditableProperty<T> Create<T>(T value, Action<T> updateSource)
    {
        return new EditableProperty<T>(value, updateSource);
    }
}
