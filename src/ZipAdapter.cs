namespace AndroidXRecyclerViewSelection
{
    using Android.Views;
    using AndroidX.RecyclerView.Selection;
    using AndroidX.RecyclerView.Widget;
    using System;
    using System.Collections.Generic;

    public class ZipAdapter : RecyclerView.Adapter
    {
        public ZipAdapter()
        {
            HasStableIds = true;
        }

        private readonly List<int> zips = new List<int>
        {
            32233,
            32266,
            90210,
            12345,
            54321,
            99999,
            12094,
            99050,
            13299,
            10952,
            88491,
            00589,
            10041,
            99501,
            09965,
            09859,
            18751,
            95878,
            18985,
            19999,
            19850
        };
        private SelectionTracker selectionTracker = null;

        public override int ItemCount => zips.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var zip = zips[position];
            ((ZipViewHolder)holder).Bind(zip);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var zipView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.item_zip, parent, false);
            return new ZipViewHolder(zipView);
        }

        public override long GetItemId(int position)
        {
            return Convert.ToInt64(position);
        }

        public void SetTracker(SelectionTracker selectionTracker)
        {
            this.selectionTracker = selectionTracker;
        }
    }
}