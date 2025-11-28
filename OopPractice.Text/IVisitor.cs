namespace OopPractice.Text
{
    /// <summary>
    /// Pattern: Visitor
    /// Allows you to add new operations on the text structure without changing the element classes.
    /// </summary>
    public interface IVisitor
    {
        void Visit(Root root);
        void Visit(Header header);
        void Visit(Paragraph paragraph);
    }
}