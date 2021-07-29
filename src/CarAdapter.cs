namespace AndroidXRecyclerViewSelection
{
    using Android.Views;
    using Android.Widget;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adapter to bind a list of <see cref="Car"/> objects to views that are displayed within a RecyclerView.
    /// </summary>
    internal class CarAdapter : RecyclerView.Adapter
    {
        private readonly List<Car> list;

        public CarAdapter(List<Car> cars)
        {
            HasStableIds = true;

            list = cars;
        }

        public SelectionTracker SelectionTracker { get; set; }

        public override int ItemCount => list.Count;

        public override long GetItemId(int position)
        {
            return Convert.ToInt64(position);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var typedHolder = holder as CarViewHolder;
            typedHolder.Bind(list[position], SelectionTracker?.IsSelected(Convert.ToInt64(position)) ?? false);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater
                .From(parent.Context)
                .Inflate(Resource.Layout.item_car, parent, false);
            return new CarViewHolder(itemView);
        }

        /// <summary>
        /// Describes a <see cref="Car"/> item view and metadata about its place within the RecyclerView.
        /// </summary>
        internal class CarViewHolder : RecyclerView.ViewHolder
        {
            private readonly TextView year;
            private readonly TextView make;
            private readonly TextView model;
            
            public CarViewHolder(View view) : base(view)
            {
                year = view.FindViewById<TextView>(Resource.Id.car_year);
                make = view.FindViewById<TextView>(Resource.Id.car_make);
                model = view.FindViewById<TextView>(Resource.Id.car_model);
            }

            public void Bind(Car car, bool isActivated)
            {
                year.Text = car.Year.ToString();
                make.Text = car.Make;
                model.Text = car.Model;

                ItemView.Activated = isActivated;
            }

            public ItemDetailsLookup.ItemDetails GetItemDetails()
            {
                return new CarItemDetails(AdapterPosition, ItemId);
            }
        }

        /// <summary>
        /// Provides the selection library with access to information about a specific RecyclerView item.
        /// </summary>
        internal class CarItemDetails : ItemDetailsLookup.ItemDetails
        {
            private readonly int position;
            private readonly Java.Lang.Object key;

            public CarItemDetails(int position, Java.Lang.Object key)
            {
                this.position = position;
                this.key = key;
            }

            public override int Position => position;

            protected override Java.Lang.Object RawSelectionKey => key;
        }

        /// <summary>
        /// Provides the selection library access to information about the area and/or ItemDetailsLookup.ItemDetails under a MotionEvent.
        /// </summary>
        internal class CarItemDetailsLookup : ItemDetailsLookup
        {
            private readonly RecyclerView recyclerView;

            public CarItemDetailsLookup(RecyclerView recyclerView)
            {
                this.recyclerView = recyclerView;
            }

            public override ItemDetails GetItemDetails(MotionEvent @event)
            {
                var view = recyclerView.FindChildViewUnder(@event.GetX(), @event.GetY());
                if (view != null)
                {
                    var viewHolder = recyclerView.GetChildViewHolder(view);
                    if (viewHolder is CarAdapter.CarViewHolder cvh)
                    {
                        return cvh.GetItemDetails();
                    }
                }
                return null;
            }
        }
    }
}
