namespace AndroidXRecyclerViewSelection
{
    using Android.Views;
    using Android.Widget;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using System;
    using System.Collections.Generic;

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
            typedHolder.Bind(list[position], SelectionTracker.IsSelected(Convert.ToInt64(position)));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater
                .From(parent.Context)
                .Inflate(Resource.Layout.item_car, parent, false);
            return new CarViewHolder(itemView);
        }

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
    }
}