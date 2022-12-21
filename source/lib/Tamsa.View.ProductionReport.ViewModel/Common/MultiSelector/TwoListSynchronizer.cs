// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoListSynchronizer.cs" company="Tenaris">
//   Tenaris.
// </copyright>
// <summary>
//   Defines the TwoListSynchronizer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tamsa.View.ProductionReport.ViewModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using Tamsa.View.ProductionReport.Model;

    /// <summary>
    /// Keeps two lists synchronized. 
    /// </summary>
    public class TwoListSynchronizer : IWeakEventListener
    {
        #region Fields

        /// <summary>
        /// Dfeault converter.
        /// </summary>
        private static readonly IListItemConverter DefaultConverter = new DoNothingListItemConverter();

        /// <summary>
        /// The master list.
        /// </summary>
        private readonly IList masteList;

        /// <summary>
        /// The master target converter
        /// </summary>
        private readonly IListItemConverter masteTargeConverter;

        /// <summary>
        /// The target list.
        /// </summary>
        private readonly IList targeList;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoListSynchronizer"/> class.
        /// </summary>
        /// <param name="masterList">The master list.</param>
        /// <param name="targetList">The target list.</param>
        /// <param name="masterTargetConverter">The master-target converter.</param>
        public TwoListSynchronizer(IList masterList, IList targetList, IListItemConverter masterTargetConverter)
        {
            this.masteList = masterList;
            this.targeList = targetList;
            this.masteTargeConverter = masterTargetConverter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoListSynchronizer"/> class.
        /// </summary>
        /// <param name="masterList">The master list.</param>
        /// <param name="targetList">The target list.</param>
        public TwoListSynchronizer(IList masterList, IList targetList)
            : this(masterList, targetList, DefaultConverter)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Change List Action delegate.
        /// </summary>
        /// <param name="list">
        /// The list value.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private delegate void ChangeListAction(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter);
        
        #endregion

        #region Public methods

        /// <summary>
        /// Starts synchronizing the lists.
        /// </summary>
        public void StartSynchronizing()
        {
            this.ListenForChangeEvents(this.masteList);
            this.ListenForChangeEvents(this.targeList);

            // Update the Target list from the Master list
            this.SetListValuesFromSource(this.masteList, this.targeList, this.ConvertFromMasterToTarget);

            // In some cases the target list might have its own view on which items should included:
            // so update the master list from the target list
            // (This is the case with a ListBox SelectedItems collection: only items from the ItemsSource can be included in SelectedItems)
            if (!this.TargetAndMasterCollectionsAreEqual())
            {
                this.SetListValuesFromSource(this.targeList, this.masteList, this.ConvertFromTargetToMaster);
            }
        }

        /// <summary>
        /// Stop synchronizing the lists.
        /// </summary>
        public void StopSynchronizing()
        {
            this.StopListeningForChangeEvents(this.masteList);
            this.StopListeningForChangeEvents(this.targeList);
        }

        /// <summary>
        /// Receives events from the centralized event manager.
        /// </summary>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager"/> calling this method.</param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        /// <returns>
        /// true if the listener handled the event. It is considered an error by the <see cref="T:System.Windows.WeakEventManager"/> handling in WPF to register a listener for an event that the listener does not handle. Regardless, the method should return false if it receives an event that it does not recognize or handle.
        /// </returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            this.HandleCollectionChanged(sender as IList, e as NotifyCollectionChangedEventArgs);

            return true;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Listens for change events on a list.
        /// </summary>
        /// <param name="list">The list to listen to.</param>
        protected void ListenForChangeEvents(IList list)
        {
            if (list is INotifyCollectionChanged)
            {
                CollectionChangedEventManager.AddListener(list as INotifyCollectionChanged, this);
            }
        }

        /// <summary>
        /// Stops listening for change events.
        /// </summary>
        /// <param name="list">The list to stop listening to.</param>
        protected void StopListeningForChangeEvents(IList list)
        {
            if (list is INotifyCollectionChanged)
            {
                CollectionChangedEventManager.RemoveListener(list as INotifyCollectionChanged, this);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// The add items.
        /// </summary>
        /// <param name="list">
        /// The items list.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private void AddItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            try
            {
                int itemCount = e.NewItems.Count;

                for (int i = 0; i < itemCount; i++)
                {
                    int insertionPoint = e.NewStartingIndex + i;

                    if (insertionPoint > list.Count)
                    {
                        list.Add(converter(e.NewItems[i]));
                    }
                    else
                    {
                        list.Insert(insertionPoint, converter(e.NewItems[i]));
                    }
                }
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Converter from master to target.
        /// </summary>
        /// <param name="masterListItem">
        /// The master list value.
        /// </param>
        /// <returns>
        /// The new object value.
        /// </returns>
        private object ConvertFromMasterToTarget(object masterListItem)
        {
            return this.masteTargeConverter == null ? masterListItem : this.masteTargeConverter.Convert(masterListItem);
        }

        /// <summary>
        /// Converter from traget to master.
        /// </summary>
        /// <param name="targetListItem">
        /// The master list value.
        /// </param>
        /// <returns>
        /// The new object value.
        /// </returns>
        private object ConvertFromTargetToMaster(object targetListItem)
        {
            return this.masteTargeConverter == null ? targetListItem : this.masteTargeConverter.ConvertBack(targetListItem);
        }

        /// <summary>
        /// Handle collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IList sourceList = sender as IList;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.PerformActionOnAllLists(this.AddItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    this.PerformActionOnAllLists(this.MoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.PerformActionOnAllLists(this.RemoveItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.PerformActionOnAllLists(this.ReplaceItems, sourceList, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.UpdateListsFromSource(sender as IList);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The move items.
        /// </summary>
        /// <param name="list">
        /// The items list value.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private void MoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            this.RemoveItems(list, e, converter);
            this.AddItems(list, e, converter);
        }

        /// <summary>
        /// Perform actions on all list.
        /// </summary>
        /// <param name="action">
        /// The action value.
        /// </param>
        /// <param name="sourceList">
        /// The source list value.
        /// </param>
        /// <param name="collectionChangedArgs">
        /// The collection changed arg.
        /// </param>
        private void PerformActionOnAllLists(ChangeListAction action, IList sourceList, NotifyCollectionChangedEventArgs collectionChangedArgs)
        {
            if (sourceList == this.masteList)
            {
                this.PerformActionOnList(this.targeList, action, collectionChangedArgs, this.ConvertFromMasterToTarget);
            }
            else
            {
                this.PerformActionOnList(this.masteList, action, collectionChangedArgs, this.ConvertFromTargetToMaster);
            }
        }

        /// <summary>
        /// Perform action list
        /// </summary>
        /// <param name="list">
        /// The list value.
        /// </param>
        /// <param name="action">
        /// The action value.
        /// </param>
        /// <param name="collectionChangedArgs">
        /// The collection changed value.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private void PerformActionOnList(IList list, ChangeListAction action, NotifyCollectionChangedEventArgs collectionChangedArgs, Converter<object, object> converter)
        {
            this.StopListeningForChangeEvents(list);
            action(list, collectionChangedArgs, converter);
            this.ListenForChangeEvents(list);
        }

        /// <summary>
        /// Remove items.
        /// </summary>
        /// <param name="list">
        /// The list values.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private void RemoveItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            try
            {
                int itemCount = e.OldItems.Count;

                // for the number of items being removed, remove the item from the Old Starting Index
                // (this will cause following items to be shifted down to fill the hole).
                for (int i = 0; i < itemCount; i++)
                {
                    list.RemoveAt(e.OldStartingIndex);
                }
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Replace items.
        /// </summary>
        /// <param name="list">
        /// The list values.
        /// </param>
        /// <param name="e">
        /// The notify event.
        /// </param>
        /// <param name="converter">
        /// The converter value.
        /// </param>
        private void ReplaceItems(IList list, NotifyCollectionChangedEventArgs e, Converter<object, object> converter)
        {
            this.RemoveItems(list, e, converter);
            this.AddItems(list, e, converter);
        }

        /// <summary>
        /// Set list values source.
        /// </summary>
        /// <param name="sourceList">
        /// The source list.
        /// </param>
        /// <param name="targetList">
        /// The target list.
        /// </param>
        /// <param name="converter">
        /// The converted value.
        /// </param>
        private void SetListValuesFromSource(IList sourceList, IList targetList, Converter<object, object> converter)
        {
            this.StopListeningForChangeEvents(targetList);

            targetList.Clear();

            foreach (object o in sourceList)
            {
                targetList.Add(converter(o));
            }

            this.ListenForChangeEvents(targetList);
        }

        /// <summary>
        /// Target and master collection.
        /// </summary>
        /// <returns>
        /// The condition result.
        /// </returns>
        private bool TargetAndMasterCollectionsAreEqual()
        {
            return this.masteList.Cast<object>().SequenceEqual(this.targeList.Cast<object>().Select(item => this.ConvertFromTargetToMaster(item)));
        }

        /// <summary>
        /// Makes sure that all synchronized lists have the same values as the source list.
        /// </summary>
        /// <param name="sourceList">The source list.</param>
        private void UpdateListsFromSource(IList sourceList)
        {
            if (sourceList == this.masteList)
            {
                this.SetListValuesFromSource(this.masteList, this.targeList, this.ConvertFromMasterToTarget);
            }
            else
            {
                this.SetListValuesFromSource(this.targeList, this.masteList, this.ConvertFromTargetToMaster);
            }
        }

        #endregion

        /// <summary>
        /// An implementation that does nothing in the conversions.
        /// </summary>
        internal class DoNothingListItemConverter : IListItemConverter
        {
            /// <summary>
            /// Converts the specified master list item.
            /// </summary>
            /// <param name="masterListItem">The master list item.</param>
            /// <returns>The result of the conversion.</returns>
            public object Convert(object masterListItem)
            {
                return masterListItem;
            }

            /// <summary>
            /// Converts the specified target list item.
            /// </summary>
            /// <param name="targetListItem">The target list item.</param>
            /// <returns>The result of the conversion.</returns>
            public object ConvertBack(object targetListItem)
            {
                return targetListItem;
            }
        }
    }
}
