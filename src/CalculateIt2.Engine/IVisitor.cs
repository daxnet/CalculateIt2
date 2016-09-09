namespace CalculateIt2.Engine
{
    /// <summary>
    /// Represents that the implemented classes are object hierarchy visitors.
    /// </summary>
    public interface IVisitor
    {
        /// <summary>
        /// Visits the given object as an acceptor.
        /// </summary>
        /// <param name="acceptor">The object being visited.</param>
        void Visit(IVisitorAcceptor acceptor);
    }
}
