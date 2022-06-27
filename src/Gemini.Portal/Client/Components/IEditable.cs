namespace Gemini.Portal.Client.Components;

public interface IEditable
{
    event EventHandler<EditableEventArgs> Changed;

    bool IsEdited { get; }

    void Reset();

    void Commit();
}

public class EditableEventArgs: EventArgs
{
    public EditableEventArgs(IEditable editable)
    {
        Property = editable;
    }

    public IEditable Property { get; }
}