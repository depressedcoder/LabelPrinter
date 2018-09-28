namespace LabelPrinter.DatabaseWatcher
{
    public abstract class AbstractWatcher
    {
        #region Abstract Methods

        public abstract void NotifyNewItem();
        public abstract string GetConnectionString(); 

        #endregion
    }
}
