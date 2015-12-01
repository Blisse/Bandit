namespace BehaviorsForTen
{
    using System;

    using Windows.Foundation.Collections;
    using Windows.UI.Xaml;

    public sealed class ActionCollection : DependencyObjectCollection
    {
        public ActionCollection()
        {
            this.VectorChanged += this.ActionCollectionVectorChanged;
        }

        private static void VerifyType(DependencyObject item)
        {
            if (!(item is IAction))
            {
                throw new InvalidOperationException("NonActionAddedToActionCollectionExceptionMessage");
            }
        }

        private void ActionCollectionVectorChanged(
            IObservableVector<DependencyObject> sender, 
            IVectorChangedEventArgs eventArgs)
        {
            var collectionChange = eventArgs.CollectionChange;
            if (collectionChange == null)
            {
                foreach (var current in this)
                {
                    VerifyType(current);
                }
            }

            if (collectionChange == CollectionChange.ItemInserted || collectionChange == CollectionChange.ItemChanged)
            {
                var item = this[(int)eventArgs.Index];
                VerifyType(item);
            }
        }
    }
}